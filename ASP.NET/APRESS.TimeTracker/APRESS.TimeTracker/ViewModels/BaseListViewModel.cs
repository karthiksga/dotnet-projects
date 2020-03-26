using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace APRESS.TimeTracker.ViewModels
{
    public abstract class BaseListViewModel<TEntity>:BaseViewModel<TEntity>
    {
        public ObservableCollection<BaseDetailsViewModel<TEntity>> Collection {get; private set;}

        public BaseDetailsViewModel<TEntity> Selected { set; get; }

        protected BaseListViewModel(List<BaseDetailsViewModel<TEntity>> collection)
        {
            Collection = new ObservableCollection<BaseDetailsViewModel<TEntity>>(collection);
        }
    }
}
