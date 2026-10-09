using GLORIA.API;
using GLORIA.API.Core;
using GLORIA.API.Core.Buff;
using GLORIA.API.Entity;
using GLORIA.API.Entity.Worker;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.Core.Entity
{
        internal class AgentAdaptor : IAgent
        {
                private readonly AgentModel _agent;
                public string Id => $"Unit_Agent_{_agent.instanceId}";

                public int RVirtue => (int)_attrs[UnitAttrTypes.Worker.RVirtue.Value];

                public int WVirtue => (int)_attrs[UnitAttrTypes.Worker.WVirtue.Value];

                public int BVirtue => (int)_attrs[UnitAttrTypes.Worker.BVirtue.Value];

                public int PVirtue => (int)_attrs[UnitAttrTypes.Worker.PVirtue.Value];

                public int MaxHP => RVirtue;

                public int MaxMental => WVirtue;

                public int WorkSpeed => BVirtue;

                public int WorkProb => BVirtue;

                public int MoveSpeed => PVirtue;

                public int AttackSpeed => PVirtue;

                private Dictionary<string, float> _attrs = new Dictionary<string, float>();

                public Dictionary<string, float> Attrs => _attrs;




                public void Panic()
                {
                        _agent.Panic();
                }

                public void RecoverHP(float amount)
                {
                        _agent.RecoverHP(amount);
                }

                public void RecoverMental(float amount)
                {
                        _agent.RecoverMental(amount);
                }






                public AgentAdaptor(AgentModel agentModel)
                {
                        _agent = agentModel;

                        _attrs = new Dictionary<string, float>();
                      
                        AddAttr(UnitAttrTypes.Worker.RVirtue, _agent.originFortitudeStat);
                        AddAttr(UnitAttrTypes.Worker.WVirtue, _agent.originPrudenceStat);
                        AddAttr(UnitAttrTypes.Worker.BVirtue, _agent.originTemperanceStat);
                        AddAttr(UnitAttrTypes.Worker.PVirtue, _agent.originJusticeStat);

                        AddAttr(UnitAttrTypes.MaxHP, _agent.primaryStat.maxHP);
                        AddAttr(UnitAttrTypes.MaxMental, _agent.primaryStat.maxMental);
                        AddAttr(UnitAttrTypes.Worker.WorkSpeed, _agent.primaryStat.workProb);
                        AddAttr(UnitAttrTypes.Worker.WorkSpeed, _agent.primaryStat.cubeSpeed);
                        AddAttr(UnitAttrTypes.Worker.AttackSpeed, _agent.primaryStat.attackSpeed);
                        AddAttr(UnitAttrTypes.Worker.AttackSpeed, _agent.primaryStat.attackSpeed);
                        AddAttr(UnitAttrTypes.Worker.MovementSpeed, _agent.primaryStat.movementSpeed);



                }





                public void AddAttr(AttrType attr,float baseValue)
                {
                        _attrs[attr.Value] = baseValue;
                }

        }
}
