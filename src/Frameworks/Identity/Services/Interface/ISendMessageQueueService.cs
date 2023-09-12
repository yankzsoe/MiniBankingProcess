using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Framework.Services.Interface {
    public interface ISendMessageQueueService {
        Task<(bool, string)> SendMessageAsync(RegisterAccountSummaryModel registerModel);
    }
}
