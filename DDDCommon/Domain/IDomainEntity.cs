using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDDCommon.Domain
{
    public interface IDomainEntity
    {
        object Id { get; }
    }
}
