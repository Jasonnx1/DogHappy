using System;
using System.Collections.Generic;
using System.Text;

namespace DogFetchApp.Commands
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
