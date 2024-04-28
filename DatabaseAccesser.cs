using Microsoft.Data.SqlClient;

namespace StepanProject
{
    internal class DatabaseAccesser
    {
        public List<string> GetData(bool getItemsWithNullPrice)
        {
            List<string> list = new List<string>();

            const string connectionString = @"Data Source=stbechyn-sql.database.windows.net;Initial Catalog=AdventureWorksDW2020;User ID=prvniit;Password=P@ssW0rd!";
            const string queryString = "SELECT EnglishProductName, DealerPrice FROM DimProduct";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(String.Format("{0}, {1}", reader[0], reader[1]));
                    }
                }
            }

            if (!getItemsWithNullPrice ) 
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Split(",")[1] == " ")
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            for(int i = 0; i < list.Count; i++) if (list[i].Split(",").Length == 3) list[i] = ReplaceAt(list[i], list[i].IndexOf(',', 0),'-');
            
            return list;
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
    }
}

