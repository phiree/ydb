﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public class DZRole:DDDCommon.Domain.Entity<Guid>
    {
       public virtual Guid Id { get; set; }
       public virtual string Name { get; set; }
       
    }
}
