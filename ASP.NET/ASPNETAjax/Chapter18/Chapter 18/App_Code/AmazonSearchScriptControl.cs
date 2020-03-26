using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using com.amazon.webservices;
namespace CustomComponents3
{
  public class AmazonSearchScriptControl : ScriptControl
  {
    public string HtmlGeneratorID
    {
      get
      {
        return ViewState["HtmlGeneratorID"] != null ?
        (string)ViewState["HtmlGeneratorID"] : string.Empty;
      }
      set
      {
        ViewState["HtmlGeneratorID"] = value;
      }
    }
    public string SearchMethod
    {
      get
      {
        return ViewState["SearchMethod"] != null ?
        (string)ViewState["SearchMethod"] : string.Empty;
      }
      set
      {
        ViewState["SearchMethod"] = value;
      }
    }
    public string Path
    {
      get
      {
        return ViewState["Path"] != null ?
        (string)ViewState["Path"] : string.Empty;
      }
      set
      {
        ViewState["Path"] = value;
      }
    }
    public string ClientControlType
    {
      get
      {
        return ViewState["ClientControlType"] != null ?
        (string)ViewState["ClientControlType"] : string.Empty;
      }
      set
      {
        ViewState["ClientControlType"] = value;
      }
    }
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptor =
      new ScriptControlDescriptor(this.ClientControlType, this.ClientID);
      descriptor.AddProperty("pageIndex", 1);
      descriptor.AddScriptProperty("searchMethod", this.SearchMethod);
      descriptor.AddElementProperty("searchTextBox",
      this.ClientID + "_SearchTextBox");
      descriptor.AddElementProperty("searchButton",
      this.ClientID + "_SearchButton");
      descriptor.AddElementProperty("searchResultAreaDiv",
      this.ClientID + "_SearchResultArea");
      descriptor.AddElementProperty("commandBarAreaDiv",
      this.ClientID + "_CommandBarArea");
      descriptor.AddElementProperty("previousButton",
      this.ClientID + "_PreviousButton");
      descriptor.AddElementProperty("nextButton", this.ClientID + "_NextButton");
      descriptor.AddComponentProperty("htmlGenerator", this.HtmlGeneratorID);
      return new ScriptDescriptor[] { descriptor };
    }
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReference reference = new ScriptReference();
      reference.Path = Path;
      return new ScriptReference[] { reference };
    }
    protected override void RenderContents(HtmlTextWriter writer)
    {
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.RenderBeginTag(HtmlTextWriterTag.Td);
      writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0");
      writer.AddStyleAttribute("cellpadding", "0");
      writer.AddStyleAttribute("cellspacing", "0");
      writer.RenderBeginTag(HtmlTextWriterTag.Table);
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.RenderBeginTag(HtmlTextWriterTag.Td);
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
      writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_SearchTextBox");
      writer.AddAttribute(HtmlTextWriterAttribute.Size, "41");
      writer.RenderBeginTag(HtmlTextWriterTag.Input);
      writer.RenderEndTag();
      writer.Write("&nbsp;&nbsp;");
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
      writer.AddAttribute(HtmlTextWriterAttribute.Id,
      this.ClientID + "_SearchButton");
      writer.RenderBeginTag(HtmlTextWriterTag.Button);
      writer.Write("Search");
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
      writer.RenderBeginTag(HtmlTextWriterTag.Td);
      writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
      writer.AddAttribute(HtmlTextWriterAttribute.Id,
      this.ClientID + "_SearchResultArea");
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
      writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
      writer.RenderBeginTag(HtmlTextWriterTag.Td);
      writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
      writer.AddAttribute(HtmlTextWriterAttribute.Id,
      this.ClientID + "_CommandBarArea");
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
      writer.AddAttribute(HtmlTextWriterAttribute.Id,
      this.ClientID + "_PreviousButton");
      writer.RenderBeginTag(HtmlTextWriterTag.Button);
      writer.Write("<< Prev");
      writer.RenderEndTag();
      writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
      writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + "_NextButton");
      writer.RenderBeginTag(HtmlTextWriterTag.Button);
      writer.Write("Next >>");
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
      writer.RenderEndTag();
    }
    protected override HtmlTextWriterTag TagKey
    {
      get { return HtmlTextWriterTag.Table; }
    }
    protected override Style CreateControlStyle()
    {
      return new TableStyle(ViewState);
    }
    public virtual int CellPadding
    {
      get { return ((TableStyle)ControlStyle).CellPadding; }
      set { ((TableStyle)ControlStyle).CellPadding = value; }
    }
    public virtual int CellSpacing
    {
      get { return ((TableStyle)ControlStyle).CellSpacing; }
      set { ((TableStyle)ControlStyle).CellSpacing = value; }
    }
    public virtual HorizontalAlign HorizontalAlign
    {
      get { return ((TableStyle)ControlStyle).HorizontalAlign; }
      set { ((TableStyle)ControlStyle).HorizontalAlign = value; }
    }
    public virtual string BackImageUrl
    {
      get { return ((TableStyle)ControlStyle).BackImageUrl; }
      set { ((TableStyle)ControlStyle).BackImageUrl = value; }
    }
    public virtual GridLines GridLines
    {
      get { return ((TableStyle)ControlStyle).GridLines; }
      set { ((TableStyle)ControlStyle).GridLines = value; }
    }
  }
}