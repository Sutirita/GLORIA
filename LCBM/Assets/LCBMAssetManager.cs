using LCBM;
using LCBM.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using static Mono.Security.X509.X520;

namespace LCBM.Asset
{
        internal static class LCBMAssetManager
        {
                internal static Assembly LCBMAssembly;

                public static bool Initialize()
                {
                        bool flag = false;
                        try
                        {
                                LCBMAssembly = Assembly.GetExecutingAssembly();
                                flag = true;

                        }
                        catch (Exception e)
                        {
                                LCBMLogger.Fatal("Failed to initialize AssetManager.");
                                LCBMLogger.Exception(e);
                        }

                        return flag;
                }

                internal static void Debug()
                {
                        LCBMLogger.Info("-----ALLManifestResource-----");
                        foreach (string name in LCBMAssembly.GetManifestResourceNames())
                        {
                                LCBMLogger.Info(name);
                        }
                        LCBMLogger.Info("-----------------------------");
                }

                public static Stream LoadResourceStream(string ResName)
                {
                        Stream stream = LCBMAssembly.GetManifestResourceStream(ResName);

                        return stream;
                }
                


                public static void Shutdown()
                {

                        //todo


                }

        }
}
