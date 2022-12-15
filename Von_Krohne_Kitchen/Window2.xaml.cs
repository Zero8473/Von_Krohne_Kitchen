using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Von_Krohne_Kitchen
{
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        List<string> ingred = new List<string>();


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
      
        private void add_Btn_click(object sender, RoutedEventArgs e)
        {
            
            ingred.Add(add_Ingridients.Text);
            string IngString = string.Join(Environment.NewLine + "-" , ingred);
            Show_Ingridients.Text = IngString;
        



        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
           
        }

        
     
        private void continue_btn_click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            win3.Show();
            this.Close();

        }

        private void added_Ingred(object sender, TextCompositionEventArgs e)
        {

        }

        private void Menge_Select(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
