using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFServiceLibrary
{
    public class Service : IService
    {
        public int GetProduct(int i, int j)
        {
            return i * j;
        }
    }
}
