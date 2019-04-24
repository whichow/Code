using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface IShareService : IService
    {
        void Share(object args, Action<bool> onShare);
    }
}
