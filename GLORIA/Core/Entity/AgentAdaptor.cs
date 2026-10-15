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

                public int RVirtue => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.RVirtue.Value];

                public int WVirtue => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.WVirtue.Value];

                public int BVirtue => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.BVirtue.Value];

                public int PVirtue => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.PVirtue.Value];

                public int MaxHP => (int)_attrsbasevalueDic[UnitAttrTypes.MaxHP.Value];

                public int MaxMental => (int)_attrsbasevalueDic[UnitAttrTypes.MaxMental.Value];

                public int WorkSpeed => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.WorkSpeed.Value];

                public int WorkProb => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.WorkProb.Value];

                public int MoveSpeed => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.MovementSpeed.Value];

                public int AttackSpeed => (int)_attrsbasevalueDic[UnitAttrTypes.Worker.AttackSpeed.Value];

                private readonly Dictionary<string, float> _attrsbasevalueDic = new Dictionary<string, float>();

                public Dictionary<string, float> Attrs => _attrsbasevalueDic;




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

                        _attrsbasevalueDic = new Dictionary<string, float>();
                      
                        AddAttr(UnitAttrTypes.Worker.RVirtue, _agent.originFortitudeStat);
                        AddAttr(UnitAttrTypes.Worker.WVirtue, _agent.originPrudenceStat);
                        AddAttr(UnitAttrTypes.Worker.BVirtue, _agent.originTemperanceStat);
                        AddAttr(UnitAttrTypes.Worker.PVirtue, _agent.originJusticeStat);

                        AddAttr(UnitAttrTypes.MaxHP, RVirtue);
                        AddAttr(UnitAttrTypes.MaxMental, WVirtue);
                        AddAttr(UnitAttrTypes.Worker.WorkProb, BVirtue);
                        AddAttr(UnitAttrTypes.Worker.WorkSpeed, BVirtue);
                        AddAttr(UnitAttrTypes.Worker.AttackSpeed, PVirtue);
                        AddAttr(UnitAttrTypes.Worker.MovementSpeed, PVirtue);



                }





                public void AddAttr(AttrType attr,float baseValue)
                {
                        _attrsbasevalueDic[attr.Value] = baseValue;
                }





        }
}
