using System;
using System.Collections.Generic;

namespace CountryGWP.Data
{
    public class DataModel
    {
        public string country { get; set; }
        public string variableId { get; set; }
        public string variableName { get; set; }
        public string lineOfBusiness { get; set; }
        public IList<DataYear> dataYear { get; set; }
    }
}
