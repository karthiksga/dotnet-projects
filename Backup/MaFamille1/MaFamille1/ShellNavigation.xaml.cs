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
using System.ComponentModel.Composition;
using Microsoft.Practices.Composite.Regions;
namespace MaFamille1
{
    [Export]
    public partial class ShellNavigation : UserControl
    {
        [Import]
        public IRegionManager RegionManager { get; set; }

        public ShellNavigation()
        {
            InitializeComponent();
        }       

        /// <summary>
        /// Determines whether the navigateUri is part of the same module that the buttonUri navigates to.
        /// </summary>
        /// <param name="buttonUri">The button URI.</param>
        /// <param name="navigateUri">The navigate URI.</param>
        /// <returns>
        /// 	<c>true</c> if the navigateUri is part of the same module that buttonUri navigates to; otherwise, <c>false</c>.
        /// </returns>
        Boolean IsObjectInModule(String buttonUri, String navigateUri)
        {

            //this code handles deep linking Uri's that are unmapped.
            if (!navigateUri.Contains("/"))
            {
                navigateUri = navigateUri.Replace(".", "/");

                //remove the assembly name from the Uri
                var firstSlashIndex = navigateUri.IndexOf("/");
                if (firstSlashIndex > 0)
                {
                    navigateUri = navigateUri.Remove(0, navigateUri.IndexOf("/"));
                }
            }

            Int32 buttonUriIndex = buttonUri.LastIndexOf("/");
            if (buttonUriIndex == 0)
            {
                //these requests are for views in the main assembly, Home and About.  The module name is not part of the Uri
                return buttonUri.Equals(navigateUri);
            }
            else
            {
                //these requests are for the modules.  The module name is the first part of the Uri.
                //we are comparing only the module names.
                return String.Compare(buttonUri, 0, navigateUri, 0, buttonUriIndex - 1) == 0;
            }
        }

        /// <summary>
        /// Handles the Navigated event of the ContentFrame control.
        /// 
        /// This method sets the UI Button state to indicate if the currently displayed
        /// view is in the same application that the button Uri points to.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        void ContentFrame_Navigated(Object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the NavigationFailed event of the ContentFrame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationFailedEventArgs"/> instance containing the event data.</param>
        void ContentFrame_NavigationFailed(Object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
    }
}
