﻿using Dianzhu.CSClient.ViewModel;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public  interface IVMIdentityAdapter
    {
        VMIdentity OrderToVMIdentity(ServiceOrder order, string customerAvatarUrl);
    }
}