Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Partial Class Content
    Inherits System.Web.UI.Page
    Dim objSqlConnString As SqlConnection
    Dim objSqlCommand As SqlCommand
    Dim objDataTable As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objSqlConnString = New SqlConnection(ConfigurationManager.AppSettings("Test"))
        objSqlConnString.Open()
        objSqlCommand = New SqlCommand()
        objSqlCommand.Connection = objSqlConnString
        objSqlCommand.CommandType = CommandType.Text
        objSqlCommand.CommandText = "SELECT * FROM Country where name like '" + Request.QueryString("name") + "%'"

        objDataTable = New DataTable
        Dim objDataAdapter As New SqlDataAdapter
        objDataAdapter.SelectCommand = objSqlCommand
        objDataAdapter.Fill(objDataTable)
        objSqlCommand.ExecuteNonQuery()
        objSqlConnString.Close()

        Response.Write("<table>")

        For Each objdatRow As DataRow In objDataTable.Rows
            Response.Write("<tr><td>" + objdatRow(0).ToString + "</td><td>" + objdatRow(1).ToString + "</td></tr>")
        Next
        Response.Write("</table>")
    End Sub
End Class
