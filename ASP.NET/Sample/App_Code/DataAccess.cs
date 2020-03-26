using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    SqlConnection objSqlConnection;
    SqlCommand objSqlCommand;
	public DataAccess()
	{
        objSqlConnection = new SqlConnection(ConfigurationManager.AppSettings["TestConnectionString"]);
        objSqlCommand = new SqlCommand();
	}

    public DataTable getData()
    {
        SqlDataAdapter objDataAdapter;
        try
        {
            objSqlCommand.Connection = objSqlConnection;
            objSqlCommand.CommandType = CommandType.StoredProcedure;
            objSqlCommand.CommandText = "Proc_GetData";
            objDataAdapter = new SqlDataAdapter();
            objDataAdapter.SelectCommand = objSqlCommand;
            DataTable objDataTable = new DataTable();
            objSqlConnection.Open();
            objDataAdapter.Fill(objDataTable);
            objSqlConnection.Close();
            return objDataTable;
        }
        catch (Exception ex)
        {

        }        
        finally
        {
            objSqlConnection.Close();
            objSqlConnection = null;
            objSqlCommand = null;
            objDataAdapter = null;
        }
        return null;
    }
}
