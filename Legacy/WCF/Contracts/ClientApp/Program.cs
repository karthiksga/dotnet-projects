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
            //AddService  -- Start
            AddService.ServiceClient client = new AddService.ServiceClient();   //this is basicHttpBinding
            Console.WriteLine(client.Add(1, 2));

            AddService.InternalServiceClient internalClient = new AddService.InternalServiceClient(); //this is netTcpBinding
            Console.WriteLine(internalClient.InternalOperationA());

            AddService.AdminServiceClient adminClient = new AddService.AdminServiceClient(); //this is basicHttpBinding
            Console.WriteLine(adminClient.AdminOperationA());
            //AddService  -- End

            //Admin Service
            AdminService.AdminServiceClient adminService = new AdminService.AdminServiceClient();   //this is basicHttpBinding
            Console.WriteLine(adminService.AdminOperationA() + ":" + adminService.AdminOperationB());

            //Internal Service
            InternalService.InternalServiceClient internalService = new InternalService.InternalServiceClient(); //this is netTcpBinding
            Console.WriteLine(internalService.InternalOperationA() + ":" + internalService.InternalOperationB());

            Console.ReadLine();
        }
    }    
}
