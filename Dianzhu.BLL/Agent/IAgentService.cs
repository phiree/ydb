﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Agent
{
    public interface IAgentService
    {
        Dianzhu.Model.DZMembership GetAreaAgent(Dianzhu.Model.Area area);
    }
}
