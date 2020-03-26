using System;
using System.Data;
using System.Configuration;
using System.Web;
using Microsoft.Web.Preview.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Web.Hosting;
using System.Globalization;
namespace CustomComponents3
{
  public class XsltBridgeTransformer : IBridgeResponseTransformer
  {
    private string _xsltVirtualPath;
    public XsltBridgeTransformer()
    {
      this._xsltVirtualPath = string.Empty;
    }
    public void Initialize(BridgeTransformData data)
    {
      this._xsltVirtualPath = data.Attributes["stylesheetFile"];
    }
    public object Transform(object results)
    {
      string xml = results as string;
      if (xml == null)
        throw new ArgumentException("String Only", "results");
      XmlDocument document = new XmlDocument();
      document.LoadXml(xml);
      XslCompiledTransform transform = new XslCompiledTransform();
      using (StringWriter writer = new StringWriter(CultureInfo.CurrentCulture))
      {
        this._xsltVirtualPath =
        VirtualPathUtility.ToAbsolute(this._xsltVirtualPath);
        using (Stream input = VirtualPathProvider.OpenFile(this._xsltVirtualPath))
        {
          using (XmlReader stylesheet = XmlReader.Create(input))
          {
            transform.Load(stylesheet);
            transform.Transform((IXPathNavigable)document, null,
            (TextWriter)writer);
          }
        }
        return writer.GetStringBuilder().ToString();
      }
    }
  }
}