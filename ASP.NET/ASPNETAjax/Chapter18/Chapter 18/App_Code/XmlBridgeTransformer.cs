using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Web.Preview.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Text;
namespace CustomComponents3
{
  public class XmlBridgeTransformer : IBridgeResponseTransformer
  {
    public void Initialize(BridgeTransformData data) { }
    public object Transform(object results)
    {
      object obj2;
      XmlSerializer serializer = new XmlSerializer(results.GetType());
      MemoryStream w = new MemoryStream();
      using (XmlTextWriter writer = new XmlTextWriter(w, Encoding.UTF8))
      {
        serializer.Serialize((XmlWriter)writer, results);
        w.Position = 0;
        using (StreamReader reader = new StreamReader(w))
        {
          obj2 = reader.ReadToEnd();
        }
      }
      return obj2;
    }
  }
}