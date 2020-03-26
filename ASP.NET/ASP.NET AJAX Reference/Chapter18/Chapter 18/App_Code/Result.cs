using System;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace CustomComponents
{
  [XmlSchemaProvider("ResultsSchema")]
  public class Results : IXmlSerializable
  {
    private Result[] resultField;
    public Result[] Result
    {
      get { return this.resultField; }
      set { this.resultField = value; }
    }
    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }
    void IXmlSerializable.ReadXml(XmlReader reader)
    {
    }
    public static XmlQualifiedName ResultsSchema(XmlSchemaSet xs)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(XmlSchema));
      XmlReader reader =
      XmlReader.Create(HttpContext.Current.Server.MapPath("Results.xsd"));
      XmlSchema schema = (XmlSchema)serializer.Deserialize(reader);
      xs.Add(schema);
      return new XmlQualifiedName("results");
    }
    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      foreach (Result result in this.Result)
      {
        writer.WriteStartElement("result");
        writer.WriteAttributeString("title", result.Title);
        writer.WriteAttributeString("author", result.Author);
        writer.WriteAttributeString("amazonUrl", result.AmazonUrl);
        writer.WriteAttributeString("imageUrl", result.ImageUrl);
        writer.WriteAttributeString("listPrice", result.ListPrice);
        writer.WriteAttributeString("price", result.Price);
        writer.WriteEndElement();
      }
    }
  }
  public class Result
  {
    private string title;
    public string Title
    {
      get { return this.title; }
      set { this.title = value; }
    }
    private string author;
    public string Author
    {
      get { return this.author; }
      set { this.author = value; }
    }
    private string amazonUrl;
    public string AmazonUrl
    {
      get { return this.amazonUrl; }
      set { this.amazonUrl = value; }
    }
    private string imageUrl;
    public string ImageUrl
    {
      get { return this.imageUrl; }
      set { this.imageUrl = value; }
    }
    private string listPrice;
    public string ListPrice
    {
      get { return this.listPrice; }
      set { this.listPrice = value; }
    }
    private string price;
    public string Price
    {
      get { return this.price; }
      set { this.price = value; }
    }
  }
}
