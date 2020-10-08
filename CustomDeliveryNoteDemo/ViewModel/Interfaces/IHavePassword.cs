using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Interfaces
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
