using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace APRESS.TimeTracker.ViewModels
{
    public abstract class BaseViewModel<T>: ObservableObject<T>
    {
        private string viewTitle;

        public string ViewTitle
        {
            get { return viewTitle; }
            set 
            {
                if (viewTitle == value)
                    return;
                viewTitle = value;
                OnPropertyChangedEvent(vm=>this.viewTitle);
            }
        }
    }
}
