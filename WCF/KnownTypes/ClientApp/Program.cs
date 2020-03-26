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
            Console.WriteLine(mathService.GetShape(new Triangle{ Type="Triangle"}));
            Console.WriteLine(mathService.GetShape(new Square { Type = "Square" }));
            
            Console.ReadLine();
        }
    }    
}
