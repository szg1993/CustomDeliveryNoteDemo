using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModel.ModelViewModel
{
    public class ModelViewModelBase: ViewModelBase
    {
        #region Ctors

        public ModelViewModelBase()
        {
            
        }

        #endregion

        #region Methods

        public virtual void CheckErrors()
        {

        }

        #endregion

        #region Static

        /// <summary>
        /// Check if the given decimal value is correct or not.
        /// </summary>
        /// <param name="nbr"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(decimal? nbr)
        {
            if (nbr == null || nbr <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
