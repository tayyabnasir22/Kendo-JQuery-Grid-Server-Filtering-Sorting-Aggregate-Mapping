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
            //string gridConfigString = "{\"Take\":50,\"Skip\":0,\"Page\":1,\"pageSize\":50,\"Sort\":null,\"Aggregate\":null,\"Filter\":{\"Logic\":\"and\",\"filters\":[{\"field\":\"Id\",\"Operator\":\"eq\",\"Value\":\"7\",\"Logic\":null,\"filters\":null},{\"field\":\"FirstName\",\"Operator\":\"contains\",\"Value\":\"Ismael\",\"Logic\":null,\"filters\":null}]}}";
            
            //DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(gridConfigString);

            //Filter filter = new Filter() {
            //    filters = request.Filter.filters,
            //    Logic = request.Filter.Logic
            //};

            //string sqlString = KendoGridFASMS.Refactor.DataSourceFilterMapHelper.RecursiveFilterExpressionBuilder(filter);



            //DataSourceFilterMapHelper_Refactor someGrid = new DataSourceFilterMapHelper_Refactor(); 



            //string refactorString = someGrid.RecursiveFilterExpressionBuilder(filter);
            KendoGridColumnMapperBuilder newItemGridColumnMapper = new KendoGridColumnMapperBuilder();

            Dictionary<string, KendoGridColumnMapping> newItemGridColumnMappings = newItemGridColumnMapper
                .AddingColumnMapping(new KendoGridColumnMapping()
                {
                    ui = "AceArticleNumber",
                    db = "ersc.AceStockNum", 
                    type = "", 
                })
                .Build(); 


            Console.ReadKey(); 
        }
    }

    #region test code

    public class KendoGridColumnMapperBuilder
    {
        private Dictionary<string, KendoGridColumnMapping> mappings = new Dictionary<string, KendoGridColumnMapping>(); 

        public KendoGridColumnMapperBuilder AddingColumnMapping(KendoGridColumnMapping mapping)
        {
            mappings.Add(mapping.ui,mapping); 

            return this;
        }

        public Dictionary<string, KendoGridColumnMapping> Build()
        {
            return mappings;
        }
    }

    public class KendoGridColumnMapping
    {
        public string ui { get; set; }

        public string db { get; set; }

        public string type { get; set; }
    }




    //KendoGridParameterParser.DataSourceFilterMapHelper.AddColumnTypeMapping("Row_Number", KendoGridParameterParser.Models.FieldType.number);
    //I'm not configuring the grid here
    //I'm Mapping Columns 
    //KendoGridColumnMapperBuilder




    //public class GridConfig
    //{
    //    public GridConfig()
    //    {
    //        columns = new List<GridColumnConfig>(); 
    //    }

    //    public List<GridColumnConfig> columns { get; set; }
    //}




    //public class Driver
    //{
    //    public void Main()
    //    {


    //        GridConfigBuilder grid = new GridConfigBuilder()
    //            .AddColumnConfig(new GridColumnConfig()
    //            {
    //                db = "",
    //                ui = "", 
    //                type = ""
    //            })
    //            .AddColumnConfig(new GridColumnConfig()
    //            {

    //            }); 





    //        //grid.AddColumnConfig(new GridColumnConfig()
    //        //{
    //        //    ui = "", 
    //        //    db = "",
    //        //    type = ""
    //        //})
    //        //.AddColumnConfig(new GridColumnConfig()
    //        //{

    //        //}); 
    //    }
    //}

    #endregion
}