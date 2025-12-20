using System;
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

namespace MaFamille.Common
{
    public partial class ConfirmDialog : ChildWindow
    {
        /// <summary>
        ///     Close action
        /// </summary>
        public Action<bool> CloseAction { get; set; }

        /// <summary>
        ///     Message
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message",
            typeof(string),
            typeof(ConfirmDialog),
            null);


        /// <summary>
        ///     Message
        /// </summary>
        public string Message
        {
            get { return GetValue(MessageProperty).ToString(); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ConfirmDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ConfirmDialog(bool allowCancel)
            : this()
        {
            CancelButton.Visibility = allowCancel ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

