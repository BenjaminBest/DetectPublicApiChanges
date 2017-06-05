Detect public API changes with roslyn
=====================================
This repository contains a console application which can be used to compare two visual studio solutions with [roslyn](https://github.com/dotnet/roslyn) using the SyntaxTree. The program supports two modi, the first one is just using existent folders to read the solution file from. The second mode supports SVN via [SharpSvn](https://sharpsvn.open.collab.net/) and automatically checkout's two revisions which then are used for comparison.

Based on the information gathered with the SyntaxTree an index for every solution is created which contains unique keys for every structure (class, interface, constructor, methods or property) including parameter and return-types, names and modifiers.

With the generated index the differences between two visual studio solutions can be found easily, because if a source-key does not exist in the target-index then it has obviously been changed or is missing.

Finally a HTML based report is generated which contains all projects, interfaces or classes which have changes on their public API. The changes are listed in detail as well as the change-log if a source control system was used to fetch the source code.

Report
--------------
![sample report](https://cloud.githubusercontent.com/assets/29073072/26785873/c5aa72a4-4a04-11e7-8e63-412f59c5c51a.png)

Running the console application
-------------------------------
Compile the solution in release mode with at least Visual Studio 2017

### Using source control
In a CMD window start the application by using parameters for the SVN connection, the solutions which should be analyzed. The program automatically creates a output directory.
> DetectPublicApiChanges.exe --job DetectChanges --repositorySourceRevision "10" --repositoryTargetRevision "20" --repositoryUrl "https://XYZ/svn/DetectPublicApiChanges/trunk" --repositoryUser "user" --repositoryPassword "password" --solutionPathSource "DetectPublicApiChanges\DetectPublicApiChanges.sln" --solutionPathTarget "DetectPublicApiChanges\DetectPublicApiChanges.sln"

The checkout is done to folders inside the working folder which is located relative to the application and it's per defaul named "Work". The folders which contains the revisions are named "Source" and "Target".

### Using normal local folders
For local folders the syntax easier.
> DetectPublicApiChanges.exe --job DetectChanges --solutionPathSource "C:\Folder1\DetectPublicApiChanges\DetectPublicApiChanges.sln" --solutionPathTarget "C:\Folder2\DetectPublicApiChanges\DetectPublicApiChanges.sln"

### The output folder
The application automatically creates a working directory named "Work", by using the parameter
> --workPath "C:\SomeAbsolutePath"
this can be changed, also a relative path can be used
> --workPath "..\SomeRelativePath"

The program always create a unique directory inside the work-folder based on a FileTime-stamp,e.g: `131411407331414512`. The folder structure looks like this:
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
