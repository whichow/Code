using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class NoLoginService : ILoginService
    {
        public void Login(object args, Action<bool> onLogin)
        {
            onLogin(true);
        }
    }
}
