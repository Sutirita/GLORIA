using GLORIA.API.Map;
using System.Collections.Generic;

namespace GLORIA.API.Mangement
{
        public interface IFacility
        {
                IEnumerable<ISefira> Sefiras {  get; }

                void AddSefira(ISefira sefira,ISefiraLocationInfo location);



        }
}
