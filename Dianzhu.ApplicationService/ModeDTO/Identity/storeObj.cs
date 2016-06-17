using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dianzhu.ApplicationService
{
    public class storeObj
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        /// <type>string</type>
        public string alias { get; set; }

        /// <summary>
        ///简介
        /// </summary>
        /// <type>string</type>
        public string introduction { get; set; }

        /// <summary>
        ///店铺电话
        /// </summary>
        /// <type>string</type>
        public string storePhone { get; set; }

        /// <summary>
        ///店铺的头像
        /// </summary>
        /// <type>string</type>
        public string imgUrl { get; set; }

        /// <summary>
        ///联系人
        /// </summary>
        /// <type>string</type>
        public string linkMan { get; set; }

        /// <summary>
        ///联系人的身份证
        /// </summary>
        /// <type>string</type>
        public string linkIdentity { get; set; }

        /// <summary>
        ///联系人的手机号
        /// </summary>
        /// <type>string</type>
        [StringLength(11, ErrorMessage = "手机号码为11位.", MinimumLength = 11)]
        [RegularExpression(@"^1[3578]\d{9}$", ErrorMessage = "手机号码不合法！")]
        public string linkPhone { get; set; }

        /// <summary>
        ///认证资质的图片地址（如身份证正反面）
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> certificateImgUrls { get; set; }

        /// <summary>
        ///店铺的展示图片
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> showImgUrls { get; set; }

        /// <summary>
        ///店铺的网页
        /// </summary>
        /// <type>string</type>
        public string url { get; set; }

        /// <summary>
        ///店铺的年份
        /// </summary>
        /// <type>string</type>
        public string vintage { get; set; }

        /// <summary>
        ///店铺总人数
        /// </summary>
        /// <type>string</type>
        public string headCount { get; set; }

        /// <summary>
        ///店铺的平均评价
        /// </summary>
        /// <type>string</type>
        public string appraise { get; set; }

        /// <summary>
        ///店铺的中文地址
        /// </summary>
        /// <type>string</type>
        public string address { get; set; }

        /// <summary>
        ///定位信息模型
        /// </summary>
        /// <type>locationObj</type>
        public locationObj location { get; set; }

        
    }
}
