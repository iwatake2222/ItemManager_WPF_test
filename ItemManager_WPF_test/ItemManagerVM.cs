using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{
    class ItemManagerVM : INotifyPropertyChanged
    {
        public enum DATA_ACCESS_METHOD
        {
            CSV,
            XML,
            XML_SERIALIZER,
            DB_SQL,
            DB_ORM,
        }

        public ObservableCollection<Item> ItemList { get; set; }
        public string SelectedItemName { get; set; }
        public int SelectedItemQty { get; set; }
        public decimal SelectedItemPrice { get; set; }
        int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set { _selectedItemIndex = value; updateSelectedItem(); }
        }

        private ItemSource itemSource;

        public ItemManagerVM()
        {
            ItemList = new ObservableCollection<Item>();
            initData();
        }

        public void Load()
        {
            List<Item> itemListSource = new List<Item>();
            if (itemSource.Load(itemListSource))
            {
                ItemList = new ObservableCollection<Item>(itemListSource);
                OnPropertyChanged("ItemList");
            }

        }

        public void Save()
        {
            itemSource.Save(ItemList.ToList<Item>());
        }

        public void Add(Item addedItem)
        {
            ItemList.Add(addedItem);
        }

        public void Update()
        {
            if (SelectedItemIndex >= 0)
            {
                ItemList[SelectedItemIndex].Name = SelectedItemName;
                ItemList[SelectedItemIndex].Qty = SelectedItemQty;
                ItemList[SelectedItemIndex].Price = SelectedItemPrice;
            }
        }

        public void Delete()
        {
            if (SelectedItemIndex >= 0)
            {
                ItemList.RemoveAt(SelectedItemIndex);
                updateSelectedItem();
            }
        }

        public void ChangeMethoc(DATA_ACCESS_METHOD method)
        {
            itemSource = null;  // todo: disposer for DB?
            switch (method)
            {
                case DATA_ACCESS_METHOD.CSV:
                    itemSource = new ItemSourceCsv();
                    break;
                case DATA_ACCESS_METHOD.XML:
                    itemSource = new ItemSourceXml();
                    break;
                case DATA_ACCESS_METHOD.XML_SERIALIZER:
                    itemSource = new ItemSourceXmlSerializer();
                    break;
                case DATA_ACCESS_METHOD.DB_SQL:
                    itemSource = new ItemSourceDbSql();
                    break;
                case DATA_ACCESS_METHOD.DB_ORM:
                    itemSource = new ItemSourceDbOrm();
                    break;
                default:
                    Console.WriteLine("Implementation Error");
                    break;
            }
        }

        private void updateSelectedItem()
        {
            if (SelectedItemIndex >= 0)
            {
                Item selectedItem = ItemList[SelectedItemIndex];
                SelectedItemName = selectedItem.Name;
                SelectedItemQty = selectedItem.Qty;
                SelectedItemPrice = selectedItem.Price;
            }
            else
            {
                SelectedItemName = "";
                SelectedItemQty = 0;
                SelectedItemPrice = 0;
            }
            OnPropertyChanged("SelectedItemName");
            OnPropertyChanged("SelectedItemQty");
            OnPropertyChanged("SelectedItemPrice");
        }

        private void initData()
        {
            ItemList.Add(new Item() { Name = "Pencil", Price = 1.2m, Qty = 10 });
            ItemList.Add(new Item() { Name = "Note", Price = 10.4m, Qty = 2 });
            ItemList.Add(new Item() { Name = "Eraser", Price = 5m, Qty = 1 });
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
