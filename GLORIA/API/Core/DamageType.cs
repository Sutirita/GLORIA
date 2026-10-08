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
                public DamageType(string id)
                {
                        Id = id;
                }
        }

        public static class DamageTypes
        {

                public static DamageType RDamage = new DamageType("Vanilla.RWBPDamage.R");

                public static DamageType WDamage = new DamageType("Vanilla.RWBPDamage.W");

                public static DamageType BDamage = new DamageType("Vanilla.RWBPDamage.B");

                public static DamageType PDamage = new DamageType("Vanilla.RWBPDamage.P");

        }
}
