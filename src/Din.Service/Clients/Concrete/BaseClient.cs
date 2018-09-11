using System;
using System.Collections.Generic;
using System.Text;

namespace Din.Service.Clients.Concrete
{
    public abstract class BaseClient
    {
       protected abstract string BuildUrl(params string[] p);
    }
}
