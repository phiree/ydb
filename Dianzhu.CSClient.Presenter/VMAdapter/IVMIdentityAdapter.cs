﻿using Dianzhu.CSClient.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public  interface IVMIdentityAdapter
    {
        VMIdentity OrderToVMIdentity(ServiceOrder order, string customerAvatarUrl);
    }
}
