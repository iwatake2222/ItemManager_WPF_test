using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{
    class ItemSourceDbSql : ItemSource
    {
        override public bool Load(List<Item> itemList)
        {
            string filename = this.getLoadFile("DB Files|*.db3; *.sqlite; *.db");
            if (!File.Exists(filename))
                return false;

            string connectString = "Data Source=" + filename;
            SQLiteConnection conn = new SQLiteConnection(connectString);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "SELECT * FROM Item;";
            SQLiteDataReader sdr = cmd.ExecuteReader();
            int ordName = sdr.GetOrdinal("Name");
            int ordQty = sdr.GetOrdinal("Qty");
            int ordPrice = sdr.GetOrdinal("Price");
            while (sdr.Read())
            {
                itemList.Add(new Item(sdr.GetString(ordName), sdr.GetInt32(ordQty), sdr.GetDecimal(ordPrice)));
            }
            sdr.Close();
            conn.Close();

            return true;
        }

        override public void Save(List<Item> itemList)
        {
            string filename = this.getSaveFile("DB Files|*.db3; *.sqlite; *.db");
            if (filename == null)
                return;

            string connectString = "Data Source=" + filename;
            SQLiteConnection conn = new SQLiteConnection(connectString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Item (_id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Qty INTEGER, Price NUMERIC);";
            cmd.ExecuteNonQuery();

            // todo: reflect update/insert everytime.
            // temporary: refresh all table data
            cmd.CommandText = "DELETE FROM item ;";
            cmd.ExecuteNonQuery();

            foreach (Item item in itemList)
            {
                cmd.CommandText = "INSERT INTO Item (Name, Qty, Price) VALUES ( $Name, $Qty, $Price );";
                cmd.Parameters.AddWithValue("Name", item.Name);
                cmd.Parameters.AddWithValue("Qty", item.Qty);
                cmd.Parameters.AddWithValue("Price", item.Price);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}

