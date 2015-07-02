using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHSuit
{
    public class PHNumber
    {
        /// <summary>
        /// 将整数平均分成其他整数之和,如果不能整除,则在其中某项做加减.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="amount_to_divid"></param>
        /// <returns></returns>
        public static IList<int> Divid(int amount, int amount_to_divid)
        {
            List<int> result = new List<int>();
            int item = (int)(amount / amount_to_divid);
             
                for (int i = 0; i < amount_to_divid-1; i++)
                {

                    result.Add(item);
                }
                int lastItem =amount- (amount_to_divid- 1) * item;
                result.Add(item);
                return result;
            
        }
    }
}
