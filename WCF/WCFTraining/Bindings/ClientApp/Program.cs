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

            //CustomBinds.CustomBindClient custom = new CustomBinds.CustomBindClient();
            //Console.WriteLine(custom.GetCounter());
            //Console.WriteLine(custom.GetCounter());
            //Console.WriteLine(custom.GetCounter());

            //SecureHW.SecureHelloWorldClient client = new SecureHW.SecureHelloWorldClient();
            //client.ClientCredentials.UserName.UserName = "Test";
            //client.ClientCredentials.UserName.Password = "test";
            //Console.WriteLine(client.HelloWorld()); 

            //MathService.ServiceClient client = new MathService.ServiceClient();
            //Console.WriteLine(client.Add(new MathService.Math { NumA = 1, NumB = 2 }));
            
            //InstanceContext instance = new InstanceContext(new HandleCallBack());
            //SimpleDup.InterfaceDuplexClient dup = new SimpleDup.InterfaceDuplexClient(instance);
            //dup.doHugeTask();
            //Console.ReadLine();
            //dup.Close();

            SampleServiceRef.Service1Client client = new SampleServiceRef.Service1Client();
            client.ClientCredentials.UserName.UserName = "Test";
            client.ClientCredentials.UserName.Password = "test";
            Console.WriteLine(client.GetData());
        }
    }   

    public class HandleCallBack:SimpleDup.InterfaceDuplexCallback
    {
        public void taskCompleted()
        {
            Console.WriteLine("Called by server");
        }
    }
}
