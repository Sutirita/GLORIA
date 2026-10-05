using GLORIA.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core.Buff
{
        public enum BuffType
        {
                Neutral,
                Positive,
                Negative
        }

        public interface IUnitBuff
        {
                IUnit Owner { get; }
                string Id { get; }
                BuffType BuffType { get; }
                bool Stackable { get; }
                int MaxStack { get; }
                IEnumerable<IStatModifier> StatModifiers { get; }

        }


        public interface ITickableScript
        {
                bool IsPaused { get; }
                void FixedUpdate();
        }

        public interface ITickableBuff:ITickableScript
        {
         
                float Duration { get; }
                float RemainTime { get; set;}
        }





        public readonly struct StatId
        {
                public string Value { get; }

                public StatId(string value)
                {
                        Value = value;
                }
        }

        public static class Stats
        {
                public static readonly StatId HP = new StatId("HP");

                public static readonly StatId Mental = new StatId("Mental");

                public static readonly StatId Movement = new StatId("Movement");

                public static readonly StatId AttackSpeed = new StatId("AttackSpeed");

                public static readonly StatId Defense = new StatId("Defense");


        }





        public enum OperationType
        {
                Add,
                Mult
        }

        public interface IStatModifier
        {
                StatId StatID { get; }


                float Value { get; }


                float FlatValue { get; }




        }




        public interface IUnitBuffListener : ITickableScript
        {

        }



}






















