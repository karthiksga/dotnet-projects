using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
public class Product
{
  public static IEnumerable Select(string sortExpression)
  {
    string connectionString =
    ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    string commandText = "Select CategoryName, ProductName, ProductID," +
                         "Products.CategoryID As CategoryID From Products, Categories " +
                         "Where Products.CategoryID = Categories.CategoryID";
    if (!string.IsNullOrEmpty(sortExpression))
      commandText += " Order By " + sortExpression;
    SqlConnection con = new SqlConnection(connectionString);
    SqlCommand com = new SqlCommand(commandText, con);
    con.Open();
    return com.ExecuteReader(CommandBehavior.CloseConnection);
  }
  public static void Update(int ProductID, string ProductName, int CategoryID)
  {
    string connectionString =
    ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    string commandText = "Update Products Set ProductName=@ProductName," +
                         "CategoryID=@CategoryID Where ProductID=@ProductID";
    SqlConnection con = new SqlConnection(connectionString);
    SqlCommand com = new SqlCommand(commandText, con);
    com.Parameters.AddWithValue("@ProductName", ProductName);
    com.Parameters.AddWithValue("@CategoryID", CategoryID);
    com.Parameters.AddWithValue("@ProductID", ProductID);
    con.Open();
    com.ExecuteNonQuery();
    con.Close();
  }
}
