using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity
{
        public interface IAgent
        {
                SefiraEnum Sefira { get; }

                string Name { get; }

                int MaxHP { get; }

                float HP { get; set; }

                int MaxMental { get; set; }

                float Mental { get; set; }

                int MovementSpeed { get; set; }

                int AttackSpeed { get; set; }

                WorkerPrimaryStatExp ExpInfo { get;}




        }
}
