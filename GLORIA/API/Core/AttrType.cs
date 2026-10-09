
namespace GLORIA.API.Core
{
        public readonly struct AttrType
        {
                public string Value { get; }

                public AttrType(string id)
                {
                        Value = id;
                }
        }
        public static class UnitAttrTypes
        {
             //单位的时间流速
             //   public static readonly StatType TimeMult = new StatType("Unit.TimeMult");
             
                //单位的最大HP
                public static readonly AttrType MaxHP = new AttrType("Unit.MaxHP");
                //单位的最大MP
                public static readonly AttrType MaxMental = new AttrType("Unit.MaxMental");
                //单位的攻击速度
                public static readonly AttrType AttackSpeed = new AttrType("Unit.AttackSpeed");
                //单位的伤害修正
                public static readonly AttrType AttackMult = new AttrType("Unit.AttackMult");
                //单位的移速
                public static readonly AttrType Movement = new AttrType("Unit.Movement");
                //移速修正
                public static readonly AttrType MovementMult = new AttrType("Unit.MovementMult");

                //原版的四项抗性
                public static readonly AttrType Rdefense = new AttrType("Unit.Defense.R");
                public static readonly AttrType Wdefense = new AttrType("Unit.Defense.W");
                public static readonly AttrType Bdefense = new AttrType("Unit.Defense.B");
                public static readonly AttrType Pdefense = new AttrType("Unit.Defense.P");

                //抗性的修正
                public static readonly AttrType RdefenseMult = new AttrType("Unit.DefenseMult.R");
                public static readonly AttrType WdefenseMult = new AttrType("Unit.DefenseMult.W");
                public static readonly AttrType BdefenseMult = new AttrType("Unit.DefenseMult.B");
                public static readonly AttrType PdefenseMult = new AttrType("Unit.DefenseMult.P");

                public static class Worker {

                        //职员的四属性
                        public static readonly AttrType RVirtue = new AttrType("Unit.Worker.Virtues.R");
                        public static readonly AttrType WVirtue = new AttrType("Unit.Worker.Virtues.W");
                        public static readonly AttrType BVirtue = new AttrType("Unit.Worker.Virtues.B");
                        public static readonly AttrType PVirtue = new AttrType("Unit.Worker.Virtues.P");

                        //工作成功率属性
                        public static readonly AttrType WorkProb = new AttrType("Unit.Worker.WorkProb");
                        //工作速度属性
                        public static readonly AttrType WorkSpeed = new AttrType("Unit.Worker.WorkSpeed");
                        //攻击速度属性
                        public static readonly AttrType AttackSpeed = new AttrType("Unit.Worker.AttackSpeed");
                        //移动速度属性
                        public static readonly AttrType MovementSpeed = new AttrType("Unit.Worker.MovementSpeed");

                }

        }

}
