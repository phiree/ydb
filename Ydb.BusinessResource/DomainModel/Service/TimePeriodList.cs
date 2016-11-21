using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.DomainModel
{
   public class TimePeriodList 
    {
        public SortedList<int, TimePeriod> list = new SortedList<int,TimePeriod>();

        public TimePeriodList(IList<TimePeriod> periods)
        {
            foreach (TimePeriod p in periods)
            {
                Add(p);
            }
        }
        /// <summary>
        /// 添加一个时间段
        /// </summary>
        /// <param name="timePeriod"></param>
        public void  Add(TimePeriod timePeriod)
        {
            if (!IsConflict(timePeriod))
            {
                list.Add(timePeriod.StartTime.TimeValue, timePeriod);
               
            }
            else
            {
                throw new Exception("时间段冲突"+timePeriod.ToString());
                
            }
        }
        public bool IsConflict(TimePeriod timePeriod)
        {
            bool conflict = true;
            //列出所有的空白区域.
            //加上头尾.
            bool includeBegin = list.Keys.Contains(0);
            bool includeEnd = list.Where(x => x.Value.EndTime.TimeValue == 24 * 60).Count()> 0;
            if (!includeBegin)
            { 
            TimePeriod listStart = new TimePeriod(new Time("00:00"), new Time("00:00"));
                list.Add(listStart.StartTime.TimeValue, listStart);
            }
         
            if (!includeEnd)
            {
                TimePeriod listEnd = new TimePeriod(new Time("24:00"), new Time("24:00"));
                list.Add(listEnd.StartTime.TimeValue, listEnd);
            }
            List<TimePeriod> blankPeriod = new List<TimePeriod>();

            for (int i= 1;i<=list.Count-1; i++)
            {
                var preItem = list[list.Keys[i - 1]];
                var currentItem = list[list.Keys[i]];
                TimePeriod blank = new TimePeriod(preItem.EndTime,currentItem.StartTime );
                blankPeriod.Add(blank);
                
            }

            //空白区域是否完全包含给定段.
            bool enoughSpace = blankPeriod.Exists(x => x.StartTime <= timePeriod.StartTime && x.EndTime >= timePeriod.EndTime);
            if (enoughSpace) 
            {
                conflict = false;
            }
            if (!includeBegin)
            { list.Remove(0); }
            if (!includeEnd)
            {
                list.Remove(60 * 24);
            }
            return conflict;
             
        }
        public int Count { get { return list.Count; } }
        /// <summary>
        /// 移除一个时间段
        /// </summary>
        /// <param name="timePeriod"></param>
        public void Remove(TimePeriod timePeriod)
        {

        }

        /// <summary>
        /// 修改工作时间
        /// </summary>
        /// <param name="oldPeriod">需要修改的工作时间</param>
        /// <param name="newPeriod">目标工作时间</param>
        /// <returns></returns>
        public bool Modify(TimePeriod oldPeriod,TimePeriod newPeriod)
        {
            throw new NotImplementedException();
        }
    }
}
