using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceContracts;
namespace Business
{
    public class Service : IService
    {
        public int GetSum(int i, int j)
        {
            return i + j;
        }
    }
}
