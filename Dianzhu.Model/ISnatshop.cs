using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public interface ISnatshop<TSource,TSnapshot>
    {
        TSnapshot CreateSnapshot(TSource source);
        
    }
    
}
