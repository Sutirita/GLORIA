using GLORIA.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core.Buff
{
        public interface IBuffManager
        {
                void AddBuff(IUnit unit,IUnitBuff buff,int stack=1);

                void RemoveBuff(IUnit unit,IUnitBuff buff,int stack=1);

                bool HasBuff(IUnit unit,string buffid);

                bool TryGetBuff(IUnit unit,string buffid,out IUnitBuff buff);

                int GetBuffStack(IUnit unit,string buffid);
        }
}
