using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers.Domain
{
    public interface Handles<T> 
        where T : IDomainEvent
    {
        void Handle(T args); 
    } 
}
