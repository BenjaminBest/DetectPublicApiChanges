Detect public API changes with Roslyn
=====================================
This repository contains a console application which can be used to compare two visual studio solutions with [roslyn](https://github.com/dotnet/roslyn) using the Syntax Tree. The program supports two modes, the first one is just using folders to read the solution files from. The second mode supports SVN via [SharpSvn](https://sharpsvn.open.collab.net/) or GIT via [libgit2sharp](https://github.com/libgit2/libgit2sharp/). The checkout of two revisions happens then automatically and these are then used for comparison.

Based on the information gathered with the Syntax Tree an index for every solution is created which contains unique keys for every structure (class, interface, struct, constructor, methods or property) including parameter and return-types, names and modifiers.

With the generated index the differences between two visual studio solutions can be found easily, because if a source-key does not exist in the target-index then it has obviously been changed or is missing.

Finally a HTML based report is generated which contains all projects, interfaces or classes which have changes on their public API. The changes are listed in detail as well as the change-log if a source control system was used to fetch the source code.

Report
--------------
![sample report](https://cloud.githubusercontent.com/assets/29073072/26785873/c5aa72a4-4a04-11e7-8e63-412f59c5c51a.png)

Usage
-------------------------------
Compile the solution in release mode with at least Visual Studio 2017

### Using source control
#### Subversion
In a CMD window start the application by using parameters which contains all the information needed to access subversion.

> DetectPublicApiChanges.exe --repositoryConnectionString "Svn;https://XYZ/svn/DetectPublicApiChanges/trunk;20;28;user;password" --solutionPathSource "DetectPublicApiChanges\DetectPublicApiChanges.sln" --solutionPathTarget "DetectPublicApiChanges\DetectPublicApiChanges.sln"

#### Git
To compare two commits of this application using `Git`, just add the URL, and 2 SHA's to the connection string:

> DetectPublicApiChanges.exe --repositoryConnectionString "Git;https://github.com/BenjaminBest/DetectPublicApiChanges.git;4b8d215a190ce8ce92d77409c3fb200ef30a60b3;1d9bd50b1fb0eb53741652022d7de5850f59cdff" --solutionPathSource "DetectPublicApiChanges\DetectPublicApiChanges.sln" --solutionPathTarget "DetectPublicApiChanges\DetectPublicApiChanges.sln"

So a connection string is defined by 4 to 6 parts: `SourceControlSystem;URL;StartRevision;EndRevision;User;Password`, whereas user and password are optional. The checkout is done to folders inside the working folder which is located relative to the application and it's per default named "Work". The folders which contains the revisions are named "Source" and "Target".

### Using normal local folders
For local folders the syntax is easier:
> DetectPublicApiChanges.exe --solutionPathSource "C:\Folder1\DetectPublicApiChanges\DetectPublicApiChanges.sln" --solutionPathTarget "C:\Folder2\DetectPublicApiChanges\DetectPublicApiChanges.sln"

### The output folder
The application automatically creates a working directory named "Work", by using the parameter
> --workPath "C:\SomeAbsolutePath"

this can be changed, also a relative path can be used

> --workPath "..\SomeRelativePath"

The program always create a unique directory inside the work-folder based on a filetime-stamp,e.g: `131411407331414512`. The folder structure looks like this:
```
|-- Work
    |-- 131411407331414512
        |-- Source
		|-- ...
	|-- Target
		|-- ...
	|-- 2017-06-05_14_52_13_Report.html
        |-- DetectPublicApiChanges.sln_analysis0.json
	|-- DetectPublicApiChanges.sln_analysis1.json
        |-- log.txt
    |-- 131411422436050525
	|-- ...
```

### Other commandline parameters
A self test is build in and can be invoked by just running the EXE without a parameter:
> DetectPublicApiChanges.exe

Then the application tests the DetectPublicApiChanges solution itself and should generate a report without any changes, because it does compare the same version. Make sure the EXE is located in the bin folder and the solution is above that.

Most likely unit test projects should not be recognized in the change detection process, therefore a regex filter exists. With the option
> --regexFilter "\.Tests"

a regex filter can be defined which filters all projects out that **matches**. The regex is analyzed in a non case sensitive way.

The title of the report can be manuelly set by using the `title` parameter:
> --title "All breaking changes of release X.Y.Z"

### Logging
The application uses log4net for logging. Be aware that using debug causes the log files to grow rapidly.

Implementation
--------------
### Important dependencies
1. [roslyn](https://github.com/dotnet/roslyn)
2. [SharpSvn](https://sharpsvn.open.collab.net/)
3. [libgit2sharp](https://github.com/libgit2/libgit2sharp/)
4. [Command Line Parser Library](https://www.nuget.org/packages/CommandLineParser/1.9.71)
5. [RazorEngine](https://github.com/Antaris/RazorEngine)
6. [NetJSON](https://github.com/rpgmaker/NetJSON)

### Subversion checkout & changelog
As source control client currently subversion is supported. To actually do the checkout just a few lines of code are involved using [SharpSvn](https://sharpsvn.open.collab.net/):
```csharp
public void CheckOut(Uri repositoryUrl, DirectoryInfo localFolder, int revision, ISourceControlCredentials credentials = null)
{
    using (var client = new SvnClient())
    {
        if (credentials != null)
            client.Authentication.ForceCredentials(credentials.User, credentials.Password);

        client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

        client.CheckOut(repositoryUrl, localFolder.FullName,
            new SvnCheckOutArgs() { Revision = new SvnRevision(revision) });
    }
}
```

Also retrieving the changelog from subversion to add it to the report is done straightforward:

```csharp
public ISourceControlChangeLog GetChangeLog(Uri repositoryUrl, int startRevision, int endRevision,
    ISourceControlCredentials credentials = null)
{
    var log = new SourceControlChangeLog(startRevision, endRevision);

    using (var client = new SvnClient())
    {
        if (credentials != null)
            client.Authentication.ForceCredentials(credentials.User, credentials.Password);

        client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

        client.Log(
            repositoryUrl,
            new SvnLogArgs
            {
                Range = new SvnRevisionRange(startRevision, endRevision)
            },
            (o, e) =>
            {
                log.AddItem(new SourceControlChangeLogItem(e.Author, e.LogMessage, e.Time));
            });
    }

    return log;
}
```

### Git checkout & changelog
The approach of getting the source code is more complex with Git: First of all a local repository needs to be initialized because Git is not a centralized source code control system. Then the connection to the remote repository needs to be established, after that the source code is being fetched. The last step involves doing a checkout, which actually switches to a specific revision.

```csharp
public void CheckOut(Uri repositoryUrl, DirectoryInfo localFolder, string revision, ISourceControlCredentials credentials = null)
{
    //Create repository
    Repository.Init(localFolder.FullName);

    //Fetch & Checkout
    using (var repo = new Repository(localFolder.FullName))
    {
        AddOrUpdateRemote(repo, "origin", repositoryUrl);

        var fetchOptions = new FetchOptions
        {
            CredentialsProvider = (url, usernameFromUrl, types) =>
                new UsernamePasswordCredentials
                {
                    Username = credentials.User,
                    Password = credentials.Password
                }
        };

        foreach (var remote in repo.Network.Remotes)
        {
            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
            Commands.Fetch(repo, remote.Name, refSpecs, fetchOptions, string.Empty);
        }

        var commit = repo.Lookup<Commit>(revision);
        Commands.Checkout(repo, commit);
    }
}
```

Getting the commit log is relatively easy by just adding a filter to specify a commit range:

```csharp
public ISourceControlChangeLog GetChangeLog(Uri repositoryUrl, DirectoryInfo localFolder, string startRevision, string endRevision,
    ISourceControlCredentials credentials = null)
{
    CheckOut(repositoryUrl, localFolder, endRevision, credentials);

    var log = new SourceControlChangeLog(startRevision, endRevision);

    using (var repo = new Repository(localFolder.FullName))
    {
        var filter = new CommitFilter
        {
            IncludeReachableFrom = endRevision,
            ExcludeReachableFrom = startRevision
        };

        foreach (var commit in repo.Commits.QueryBy(filter))
        {
            log.AddItem(new SourceControlChangeLogItem(commit.Author.Name, commit.Message, commit.Author.When.DateTime));
        }
    }

    return log;
}
```

### Roslyn syntax tree analysis
Basically [roslyn](https://github.com/dotnet/roslyn) divides the analysis in syntax and semantic analysis. As in the documentation of [roslyn](https://github.com/dotnet/roslyn) outlined: 'Syntax trees are the primary structure used for compilation, code analysis, binding, refactoring, IDE features, and code generation'. This model is used to analyze the source code of the given solution.

The structure looks like this:
```
|-- Workspace
    |-- Solution
        |-- Project
          |-- Document
            |--SyntaxTree
              |--SyntaxNode Root
                |--SyntaxNode
                ...
        |-- Project
		|-- ...
```

For the basic comparison of public members these [roslyn](https://github.com/dotnet/roslyn)  classes are used:
```
|-- SyntaxNode
    |-- MemberDeclarationSyntax
        |-- BaseMethodDeclarationSyntax
          |-- MemberDeclarationSyntax
          |-- ConstructorDeclarationSyntax
        |-- BaseTypeDeclarationSyntax
                |-- TypeDeclarationSyntax
                  |-- ClassDeclarationSyntax
                  |-- InterfaceDeclarationSyntax
                  |-- StructDeclarationSyntax
        |-- BasePropertyDeclarationSyntax
          |-- PropertyDeclarationSyntax
```

The most generic type which can be used is the `SyntaxNode`.

Below the basic C# code to read the solution file is described. It is also possible to directly analyze C# via a string, which is useful for unit tests.

#### Get all syntax nodes
The basic C# code to go over all projects in a solution, load the document and then get the syntax tree looks like this. It analyzes every class by filtering with

> `OfType<ClassDeclarationSyntax>()`

and retrieves the name and the full namespace.

```csharp
private void AnalyzeSyntaxTree(string solutionPath)
{
    //Solution
    var solution = WorkspaceHelper.GetSolution(solutionPath);
    solution.Wait();

    //Project
    foreach (var projectId in solution.Result.ProjectIds)
    {
        var project = solution.Result.GetProject(projectId);

        //Document
        foreach (var documentId in project.DocumentIds)
        {
            var document = solution.Result.GetDocument(documentId);
            if (document.SupportsSyntaxTree)
            {
                //Syntax Tree
                var syntaxTree = document.GetSyntaxTreeAsync().Result;

                //Syntax Node
                var syntaxNode = tree?.GetRoot();

                //.. Analyze the syntax tree and get Name and Fullname of every class
                var classItems = syntaxNode.DescendantNodes().OfType<ClassDeclarationSyntax>()
                  .Select(
                    c => new {
                      Name = c.GetName(),
                      FullNameSpace = c.GetFullName()});

                var interfaceItems = syntaxNode.DescendantNodes().OfType<InterfaceDeclarationSyntax>();

                //.. get modifiers, ctor, properties etc
            }
        }
    }
}

public static string GetName(this ClassDeclarationSyntax syntax)
{
    return syntax.Identifier.ValueText;
}

public static string GetFullName(this ClassDeclarationSyntax syntax)
{
    NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
    if (!SyntaxNodeHelper.TryGetParentSyntax(syntax, out namespaceDeclarationSyntax))
        return string.Empty;

    var namespaceName = namespaceDeclarationSyntax.Name.ToString();
    var fullClassName = namespaceName + "." + syntax.Identifier;

    return fullClassName;
}
```
#### Retrieve information about modifiers, contructors, properties or methods

Now that we have the complete syntax tree loaded, we can iterate over every syntax node, cast it to e.g. a `ClassDeclarationSyntax` and get more information:

```csharp
public static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(this ClassDeclarationSyntax syntax)
{
  var ctors = syntax
      .ChildNodes()
      .OfType<ConstructorDeclarationSyntax>();

  return ctors;
}

public static IEnumerable<MethodDeclarationSyntax> GetMethods(this ClassDeclarationSyntax syntax)
{
    var methods = syntax
        .ChildNodes()
        .OfType<MethodDeclarationSyntax>();

    return methods;
}

public static IEnumerable<PropertyDeclarationSyntax> GetProperties(this ClassDeclarationSyntax syntax)
{
    var properties = syntax
        .ChildNodes()
        .OfType<PropertyDeclarationSyntax>();

    return properties;
}
```

A `MethodDeclarationSyntax` then contains for example the modifiers and parameters. That helps us to search for this combination in the target solution.


### Create index and compare

The application DetectPublicApiChanges does a simple index key comparison in the basic version. The unique key for a class and an interface is determined just by the full name space including the class or interface name. For a method, property or a constructor the return-type, the structure-name and the parameters (type and name) are included in the key.

Here is an example of how an key for a method:

```csharp
public string CreateIndexKey(MethodDeclarationSyntax syntax, ClassDeclarationSyntax parent)
{
    var parentNameSpace = string.Empty;
    if (parent != null)
        parentNameSpace = parent.GetFullName();

    var key = new StringBuilder(parentNameSpace);

    key.Append(syntax.ReturnType);
    key.Append(syntax.Identifier.Text);

    foreach (var param in syntax.GetParameters())
    {
        key.Append(param.Identifier.Text);
        key.Append(param.Type);
    }

    foreach (var param in syntax.Modifiers)
    {
        key.Append(param.ValueText);
    }

    return key.ToString();
}
```

We then add all of these keys to 2 different indexes, then we compare, but first of all we only recognize structures which contains a public modifier. Every key which cannot be found in the target index can be considered as a breaking change.

### Using MVC Razor templates to generate report
The report is created using razor templates utilizing the [RazorEngine](https://github.com/Antaris/RazorEngine).
