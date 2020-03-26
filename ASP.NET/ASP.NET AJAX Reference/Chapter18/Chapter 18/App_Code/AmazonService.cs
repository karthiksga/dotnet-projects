using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Collections;
using com.amazon.webservices;
namespace CustomComponents3
{
  public class AmazonService
  {
    public Items Search(int pageIndex, string searchQuery)
    {
      ItemSearchRequest itemSearchRequest = new ItemSearchRequest();
      itemSearchRequest.Keywords = searchQuery;
      itemSearchRequest.SearchIndex = "Books";
      itemSearchRequest.ResponseGroup =
                new string[] { "Small", "Images", "ItemAttributes", "OfferFull" };
      itemSearchRequest.ItemPage = pageIndex.ToString();
      ItemSearch itemSearch = new ItemSearch();
      itemSearch.SubscriptionId =
      ConfigurationManager.AppSettings["SubscriptionID"];
      itemSearch.AssociateTag = "";
      itemSearch.Request = new ItemSearchRequest[1] { itemSearchRequest };
      ItemSearchResponse itemSearchResponse;
      try
      {
        AWSECommerceService amazonService = new AWSECommerceService();
        itemSearchResponse = amazonService.ItemSearch(itemSearch);
      }
      catch (Exception e)
      {
        throw e;
      }
      Items[] itemsResponse = itemSearchResponse.Items;
      // Check for errors in the reponse
      if (itemsResponse == null)
        throw new Exception("Response from amazon.com contains not items!");
      if (itemsResponse[0].Request.Errors != null)
        throw new Exception(
                    "Response from amazon.com contains this error message: " +
                     itemsResponse[0].Request.Errors[0].Message);
      Items items = itemsResponse[0];
      return items;
    }
  }
}