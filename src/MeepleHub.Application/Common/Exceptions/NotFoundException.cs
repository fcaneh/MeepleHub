using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName, object resourceId)
            :base($"{resourceName} with identifier '{resourceId}' was not found.")
        {
        }
    }
}
