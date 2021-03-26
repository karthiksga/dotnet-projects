using DynamicLinq.Model;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DynamicLinq.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new ExpressionParser();
            var predicate = parser.ParsePredicateOf<Transaction>(JsonDocument.Parse(
                                File.ReadAllText("rules.json")));
            var transactions = Transaction.GetList(1000);
            var filteredTransactions = transactions.Where(predicate).ToList();
            var s = string.Join(Environment.NewLine, filteredTransactions.Select(x => string.Format("Id:{0} Category:{1} Mode:{2} Type:{3} Amount:{4}", x.Id, x.Category, x.PaymentMode, x.TransactionType, x.Amount)).ToArray());
            Console.WriteLine(s);
            //filteredTransactions.ForEach(f=> Console.WriteLine());
        }
    }
}
