﻿using System;
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
            //ChannelFactory<ServiceLib.IService> factory = new ChannelFactory<ServiceLib.IService>("Add");
            //factory.Open();
            //ServiceLib.IService addService = factory.CreateChannel();

            //AddService.ServiceClient client = new AddService.ServiceClient();
            //SampleService.AdminServiceClient adminService = new SampleService.AdminServiceClient();
            //Console.WriteLine(adminService.AdminOperationA() + ":" + adminService.AdminOperationB());
                        
            //SampleService.InternalServiceClient internalService = new SampleService.InternalServiceClient();
            //Console.WriteLine(internalService.InternalOperationA() + ":" + internalService.InternalOperationB());

            //Console.WriteLine(client.Add(1,2)+ ":"+ addService.Add(2,4));

            //AdminService.AdminServiceClient adminService = new AdminService.AdminServiceClient();
            //Console.WriteLine(adminService.AdminOperationA() + ":" + adminService.AdminOperationB());

            //InternalService.InternalServiceClient internalService = new InternalService.InternalServiceClient();
            //Console.WriteLine(internalService.InternalOperationA() + ":" + internalService.InternalOperationB());

            MathService.ServiceClient mathService = new MathService.ServiceClient();
            //MathService.Math mat = new MathService.Math();            
            
            
            Console.WriteLine(mathService.Add(new MathService.Math{ NumA=1,NumB=3, StartTime=DateTime.Now}));             

            //Console.WriteLine(mathService.AddResult(new MathService.Math { NumA = 1, NumB = 3 }).Result);
            //Console.WriteLine(mathService.AddResult(new MathService.Math { NumA = 1, NumB = 4 }).Result);

            Console.ReadLine();
        }
    }    
}
