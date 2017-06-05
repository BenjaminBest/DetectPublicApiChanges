using CommandLine;
using DetectPublicApiChanges.Interfaces;
using System;

namespace DetectPublicApiChanges.Extensions
{
    public static class OptionsExtensions
    {
        public static void ParseFromArguments(this IOptions options, string[] args)
        {
            if (!Parser.Default.ParseArguments(args, options))
                throw new ArgumentException("Could not parse commandline arguments");
        }
    }
}
