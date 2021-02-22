using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;
using ViewModel.Util;

namespace ViewModel
{
    public class NoteSearchViewModel : ViewModelBase
    {
        #region Declaration

        private AsyncObservableCollection<NoteLineViewModel> noteLineVMList = new AsyncObservableCollection<NoteLineViewModel>();

        public AsyncObservableCollection<NoteLineViewModel> NoteLineVMList
        {
            get { return noteLineVMList; }
            set { noteLineVMList = value; OnPropertyChanged(); }
        }

        private ICollectionView noteLineVMListView;

        public ICollectionView NoteLineVMListView
        {
            get { return noteLineVMListView; }
            set { noteLineVMListView = value; OnPropertyChanged(); }
        }

        private string findText;
        /// <summary>
        /// The text what the user enters to the search field.
        /// </summary>
        public string FindText
        {
            get { return findText; }
            set { findText = value; FilterList(); OnPropertyChanged(); }
        }
        
        #endregion

        #region Commands

        private RelayCommand acceptNoteCommand;
        public RelayCommand AcceptNoteCommand
        {
            get
            {
                if (this.acceptNoteCommand == null)
                {
                    this.acceptNoteCommand = new RelayCommand(x => SetNoteStatus(NoteStatus.ACCEPTED));
                }

                return acceptNoteCommand;
            }
        }


        private RelayCommand infirmNoteCommand;
        public RelayCommand InfirmNoteCommand
        {
            get
            {
                if (this.infirmNoteCommand == null)
                {
                    this.infirmNoteCommand = new RelayCommand(x => SetNoteStatus(NoteStatus.INFIRMED));
                }

                return infirmNoteCommand;
            }
        }

        #endregion

        #region Ctors

        public NoteSearchViewModel()
        {
            this.NoteLineVMListView = CollectionViewSource.GetDefaultView(this.NoteLineVMList);
        }

        #endregion

        #region Methods

        public void CallGetNoteLines()
        {
            Task.Run(() => GetNoteLines());
        }

        /// <summary>
        /// Create view model from the model objects.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="line"></param>
        private void MapProperties(Mapper mapper, NoteLine line)
        {
            NoteLineViewModel lineVM = mapper.Map<NoteLineViewModel>(line);
            NoteViewModel noteVM = mapper.Map<NoteViewModel>(line.Note);
            RecipientViewModel recVM = mapper.Map<RecipientViewModel>(line.Note.Rec);

            noteVM.RecVM = recVM;
            lineVM.NoteVM = noteVM;

            this.NoteLineVMList.Add(lineVM);
        }

        /// <summary>
        /// Change the status of the delivery note and save to the database.
        /// </summary>
        /// <param name="noteStatus"></param>
        private void SetNoteStatus(NoteStatus noteStatus)
        {
            try
            {
                OnCursorHandling(true);

                if (this.NoteLineVMListView.CurrentItem != null && this.NoteLineVMListView.CurrentItem is NoteLineViewModel)
                {
                    Mapper mapper = new Mapper(MapperConfig);

                    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
                    {
                        Note dbNote = ctx.Note.FirstOrDefault(x => x.Id == (this.NoteLineVMListView.CurrentItem as NoteLineViewModel).NoteVM.Id);
                        if (dbNote != null)
                        {
                            dbNote.Status = (int)noteStatus;
                        }

                        ctx.SaveChanges();
                    }

                    ReportSuccess();
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
        /// Filter the list based on the desired columns.
        /// </summary>
        private void FilterList()
        {
            try
            {
                OnCursorHandling(true);

                this.NoteLineVMListView.Filter = null;

                if (this.NoteLineVMListView.CurrentItem != null)
                {
                    this.NoteLineVMListView.MoveCurrentTo(null);
                }

                Expression<Func<object, bool>> predicate = PredicateBuilder.True<object>();
                
                if (!String.IsNullOrEmpty(this.FindText) && this.NoteLineVMList.Count != 0)
                {
                    predicate = PredicateBuilder.And(o => ((NoteLineViewModel)o).PartCode.ToUpper().Contains(this.FindText.ToUpper()), predicate);
                    predicate = PredicateBuilder.Or(o => ((NoteLineViewModel)o).PartDesc.ToUpper().Contains(this.FindText.ToUpper()), predicate);
                    predicate = PredicateBuilder.Or(o => ((NoteLineViewModel)o).NoteVM.RecVM.Code.ToUpper().Contains(this.FindText.ToUpper()), predicate);
                    predicate = PredicateBuilder.Or(o => ((NoteLineViewModel)o).NoteVM.RecVM.Name.ToUpper().Contains(this.FindText.ToUpper()), predicate);
                }

                Func<object, bool> func = predicate.Compile();

                this.NoteLineVMListView.Filter = new Predicate<object>(func);
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
    }
}
