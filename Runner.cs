using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepanProject
{
    internal class Runner
    {
        public void Run(string currency, string date)
        {
            Parser parser = new Parser();
            Console.WriteLine(parser.ReturnRate(currency.ToUpper(), date));
        }
    }
}
