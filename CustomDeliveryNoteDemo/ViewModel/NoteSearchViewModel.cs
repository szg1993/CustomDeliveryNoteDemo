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
using ViewModel.Excep;
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

            this.NoteLineVMList.Add(lineVM);
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

                    this.NoteLineVMList.Clear();

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
