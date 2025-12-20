using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.RulesService
{
    public class RuleBusiness : IRuleService
    {
        IDataAccess DataAccess;
        public RuleBusiness(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        public List<Rule> GetRule(string state, RuleType type)
        {
            var stateRules = DataAccess.StateRules.Where(s => s.State.Equals(state))?.Select(t=> t.RuleId)?.Distinct()?.Select(r=> new { Id=r, LType=type }).ToList();
            var op = (from s in stateRules
                     join r in DataAccess.Rules on new { s.Id } equals new { r.Id } into grp
                     from item in grp.DefaultIfEmpty(new Rule { Id=1, IsTaxAppliedBeforeDiscount=false, LuxuryItemTaxRatio=1, TypeLookup="Product" })
                     select new Rule { Id=item.Id, IsTaxAppliedBeforeDiscount=item.IsTaxAppliedBeforeDiscount, LuxuryItemTaxRatio=item.LuxuryItemTaxRatio, TypeLookup=item.TypeLookup, TaxPercent = item.TaxPercent }).ToList();
            return op.Where(o=>o.Type==type).ToList();
        }
    }
}
