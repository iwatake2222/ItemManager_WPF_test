using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ItemManager_WPF_test
{
    [System.Xml.Serialization.XmlRoot("ItemList")]
    public class ItemListXmlStructure
    {
        [System.Xml.Serialization.XmlElement("Item")]
        public List<ItemWrapper> Items { get; set; }
    }

    public class ItemWrapper
    {
        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlElement("qty")]
        public string Qty { get; set; }
        [System.Xml.Serialization.XmlElement("price")]
        public string Price { get; set; }

        public ItemWrapper() { }
        public ItemWrapper(string name, int qty, decimal price)
        {
            Name = name;
            Qty = qty.ToString();
            Price = price.ToString();
        }
    }

    class ItemSourceXmlSerializer : ItemSource
    {
        override public bool Load(List<Item> itemList)
        {
            bool ret = true;
            string filename = this.getLoadFile("XML Files|*.xml");
            if (!File.Exists(filename))
                return false;

            StreamReader sr = new StreamReader(filename);
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemListXmlStructure));
                ItemListXmlStructure itemListXml = (ItemListXmlStructure)serializer.Deserialize(sr);
                foreach (ItemWrapper itemWrapper in itemListXml.Items)
                {
                    itemList.Add(new Item(itemWrapper.Name, int.Parse(itemWrapper.Qty), decimal.Parse(itemWrapper.Price)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                ret = false;
            }
            sr.Close();
            sr = null;


            return ret;
        }

        override public void Save(List<Item> itemList)
        {
            string filename = this.getSaveFile("XML Files|*.xml");
            if (filename == null)
                return;

            ItemListXmlStructure itemListXml = new ItemListXmlStructure();
            itemListXml.Items = new List<ItemWrapper>();
            foreach(Item item in itemList){
                itemListXml.Items.Add(new ItemWrapper(item.Name, item.Qty, item.Price));
            }
            
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItemListXmlStructure));
                serializer.Serialize(sw, itemListXml, ns);
                sw.Flush();
                sw.Close();
                sw = null;
            }
            catch (Exception e)
            {
                Console.Write(e.InnerException);                
            }
        }
    }
}

