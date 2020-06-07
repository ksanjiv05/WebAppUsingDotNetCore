using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface INullMailService
    {
        void SendMessage(string to, string title, string message);
    }
}
