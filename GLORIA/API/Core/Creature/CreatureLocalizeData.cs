using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core.Creature
{
        public interface ICreatureLocalizeData
        {

                string GetName(string lang, int observeLevel);

                string GetCodeName(string lang, int observeLevel);

                string GetOpenDesc(string lang, int observeLevel);

                string GetStoryData(string lang, int observeLevel);

                string GetSpecialSkillTips(string lang, int index);


        }
}
