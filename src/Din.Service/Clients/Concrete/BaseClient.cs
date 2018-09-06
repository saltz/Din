using System;
using System.Collections.Generic;
using System.Text;

namespace Din.Service.Clients.Concrete
{
    public abstract class BaseClient
    {
        protected virtual string BuildUrl(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
