using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        //int i = 1;
        //int j = 0;
        //int k = i / j;  
        
        //Trace.Write("This is trace");
        //Log.WriteToLog();

        TraceSwitch oSwitch = new TraceSwitch("showErrors","");
        if(oSwitch.TraceError)
        {
            Trace.Write("This is error");
        }
        else if(oSwitch.TraceWarning)
        {
            Trace.Write("This is warning");
        }
        //Trace.Write(oSwitch.TraceWarning, "This is a warning message");

    }
}
