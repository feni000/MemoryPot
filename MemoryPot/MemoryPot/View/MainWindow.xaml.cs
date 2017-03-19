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

namespace MemoryPot.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeMenu();
        }

        public void InitializeMenu()
        {
            Practice.Items.Clear();
            Delete.Items.Clear();
            string[] languageSetsFromDatabase = new string[4] { "a", "b", "c", "d" };
            foreach (string language in languageSetsFromDatabase)
            {
                MenuItem item = new MenuItem();
                item.Header = language;
                //item.Click += new RoutedEventHandler(File_Language_Click);
                Practice.Items.Add(item);
                MenuItem itemD = new MenuItem();
                itemD.Header = language;
                Delete.Items.Add(itemD);
            }
        }
    }
}
