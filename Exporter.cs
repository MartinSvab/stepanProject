using CsvHelper;
using System.Globalization;
using System.IO;

namespace StepanProject
{
    internal class Exporter
    {
        public void Export(List<string> products, string rate, string rateDate)
        {
            var toExport = new List<Row> { };

            toExport.Add(new Row
            {
                Date = "Date",
                EPName = "English Product Name",
                PriceUSD = "Price in USD",
                PriceCZK = "Price in CZK",
                ConversionDate = "Date of currency conversation"
            });

            for (int i = 0; i < products.Count; i++)
            {
                toExport.Add(new Row
                {
                    Date = DateTime.Now.ToString("yyyy.mm.dd:hh.mm.ss"),
                    EPName = products[i].Split(",")[0],
                    PriceUSD = (checkIfHasCost(products[i])).ToString(),
                    PriceCZK = (checkIfHasCost(products[i]) * double.Parse(rate)).ToString(),
                    ConversionDate = rateDate
                });
            }

            using (var writer = new StreamWriter($"../{DateTime.Now.ToString("yyyy.mm.dd.hh.mm.ss")}-adventureworks.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(toExport);
            }
        }

        double checkIfHasCost(string product)
        {
            double output;

            Console.WriteLine(product.Split(",")[1].Replace(',', '.'));
            Console.WriteLine(double.TryParse(product.Split(",")[1].Replace(',', '.'), CultureInfo.CurrentCulture, out output));
            try { Console.WriteLine(double.Parse(product.Split(",")[1].Replace(',', '.'))); }
            catch (FormatException e) 
            {
                Console.WriteLine(e);
            }

            if (double.TryParse(product.Split(",")[1].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out output))
            {

                return output; 
            }
            else return 0;
        }
    }

    public class Row
    {
        public string Date { get; set; }
        public string EPName { get; set; }
        public string PriceUSD { get; set; }
        public string PriceCZK { get; set; }
        public string ConversionDate { get; set; }
    }
}
