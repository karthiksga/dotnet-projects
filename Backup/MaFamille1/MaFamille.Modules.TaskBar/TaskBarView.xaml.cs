﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using SilverlightControls;

namespace MaFamille.Modules.TaskBar
{    
    public partial class TaskBarView : UserControl
    {
        public TaskBarView()
        {
            InitializeComponent();
        }

        private void btnMyUpload_Click(object sender, RoutedEventArgs e)
        {
            FloatWindow window = new FloatWindow();
            host.Add(window);
            window.DisplayWindow();
        }
    }
}
