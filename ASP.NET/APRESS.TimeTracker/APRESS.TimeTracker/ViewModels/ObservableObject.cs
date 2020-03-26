using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;

namespace APRESS.TimeTracker.ViewModels
{
    /// <summary>
    /// Objservable object tht implements INotifyPropertyChanged Event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObservableObject<T> :INotifyPropertyChanged
    {
        /// <summary>
        /// Occures when a property value is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when property is changed
        /// </summary>
        /// <param name="property"></param>
        public void OnPropertyChangedEvent(Expression<Func<T, object>> property)
        {
            var propertyName = GetPropertyName(property);
            if (PropertyChanged != null)
            {
                var handler = PropertyChanged;
                handler(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        private string GetPropertyName(Expression<Func<T,object>> expression)
        {
            if (expression == null)
                throw new ArgumentException("PropertyExpression");

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The expression is not a member access expression", "expression");
            
            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("The member access expression does not access a propery","expression");

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
                throw new ArgumentException("The referenced property is a static property","expression");

            return memberExpression.Member.Name;
        }       
    }
}
