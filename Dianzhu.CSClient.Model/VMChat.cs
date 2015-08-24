using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.Model
{
    public class VMChat
    {
        public DateTime SavedTime { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string MessageBody { get; set; }
        public string MessageMediaUrl { get; set; }
    }
}
