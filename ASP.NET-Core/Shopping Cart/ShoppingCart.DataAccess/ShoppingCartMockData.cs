using Newtonsoft.Json;
using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShoppingCart.DataAccess
{
    public class ShoppingCartMockData : IDataAccess
    {
        private ShoppingCartData ShoppingData { get; set; }

        public string ConfigFilePath
        {
            get
            {
                var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"MockData.json");
                return path;
            }
        }

        public List<Product> Products => ShoppingData.Products;

        public List<Discount> Discounts => ShoppingData.Discounts;

        public List<Rule> Rules => ShoppingData.Rules;

        public List<StateRuleMap> StateRules => ShoppingData.StateRules;

        public ShoppingCartMockData()
        {
            ShoppingData= JsonConvert.DeserializeObject<ShoppingCartData>(File.ReadAllText(ConfigFilePath));
        }       
    }


}
