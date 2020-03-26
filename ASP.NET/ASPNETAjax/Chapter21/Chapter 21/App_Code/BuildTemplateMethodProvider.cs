using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
namespace CustomComponents5
{
  public class BuildTemplateMethodProvider
  {
    Label label1;
    public void BuildTemplate(Control c)
    {
      label1 = new Label();
      label1.Text = DateTime.Now.ToString();
      c.Controls.Add(label1);
      Button button1 = new Button();
      button1.Text = "Update";
      button1.Click += new EventHandler(Button1_Click);
      c.Controls.Add(button1);
    }
    void Button1_Click(object sender, EventArgs e)
    {
      label1.Text = DateTime.Now.ToString();
    }
    public BuildTemplateMethod GetBuildTemplateMethod()
    {
      return new BuildTemplateMethod(BuildTemplate);
    }
  }
}