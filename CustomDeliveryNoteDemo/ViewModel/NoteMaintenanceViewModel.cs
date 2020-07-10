using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;
using ViewModel.Util;

namespace ViewModel
{
    public class NoteMaintenanceViewModel : ViewModelBase
    {
        #region Properties

        private NoteViewModel actNoteVM;
        /// <summary>
        /// The actual note.
        /// </summary>
        public NoteViewModel ActNoteVM
        {
            get { return actNoteVM; }
            set { actNoteVM = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<RecipientViewModel> allRecipientVMList = new AsyncObservableCollection<RecipientViewModel>();
        /// <summary>
        /// The list of the selectable recipients.
        /// </summary>
        public AsyncObservableCollection<RecipientViewModel> AllRecipientVMList
        {
            get { return allRecipientVMList; }
            set { allRecipientVMList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        //private IAsyncCommand getRecipientsCommand;

        //public IAsyncCommand GetRecipientsCommand
        //{
        //    get
        //    {
        //        if (getRecipientsCommand == null)
        //        {
        //            getRecipientsCommand = new AsyncCommand(GetRecipientList, CanExecuteAsyncCommand);
        //        }

        //        return getRecipientsCommand;
        //    }
        //}

        private RelayCommand uploadCommand;

        public RelayCommand UploadCommand
        {
            get
            {
                if (uploadCommand == null)
                {
                    uploadCommand = new RelayCommand(c => Upload());
                }

                return uploadCommand;
            }
        }

        #endregion

        #region Ctors

        public NoteMaintenanceViewModel()
        {
            this.ActNoteVM = new NoteViewModel();
        }

        #endregion

        #region Methods

        public void CallGetRecipientList()
        {
            Task.Run(() => GetRecipientListAsync());
        }

        private void Upload()
        {
            try
            {
                OnCursorHandling(true);

                using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                {
                    Mapper mapper = new Mapper(MapperConfig);
                    Note note = mapper.Map<Note>(this.ActNoteVM);
                    Recipient rec = mapper.Map<Recipient>(this.ActNoteVM.RecVM);
                    note.RecId = rec.Id;

                    foreach (NoteLineViewModel lineVM in this.ActNoteVM.NoteLineVMList)
                    {
                        NoteLine noteLine = mapper.Map<NoteLine>(lineVM);
                        note.NoteLine.Add(noteLine);
                    }

                    ctx.Note.Add(note);

                    ctx.SaveChanges();
                }
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
                OnCursorHandling(false);
            }
        }

        #endregion

        #region Tasks

        private async Task GetRecipientListAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    this.IsBusy = true;

                    OnCursorHandling(true);

                    this.AllRecipientVMList.Clear();

                    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        List<Recipient> recList = await ctx.Recipient.Where(x => x.Code != null).ToListAsync();

                        foreach (Recipient rec in recList)
                        {
                            Mapper mapper = new Mapper(MapperConfig);
                            RecipientViewModel recVM = mapper.Map<RecipientViewModel>(rec);
                            this.AllRecipientVMList.Add(recVM);
                        }
                    }
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
                    OnCursorHandling(false);

                    this.IsBusy = false;
                }
            });
        }

        #endregion
    }
}
