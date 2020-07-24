using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;
using ViewModel.Util;

namespace ViewModel
{
    public class NoteSearchViewModel : ViewModelBase
    {
        #region Properties

        private bool isDisposed = false;

        private AsyncObservableCollection<NoteLineViewModel> noteLineVMList = new AsyncObservableCollection<NoteLineViewModel>();

        public AsyncObservableCollection<NoteLineViewModel> NoteLineVMList
        {
            get { return noteLineVMList; }
            set { noteLineVMList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<NoteLineViewModel> defaultNoteLineVMList = new AsyncObservableCollection<NoteLineViewModel>();

        public AsyncObservableCollection<NoteLineViewModel> DefaultNoteLineVMList
        {
            get { return defaultNoteLineVMList; }
            set { defaultNoteLineVMList = value; OnPropertyChanged(); }
        }

        private string findText;
        /// <summary>
        /// The text what the user enters to the search field.
        /// </summary>
        public string FindText
        {
            get { return findText; }
            set { findText = value; Task.Run(() => FilterList()); OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        //private IAsyncCommand searchCommand;

        //public IAsyncCommand SearchCommand
        //{
        //    get
        //    {
        //        if (searchCommand == null)
        //        {
        //            searchCommand = new AsyncCommand(SearchAsync, CanExecuteAsyncCommand);
        //        }

        //        return searchCommand;
        //    }
        //}

        #endregion

        #region Ctors

        public NoteSearchViewModel()
        {

        }

        #endregion

        #region Methods

        public void CallGetNoteLines()
        {
            Task.Run(() => GetNoteLines());
        }

        private void MapProperties(Mapper mapper, NoteLine line)
        {
            NoteLineViewModel lineVM = mapper.Map<NoteLineViewModel>(line);
            NoteViewModel noteVM = mapper.Map<NoteViewModel>(line.Note);
            RecipientViewModel recVM = mapper.Map<RecipientViewModel>(line.Note.Rec);

            noteVM.RecVM = recVM;
            lineVM.NoteVM = noteVM;

            this.DefaultNoteLineVMList.Add(lineVM);
        }

        /// <summary>
        /// Filter the note line list.
        /// </summary>
        private void FilterList()
        {
            try
            {
                this.IsBusy = true;

                OnCursorHandling(true);

                this.NoteLineVMList = this.DefaultNoteLineVMList;

                if (!String.IsNullOrEmpty(this.FindText))
                {
                    IEnumerable<NoteLineViewModel> filteredList;

                    if (!String.IsNullOrEmpty(this.FindText))
                    {
                        filteredList = this.DefaultNoteLineVMList.Where(x =>
                        x.NoteVM.NoteNbr.ToUpper().Contains(this.FindText.ToUpper())
                        || x.PartCode.ToUpper().Contains(this.FindText.ToUpper())
                        || x.PartDesc.ToUpper().Contains(this.FindText.ToUpper())
                        || x.NoteVM.RecVM.Code.ToUpper().Contains(this.FindText.ToUpper())
                        || x.NoteVM.RecVM.Name.ToUpper().Contains(this.FindText.ToUpper()));

                        if (filteredList != null)
                        {
                            this.NoteLineVMList = new AsyncObservableCollection<NoteLineViewModel>(filteredList);
                        }
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
        }

        #endregion

        #region Tasks

        /// <summary>
        /// Get the list of the available recipients async.
        /// </summary>
        /// <returns></returns>
        private async Task GetNoteLines()
        {
            await Task.Run(async () =>
            {
                try
                {
                    this.IsBusy = true;

                    OnCursorHandling(true);

                    this.DefaultNoteLineVMList.Clear();

                    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        List<NoteLine> lineList = await ctx.NoteLine.Where(x => x.Note != null)
                        .Include(y => y.Note)
                        .Include(z => z.Note.Rec)
                        .ToListAsync();

                        Mapper mapper = new Mapper(MapperConfig);

                        foreach (NoteLine line in lineList)
                        {
                            MapProperties(mapper, line);
                        }
                    }

                    this.NoteLineVMList = this.DefaultNoteLineVMList;
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

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.NoteLineVMList != null) { this.NoteLineVMList = null; }
            }

            isDisposed = true;
        }

        #endregion
    }
}
