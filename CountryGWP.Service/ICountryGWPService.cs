using CountryGWP.Data;
using System.Collections.Generic;

namespace CountryGWP.Service
{
    public interface ICountryGWPService
    {
        //List<DataModel> LoadCSV();
        decimal? GetAverageGWP(string country, string lineOfBusiness);
    }
}