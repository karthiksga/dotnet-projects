using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal FinalCost { get; set; }        
        public decimal TaxPercent { get; set; }
        public string TypeLookup { get; set; }
       
        public ProductValueType ValueType { get => (ProductValueType)Enum.Parse(typeof(ProductValueType), TypeLookup); }
    }

    public enum ProductValueType
    {
        Standard=1,
        Luxury=2
    }
}
