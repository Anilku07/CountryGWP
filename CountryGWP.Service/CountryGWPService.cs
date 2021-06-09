using CountryGWP.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CountryGWP.Service
{
    public class CountryGWPService : ICountryGWPService
    {
        private const int MINYEAR = 2008;
        private const int MAXYEAR = 2015;
        private static IList<DataModel> dataModels { get; set; }
        public CountryGWPService() => dataModels =  LoadCSV().Result;

        private async Task <IList<DataModel>> LoadCSV()
        {
            IList<DataModel> listA = new List<DataModel>();
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);
            IList<string> lines = new List<string>();
            using (var reader = new StreamReader(dirPath + @"\1.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    lines.Add(line);
                }
            }

            Parallel.ForEach(lines, line =>
            {
                var values = line.Split(',');
                DataModel dataModel = new DataModel()
                {
                    country = values[0],
                    variableId = values[1],
                    variableName = values[2],
                    lineOfBusiness = values[3],
                    dataYear = new List<DataYear>()
                };
                int i = 0;
                foreach (var item in values)
                {
                    i++;
                    if (i > 3)
                    {
                        DataYear dataYear = new DataYear();
                        dataYear.Year = 2000 + i - 4;

                        try
                        {
                            dataYear.Value = Convert.ToDecimal(item);
                        }
                        catch (Exception)
                        {
                            dataYear.Value = null;
                        }
                        dataYear.Value = dataYear.Value == 0 ? null : dataYear.Value;
                        dataModel.dataYear.Add(dataYear);
                    }

                }
                listA.Add(dataModel);
            });

            //foreach (var line in lines)
            //{
            //    var values = line.Split(',');
            //    DataModel dataModel = new DataModel()
            //    {
            //        country = values[0],
            //        variableId = values[1],
            //        variableName = values[2],
            //        lineOfBusiness = values[3],
            //        dataYear = new List<DataYear>()
            //    };
            //    int i = 0;
            //    foreach (var item in values)
            //    {
            //        i++;
            //        if (i > 3)
            //        {
            //            DataYear dataYear = new DataYear();
            //            dataYear.Year = 2000 + i - 4;

            //            try
            //            {
            //                dataYear.Value = Convert.ToDecimal(item);
            //            }
            //            catch (Exception)
            //            {
            //                dataYear.Value = null;
            //            }
            //            dataYear.Value = dataYear.Value == 0 ? null : dataYear.Value;
            //            dataModel.dataYear.Add(dataYear);
            //        }

            //    }
            //    listA.Add(dataModel);
            //}
            return listA;
        }

        public decimal? GetAverageGWP(string country, string lineOfBusiness)
        {
            var _r = dataModels.FirstOrDefault(x => x.country == country && x.lineOfBusiness == lineOfBusiness);
            if (_r==null)
            {
                return null;
            }
           return _r.dataYear.Where(x => x.Year >= MINYEAR && x.Year <= MAXYEAR).Average(x => x.Value);
        }
    }
}
