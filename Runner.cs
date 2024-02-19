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
            DatabaseAccesser da = new DatabaseAccesser();
            Parser parser = new Parser();
            Exporter exporter = new Exporter();


            var products = da.GetData(false);
            string returnedRate = parser.ReturnRate(currency.ToUpper(), date);\
            exporter.Export(products, returnedRate);
        }
    }
}
