using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Rules
{
    public class RuleData
    {
        //List<KeyValuePair<string, RuleBase>> RuleInfo = new List<KeyValuePair<string, RuleBase>>();
        Dictionary<string, RuleBase> RuleInfo = new Dictionary<string, RuleBase>();

        public RuleData()
        {
            RuleInfo.Add("GA", new RuleOne());            
            RuleInfo.Add("NY", new RuleOne());
            RuleInfo.Add("FL", new RuleTwo());
            RuleInfo.Add("NM", new RuleTwo());
            RuleInfo.Add("NV", new RuleTwo());

            //RuleInfo.Add(new KeyValuePair<string, RuleBase>("GA", new RuleOne()));
            //RuleInfo.Add(new KeyValuePair<string, RuleBase>("FL", new RuleOne()));
            //RuleInfo.Add(new KeyValuePair<string, RuleBase>("NY", new RuleOne()));
            //RuleInfo.Add(new KeyValuePair<string, RuleBase>("NM", new RuleOne()));
            //RuleInfo.Add(new KeyValuePair<string, RuleBase>("NV", new RuleOne()));            
        }
        public RuleBase GetRuleByState(string state)
        {
            return RuleInfo[state];
        }
    }
}
