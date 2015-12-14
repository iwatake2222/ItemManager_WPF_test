using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ItemManager_WPF_test
{
    class ItemSourceXml : ItemSource
    {
        override public bool Load(List<Item> itemList)
        {
            string filename = this.getLoadFile("XML Files|*.xml");
            if (!File.Exists(filename))
                return false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNode xNode = doc.SelectSingleNode("/ItemList");
                foreach (XmlElement item in xNode.ChildNodes)
                {
                    // Parse each element to RetailItem class
                    string name = item.SelectSingleNode("@Name").Value;
                    string qty = item.SelectSingleNode("@Qty").Value;
                    string price = item.SelectSingleNode("@Price").Value;

                    // Add generated class into list
                    itemList.Add(new Item(name, int.Parse(qty), decimal.Parse(price)));
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        override public void Save(List<Item> itemList)
        {
            string filename = this.getSaveFile("XML Files|*.xml");
            if (filename == null)
                return;

            try
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Encoding = new UTF8Encoding(false);
                xws.ConformanceLevel = ConformanceLevel.Document;
                xws.Indent = true;
                XmlWriter writer = XmlWriter.Create(filename, xws);
                writer.WriteStartDocument();
                writer.WriteStartElement("ItemList");
                foreach (Item item in itemList)
                {
                    writer.WriteStartElement("ItemList");
                    writer.WriteAttributeString("Name", item.Name);
                    writer.WriteAttributeString("Qty", item.Qty.ToString());
                    writer.WriteAttributeString("Price", item.Price.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
                writer = null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

        }
    }
}
