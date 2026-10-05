using GLORIA.API.Core.Buff;
using GLORIA.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Event.SystemEvent
{
        public class BuffAddEvent : IBaseEvent
        {
                public IUnit Target { get; }
                public IUnitBuff Buff { get; }

                public BuffAddEvent(IUnit target, IUnitBuff buff)
                {
                        Target = target;
                        Buff = buff;
                }


        }

        public class BuffRemoveEvent : IBaseEvent
        {
                public IUnit Target { get; }
                public IUnitBuff Buff { get; }

                public BuffRemoveEvent(IUnit target, IUnitBuff buff)
                {
                        Target = target;
                        Buff = buff;
                }

        }











}
