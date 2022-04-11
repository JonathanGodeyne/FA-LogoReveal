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
using System.Windows.Forms;

namespace Febelfin_academy_Logo_reveal
{
    public partial class MainWindow : Window
    {
        public int AmountOf { get; set; } = 14;
        public List<Coördinate> coördinates { get; set; } = new List<Coördinate>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void CreateCanvas()
        {
            for (int i = 0; i < AmountOf; i++)
            {
                var column = new ColumnDefinition();
                column.Width = new GridLength(1.0, GridUnitType.Star);
                var row = new RowDefinition();
                row.Height = new GridLength(1.0, GridUnitType.Star);
                canvasImage.RowDefinitions.Add(row);
                canvasImage.ColumnDefinitions.Add(column);
            }

            var image = new Image();
            image.Source = new BitmapImage(new Uri("../Resources/FA_Logo.png"));
            Grid.SetColumnSpan(image, AmountOf);
            Grid.SetRowSpan(image, AmountOf);
            image.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            
            canvasImage.Children.Add(image);                 
        }

        public void FillSquare(Brush color)
        {
            Random rnd = new Random();
            var position = rnd.Next(coördinates.Count);
            var coord = coördinates[position];
            if (coord != null)
            {
                int row = coord.Row;
                int column = coord.Column;
                coördinates.Remove(coord);

                Rectangle square = new Rectangle();
                Grid.SetColumn(square, column);
                Grid.SetRow(square, row);
                square.Fill = color;
                canvasImage.Children.Add(square);                
            }
            //TODO
            //Savestate
        }

        public void AddSquares()
        {
            for (int i = 0; i < AmountOf; i++)
            {
                for (int j = 0; j < AmountOf; j++)
                {
                    Rectangle square = new Rectangle();

                    Grid.SetColumn(square, j);
                    Grid.SetRow(square, i);
                    square.Fill = new SolidColorBrush(Colors.White);
                    square.Width = 20;
                    square.Height = 20;

                    canvasImage.Children.Add(square);
                    coördinates.Add(new Coördinate { Column = i, Row = j });
                }
            }
        }

        public void RevealAll()
        {

        }

        public void MaximizeToSecondaryMonitor()
        {
            var secondaryScreen = Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            if (secondaryScreen != null)
            {
                var workingArea = secondaryScreen.WorkingArea;
                Left = workingArea.Left;
                Top = workingArea.Top;
                Width = workingArea.Width;
                Height = workingArea.Height;

                if (IsLoaded)
                {
                    WindowState = WindowState.Maximized;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            CreateCanvas();
            AddSquares();
            //TODO
            //GetSavestate
            var buttonWindow = new ButtonWindow(this);
            buttonWindow.Show();
            MaximizeToSecondaryMonitor();
        }

    }
}
