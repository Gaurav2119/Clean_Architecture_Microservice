using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Interfaces
{
    public interface IConsumer
    {
        void ConsumeMessage(CancellationToken cts);
        void RunInBackground();
    }
}
