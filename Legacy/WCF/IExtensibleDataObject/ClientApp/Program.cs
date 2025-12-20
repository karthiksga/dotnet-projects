using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ClientApp.MathService;
namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MathService.ServiceClient mathService = new MathService.ServiceClient();
            
            MathService.StudentA studA=mathService.GetStudA(new StudentA { Name="sdfsf", StudentId=1,Description="descA" });
            Console.WriteLine(studA.Name + ":" + studA.Description);

            MathService.StudentB studB = mathService.GetStudB(new StudentB { Name = "sdfsf", StudentId = 1, Description="descB" });
            Console.WriteLine(studB.Name + ":" + studB.Description);

            //this setting <dataContractSerializer ignoreExtensionDataObject="true"/> will omit additional data from client without any code change
                  
            Console.ReadLine();
        }
    }    
}
