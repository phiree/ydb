using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dianzhu.Model.Resource.Specs.AdvertisementSpec
{
    public class AdvertisementInPeriod : DDDCommon.SpecificationBase<Advertisement>
    {
        DateTime datetime;
        public AdvertisementInPeriod(DateTime datetime)
        { this.datetime = datetime; }
        public override Expression<Func<Advertisement, bool>> SpecExpression
        {
            get
            {
                
                return adv => adv.StartTime != null && adv.EndTime != null
                && adv.EndTime >= datetime && adv.StartTime <= datetime;
            }
        }
    }
}
