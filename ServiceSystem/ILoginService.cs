using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface ILoginService : IService
    {
        void Login(object args, Action<bool> onLogin);
    }
}
