using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
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

        public delegate void ComboBoxNotify();
        public event ComboBoxNotify ComboBoxEvent;

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

        public NoteMaintenanceViewModel()
        {
            this.ActNoteVM = new NoteViewModel();
        }

        #endregion

        #region Methods

        protected virtual void OnComboBoxHandling()
        {
            ComboBoxEvent.Invoke();
        }

        public void CallGetRecipientList()
        {
            Task.Run(() => GetRecipientListAsync());
        }

        /// <summary>
        /// Check the data before upload.
        /// </summary>
        private void CheckData()
        {
            if (this.ActNoteVM != null && this.ActNoteVM.RecVM != null
                && this.ActNoteVM.NoteLineVMList != null
                && this.ActNoteVM.NoteLineVMList.Count > 0)
            {
                this.ActNoteVM.CheckErros();
                this.ActNoteVM.RecVM.CheckErros();
                
                foreach (NoteLineViewModel lineVM in this.ActNoteVM.NoteLineVMList)
                {
                    lineVM.CheckErros();
                }
            }
            else
            {
                throw new MessageException("The program is unable to create a delivery note until it doesn't have a recipient and a least one line.");
            }
        }

        /// <summary>
        /// Reset the view and report success.
        /// </summary>
        private void ReportSuccess()
        {
            this.ActNoteVM = new NoteViewModel
            {
                RecVM = new RecipientViewModel(),
                NoteLineVMList = new ObservableCollection<NoteLineViewModel>()
            };

            OnComboBoxHandling();

            OnMessageBoxHandling("The upload was successful!", DeliveryNoteMessageBoxType.Information);
        }

        #endregion

        #region Tasks

        /// <summary>
        /// Get the list of the available recipients async.
        /// </summary>
        /// <returns></returns>
        private async Task GetRecipientListAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
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
                }
            });
        }

        /// <summary>
        /// Upload the new delivery note async.
        /// </summary>
        /// <returns></returns>
        private async Task UploadAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    this.IsBusy = true;

                    OnCursorHandling(true);

                    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        this.ActNoteVM.NoteNbr = NoteViewModel.CreateNoteNbr(ctx);
                        this.ActNoteVM.CreatedDate = DateTime.Now; //Because this is just a demo app, I don't use the server time.
                        this.ActNoteVM.RecId = this.ActNoteVM.RecVM.Id;
                        
                        CheckData();

                        Mapper mapper = new Mapper(MapperConfig);
                        Note note = mapper.Map<Note>(this.ActNoteVM);
                        Recipient rec = mapper.Map<Recipient>(this.ActNoteVM.RecVM);

                        foreach (NoteLineViewModel lineVM in this.ActNoteVM.NoteLineVMList)
                        {
                            NoteLine noteLine = mapper.Map<NoteLine>(lineVM);
                            note.NoteLine.Add(noteLine);
                        }

                        ctx.Note.Add(note);

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
