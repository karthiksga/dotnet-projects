using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFServiceLibrary
{
    public class Service : IService
    {
        #region IService Members

        public int GetProduct(int x, int y)
        {
            return x * y;
        }

        #endregion
    }
}
