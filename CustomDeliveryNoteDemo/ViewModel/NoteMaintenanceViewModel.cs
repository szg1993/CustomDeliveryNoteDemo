using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;
using ViewModel.Singleton;
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

        private IAsyncCommand refreshRecListCommand;

        public IAsyncCommand RefreshRecListCommand
        {
            get
            {
                if (refreshRecListCommand == null)
                {
                    refreshRecListCommand = new AsyncCommand(GetRecipientListAsync, CanExecuteAsyncCommand);
                }

                return refreshRecListCommand;
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

        /// <summary>
        /// Check the data before upload.
        /// </summary>
        private void CheckData()
        {
            if (this.ActNoteVM != null && this.ActNoteVM.RecVM != null
                && this.ActNoteVM.NoteLineVMList != null
                && this.ActNoteVM.NoteLineVMList.Count > 0)
            {
                this.ActNoteVM.CheckProps();
                this.ActNoteVM.RecVM.CheckProps();
                
                foreach (var noteLineVM in this.ActNoteVM.NoteLineVMList)
                {
                    noteLineVM.CheckProps();
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
        public override void ReportSuccess()
        {
            this.ActNoteVM = new NoteViewModel
            {
                RecVM = new RecipientViewModel(),
                NoteLineVMList = new ObservableCollection<NoteLineViewModel>()
            };

            OnComboBoxHandling();

            base.ReportSuccess();
        }

        #endregion

        #region Tasks

        /// <summary>
        /// Get the list of the available recipients async.
        /// </summary>
        /// <returns></returns>
        private async Task GetRecipientListAsync()
        {
            try
            {
                OnCursorHandling(true);

                this.AllRecipientVMList.Clear();

                using (UnitOfWork unitOfWork = new UnitOfWork(new CustomDeliveryNoteContext()))
                {
                    var recList = await unitOfWork.RecipientRepo.GetAllAsync();

                    var mapper = new Mapper(MapperConfig);

                    foreach (var rec in recList)
                    {
                        var recVM = mapper.Map<RecipientViewModel>(rec);
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
        }

        /// <summary>
        /// Upload the new delivery note async.
        /// </summary>
        /// <returns></returns>
        private async Task UploadAsync()
        {
            await Task.Run(async() =>
            {
                try
                {
                    this.IsBusy = true;

                    OnCursorHandling(true);

                    using (UnitOfWork unitOfWork = new UnitOfWork(new CustomDeliveryNoteContext()))
                    {
                        string lastNoteNbr = await unitOfWork.NoteRepo.GetLastNoteNumberAsync();

                        this.ActNoteVM.NoteNbr = CreateNoteNbr(lastNoteNbr);

                        this.ActNoteVM.Status = (int)NoteStatus.NEW;
                        this.ActNoteVM.CreatedDate = DateTime.Now; //Because this is just a demo app, I don't use the server time.
                        this.ActNoteVM.RecId = this.ActNoteVM.RecVM.Id;

                        CheckData();

                        var mapper = new Mapper(MapperConfig);
                        var note = mapper.Map<Note>(this.ActNoteVM);
                        var recipient = mapper.Map<Recipient>(this.ActNoteVM.RecVM);

                        note.UserId = LoggedUser.Employee.Id;

                        foreach (var noteLineVM in this.ActNoteVM.NoteLineVMList)
                        {
                            var noteLine = mapper.Map<NoteLine>(noteLineVM);
                            note.NoteLine.Add(noteLine);
                        }

                        await unitOfWork.NoteRepo.AddAsync(note);
                        await unitOfWork.SaveAsync();
                    }

                    ReportSuccess();

                    /*using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        this.ActNoteVM.NoteNbr = NoteViewModel.CreateNoteNbr(ctx);
                        this.ActNoteVM.Status = (int)NoteStatus.NEW;
                        this.ActNoteVM.CreatedDate = DateTime.Now; //Because this is just a demo app, I don't use the server time.
                        this.ActNoteVM.RecId = this.ActNoteVM.RecVM.Id;
                        
                        CheckData();

                        Mapper mapper = new Mapper(MapperConfig);
                        Note note = mapper.Map<Note>(this.ActNoteVM);
                        Recipient rec = mapper.Map<Recipient>(this.ActNoteVM.RecVM);

                        note.UserId = LoggedUser.Employee.Id;

                        foreach (NoteLineViewModel lineVM in this.ActNoteVM.NoteLineVMList)
                        {
                            NoteLine noteLine = mapper.Map<NoteLine>(lineVM);
                            note.NoteLine.Add(noteLine);
                        }                     

                        ctx.Note.Add(note);

                        ctx.SaveChanges();
                    }*/


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

        #region Static

        /// <summary>
        /// Create a new unique ID for the deliver note.
        /// </summary>
        /// <returns></returns>
        public static string CreateNoteNbr(string lastNoteNbr)
        {
            string prefix = "N";
            string postfix;
            string firstPostfix = "00001";
            string nbrSerialFormat = "00000";;

            if (lastNoteNbr != null)
            {
                postfix = lastNoteNbr.Substring(prefix.Length, nbrSerialFormat.Length);

                if (Convert.ToInt32(postfix) < Convert.ToInt32(nbrSerialFormat.Replace('0', '5')))
                {
                    return prefix + (Convert.ToInt32(postfix) + 1).ToString(nbrSerialFormat);
                }
                else
                {
                    throw new MessageException("The program is unable to calculate the next delivery note number, because there is more available serial number.");
                }
            }
            else
            {
                return prefix + firstPostfix;
            }
        }

        #endregion
    }
}
