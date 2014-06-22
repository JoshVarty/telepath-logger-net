using System;
using System.IO;
using System.Reflection;

namespace WPFCSharpWebCam
{

    public static class Utilities
    {
        /// <summary>
        /// Returns the directory of the currently executing assembly.
        /// See: http://stackoverflow.com/a/283917/300908
        /// </summary>
        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
