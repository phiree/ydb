using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dianzhu.Model.Resource.Specs.AdvertisementSpec
{
    public class AdvertisementUseful : DDDCommon.SpecificationBase<Advertisement>
    {
        bool isUseful;
        public AdvertisementUseful(bool isUseful)
        { this.isUseful = isUseful; }
        public override Expression<Func<Advertisement, bool>> SpecExpression
        {
            get
            {
                return adv => adv.IsUseful == true;
            }
        }
    }
}
