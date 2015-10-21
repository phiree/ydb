using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IVew
{
    public interface IScreenCaptureForm
    {
        event MediaMessageSent Captured;
    }
}
