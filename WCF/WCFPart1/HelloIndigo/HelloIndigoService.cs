using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloIndigo
{
    // NOTE: If you change the class name "HelloIndigoService" here, you must also update the reference to "HelloIndigoService" in App.config.
    public class HelloIndigoService : IHelloIndigoService
    {   
        #region IHelloIndigoService Members

        public string HelloIndigo(string message)
        {
            return string.Format("Message Received at {0}:{1}", DateTime.Now, message);
        }

        #endregion
    }
}
