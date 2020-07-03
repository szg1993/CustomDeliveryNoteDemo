using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace CustomDeliveryNoteDemo
{
    /// <summary>
    /// Interaction logic for DeliveryNoteMessageBox.xaml
    /// </summary>
    public partial class DeliveryNoteMessageBox : Window
    {
        static DeliveryNoteMessageBox messageBox;
        static MessageBoxResult result = MessageBoxResult.No;

        public DeliveryNoteMessageBox()
        {
            InitializeComponent();
            //Mouse.OverrideCursor = null; //remove the wait cursor when the box apperars.
        }

        /// <summary>
        /// Shows notification about the unknown error.
        /// </summary>
        /// <returns></returns>
        public static MessageBoxResult ShowUnknownError()
        {
            messageBox = new DeliveryNoteMessageBox
            {
                txtMsg =
                {
                    Text = "Unknown error occured.\nPlease contact the support for more information."
                }
            };

            SetCaptionOfMessageBox(DeliveryNoteMessageBoxType.Error);

            SetVisibilityOfButtons(DeliveryNoteMessageBoxType.Error);

            SetImageOfMessageBox(DeliveryNoteMessageBoxType.Error);

            messageBox.ShowDialog();

            return result;
        }

        /// <summary>
        /// Shows notification about the databae connection error.
        /// </summary>
        /// <returns></returns>
        public static MessageBoxResult ShowDatabaseError()
        {
            messageBox = new DeliveryNoteMessageBox
            {
                txtMsg =
                {
                    Text = "The database is unavailable!\nPlease try again after a few seconds."
                }
            };

            SetCaptionOfMessageBox(DeliveryNoteMessageBoxType.Error);

            SetVisibilityOfButtons(DeliveryNoteMessageBoxType.Error);

            SetImageOfMessageBox(DeliveryNoteMessageBoxType.Error);

            messageBox.ShowDialog();

            return result;
        }

        /// <summary>
        /// Show the MessageBox based on it's type.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string text, DeliveryNoteMessageBoxType type)
        {
            messageBox = new DeliveryNoteMessageBox
            {
                txtMsg = { Text = text },
            };

            SetCaptionOfMessageBox(type);

            SetVisibilityOfButtons(type);

            SetImageOfMessageBox(type);

            messageBox.btnOk.Focus();
            messageBox.ShowDialog();

            return result;
        }

        /// <summary>
        /// Set the caption.
        /// </summary>
        /// <param name="type"></param>
        private static void SetCaptionOfMessageBox(DeliveryNoteMessageBoxType type)
        {
            switch (type)
            {
                case DeliveryNoteMessageBoxType.Information:
                    messageBox.messageTitle.Text = "Information";
                    break;
                case DeliveryNoteMessageBoxType.Warning:
                    messageBox.messageTitle.Text = "Warning";
                    break;
                case DeliveryNoteMessageBoxType.Error:
                    messageBox.messageTitle.Text = "Error";
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNo:
                    messageBox.messageTitle.Text = "Confirmation";
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNoCancel:
                    messageBox.messageTitle.Text = "Confirmation";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Set the visibility of the buttons.
        /// </summary>
        /// <param name="type"></param>
        private static void SetVisibilityOfButtons(DeliveryNoteMessageBoxType type)
        {
            switch (type)
            {
                case DeliveryNoteMessageBoxType.Information:
                    messageBox.btnOk.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Visibility = Visibility.Collapsed;
                    messageBox.btnYes.Visibility = Visibility.Collapsed;
                    break;
                case DeliveryNoteMessageBoxType.Warning:
                    messageBox.btnOk.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Visibility = Visibility.Collapsed;
                    messageBox.btnYes.Visibility = Visibility.Collapsed;
                    break;
                case DeliveryNoteMessageBoxType.Error:
                    messageBox.btnOk.Visibility = Visibility.Visible;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Visibility = Visibility.Collapsed;
                    messageBox.btnYes.Visibility = Visibility.Collapsed;
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNo:
                    messageBox.btnOk.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnNo.Visibility = Visibility.Visible;
                    messageBox.btnYes.Visibility = Visibility.Visible;
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNoCancel:
                    messageBox.btnOk.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Visibility = Visibility.Visible;
                    messageBox.btnNo.Visibility = Visibility.Visible;
                    messageBox.btnYes.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Set the icon of the MessageBox.
        /// </summary>
        /// <param name="type"></param>
        private static void SetImageOfMessageBox(DeliveryNoteMessageBoxType type)
        {
            switch (type)
            {
                case DeliveryNoteMessageBoxType.Information:
                    messageBox.SetImage("WpfInformationIcon.png");
                    break;
                case DeliveryNoteMessageBoxType.Warning:
                    messageBox.SetImage("WpfWarningIcon.png");
                    break;
                case DeliveryNoteMessageBoxType.Error:
                    messageBox.SetImage("WpfErrorIcon.png");
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNo:
                    messageBox.SetImage("WpfQuestionIcon.png");
                    break;
                case DeliveryNoteMessageBoxType.ConfirmationWithYesNoCancel:
                    messageBox.SetImage("WpfWarningIcon.png");
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOk) { result = MessageBoxResult.OK; }
            else if (sender == btnYes) { result = MessageBoxResult.Yes; }
            else if (sender == btnNo) { result = MessageBoxResult.No; }
            else if (sender == btnCancel) { result = MessageBoxResult.Cancel; }
            else { result = MessageBoxResult.None; }

            messageBox.Close();
            messageBox = null;
        }

        /// <summary>
        /// Find the desired image.
        /// </summary>
        /// <param name="imageName"></param>
        private void SetImage(string imageName)
        {
            string uri = string.Format("/Pictures/{0}", imageName);
            var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uriSource);
        }
    }
}
