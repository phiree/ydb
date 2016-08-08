using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Dianzhu.Model;

namespace Dianzhu.DAL.Mapping
{
    public class StorageFileInfoMap : ClassMap<StorageFileInfo>
    {
        public StorageFileInfoMap()
        {
            Id(x => x.Id);
            Map(x => x.UploadTime);
            Map(x => x.FileName);
            Map(x => x.OriginalFileName);
            Map(x => x.DomainType);
            Map(x => x.FileType);
            Map(x => x.Height);
            Map(x => x.Width);
            Map(x => x.Size);
            Map(x => x.Length);
            Map(x => x.UseTime);
            Map(x => x.UploadUser);
        }
    }
}
