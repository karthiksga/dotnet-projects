using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace APRESS.TimeTracker.Services
{
    //contract of navigation service
    public interface INavigationService
    {
        bool ConfirmClose();
    }

    public sealed class NavigationService : INavigationService
    {
        [Dependency]
        public IDialogService DialogService { get; set; }
        public bool ConfirmClose()
        {
            return DialogService.AskConfirmation("Close TimeTracker.", "Do you want to close TimeTracker?");
        }
    }

}
