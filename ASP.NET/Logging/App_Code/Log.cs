using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
	public Log()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void WriteToLog()
    {
        //FileStream objStream = new FileStream("F:\\AppLog1.txt", FileMode.OpenOrCreate);
        //TextWriterTraceListener objTraceListener = new TextWriterTraceListener(objStream);
        //Trace.Listeners.Add(objTraceListener);
        //Trace.WriteLine("Hello 15Seconds Reader -- This is first trace message");
        //Trace.WriteLine("Hello again -- This is second trace message");
        //Debug.WriteLine("Hello again -- This is first debug message");
        //Trace.Flush();
        //objStream.Close();
        Trace.WriteLine("hell0 all");
        //Debug.WriteLine("hell0 all this is debug");

        TraceSwitch oSwitch = new TraceSwitch("showErrors", "This is trace for error and warning messages", "1");        
        Trace.WriteLineIf(oSwitch.TraceWarning, "This is a warning message");
    }
}
