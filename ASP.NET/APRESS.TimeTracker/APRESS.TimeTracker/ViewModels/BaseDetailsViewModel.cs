using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APRESS.TimeTracker.ViewModels
{
    public abstract class BaseDetailsViewModel<TEntity>:BaseViewModel<TEntity>
    {
        protected BaseDetailsViewModel(TEntity currentEntity)
        {
            CurrentEntity = currentEntity;
        }

        public TEntity CurrentEntity { get; private set; }

        protected abstract void FromModelToView();

        protected abstract void FromViewToModel();
    }
}
