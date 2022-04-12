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

namespace Febelfin_academy_Logo_reveal
{    
    public partial class ButtonWindow : Window
    {
        private MainWindow _main;
        public ButtonWindow(MainWindow main)
        {            
            InitializeComponent();
            _main = main;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
           var buttonClicked = (Button)sender;
            _main.FillSquare(buttonClicked.Background);
        }

    }
}
