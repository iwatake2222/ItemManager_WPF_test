using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{
    /* memo: need OnPropertyChanged to reflect update */
    public class Item : INotifyPropertyChanged
    {
        string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        int _qty;
        public int Qty { get { return _qty; } set { _qty = value; OnPropertyChanged(); } }
        decimal _price;
        public decimal Price { get { return _price; } set { _price = value; OnPropertyChanged(); } }

        public Item() { }
        public Item(string name, int qty, decimal price)
        {
            Name = name; Qty = qty; Price = price;
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
