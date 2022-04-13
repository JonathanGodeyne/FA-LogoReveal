using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Febelfin_academy_Logo_reveal
{
    public static class Screenhelper
    {


        public static Screen[] AllScreens { get { return Screen.AllScreens; } }


        public static Screen MainScreen { get => _mainScreen; set => _mainScreen = value; }
        private static Screen _mainScreen;
      

        public static Screen ControllerScreen { get => _controllerScreen; set => _controllerScreen = value; }
        private static Screen _controllerScreen;

        private static Window _mainWindow;

        public static Window MainWindow
        {
            get { return _mainWindow; }
            set { _mainWindow = value; }
        }

        private static Window _controllerWindow;

        public static Window ControllerWindow
        {
            get { return _controllerWindow; }
            set { _controllerWindow = value; }
        }






        public static void SetMainScreen()
        {

            if (_mainWindow != null)
            {
                _mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                _controllerWindow.WindowState = WindowState.Minimized;
                var workingArea = _mainScreen.WorkingArea;
                _mainWindow.Left = workingArea.Left - 1;
                _mainWindow.Top = workingArea.Top;
                _mainWindow.Width = workingArea.Width;
                _mainWindow.Height = workingArea.Height;
                _mainWindow.WindowState = WindowState.Maximized;               
            }

        }

        public static void SetControllerScreen()
        {

            if (_controllerScreen != null)
            {
                _controllerWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                _controllerWindow.WindowState = WindowState.Minimized;
                var workingArea = _controllerScreen.WorkingArea;
                _controllerWindow.Left = workingArea.Left - 1;
                _controllerWindow.Top = workingArea.Top;
                _controllerWindow.Width = workingArea.Width;
                _controllerWindow.Height = workingArea.Height;
                _controllerWindow.WindowState = WindowState.Maximized;
                
            }

        }

    }
}
