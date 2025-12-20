using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using ServiceContracts;
namespace WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ChannelFactory<ServiceContracts.IService> factory = new ChannelFactory<IService>("ClientBinding");
            //factory.Open();
            //IService service = factory.CreateChannel();
            //lblTest.Text = service.GetSum(1, 4).ToString();
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();
            lblTest.Text = client.GetSum(1, 4).ToString();
        }
    }
}