using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace CustomComponents3
{
  public class HelperMethods
  {
    public static string QuoteString(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder builder = null;
      int startIndex = 0;
      int count = 0;
      for (int i = 0; i < value.Length; i++)
      {
        char c = value[i];
        if ((((c == '\r') || (c == '\t')) || ((c == '"') ||
        (c == '\''))) || ((((c == '<') || (c == '>')) ||
        ((c == '\\') || (c == '\n'))) || (((c == '\b') ||
        (c == '\f')) || (c < ' '))))
        {
          if (builder == null)
            builder = new StringBuilder(value.Length + 5);
          if (count > 0)
            builder.Append(value, startIndex, count);
          startIndex = i + 1;
          count = 0;
        }
        switch (c)
        {
          case '<':
          case '>':
          case '\'':
            HelperMethods.AppendCharAsUnicode(builder, c);
            continue;
          case '\\':
            builder.Append(@"\\");
            continue;
          case '\b':
            builder.Append(@"\b");
            continue;
          case '\t':
            builder.Append(@"\t");
            continue;
          case '\n':
            builder.Append(@"\n");
            continue;
          case '\f':
            builder.Append(@"\f");
            continue;
          case '\r':
            builder.Append(@"\r");
            continue;
          case '"':
            builder.Append("\\\"");
            continue;
        }
        if (c < ' ')
          HelperMethods.AppendCharAsUnicode(builder, c);
        else
          count++;
      }
      if (builder == null)
        return value;
      if (count > 0)
        builder.Append(value, startIndex, count);
      return builder.ToString();
    }
    public static void AppendCharAsUnicode(StringBuilder builder, char c)
    {
      builder.Append(@"\u");
      builder.AppendFormat("{0:x4}", new object[] { (int)c });
    }
  }
}