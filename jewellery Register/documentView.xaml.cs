﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace jewellery_Register
{
    /// <summary>
    /// Interaction logic for documentView.xaml
    /// </summary>
    public partial class documentView : Window
    {
        public documentView( FlowDocument doc)
        {
           
            
            InitializeComponent();
            docViewer.Document = doc;
        }
    }
}
