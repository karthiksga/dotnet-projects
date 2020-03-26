using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CustomComponents
{
  public class MasterDetailContainer : TableCell, INamingContainer
  {
    private ContainerType containerType;
    public MasterDetailContainer(ContainerType containerType)
    {
      this.containerType = containerType;
    }
    public ContainerType ContainerType
    {
      get { return containerType; }
    }
  }
}