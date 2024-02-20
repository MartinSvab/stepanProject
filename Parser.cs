using System.ComponentModel.DataAnnotations;
using System.Net;

namespace StepanProject
{
    internal class Parser
    {
        public string[] ReturnRate(string currency, string date)
        {
            string[] data = GetData("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/rok.txt?rok=2024").Split("\n");
            date = FormatDate(date);
            if(!doesCurrencyExist(currency, data)) throw new Exception("Currency is not in database");
            
            string dateToCompare = date == null ? DateTime.Now.ToString("dd.mm.yyyy") : date;
            int currencyIndex = 0;

            for (int i = 0; i < data[0].Split("|").Length; i++)
            {
                try { 
                    if (data[0].Split("|")[i].Split(" ")[1] == currency.ToUpper())
                    {
                        currencyIndex = i;
                        break;
                    }
                }
                catch(IndexOutOfRangeException e){}
            }

            string[] toReturn = new string[2];

            for (int i = 0; i < data.Length; i++)
            {
                if (dateToCompare == data[i].Split("|")[0]){
                    toReturn[0] = data[i].Split("|")[currencyIndex];
                    toReturn[1] = data[i].Split("|")[0];
                    return toReturn;
                }
            }

            Console.WriteLine("DATE NOT FOUND!!!");
            Console.WriteLine($"Date instead: {data[data.Length - 2].Split("|")[0]}");
            toReturn[0] = data[data.Length - 2].Split("|")[currencyIndex];
            toReturn[1] = data[(data.Length - 2)].Split("|")[0];
            return toReturn;
        }

        string GetData(string args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ApplicationException("Specify the URI of the resource to retrieve.");
            }
            using WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            using Stream data = client.OpenRead(args);
            using StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            return s;
        }
    
        bool doesCurrencyExist(string currencyToCheck, string[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                try
                {
                    if (data[0].Split("|")[i].Split(" ")[1] == currencyToCheck)
                    {
                        return true;
                    }
                }
                catch (IndexOutOfRangeException e){}
            }
            return false;
        }

        string FormatDate(string date)
        {
            string[] formattedDate = new string[3];

            formattedDate[0] = date.Substring(0, 4);
            formattedDate[1] = date.Substring(3, 2);
            formattedDate[2] = date.Substring(5, 2);

            return String.Join(".", formattedDate);
        }
    }
}
