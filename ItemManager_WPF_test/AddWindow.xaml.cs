using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItemManager_WPF_test
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        Item addedItem;
        public AddWindow(Item addedItem)
        {
            InitializeComponent();

            this.addedItem = addedItem;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bool parseError = false;
            int parsedQty;
            if (!int.TryParse(tbxQty.Text, out parsedQty)) parseError = true;
            decimal parsedPrice;
            if (!decimal.TryParse(tbxPrice.Text, out parsedPrice)) parseError = true;
            if (parseError)
            {
                MessageBox.Show("Input Error");
                return;
            }
            addedItem.Name = tbxName.Text;
            addedItem.Qty = parsedQty;
            addedItem.Price = parsedPrice;

            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            addedItem = null;
            this.Close();
        }
    }
}
