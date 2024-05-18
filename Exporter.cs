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

            for (int i = 0; i < products.Count; i++)
            {
                double priceUSD = checkIfHasCost(products[i]);
                double priceCZK = priceUSD * double.Parse(rate);

                toExport.Add(new Row{
                    Date = DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss"),
                    EnglishProductName = products[i].Split(",")[0],
                    PriceUSD = priceUSD,
                    PriceCZK = priceCZK,
                    ConversionDate = rateDate
                });
            }

            string fileNameOrLikePathIG = $"../{DateTime.Now.ToString("yyyy.MM.dd-HH-mm-ss")}-adventureworks.csv";

            using (var writer = new StreamWriter(fileNameOrLikePathIG))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(toExport);
            }

            Console.WriteLine($"\n\nData saved to {Path.GetFullPath(fileNameOrLikePathIG)}\nPress any button to exit");

            Console.ReadKey();
        }

        double checkIfHasCost(string product)
        {
            double output = 0;

            for (int i = 0; i < product.Length; i++)
            {
                if (product[i] == ',')
                {
                    try { return double.Parse(product.Substring(i + 1).Trim()); }
                    catch (FormatException e) { }
                }
            }

            return output;
        }

        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public class Row
        {
            public string Date { get; set; }
            public string EnglishProductName { get; set; }
            public double PriceUSD { get; set; }
            public double PriceCZK { get; set; }
            public string ConversionDate { get; set; }
        }
    }
}
