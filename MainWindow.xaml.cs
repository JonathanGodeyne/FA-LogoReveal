using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace Febelfin_academy_Logo_reveal
{
    public partial class MainWindow : Window
    {
        public int AmountOf { get; set; } = 16;
        private List<Rectangle> _rects;
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
        }

        public void AddLogo()
        {
            var image = new Image();
            image.Source = new BitmapImage(new Uri(@"Resources\FA_Logo.png", UriKind.Relative));
            image.Stretch = Stretch.Uniform;
            image.Opacity = 0.8;

            Grid.SetColumnSpan(image, AmountOf);
            Grid.SetRowSpan(image, AmountOf);

            canvasImage.Children.Add(image);
        }

        public void AddBorder()
        {
            var image = new Image();
            image.Source = new BitmapImage(new Uri(@"Resources\FA_Circle.png", UriKind.Relative));
            image.Stretch = Stretch.Fill;

            Grid.SetColumnSpan(image, AmountOf);
            Grid.SetRowSpan(image, AmountOf);

            parentGrid.Children.Add(image);
        }

        public void FillSquare(Brush color)
        {
            Random random = new Random();
            if (_rects.Count > 0)
            {
                var square = _rects[random.Next(_rects.Count)];
                square.Fill = color;
                _rects.Remove(square);
            }
            if (_rects.Count == 0)
            {
                var img = canvasImage.Children.OfType<Image>().First();
                img.Opacity = 1;
            }
            State.Save(canvasImage.Children.OfType<Rectangle>().ToList());
        }
        private void Screenshot(object sender, RoutedEventArgs e)
        {
            Screenshot();
        }
        private void Screenshot()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folderPath = $"{myDocuments}\\FA-Logo Reveal";
            Directory.CreateDirectory(folderPath);

            int scale = 20;
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)parentGrid.ActualWidth * scale, (int)parentGrid.ActualHeight * scale, 96 * scale, 96 * scale, PixelFormats.Pbgra32);
            bmp.Render(parentGrid);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            FileStream fs = new FileStream(folderPath + "//Febelfin-Academy Logo.png", FileMode.Create);
            encoder.Save(fs);
            fs.Close();
            System.Windows.MessageBox.Show("Afbeelding opgeslagen in: " + folderPath, "Opgeslaan!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RestoreSquares()
        {
            var cords = State.Load();
            foreach (var c in cords)
            {
                canvasImage.Children.Add(c);
            }
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            canvasImage.Children.Clear();
            CreateCanvas();
            AddSquares();
            AddLogo();
            _rects = canvasImage.Children.OfType<Rectangle>().Where(x => x.Fill == null).ToList();
        }

        public void AddSquares()
        {
            int empty;

            for (int i = 0; i < AmountOf; i++)
            {
                if (i == 0 || i == 15)
                {
                    empty = 4;
                }
                else if (i == 1 || i == 14)
                {
                    empty = 2;
                }
                else if (i <= 3 || i >= 12)
                {
                    empty = 1;
                }
                else
                {
                    empty = 0;
                }

                for (int j = 0; j < AmountOf; j++)
                {
                    if (empty == 0 || j >= empty && j < (AmountOf - empty))
                    {
                        MakeSquare(i, j);
                    }
                }
            }
        }

        public void MakeSquare(int row, int column)
        {
            Rectangle square = new Rectangle();
            Grid.SetColumn(square, column);
            Grid.SetRow(square, row);
            canvasImage.Children.Add(square);
        }

        private void FillRemainingSquares(object sender, RoutedEventArgs e)
        {
            var buttonColors = ButtonColors.Colors;
            Random random = new Random();
            var amountLeft = _rects.Count;

            for (int i = 0; i < amountLeft; i++)
            {
                FillSquare(buttonColors[random.Next(buttonColors.Count)]);               
            }
        }
        private void CloseAllWindows(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenScreenSelector(object sender, RoutedEventArgs e)
        {
            new ScreenSelector().ShowDialog();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var screenSelector = new ScreenSelector();
            var buttonWindow = new ButtonWindow(this);

            Screenhelper.MainWindow = this;
            Screenhelper.ControllerWindow = buttonWindow;

            screenSelector.ShowDialog();


            buttonWindow.Show();
            this.Show();



           
           
            

            CreateCanvas();

            if (State.Exists())
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Vorige sessie onderbroken, doorgaan?", "Welkom", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    RestoreSquares();
                }
                else
                {
                    AddSquares();
                }
            }
            else
            {
                AddSquares();
            }

            AddLogo();
            AddBorder();
            _rects = canvasImage.Children.OfType<Rectangle>().Where(x => x.Fill == null).ToList();


            
            
            
        }

       
    }
}
