﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TimeThatTellsTimeWpf
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

        private void ImageUpload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select";
            //op.Multiselect = true;
            op.Filter = "Png and Jpg | *.png; *jpg|" +
                 "Png|*png;";

            if (op.ShowDialog() == true)
            {
                path = op.FileName;
            }

        }
        string path;
        string input;

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            input = inputBox.Text;
        }

           
        private void ConfirmBtn(object sender, RoutedEventArgs e)
        {
            if (path == null)
                return;
            if (input == string.Empty)
                return;

            Debug.WriteLine("ok");
            Logik logik = new Logik(input,path);
        }
    }
}
