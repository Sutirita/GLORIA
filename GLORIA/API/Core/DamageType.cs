using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static Mono.Security.X509.X520;

namespace GLORIA.API.Core
{
        public readonly struct DamageType
        {
                public string Id { get; }
                public DamageType(string id,Sprite ico)
                {
                        Id = id;
                        Icon = ico;
                }

                Sprite Icon { get; }

        }

        public static class DamageTypes
        {
                public static Sprite RDamageIco = Resources.Load<Sprite>("RDamage");

                public static Sprite WDamageIco = Resources.Load<Sprite>("WDamage");

                public static Sprite BDamageIco = Resources.Load<Sprite>("BDamage");

                public static Sprite PDamageIco = Resources.Load<Sprite>("PDamage");

                public static DamageType RDamage = new DamageType("Vanilla.RWBPDamage.R", RDamageIco);

                public static DamageType WDamage = new DamageType("Vanilla.RWBPDamage.W", WDamageIco);

                public static DamageType BDamage = new DamageType("Vanilla.RWBPDamage.B", BDamageIco);

                public static DamageType PDamage = new DamageType("Vanilla.RWBPDamage.P", PDamageIco);

        }
}
