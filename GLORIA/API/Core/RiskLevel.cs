using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GLORIA.API.Core.Creature
{


        public readonly struct RiskLevel
        {

                public string Id { get; }

                public int Level { get; }

                public Color Color { get; }

                public RiskLevel(string id,int level,  Color color)
                {
                        Id = id;

                        Level = level;

                        Color = color;
                }
        }


        public static class RiskLevels
        {
                public static Color Color_Zayin = new Color(0.1216f, 0.9725f, 0.0039f, 1f);

                public static RiskLevel ZAYIN = new RiskLevel("Vanilla.ZAYIN", 1, Color_Zayin);

                public static Color Color_Teth = new Color(0.0824f, 0.6314f, 0.9922f, 1f);

                public static RiskLevel TETH = new RiskLevel("Vanilla.TETH", 2, Color_Teth);

                public static Color Color_He = new Color(0.9922f, 0.9765f, 0f, 1f);

                public static RiskLevel HE = new RiskLevel("Vanilla.HE", 3, Color_He);

                public static Color Color_Waw = new Color(0.4941f, 0.1804f, 0.9451f, 1f);

                public static RiskLevel WAW = new RiskLevel("Vanilla.WAW", 4, Color_Waw);

                public static Color Color_Aleph = new Color(0.9059f, 0.0392f, 0.0275f, 1f);

                public static RiskLevel ALEPH = new RiskLevel("Vanilla.ALEPH", 5, Color_Aleph);
        }





}
