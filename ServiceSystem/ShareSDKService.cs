using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ShareSDKService : ILoginService, IShareService
    {
        public void Login(object args, Action<bool> onLogin)
        {
            throw new NotImplementedException();
        }

        public void Share(object args, Action<bool> onShare)
        {
            throw new NotImplementedException();
        }
    }
}
