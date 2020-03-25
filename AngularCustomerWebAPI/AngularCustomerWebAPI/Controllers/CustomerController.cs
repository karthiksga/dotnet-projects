using AngularCustomerWebAPI.CustomerSvc;
using AngularCustomerWebAPI.Models;
using CustomerModel;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace AngularCustomerWebAPI.Controllers
{
    [EnableCors(origins:"*",headers:"*", methods:"*")]
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
               
        public JsonResult<List<Customer>> Get()
        {
            try
            {
                CustomerSvc.CustomerServiceClient client = new CustomerSvc.CustomerServiceClient();
                return Json(client.GetCustomers(), new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (FaultException<CustomerError> ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Detail.ErrorMsg));
                //Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Detail.ErrorMsg);
                //throw new Exception(ex.Detail.ErrorMsg);
            }
           
            //return null;
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
