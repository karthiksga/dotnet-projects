using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaFamilleService.Model
{
    public class CustomException
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}