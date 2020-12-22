using System;

namespace GitInfo
{
    public static class GitVersion
    {
        public const string Short = ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch;
        public const string Long = Short + "." + ThisAssembly.Git.Commits;
        public const string Verbose = ThisAssembly.Git.BaseTag + "-g" + ThisAssembly.Git.Commit;

        public static class SemVer
        {
            public const string Short = ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch;
            public const string Long = Short + "." + ThisAssembly.Git.Commits;
        }

    }

    public static class GitExtensions
    {
        public static Version ToVersion(this string str)
        {
            return Version.Parse(str);
        }
    }
}
