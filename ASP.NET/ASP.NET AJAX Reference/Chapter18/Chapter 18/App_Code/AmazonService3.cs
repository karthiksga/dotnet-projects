using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Xml;
using System.Collections;
using com.amazon.webservices;
using System.Xml.Serialization;
using System.Xml.Schema;
namespace CustomComponents3
{
  public class AmazonService3
  {
    public CustomComponents.Results Search(int pageIndex, string searchQuery)
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
      Item[] results = items.Item;
      if (results == null || results.Length == 0)
        return null;
      Item item;
      CustomComponents.Result result;
      ArrayList list = new ArrayList();
      for (int i = 0; i < results.Length; i++)
      {
        item = results[i];
        if (item == null)
          continue;
        result = new CustomComponents.Result();
        if (!string.IsNullOrEmpty(item.ItemAttributes.Title))
          result.Title = item.ItemAttributes.Title;
        if (item.ItemAttributes.Author != null &&
        item.ItemAttributes.Author.Length != 0)
          result.Author = item.ItemAttributes.Author[0];
        if (!string.IsNullOrEmpty(item.DetailPageURL))
          result.AmazonUrl = item.DetailPageURL;
        if (item.MediumImage != null)
          result.ImageUrl = item.MediumImage.URL;
        if (item.ItemAttributes.ListPrice != null)
          result.ListPrice = item.ItemAttributes.ListPrice.FormattedPrice;
        if (item.Offers != null)
        {
          Offer[] offerArray = item.Offers.Offer;
          if (offerArray != null && offerArray.Length != 0)
          {
            if (offerArray[0].OfferListing != null &&
            offerArray[0].OfferListing.Length != 0)
            {
              if (offerArray[0].OfferListing[0].Price != null)
                result.Price =
                item.Offers.Offer[0].OfferListing[0].Price.FormattedPrice;
            }
          }
        }
        list.Add(result);
      }
      CustomComponents.Results results2 = new CustomComponents.Results();
      results2.Result = new CustomComponents.Result[list.Count];
      list.CopyTo(results2.Result);
      return results2;
    }
  }
}