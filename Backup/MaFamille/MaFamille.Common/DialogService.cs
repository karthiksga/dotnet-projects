using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MaFamille.Common
{
    public class DialogService
    {
        public void ShowDialog(string title, string message, bool allowCancel, Action<bool> response)
        {
            var dialog = new ConfirmDialog(allowCancel) { Title = title, Message = message, CloseAction = response };
            dialog.Closed += DialogClosed;
            dialog.Show();
        }
         
        /// <summary>
        ///     Closed action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void DialogClosed(object sender, EventArgs e)
        {
            ((ConfirmDialog)sender).CloseAction(((ConfirmDialog)sender).DialogResult == true);
        }
    }
}
