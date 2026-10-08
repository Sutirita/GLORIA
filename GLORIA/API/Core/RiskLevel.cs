using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GLORIA.API.Core.Creature
{


        public readonly struct RiskLevel
        {
                public Color Color { get; }
                public string Value { get; }

                public RiskLevel(string value, Color color)
                {
                        Value = value;

                        Color = color;
                }
        }


        public static class RiskLevels
        {
                public static RiskLevel ZAYIN = new RiskLevel("Vanilla.ZAYIN", new Color(0.1216f, 0.9725f, 0.0039f, 1f));

                
                public static RiskLevel TETH = new RiskLevel("Vanilla.TETH", new Color(0.0824f, 0.6314f, 0.9922f, 1f));

                
                public static RiskLevel HE = new RiskLevel("Vanilla.HE", new Color(0.9922f, 0.9765f, 0f, 1f));


                public static RiskLevel WAW = new RiskLevel("Vanilla.WAW", new Color(0.4941f, 0.1804f, 0.9451f, 1f));

                
                public static RiskLevel ALEPH = new RiskLevel("Vanilla.ALEPH", new Color(0.9059f, 0.0392f, 0.0275f, 1f));
        }





}
