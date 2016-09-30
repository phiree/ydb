using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Model
{
    public class SerialNo:DDDCommon.Domain.Entity<Guid>
    {
        public virtual string SerialKey { get; set; }
        public virtual int SerialValue { get; set; }

        public virtual string BuildFormatedNo(int serialNoLength)
        {
             
                return SerialKey + SerialValue.ToString().PadLeft(serialNoLength, '0');

            
        }
    }
}
