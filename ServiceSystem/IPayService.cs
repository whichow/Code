using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface IPayService : IService
    {
        void Pay(object args, Action<bool> onPay);
    }
}
