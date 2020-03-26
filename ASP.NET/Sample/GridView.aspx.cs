using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class GridView : System.Web.UI.Page
{
    int index = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        gvCustomers.DataSource = CreateList();
        gvCustomers.DataBind();
    }

    private List<Employee> CreateList()
    {
        Employee objEmp1 = new Employee();
        objEmp1.FirstName = "A";
        objEmp1.LastName = "A";
        ArrayList objArray1 = new ArrayList();
        objArray1.Add("Engineer");
        objArray1.Add("Engineer2");
        objEmp1.Profession = objArray1;

        Employee objEmp2 = new Employee();
        objEmp2.FirstName = "B";
        objEmp2.LastName = "B";
        ArrayList objArray2 = new ArrayList();
        objArray2.Add("Doctor");
        objArray2.Add("Doctor2");
        objEmp2.Profession = objArray2;

        List<Employee> objEmps = new List<Employee>();
        objEmps.Add(objEmp1);
        objEmps.Add(objEmp2);
        return objEmps;
    }
    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlProfession = (DropDownList)e.Row.Cells[2].FindControl("ddlProfession");
            List<Employee> objEmps = CreateList();
            ddlProfession.DataSource = objEmps[index].Profession;
            ddlProfession.DataBind();
            index = index + 1;
        }
    }
}
