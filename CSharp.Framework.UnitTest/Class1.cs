using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CSharp.Framework.Extensions;
using CSharp.Framework.Helper;
using Xunit;

namespace CSharp.Framework.UnitTest
{
    public class Class1
    {
        [Fact]
        public void First()
        {
            var num = 1 + 1;

            var user = new User
            {
                // CreateByAt = DateTime.Now,
                // Id = Guid.NewGuid().ToString()
                Items = new List<User>()
            };

            var properties = user.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var datetimeText = "";
                var val = propertyInfo.GetValue(user, null);
                var valType = val.GetType().ToString();
                if (valType == "System.DateTime")
                    datetimeText = Convert.ToDateTime(propertyInfo.GetValue(user, null)).ToString("yyyy-MM-dd");
                if (propertyInfo.PropertyType.IsEnum)
                {
                }
                else if (propertyInfo.PropertyType.IsArray)
                {
                }

                var descAttr = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var desc = descAttr == null ? null : ((DescriptionAttribute) descAttr[0]).Description;


                var valText = val.ToString();
            }
        }


        [Fact]
        public void TestDynamicObjectExtension()
        {
            dynamic d = new ExcelDynamicObject();
            d.AddProperty("item1", "item1");
            d.AddProperty("item2", "item2");

            var success = d is ExcelDynamicObject;
            dynamic a = new Object();
            
            success = a is ExcelDynamicObject;
            
            Console.WriteLine(d.item1);
            Console.WriteLine(d.item2);
        }
    }


    public class User
    {
        //public DateTime CreateByAt { get; set; }

        //public string Id { get; set; }
        //[Description("好人")]
        public UserStatus UserStatus { get; set; }
        public List<User> Items { get; set; }
    }

    public enum UserStatus
    {
        正常 = 0,
    }
}