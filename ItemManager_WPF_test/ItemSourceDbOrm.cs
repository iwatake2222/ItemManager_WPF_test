using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{

    public class ItemWrapperDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

    }


    class ItemSourceDbOrm : ItemSource
    {
        override public bool Load(List<Item> itemList)
        {
            string filename = this.getLoadFile("DB Files|*.db3; *.sqlite; *.db");
            if (!File.Exists(filename))
                return false;

            var db = new SQLiteConnection(filename, true);

            List<ItemWrapperDb> itemDbList = db.Table<ItemWrapperDb>().ToList();
            foreach (ItemWrapperDb itemDb in itemDbList)
            {
                itemList.Add(new Item(itemDb.Name, itemDb.Qty, itemDb.Price));
            }

            return true;
        }

        override public void Save(List<Item> itemList)
        {
            string filename = this.getSaveFile("DB Files|*.db3; *.sqlite; *.db");
            if (filename == null)
                return;
            
            var db = new SQLiteConnection(filename, true);
            // create table if not exist
            db.CreateTable<ItemWrapperDb>();

            // todo: reflect update/insert everytime.
            // temporary: refresh all table data
            db.DeleteAll<ItemWrapperDb>();

            foreach (Item item in itemList)
            {
                ItemWrapperDb itemWrapper = new ItemWrapperDb() { Name = item.Name, Qty = item.Qty, Price = item.Price };
                db.Insert(itemWrapper);
            }

        }
    }
}
