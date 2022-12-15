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

namespace Von_Krohne_Kitchen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

         
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

   

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     

        private void New_Recipe_click(object sender, RoutedEventArgs e)
        {
 
            Window1 win1 = new Window1();
            win1.Show();
            this.Close();
        }

        private void Category_Select_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
