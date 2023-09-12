using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Framework.Services.Interface {
    public interface IReceiveMessageService {
        Task RunAsync();
    }
}
