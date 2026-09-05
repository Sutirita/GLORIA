using BepInEx.Configuration;
using LCBM.Asset;
using LCBM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace LCBM.Core
{
        internal static class LCBMConfig
        {
                private static ConfigFile _config;

                private static readonly Dictionary<string, Dictionary<string, string>>
                        _configDescDict = new Dictionary<string, Dictionary<string, string>>
                        {
                            {"cn", new Dictionary<string, string>()},

                            {"en", new Dictionary<string, string>()},

                            {"jp", new Dictionary<string, string>()},

                            {"ru", new Dictionary<string, string>()},

                            {"kr", new Dictionary<string, string>()}
                        };




                //-----------------设置---------------

                //启用自动备份存档
                public static ConfigEntry<bool> EnableAutoSaveBackUp;

                //启用控制台
                public static ConfigEntry<bool> LCBMConsole;

                //启用BUG修复

                public static ConfigEntry<bool> EnableBugFix;

                //------------------------------------

                //数值精度显示调整
                public static ConfigEntry<int> Precision_Damage;
                public static ConfigEntry<int> Precision_WorkPorb;
                public static ConfigEntry<int> Precision_Defense;
                public static ConfigEntry<int> Precision_CreatureHP;
                public static ConfigEntry<int> Precision_AgentStat;

                //------------------------------------


                public static void Initialize(LCBaseModPlugin plugin)
                {
                        _config = plugin.Config;


                        LoadConfigDesc();

                        //function
                        EnableAutoSaveBackUp = BindConfig<bool>( "LCBaseMod", "AutoSaveBackUp", true, GetConfigDesc("AutoSaveBackUp"));


                        LCBMConsole = BindConfig<bool>( "LCBaseMod", "LCBMConsole", true, GetConfigDesc("LCBMConsole"));

                        EnableBugFix = BindConfig<bool>("LCBaseMod", "EnableBugFix", true, GetConfigDesc("EnableBugFix"));


                        //MoreDetails Setting

                        Precision_Damage = BindConfig<int>( "MoreDetails", "DamagePrecision", 2, GetConfigDesc("DmgPrecision"));

                        Precision_Defense = BindConfig<int>("MoreDetails", "DefensePrecision", 3, GetConfigDesc("DefensePrecision"));

                        Precision_AgentStat = BindConfig<int>("MoreDetails", "AgentStatPrecision", 2, GetConfigDesc("AgentSoltPrecision"));

                        Precision_WorkPorb = BindConfig<int>("MoreDetails", "WorkSuccess", 2, GetConfigDesc("WorkSuccessPrecision"));

                        Precision_CreatureHP = BindConfig<int>("MoreDetails", "HpPrecision", 2, GetConfigDesc("CreatureHpPrecision"));



                }


                private static string GetConfigDesc(string key, string lang = LCBMStaticData.DEFAULT_LANG)
                {
                        if (!_configDescDict[lang].TryGetValue(key, out string Desc)) return LCBMStaticData.DESC_NO_FOUND;
                        
                        if(String.IsNullOrEmpty(Desc)) return LCBMStaticData.DESC_NO_FOUND;
                                
                        return Desc;
          
                }

                private static ConfigEntry<T> BindConfig<T>(string section, string key, T defaultValue, string Desc)
                {
                        ConfigDescription configDescription = new ConfigDescription(Desc, null, LCBMStaticData.EmptyObjList);

                        ConfigEntry<T> configEntry = _config.Bind<T>(section, key, defaultValue, configDescription);

                        return configEntry;
                }





                private static void LoadConfigDesc()
                {
                        try
                        {
                                Stream stream = LCBMAssetManager.LoadResourceStream(LCBMStaticData.ConfigDescRes);
                                XmlDocument ConfigxmlDocument = new XmlDocument();
                                ConfigxmlDocument.Load(stream);
                                foreach (XmlNode cfgnode in ConfigxmlDocument.SelectNodes("ConfigDesc/Config"))
                                {
                                        string cfgname = cfgnode.Attributes.GetNamedItem("name").InnerText;

                                        foreach (XmlNode DescNode in cfgnode)
                                        {
                                                string lang = DescNode.Attributes.GetNamedItem("lang").InnerText;

                                                string desc = DescNode.InnerText;

                                                if (_configDescDict.ContainsKey(lang))
                                                {
                                                        _configDescDict[lang].Add(cfgname, desc);
                                                }


                                        }
                                }
                        }
                        catch (Exception e)
                        {
                                LCBMLogger.Error(e.Source + e.StackTrace);
                        }

                }




                public static void Shutdown()
                {

                        _config.Save();
                        //todo


                }

        }
}
