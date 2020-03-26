using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace APRESS.TimeTracker.Services
{
    public interface IDialogService
    {
        void ShowInfo(string title, string message);
        void ShowError(string title, string message);
        void ShowAlert(string title, string message);
        bool AskConfirmation(string title, string message);
    }

    public sealed class DialogService : IDialogService
    {
        public void ShowInfo(string title, string message)
        {
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowAlert(string title, string message)
        {
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        public bool AskConfirmation(string title, string message)
        {
            return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
