using LCBM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LCBM.Asset
{
        internal static class AssetManager
        {
                private static Assembly _assembly;

                public static void Initialize()
                {
                        _assembly = Assembly.GetExecutingAssembly();
                }
                public static Stream LoadResourceStream(string ResName)
                {
                        Stream stream = _assembly.GetManifestResourceStream(ResName);

                        return stream;
                }




        }
}
