using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;

namespace ViewModel
{
    public class RecipientMaintenanceViewModel: ViewModelBase
    {
        #region Properties

        private RecipientViewModel actRecVM;
        /// <summary>
        /// The actual recipientVM what is editing by the user.
        /// </summary>
        public RecipientViewModel ActRecVM
        {
            get { return actRecVM; }
            set { actRecVM = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        private IAsyncCommand uploadCommand;

        public IAsyncCommand UploadCommand
        {
            get
            {
                if (uploadCommand == null)
                {
                    uploadCommand = new AsyncCommand(UploadAsync, CanExecuteAsyncCommand);
                }

                return uploadCommand;
            }
        }

        #endregion

        #region Ctors

        public RecipientMaintenanceViewModel()
        {
            this.ActRecVM = new RecipientViewModel();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reset the view and report success.
        /// </summary>
        public override void ReportSuccess()
        {
            this.ActRecVM = new RecipientViewModel();

            base.ReportSuccess();
        }

        #endregion

        #region Tasks

        private async Task UploadAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    this.IsBusy = true;

                    OnCursorHandling(true);

                    this.ActRecVM.CheckErros();

                    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        Mapper mapper = new Mapper(MapperConfig);

                        Recipient rec = mapper.Map<Recipient>(this.ActRecVM);
                        ctx.Recipient.Add(rec);

                        ctx.SaveChanges();
                    }

                    ReportSuccess();
                }
                catch (MessageException mex)
                {
                    OnMessageBoxHandling(mex.Message, DeliveryNoteMessageBoxType.Warning);
                }
                catch (Exception ex)
                {
                    OnMessageBoxHandling(ex.Message, DeliveryNoteMessageBoxType.Error);
                }
                finally
                {
                    this.IsBusy = false;
                    OnCursorHandling(false);
                }
            });
        }

        #endregion
    }
}
