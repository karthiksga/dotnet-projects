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
	public DataAccess()
	{
		
	}

    public DataTable GetCustomerDetails()
    {
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Test"].ToString())) 
            {
                using (SqlCommand objSqlCommand = new SqlCommand())
                {
                    objSqlCommand.Connection = objSqlConnection;
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandText = "Proc_GetCustomers";
                    SqlDataAdapter objAdapter = new SqlDataAdapter();
                    objAdapter.SelectCommand = objSqlCommand;
                    DataTable objDataTable = new DataTable();
                    objAdapter.Fill(objDataTable);
                    return objDataTable;
                }
            }
        }
        catch (Exception ex)
        {
            
        }
        return null;
    }

    public int CreateCustomer(string _firstName,string _lastName)
    {
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Test"].ToString()))
            {
                using (SqlCommand objSqlCommand = new SqlCommand())
                {
                    objSqlCommand.Connection = objSqlConnection;
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandText = "Proc_CreateCustomer";
                    objSqlCommand.Parameters.Add("@FirstName_IN", SqlDbType.VarChar).Value = _firstName.Trim();
                    objSqlCommand.Parameters.Add("@LastName_IN", SqlDbType.VarChar).Value = _lastName.Trim();
                    objSqlCommand.Parameters.Add("@Address1_IN", SqlDbType.VarChar).Value = "a1";
                    objSqlCommand.Parameters.Add("@Address2_IN", SqlDbType.VarChar).Value = "a2";
                    objSqlCommand.Parameters.Add("@Country_IN", SqlDbType.Int).Value = 17;
                    objSqlCommand.Parameters.Add("@State_IN", SqlDbType.Int).Value = 1;
                    objSqlCommand.Parameters.Add("@City_IN", SqlDbType.Int).Value = 8;
                    objSqlCommand.Parameters.Add("@Zip_IN", SqlDbType.VarChar).Value = "12345";
                    objSqlCommand.Parameters.Add("@Phone_IN", SqlDbType.VarChar).Value = "34232";
                    objSqlCommand.Parameters.Add("@Email_IN", SqlDbType.VarChar).Value = "test@test.com";
                    objSqlCommand.Parameters.Add("@IsExist_OUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                    objSqlConnection.Open();
                    objSqlCommand.ExecuteNonQuery();
                    return Convert.ToInt32(objSqlCommand.Parameters["@IsExist_OUT"].Value);
                }
            }
        }
        catch (Exception ex)
        {

        }       
        return 0;
    }

}
