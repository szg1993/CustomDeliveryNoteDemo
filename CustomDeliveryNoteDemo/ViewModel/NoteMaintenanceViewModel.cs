using AutoMapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Excep;
using ViewModel.ModelViewModel;
using ViewModel.Util;

namespace ViewModel
{
    public class NoteMaintenanceViewModel : ViewModelBase
    {
        #region Declaration

        private NoteViewModel actNoteVM;
        /// <summary>
        /// The actual note.
        /// </summary>
        public NoteViewModel ActNoteVM
        {
            get { return actNoteVM; }
            set { actNoteVM = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RecipientViewModel> allRecipientVMList;
        /// <summary>
        /// The list of the selectable recipients.
        /// </summary>
        public ObservableCollection<RecipientViewModel> AllRecipientVMList
        {
            get { return allRecipientVMList; }
            set { allRecipientVMList = value; OnPropertyChanged(); }
        }

        private IAsyncCommand getRecipientsCommand;

        public IAsyncCommand GetRecipientsCommand
        {
            get
            {
                if (getRecipientsCommand == null)
                {
                    getRecipientsCommand = new AsyncCommand(GetRecipientList, CanExecuteAsyncCommand);
                }

                return getRecipientsCommand;
            }
        }

        #endregion

        #region Ctors

        public NoteMaintenanceViewModel()
        {
            this.ActNoteVM = new NoteViewModel();
        }

        /// <summary>
        /// Get the available recipients from the database.
        /// </summary>
        //public void GetRecipientList()
        //{
        //    try
        //    {
        //        OnCursorHandling(true);

        //        List<Recipient> allRecipientList = new List<Recipient>();
        //        this.AllRecipientVMList = new ObservableCollection<RecipientViewModel>();

        //        using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
        //        {
        //            allRecipientList = ctx.Recipient.Where(x => x.Code != null).ToList();
        //        }

        //        foreach (Recipient rec in allRecipientList)
        //        {
        //            Mapper mapper = new Mapper(MapperConfig);
        //            RecipientViewModel recVM = mapper.Map<RecipientViewModel>(rec);
        //            this.AllRecipientVMList.Add(recVM);
        //        }

        //        for (int i = 0; i < 2_000_000_000; i++)
        //        {

        //        }
        //    }
        //    catch (MessageException mex)
        //    {
        //        OnMessageBoxHandling(mex.Message, DeliveryNoteMessageBoxType.Warning);
        //    }
        //    catch (Exception ex)
        //    {
        //        OnMessageBoxHandling(ex.Message, DeliveryNoteMessageBoxType.Error);
        //    }
        //    finally
        //    {
        //        OnCursorHandling(false);
        //    }
        //}

        private void GetRecipientListInternal()
        {
            List<Recipient> allRecipientList = new List<Recipient>();
            this.AllRecipientVMList = new ObservableCollection<RecipientViewModel>();

            using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
            {
                allRecipientList = ctx.Recipient.Where(x => x.Code != null).ToList();
            }

            foreach (Recipient rec in allRecipientList)
            {
                Mapper mapper = new Mapper(MapperConfig);
                RecipientViewModel recVM = mapper.Map<RecipientViewModel>(rec);
                this.AllRecipientVMList.Add(recVM);
            }
        }

        public async Task GetRecipientList()
        {
            OnCursorHandling(true);

            await Task.Run(() =>
            {
                try
                {                    
                    this.IsBusy = true;
                    
                    for (int i = 0; i < 2_000_000_000; i++)
                    {

                    }

                    throw new MessageException("Megálltam a task futása közben.");
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
                }                
            });

            OnCursorHandling(false);
        }

        #endregion
    }
}
