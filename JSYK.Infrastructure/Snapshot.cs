using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace JSYK.Infrastructure
{
    
    public class SnapshotGeneric<TSource, TSnapshot> : ISnatshop<TSource, TSnapshot>
    {
        public TSnapshot CreateSnapshot(TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
