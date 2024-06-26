﻿using SCBXML2TXT;
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

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for dynamicBind.xaml
    /// </summary>
    public partial class dynamicBind : UserControl
    {
        public MyBinds Bind { get; set; }
        public string BindName { get; set; }

        public dynamicBind(MyBinds bind)
        {
            DataContext = this;

            Bind = bind;
            BindName = bind.DisplayName;

            InitializeComponent();
        }
    }
}
