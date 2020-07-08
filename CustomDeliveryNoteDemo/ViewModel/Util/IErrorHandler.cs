using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Util
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
