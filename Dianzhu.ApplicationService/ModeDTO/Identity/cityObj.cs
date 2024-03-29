﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class cityObj
    {
        string _name = "";
        /// <summary>
        /// 城市名称
        /// </summary>
        /// <type>string</type>
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        /// <summary>
        /// 城市关键字母（如“A”）
        /// </summary>
        /// <type>string</type>
        public string key
        {
            get
            {
                string spell = utils.GetChineseSpell(name);
                return spell.Substring(0, 1);
            }
            set { }
        }

        string _code = "";
        /// <summary>
        /// 城市代码
        /// </summary>
        /// <type>string</type>
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }
    }
}
