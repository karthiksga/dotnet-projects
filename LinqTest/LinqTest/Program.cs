using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqTest
{
    //http://www.extensionmethod.net/csharp/ienumerable-t/pivot
    internal class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Function { get; set; }
        public decimal Salary { get; set; }
    }

    public class PivotedEntity
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Version { get; set; }
    }

    public class PivotedValue
    {
        public object DataID { get; set; }
        public Dictionary<object, object> PivotedFields { get; set; }
    }

    public class UnpivotedDbRecord
    {
        public int? tID { get; set; }
        public int? ColID { get; set; }
        public string Txt { get; set; }
    }
    class Program
    {        
        static void Main(string[] args)
        {
            //Third Solution
            var context = new DataClasses1DataContext();
            var table3 = context.Table3s;            
            var result2 = new List<PivotedEntity>();

            table3.ToList().ForEach(r =>
            {
                PivotedEntity entity = new PivotedEntity();
                foreach (var item in context.Table1s)
                {
                    entity.Id = item.ColId;
                    entity.SetPropertyValue(item.ColName, r.GetPropertyValue(item.FieldName));                    
                }
                result2.Add(entity);
            });

            foreach (var item in result2)
            {
                Console.WriteLine(item.Id + "\t" + item.Country + "\t" + item.Month + "\t" + item.Day+"\t"+item.Version);
            }

            /*
             * Second Solution
             * /
            var context = new DataClasses1DataContext();            
            var table3 = context.Table3s;
            var result1 = new List<UnpivotedDbRecord>();                     
            
            table3.ToList().ForEach(r=>{
                    
                    foreach (var item in context.Table1s)
                    {
                        result1.Add(new UnpivotedDbRecord{ tID=r.tID, ColID=item.ColId, Txt=r.GetPropertyValue(item.FieldName)});
                    }   
                    
                });

            var table1 = context.Table1s.AsEnumerable();           

            var result = from t1 in table1
                         join t2 in result1 on t1.ColId equals t2.ColID                         
                         select new
                         {
                             t2.tID,                             
                             t1.ColName,
                             t2.Txt
                         };

            var res = result.AsEnumerable().GroupBy(a => a.tID).Select(g =>
            {
                return new
                {
                    ID = g.Key,
                    data = g
                };
            });

            var pivoted = new List<PivotedEntity>();

            foreach (var row in res)
            {
                pivoted.Add(new PivotedEntity
                {
                    Id = (int)row.ID,
                    Country = row.data.Any(a => a.ColName == "Country") ? row.data.Where(x => x.ColName == "Country").FirstOrDefault().Txt : null,
                    Month = row.data.Where(x => x.ColName == "Month").FirstOrDefault().Txt,
                    Day = row.data.Where(x => x.ColName == "Day").FirstOrDefault().Txt,
                    Version = row.data.Any(a => a.ColName == "Version") ? row.data.Where(x => x.ColName == "Version").FirstOrDefault().Txt : null,
                });
            }

            foreach (var item in pivoted)
            {
                Console.WriteLine(item.Id + "\t" + item.Country + "\t" + item.Month + "\t" + item.Day);
            }

            /*
             Get Column Names
             */
            //var columnNames = context.Mapping.MappingSource
            //          .GetModel(typeof(DataClasses1DataContext))
            //          .GetMetaType(typeof(Table3))
            //          .DataMembers;
            //foreach (var item in columnNames)
            //{
            //    Console.WriteLine(item.Ordinal +"\t"+item.Name);    
            //}

            /***
             First Solution for the follwing input table
             * ColId ColName  FieldName- Table1
                    1 Country Field1
                    2 Month   Field2
                    3 Day     Field3
             * 
             * tID ColID Txt - Table2
                1   1   US 
                1   2   July 
                1   3   4 
                2   1   US 
                2   2   Sep 
                2   3   11 
                3   1   US 
                3   2   Dec 
                3   3   25 
             * tID Field1 Field2 Field3 - Table3
                1   US      July    4 
                2   US      Sep     11 
                3   US      Dec     25 

             * */
            //var table1 = context.Table1s;
            //var table2 = context.Table2s;

            //var result = from t1 in table1
            //             join t2 in table2 on t1.ColId equals t2.ColID
            //             select new
            //             {
            //                 t2.tID,
            //                 t1.ColName,
            //                 t2.Txt
            //             };

            //var res = result.AsEnumerable().GroupBy(a => a.tID).Select(g =>
            //{
            //    return new
            //    {
            //        ID = g.Key,
            //        data = g
            //    };
            //});

            //var pivoted = new List<PivotedEntity>();

            //foreach (var row in res)
            //{
            //    pivoted.Add(new PivotedEntity
            //    {
            //        Id = (int)row.ID,
            //        Country = row.data.Any(a => a.ColName == "Country") ? row.data.Where(x => x.ColName == "Country").FirstOrDefault().Txt : null,
            //        Month = row.data.Where(x => x.ColName == "Month").FirstOrDefault().Txt,
            //        Day = row.data.Where(x => x.ColName == "Day").FirstOrDefault().Txt,
            //        Version = row.data.Any(a => a.ColName == "Version") ? row.data.Where(x => x.ColName == "Version").FirstOrDefault().Txt : null,
            //    });
            //}

            ////var dict = new Dictionary<object, PivotedValue>();
            ////foreach (var t in result)
            ////{
            ////    if (!dict.ContainsKey(t.tID)) dict.Add(t.tID, new PivotedValue { DataID = t.tID });
            ////    dict[t.tID].PivotedFields.Add(t.ColName, t.Txt);
            ////}

            //foreach (var item in pivoted)
            //{
            //    Console.WriteLine(item.Id+"\t"+item.Country + "\t" + item.Month + "\t" + item.Day);
            //}

            #region 'Commented
            //var result1 = result.Pivot(r => r.tID, r1 => r1.ColName, r2 => r2.Select(rs => rs.Txt));
            //foreach (var row in result1)
            //{
            //    //Console.WriteLine(item.tID+"\t"+item.ColName+"\t"+item.Txt);
            //    Console.WriteLine(row.Key);
            //    foreach (var column in row.Value)
            //    {
            //        Console.WriteLine("  " + column.Key + "\t" + column.Value);

            //    }
            //}
        //    var l = new List<Employee>() {
        //    new Employee() { Name = "Fons", Department = "R&D", Function = "Trainer", Salary = 2000 },
        //    new Employee() { Name = "Jim", Department = "R&D", Function = "Trainer", Salary = 3000 },
        //    new Employee() { Name = "Ellen", Department = "Dev", Function = "Developer", Salary = 4000 },
        //    new Employee() { Name = "Mike", Department = "Dev", Function = "Consultant", Salary = 5000 },
        //    new Employee() { Name = "Jack", Department = "R&D", Function = "Developer", Salary = 6000 },
        //    new Employee() { Name = "Demy", Department = "Dev", Function = "Consultant", Salary = 2000 }};
                       

        //    var result1 = l.Pivot(emp => emp.Department, emp2 => emp2.Function, lst => lst.Sum(emp => emp.Salary));

        //    foreach (var row in result1)
        //    {
        //        Console.WriteLine(row.Key);
        //        foreach (var column in row.Value)
        //        {
        //            Console.WriteLine("  " + column.Key + "\t" + column.Value);

        //        }
        //    }

        //    Console.WriteLine("----");

        //    var result2 = l.Pivot(emp => emp.Function, emp2 => emp2.Department, lst => lst.Count());

        //    foreach (var row in result2)
        //    {
        //        Console.WriteLine(row.Key);
        //        foreach (var column in row.Value)
        //        {
        //            Console.WriteLine("  " + column.Key + "\t" + column.Value);

        //        }
        //    }

            //    Console.WriteLine("----");
            #endregion
            Console.ReadLine();
        }
    }

    internal static class LinqHelper
    {
        public static string GetPropertyValue(this Table3 myObj, string propertyName)
        {
            var propInfo = typeof(Table3).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static void SetPropertyValue(this PivotedEntity myObj, string propertyName, object value)
        {
            var propInfo = typeof(PivotedEntity).GetProperty(propertyName);

            if (propInfo != null)
            {
                myObj.GetType().InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, myObj, new object[] { value});
                //propInfo.SetValue(myObj,value,null).ToString();
            }            
        }
    }

    public static class LinqExtenions
    {
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();
            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }
            return retVal;
        }

    }
}
