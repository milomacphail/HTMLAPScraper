using System;
using System.Data.SqlClient;


namespace HTMLAPScraper
{
    class DatabaseControl
    {
        private const string _connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HAPStockTable;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void InsertScrapeToDatabase(HAPStock stock)
        {
            DataToTable(stock);
        }

        public static void LatestScrapeToDatabase(HAPStock stock)
        {
            LastScrapeToDatabase(stock);
        }

        private static void LastScrapeToDatabase(HAPStock stock)
        {
            string lastScrape = @"IF EXISTS(SELECT* FROM HAPStockTable WHERE Stock Symbol = @stock_symbol)
                                    UPDATE HAPStockTable
                                    SET Time_Scraped=@time_scraped, Last_Price = @last_price, Change = @change, Change_Percent = @change_percent;
                                    WHERE Stock_Symbol = @stock_symbol
                                ELSE 
                                    INSERT INTO HAPStockTable VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent);";

            using (SqlConnection db = new SqlConnection(_connection))
            {

                db.Open();

                Console.WriteLine("Database has been opened.");

                if (db.State == System.Data.ConnectionState.Open)
                {

                    using (SqlCommand command = new SqlCommand(lastScrape, db))
                    {
                        command.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                        command.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                        command.Parameters.AddWithValue("@last_price", stock.LastPrice);
                        command.Parameters.AddWithValue("@change", stock.Change);
                        command.Parameters.AddWithValue("@change_percent", stock.ChangePercent);

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    Console.WriteLine("No database found. Please check database connection.");
                }

                db.Close();
            }
        }

        /*string[] stockFields = { "@time_scraped", "@stock_symbol", "@last_price", "@change", "@change_percent", "@volume", "@shares", "@average_volume", "@market_cap" };

        string[] stockData = { System.Convert.ToString(DateTime.Now), "TEST", "$", "1", "1%", "2", "2", "2", "2" };*/

        private static void DataToTable(HAPStock stock)
        {

            using (SqlConnection db = new SqlConnection(_connection))
            {
                string insertToTable = "INSERT INTO dbo.HAPStockTable (Time_Scraped, Stock_Symbol, Last_Price, Change, Change_Percent) VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent);";
                {
                    db.Open();

                    Console.WriteLine("Database has been opened");

                    if (db.State == System.Data.ConnectionState.Open)
                    {

                        using (SqlCommand dataToTable = new SqlCommand(insertToTable, db))
                        {

                            dataToTable.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                            dataToTable.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                            dataToTable.Parameters.AddWithValue("@last_price", stock.LastPrice);
                            dataToTable.Parameters.AddWithValue("@change", stock.Change);
                            dataToTable.Parameters.AddWithValue("@change_percent", stock.ChangePercent);

                            dataToTable.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No database found. Please check database connection.");
                    }

                    db.Close();
                }
            }
        }
    }
}