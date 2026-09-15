using GLORIA;
using GLORIA.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GLORIA.Asset
{
        internal static class AssetManager
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
                                Logger.Fatal("Failed to initialize AssetManager.");
                                Logger.Exception(e);
                        }

                        return flag;
                }

                internal static void Debug()
                {
                        Logger.Info("-----ALLManifestResource-----");
                        foreach (string name in LCBMAssembly.GetManifestResourceNames())
                        {
                                Logger.Info(name);
                        }
                        Logger.Info("-----------------------------");
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
