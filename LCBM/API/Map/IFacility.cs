using LCBM.API.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Mangement
{
        internal interface IFacility
        {
                IEnumerable<ISefira> Sefiras {  get; }

                void AddSefira(ISefira sefira,ISefiraLocationInfo location);



        }
}
