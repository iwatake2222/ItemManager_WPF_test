using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager_WPF_test
{
    abstract class ItemSource
    {
        abstract public bool Load(List<Item> itemList);
        abstract public void Save(List<Item> itemList);

         protected string getLoadFile(string filter)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Filter = filter;
             openFileDialog.Title = "Load Filename";
             if (openFileDialog.ShowDialog() == true)
             {
                 return openFileDialog.SafeFileName;
             }
             return null;
         }

         protected string getSaveFile(string filter)
         {
             SaveFileDialog saveFileDialog = new SaveFileDialog();
             saveFileDialog.Filter = filter;
             saveFileDialog.Title = "Save Filename";
             if (saveFileDialog.ShowDialog() == true)
             {
                 return saveFileDialog.SafeFileName;
             }
             return null;
         }
    }
}
