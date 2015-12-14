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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItemManager_WPF_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemManagerVM itemManagerVM;

        public MainWindow()
        {
            InitializeComponent();

            itemManagerVM = new ItemManagerVM();
            DataContext = itemManagerVM;
            rbCSV.IsChecked = true;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            itemManagerVM.Load();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            itemManagerVM.Save();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Item addedItem = new Item();
            AddWindow addWindow = new AddWindow(addedItem);
            addWindow.ShowDialog();
            if (addedItem != null)
                itemManagerVM.Add(addedItem);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            itemManagerVM.Delete();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            itemManagerVM.Update();
        }

        private void rbMethod_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb == null)
            {
                Console.WriteLine("Implementation Error");
                return;
            }
            switch(rb.Name){
                case "rbCSV":
                    itemManagerVM.ChangeMethoc(ItemManagerVM.DATA_ACCESS_METHOD.CSV);
                    break;
                case "rbXML":
                    itemManagerVM.ChangeMethoc(ItemManagerVM.DATA_ACCESS_METHOD.XML);
                    break;
                case "rbXML_SERIALIZER":
                    itemManagerVM.ChangeMethoc(ItemManagerVM.DATA_ACCESS_METHOD.XML_SERIALIZER);
                    break;
                case "rbDB_SQL":
                    itemManagerVM.ChangeMethoc(ItemManagerVM.DATA_ACCESS_METHOD.DB_SQL);
                    break;
                case "rbDB_ORM":
                    itemManagerVM.ChangeMethoc(ItemManagerVM.DATA_ACCESS_METHOD.DB_ORM);
                    break;
                default:
                    Console.WriteLine("Implementation Error");
                    break;
            }
            
        }

        private void tbxCheckTextInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            if (tbx == null)
            {
                Console.WriteLine("Implementation Error");
                return;
            }
            int parsedVal;           
            if (!int.TryParse(tbx.Text + e.Text, out parsedVal))
                e.Handled = true;

        }

        private void tbxCheckTextDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            if (tbx == null)
            {
                Console.WriteLine("Implementation Error");
                return;
            }
            decimal parsedVal;
            if (!decimal.TryParse(tbx.Text + e.Text, out parsedVal))
                e.Handled = true;

        }

    }
}
