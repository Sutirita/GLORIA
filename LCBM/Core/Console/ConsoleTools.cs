using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace LCBM.Core.Console
{
        internal static class ConsoleTools
        {



                public static string  LogEntryToRichText(LogEntry entry)
                {
                        string colorHex = ColorUtility.ToHtmlStringRGB(entry.color);
                        string openTag = $"<color=#{colorHex}><size={entry.fontSize}>";
                        if (entry.bold) openTag += "<b>";
                        string closeTag = entry.bold ? "</b>" : "";
                        closeTag += "</size></color>";
                        return openTag +entry.message + closeTag;
                }


                /// <summary>
                /// 计算两个字符串的 Jaro-Winkler 相似度，范围 0.0 ~ 1.0。
                /// </summary>
                public static double CalcSimilarity(string s1, string s2)
                {
                        if (s1 == null || s2 == null)return 0.0;

                        if (s1.Equals(s2))return 1.0;

                        // 确保 s1 是较短（或等长）的字符串
                        if (s1.Length > s2.Length)
                        {
                                string tmp = s2;
                                s2 = s1;
                                s1 = tmp;
                        }

                        int maxdist = s2.Length / 2;
                        int c = 0;      
                        int t = 0;                                 
                        int prevpos = -1;

                        for (int ix = 0; ix < s1.Length; ix++)
                        {
                                char ch = s1[ix];
                                int start = Math.Max(0, ix - maxdist);
                                int end = Math.Min(s2.Length, ix + maxdist);
                                for (int ix2 = start; ix2 < end; ix2++)
                                {
                                        if (ch == s2[ix2])
                                        {
                                                c++;
                                                if (prevpos != -1 && ix2 < prevpos)
                                                {
                                                        t++;   
                                                }
                                                prevpos = ix2;
                                                break;
                                        }
                                }
                        }

                     
                        if (c == 0)return 0.0;

                        // 计算 Jaro 得分
                        double score = ((c / (double)s1.Length) +
                                        (c / (double)s2.Length) +
                                        ((c - t) / (double)c)) / 3.0;

                        // (2) 前缀调整（Winkler 修改）
                        int p = 0;                          // 共同前缀长度（最多 4）
                        int last = Math.Min(4, s1.Length);
                        while (p < last && s1[p] == s2[p])
                        {
                                p++;
                        }
                        score += (p * (1 - score)) / 10.0;

                        return score;
                }


                public static List<string> SortBySimlarity(Dictionary<string, double> dict)
                {

                        if(dict == null || dict.Count == 0 ) return null;

                        List<KeyValuePair<string,double>> list  = new List<KeyValuePair<string, double>>(dict);

                        list.Sort((a,b)=>b.Value.CompareTo(a.Value));

                        List<string> result = new List<string>(list.Count);
                        
                        for(int i = 0; i < list.Count; i++)
                        {
                                result.Add(list[i].Key);
                        }
                        return result;
                }




        }

}
