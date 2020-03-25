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
        public List<Discount> Discounts { get; set; }
        public ProductValueType ValueType { get; set; }
    }

    public enum ProductValueType
    {
        Standard=1,
        Luxury=2
    }
}
