using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{
    class ItemSourceCsv : ItemSource
    {
        const char SEPARATOR = ',';

        override public bool Load(List<Item> itemList)
        {
            string filename = getLoadFile("CSV Files|*.csv");
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(SEPARATOR);
                    if (line.Length != 3)
                    {
                        Console.WriteLine("Invalid CSV Input");
                        continue;
                    }
                    string name = line[0];
                    int qty;
                    decimal price;
                    if (!int.TryParse(line[1], out qty))
                    {
                        Console.WriteLine("Invalid CSV Input");
                        continue;
                    }
                    if (!decimal.TryParse(line[1], out price))
                    {
                        Console.WriteLine("Invalid CSV Input");
                        continue;
                    }
                    itemList.Add(new Item(name, qty, price));
                }
                sr.Close();
                sr = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        override public void Save(List<Item> itemList)
        {
            string filename = getSaveFile("CSV Files|*.csv");
            if (filename == null) return;
            StreamWriter sw = new StreamWriter(filename);
                
            foreach (Item item in itemList)
            {
                sw.WriteLine(item.Name + SEPARATOR + item.Qty + SEPARATOR + item.Price);
            }
            sw.Close();
            sw = null;

        }
    }
}

