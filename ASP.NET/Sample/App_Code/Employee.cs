using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for Employee
/// </summary>
public class Employee
{
    private string _firstName;

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    private string _LastName;

    public string LastName
    {
        get { return _LastName; }
        set { _LastName = value; }
    }
    private ArrayList _profession;

    public ArrayList Profession
    {
        get { return _profession; }
        set { _profession = value; }
    }

	public Employee()
	{

	}
}
