using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace TimeThatTellsTimeWpf
{
    class Logik
    {
        public Logik(string name, string path)
        {
            if (CheckIfNameIsCorrent(CheckCorrentInput(name)) == "Corrent")
                InsertImageAndName(path, name);
            else
                Debug.WriteLine("No name");
        }
        string api = null;
        private string CheckCorrentInput(string name)
        {
            try
            {
                string html = string.Empty;
                name = "https://steamcommunity.com/market/priceoverview/?currency=3&appid=252490&market_hash_name=" + name;
                api = name;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(name);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                return html;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        string CheckIfNameIsCorrent(string nameOfItem)
        {
            if (nameOfItem.Contains("true"))
                return "Corrent";
            else
                return "No item with that name";
        }



        SqlConnection conn = new SqlConnection();
        private void InsertImageAndName(string path, string name)
        {
            conn.ConnectionString = "Data Source=ZBC-EMA-23111; Initial Catalog=Inject; Integrated Security=true";
            Debug.WriteLine(path);
            string commandText = "insert into SteamMarket ([Price], [Name], [API], [Img]) select @Price,@Name,@API,BulkColumn from Openrowset (Bulk '" + path + "', SINGLE_BLOB) as image";

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                //string img = "BulkColumn from Openrowset (Bulk '" + path + "', SINGLE_BLOB) as image";
                conn.Open();
                cmd.Parameters.AddWithValue("@Price", 100);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@API", 200);
                //cmd.Parameters.AddWithValue("@Img", img);
                
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
