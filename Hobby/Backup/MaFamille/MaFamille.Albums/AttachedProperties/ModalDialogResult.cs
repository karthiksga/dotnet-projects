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

namespace MaFamille.Albums.AttachedProperties
{
    public static class ModalDialogResult
    {
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(Boolean?), typeof(ModalDialogResult),
            new PropertyMetadata(OnSetDialogResultCallback));

        public static void SetDialogResult(ChildWindow childWindow, Boolean? dialogResult)
        {
            childWindow.SetValue(DialogResultProperty, dialogResult);
        }

        public static Boolean? GetDialogResult(ChildWindow childWindow)
        {
            return childWindow.GetValue(DialogResultProperty) as Boolean?;
        }

        private static void OnSetDialogResultCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = dependencyObject as ChildWindow;
            if (childWindow != null)
                childWindow.DialogResult = e.NewValue as bool?;
        }
    }
}
