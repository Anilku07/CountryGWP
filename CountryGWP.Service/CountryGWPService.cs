using CountryGWP.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace CountryGWP.Service
{
    public class CountryGWPService : ICountryGWPService
    {
        private List<DataModel> dataModels { get; set; }
        public CountryGWPService()
        {
            dataModels = LoadCSV();
        }

        private List<DataModel> LoadCSV()
        {
            List<DataModel> listA = new List<DataModel>();
            using (var reader = new StreamReader(@"D:\P\core\1\CountryGwp\files\1.csv"))
            {
                //List<string> listB = new List<string>();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
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
                }
            }
            return listA;
        }
    }
}
