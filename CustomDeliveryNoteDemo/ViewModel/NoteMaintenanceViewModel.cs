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
        #region Declaration

        public delegate void NewLineNotify();
        public event NewLineNotify NewLineEvent;

        private NoteViewModel actNoteVM;
        /// <summary>
        /// The actual note.
        /// </summary>
        public NoteViewModel ActNoteVM
        {
            get { return actNoteVM; }
            set { actNoteVM = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NoteLineViewModel> lineList = new ObservableCollection<NoteLineViewModel>();

        public ObservableCollection<NoteLineViewModel> LineList
        {
            get { return lineList; }
            set { lineList = value; OnPropertyChanged(); }
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

        private RelayCommand addLineCommand;

        public RelayCommand AddLineCommand
        {
            get
            {
                if (addLineCommand == null)
                {
                    addLineCommand = new RelayCommand(c => AddNewLine());
                }

                return addLineCommand;
            }
        }


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

        #endregion

        #region Ctors

        public NoteMaintenanceViewModel()
        {
            this.ActNoteVM = new NoteViewModel();
        }

        #endregion

        #region Methods

        private void AddNewLine()
        {
            NoteLineViewModel newLineVM = new NoteLineViewModel();
            this.ActNoteVM?.NoteLineVMList?.Add(newLineVM);

            this.LineList.Add(newLineVM);

            OnNewLineHandling();
        }

        public void CallGetRecipientList()
        {
            Task.Run(() => GetRecipientList());
        }

        protected virtual void OnNewLineHandling()
        {
            NewLineEvent.Invoke();
        }

        #endregion

        #region Tasks

        private async Task GetRecipientList()
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
