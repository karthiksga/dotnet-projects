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
            #region Http Client request
            MyService.ServiceClient httpClient = new MyService.ServiceClient();
            try
            {
                httpClient.ThrowException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Console.WriteLine(httpClient.GoodOperation());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region wsHttpClient Request
            MyService.WSServiceClient wsClient = new MyService.WSServiceClient();
            try
            {
                wsClient.ThrowExceptionWS();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Console.WriteLine(wsClient.GoodOperationWS());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region Throw Custom Exception
            try
            {
                httpClient.SetCounter1(16);
            }
            catch (FaultException<MyService.MyFault> fault)
            {
                Console.WriteLine(fault.Message + ":" + fault.Detail.Description);
            }
            #endregion

            //uncomment <errorHandler/> tag in the host config before you run the below code
            #region Handle error using IErrorHandler which is centralized error handler for all your services
            try
            {
                httpClient.SetCounter2(9); //throws invalid operation exception
            }
            catch (FaultException<MyService.MyFault> ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                httpClient.SetCounter2(11); //throws not implemented exception
            }
            catch (FaultException<MyService.MyFault> ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            Console.ReadLine();
        }
    }    
}
