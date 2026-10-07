using GLORIA.API.Core.Creature;
using GLORIA.API.Entity.Creature;
using GLORIA.API.Mangement;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.Core.Creature
{
        internal class CreatureManager : ICreatureManager
        {
                private readonly Dictionary<string, ICreatureUnit> creaturesLib = new Dictionary<string, ICreatureUnit>();


                private readonly Dictionary<SefiraEnum,ISefira> sefiras = new Dictionary<SefiraEnum, ISefira> ();

                public bool Initialize()
                {
                        throw new NotImplementedException();
                }

                IEnumerable<ICreatureUnit> ICreatureManager.GetAll()
                {
                        return creaturesLib.Values;
                }

                ICreatureUnit ICreatureManager.GetCreature(string id)
                {
                        if(!creaturesLib.TryGetValue(id, out ICreatureUnit creature))
                        {
                                Logger.Error($"Can not find Creature{id} in Facility.");
                                return null;
                        }
                        return creature;
                }


                IEnumerable<ICreatureUnit> ICreatureManager.GetCreatures(SefiraEnum sefiraName)
                {
                        if (!sefiras.TryGetValue(sefiraName,out ISefira sefira))
                        {
                                Logger.Error($"Sefira:{sefiraName} No Found.");
                                return new List<ICreatureUnit>();
                        }
                        return sefira.Creatures;
                }
        }
}
