using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Interfaces
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
