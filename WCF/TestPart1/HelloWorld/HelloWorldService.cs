using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWorld
{
    // NOTE: If you change the class name "HelloWorldService" here, you must also update the reference to "HelloWorldService" in App.config.
    public class HelloWorldService : IHelloWorldService
    {
        #region IHelloWorldService Members

        public string Message(string message)
        {
            return string.Format("Message received from client at {0}:{1}", DateTime.Now, message);
        }

        #endregion
    }
}
