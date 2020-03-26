<%@ WebService Language="C#" Class="MyWebService" %>
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections;

public class Author
{
  private string authorName;
  public string AuthorName
  {
    get { return this.authorName; }
    set { this.authorName = value; }
  }
  private int authorID;
  public int AuthorID
  {
    get { return this.authorID; }
    set { this.authorID = value; }
  }
}

public class Book
{
  private string title;
  public string Title
  {
    get { return this.title; }
    set { this.title = value; }
  }
  private string authorName;
  public string AuthorName
  {
    get { return this.authorName; }
    set { this.authorName = value; }
  }
  private string publisher;
  public string Publisher
  {
    get { return this.publisher; }
    set { this.publisher = value; }
  }
  private decimal price;
  public decimal Price
  {
    get { return this.price; }
    set { this.price = value; }
  }
}

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class MyWebService : System.Web.Services.WebService
{
  [WebMethod]
  public Book[] GetBooks(int authorID)
  {
    ConnectionStringSettings settings =
    ConfigurationManager.ConnectionStrings["MyConnectionString2"];
    string connectionString = settings.ConnectionString;
    string commandText = "Select Title, AuthorName, Publisher, Price " +
                         "From Books Inner Join Authors " +
                         "On Books.AuthorID = Authors.AuthorID " +
                         "Where Authors.AuthorID=@AuthorID";
    DataTable dt = new DataTable();
    SqlDataAdapter ad = new SqlDataAdapter(commandText, connectionString);
    SqlParameter parameter = new SqlParameter();
    parameter.ParameterName = "@AuthorID";
    parameter.Value = authorID;
    ad.SelectCommand.Parameters.Add(parameter);
    ad.Fill(dt);
    Book[] books = new Book[dt.Rows.Count];
    for (int i = 0; i < dt.Rows.Count; i++)
    {
      books[i] = new Book();
      books[i].Title = (string)dt.Rows[i]["Title"];
      books[i].AuthorName = (string)dt.Rows[i]["AuthorName"];
      books[i].Publisher = (string)dt.Rows[i]["Publisher"];
      books[i].Price = (decimal)dt.Rows[i]["Price"];
    }
    return books;
  }
  
  [WebMethod]
  public Author[] GetAuthors()
  {
    ConnectionStringSettings settings =
    ConfigurationManager.ConnectionStrings["MyConnectionString"];
    string connectionString = settings.ConnectionString;
    string commandText = "Select AuthorID, AuthorName From Authors";
    DataTable dt = new DataTable();
    SqlDataAdapter ad = new SqlDataAdapter(commandText, connectionString);
    ad.Fill(dt);
    Author[] authors = new Author[dt.Rows.Count];
    for (int i = 0; i < dt.Rows.Count; i++)
    {
      authors[i] = new Author();
      authors[i].AuthorID = (int)dt.Rows[i]["AuthorID"];
      authors[i].AuthorName = (string)dt.Rows[i]["AuthorName"];
    }
    return authors;
  }
}