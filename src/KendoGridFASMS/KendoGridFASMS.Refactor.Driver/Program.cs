using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KendoGridFASMS.Refactor.Models;
using Newtonsoft.Json;
using KendoGridFASMS.Refactor.Models; 

namespace KendoGridFASMS.Refactor.Driver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string gridConfigString = "{\"Take\":50,\"Skip\":0,\"Page\":1,\"pageSize\":50,\"Sort\":null,\"Aggregate\":null,\"Filter\":{\"Logic\":\"and\",\"filters\":[{\"field\":\"Id\",\"Operator\":\"eq\",\"Value\":\"7\",\"Logic\":null,\"filters\":null},{\"field\":\"FirstName\",\"Operator\":\"contains\",\"Value\":\"Ismael\",\"Logic\":null,\"filters\":null}]}}";
            
            DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(gridConfigString);

            Filter filter = new Filter() {
                filters = request.Filter.filters,
                Logic = request.Filter.Logic
            };

            DataSourceFilterMapHelper.AddColumnMapping("Id", new ColumnMapping()
            {
                DatabaseColumnName = "DatabaseIdColumn", 
                Type = FieldType.number
            });

            //DataSourceFilterMapHelper.AddColumnMapping("FirstName", new ColumnMapping()
            //{
            //    DatabaseColumnName = "DatabaseFirstName",
            //    Type = FieldType.@string
            //});

            //testing my adapter code to ensure old functionality still works 
            DataSourceFilterMapHelper.AddColumnMapping("FirstName", FieldType.@string); 

            string sqlString = DataSourceFilterMapHelper.RecursiveFilterExpressionBuilder(filter);

            Console.ReadKey(); 
        }
    }
}