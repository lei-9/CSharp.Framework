using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using CSharp.Framework.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace CSharp.Framework.UnitTest
{
    public class TestExcelHelper
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestExcelHelper(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestExport()
        {
            var random = new Random();
            //
            // var list = new List<TestExcelExportModel>();
            // for (int i = 0; i < 10000; i++)
            // {
            //     list.Add(new TestExcelExportModel
            //     {
            //         Id = Guid.NewGuid().ToString(),
            //         CreateByAt = DateTime.Now.AddDays(i),
            //         ExportEnum = (TestExcelExportEnum) random.Next(0, 2)
            //     });
            // }

            var list = new List<dynamic>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateByAt = DateTime.Now.AddDays(i),
                    ExportEnum = (TestExcelExportEnum) random.Next(0, 3)
                });
            }


            ExcelHelper.Export(list, "template/2.xlsx");
        }

        [Fact]
        public void TestRead()
        {
            var excelPath = "template/1.xlsx";
            //var list = ExcelHelper.Read<TestReadExcelModel>(excelPath);

            //var list = ExcelHelper.Read(excelPath);

            //var json = JsonConvert.SerializeObject(list);

            // foreach (IDictionary<string, object> item in list)
            // {
            // }


            var list = ExcelHelper.Read(excelPath, new Dictionary<string, string>
            {
                {"日期", "Date"},
                {"姓名", "Name"},
                {"饭否", "HasEat"},
            });

            foreach (var item in list)
            {
                Console.WriteLine(item.Date);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.HasEat);
            }
        }
    }

    public class TestReadExcelModel
    {
        public string Name { get; set; }

        public int Num { get; set; }

        public DateTime? CreateAt { get; set; }
    }

    public class TestExcelExportModel
    {
        [Description("主键")] public string Id { get; set; }

        public DateTime CreateByAt { get; set; }
        [Description("导出状态")] public TestExcelExportEnum ExportEnum { get; set; }
    }

    public enum TestExcelExportEnum
    {
        [Description("无")] None = 0,
        [Description("正常")] Success = 1,
        [Description("很好")] Fail = 2,
    }
}