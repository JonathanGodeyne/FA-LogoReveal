using System;
using System.Windows;
using System.Windows.Forms;

namespace Febelfin_academy_Logo_reveal
{
    public partial class ScreenSelector : Window
    {


        public ScreenSelector()
        {
            InitializeComponent();
            
        }

      

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            Screenhelper.ControllerScreen = cbControllerScreen.SelectedItem as Screen;
            Screenhelper.MainScreen = cbMainScreen.SelectedItem as Screen;
            Screenhelper.SetMainScreen();
            Screenhelper.SetControllerScreen();
          

            if(Screenhelper.ControllerScreen != null && Screenhelper.MainScreen != null)
            {
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Geen schermen geselecteerd!", "Fout") ;
            }
            
        }





    }
}
