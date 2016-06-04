﻿using System.Windows;
using SerializationTool.ViewModels;

namespace SerializationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(NavigationViewModel navigationViewModel)
        {
            InitializeComponent();
            
            DataContext = navigationViewModel;
        }
    }
}
