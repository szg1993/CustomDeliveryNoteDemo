using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using ViewModel;

namespace CustomDeliveryNoteDemo.Templates
{
    public partial class DeliveryNoteWindowTemplate : ResourceDictionary
    {
        public DeliveryNoteWindowTemplate()
        {
            
        }

        /// <summary>
        /// Send the application to the tray if the user clicks to the proper icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideWindowIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if (activeWindow != null)
            {
                activeWindow.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// Grow and shrink the application if the user clicks to the proper icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullScreenIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Close the application if the user clicks to the proper icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosingIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse elipse)
            {
                if (elipse.DataContext is MainViewModel || elipse.DataContext is LoginViewModel)
                {
                    Application.Current.Shutdown();
                }
                else if (elipse.DataContext is RecipientMaintenanceViewModel)
                {
                    Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    activeWindow.Close();
                }
            }
        }
    }
}
