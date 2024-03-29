﻿using System;
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
using System.Windows.Shapes;

namespace StorageApp
{
    /// <summary>
    /// Interaction logic for HintWondow.xaml
    /// </summary>
    public partial class HintWondow : Window
    {
        private string HintText = "";
        public delegate void OnClosedDelegate();
        public event OnClosedDelegate OnClosedEvent;

        public HintWondow(string hintText, CornerRadius borderCornerRadius, double left, double top)
        {
            InitializeComponent();

            mainBorder.CornerRadius = borderCornerRadius;

            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = left;
            this.Top = top;

            HintText = hintText;
            Binding binding = new Binding();
            binding.Source = HintText;
            tbHint.SetBinding(TextBlock.TextProperty, binding);
        }

        private void EventClose(object sender, RoutedEventArgs e)
        {
            this.Close();
            OnClosedEvent?.Invoke();
        }
    }
}
