using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFServiceLibrary;
namespace WCFBusiness
{
    public class Service : IService
    {
        public int GetProduct(int i, int j)
        {
            return i * j;
        }
    }
}
