using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class NullMailService : INullMailService
    {
        private ILogger<NullMailService> _logger = null;
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to,string title, string message)
        {
            _logger.LogInformation($"to : {to} title : {title } message : {message}");
        }
    }
}
