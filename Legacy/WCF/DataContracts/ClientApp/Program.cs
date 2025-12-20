using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MathService.ServiceClient mathService = new MathService.ServiceClient();
            MathService.MathA mathA = new MathService.MathA(); 
            //mathA.StartTime - this property does not exists if it is set as NonSerialized

            MathService.MathB mathB = new MathService.MathB();
            //mathB.StartTime - this property does not exists if it is not set as [DataMember]  

            try
            {
                mathB = mathService.AddResultB(new MathService.MathB { NumA = 1, NumB = 2 });    //this works if StartTime has isRequired=true and emitDefaultValue=true                
                Console.WriteLine(mathB.Result);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }            

            mathB = mathService.AddResultB(new MathService.MathB { NumA = 1, NumB = 2,StartTime=DateTime.Now });    //this works if StartTime has isRequired=true and emitDefaultValue=false
            Console.WriteLine(mathB.Result);
            Console.ReadLine();
        }
    }    
}
