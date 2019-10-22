using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddingColorToWpf
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
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawRect();
        }

        void DrawRect()
        {
            // Create a background recntangle  
            Rectangle chessBoard = new Rectangle();
            chessBoard.Width = 300;
            chessBoard.Height = 300;
            // Create a DrawingBrush  
            DrawingBrush blackBrush = new DrawingBrush();
            // Create a Geometry with white background  
            GeometryDrawing backgroundSquare = new GeometryDrawing(Brushes.Blue, null, new RectangleGeometry(new Rect(0, 0, 1024, 1024)));
            // Create a GeometryGroup that will be added to Geometry  
            GeometryGroup gGroup = new GeometryGroup();
            gGroup.Children.Add(new RectangleGeometry(new Rect(0, 0, 1024, 1024)));
            gGroup.Children.Add(new RectangleGeometry(new Rect(1024, 1024, 1024, 1024)));
            // Create a GeomertyDrawing  
            GeometryDrawing checkers = new GeometryDrawing(new SolidColorBrush(Colors.AliceBlue), null, gGroup);
            DrawingGroup checkersDrawingGroup = new DrawingGroup();
            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);
            blackBrush.Drawing = checkersDrawingGroup;
            // Set Viewport and TimeMode  
            blackBrush.Viewport = new Rect(0, 0, 0.055, 0.05);
            blackBrush.TileMode = TileMode.Tile;
            // Fill rectangle with a DrawingBrush  
            chessBoard.Fill = blackBrush;
            LayoutRoot.Children.Add(chessBoard);
        }

    }
}
