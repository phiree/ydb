using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace Com.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        public readonly static string paytype = "1";
        public readonly static string partner = "2088021632422534";
        public readonly static string app_id = "2016012201112719";
        public readonly static string seller_email = "jsyk_company@126.com";
        public readonly static string seller_id = "2088021632422534";
        public readonly static string private_key = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAK4+63BQXGjyOdvSyYKHbL/aA71DVV9GYq3ZUqMKiuZRwrCoNjw1OO3OKawIcRXXT6+ks4f5Cmrjiv3cr6uuEfu4AGAT0msS0XUtZFQbKwtuUQTARiO/EwjG4QBiWl5yyjgeaQcBS50XEBIJtG9gc6fKS3lrlgo7339T2AlFIa3NAgMBAAECgYBguZTAeBuUNkJgAJbT2pFHvqIswd/2T4AfuW/iCcwpJmHI5teUfIbpm3QOh3EfCrK1cdwnMtvRkhZp7cOmra5U/qUEPGyM+EyatWUXh1l5CYPLrNK5hWU42KPnPTh1siaSYZAS/gkeOOmnBnWRjk+fycB/o1e3f29JiNynnpiKgQJBANp3xWidBB1xRSHn1ndNlVJb8xbcNeUJWA6Np8lWDhV5XJdWQZ9JQIwSnsOOWhkaSPhKWbC9pE+c1sCfZJH/CPkCQQDMLkJVEn1bmIR5CYCe4D7HVy6mMCd/IXy4Aruj6XgP4XJXEJmK6BHwLjrk5UJ78rr+P/7McSI8Cdmw7e/bwDR1AkEAliheahIKAwaYor5LvYJ40LvccGj5Lixm9tHMcmkQkxfwWSBzSD07/UrLDtRn/vr/DpFj5kPijMnbHVgw8twdWQJBAJ4xWQNAiA9nY3vDePufEgSv71yjAnblIxQOSgL86CCls0jxe4S7uOo1ZzvgxFnz/hzuyCLtpHCP0THbp3LCv+ECQBnbhcKtNiJt9AhpqShJLYBy66GFonI+DoPkt94PgA5DgtzvpyuULcisAroZysjhAjRjlJA2tT7IJ6Vtzt+DhQU=";
        //public readonly static string private_key = @"MIICXQIBAAKBgQCuPutwUFxo8jnb0smCh2y/2gO9Q1VfRmKt2VKjCormUcKwqDY8NTjtzimsCHEV10+vpLOH+Qpq44r93K+rrhH7uABgE9JrEtF1LWRUGysLblEEwEYjvxMIxuEAYlpecso4HmkHAUudFxASCbRvYHOnykt5a5YKO99/U9gJRSGtzQIDAQABAoGAYLmUwHgblDZCYACW09qRR76iLMHf9k+AH7lv4gnMKSZhyObXlHyG6Zt0DodxHwqytXHcJzLb0ZIWae3Dpq2uVP6lBDxsjPhMmrVlF4dZeQmDy6zSuYVlONij5z04dbImkmGQEv4JHjjppwZ1kY5Pn8nAf6NXt39vSYjcp56YioECQQDad8VonQQdcUUh59Z3TZVSW/MW3DXlCVgOjafJVg4VeVyXVkGfSUCMEp7DjloZGkj4SlmwvaRPnNbAn2SR/wj5AkEAzC5CVRJ9W5iEeQmAnuA+x1cupjAnfyF8uAK7o+l4D+FyVxCZiugR8C465OVCe/K6/j/+zHEiPAnZsO3v28A0dQJBAJYoXmoSCgMGmKK+S72CeNC73HBo+S4sZvbRzHJpEJMX8Fkgc0g9O/1Kyw7UZ/76/w6RY+ZD4ozJ2x1YMPLcHVkCQQCeMVkDQIgPZ2N7w3j7nxIEr+9cowJ25SMUDkoC/OggpbNI8XuEu7jqNWc74MRZ8/4c7sgi7aRwj9Ex26dywr/hAkAZ24XCrTYibfQIaakoSS2AcuuhhaJyPg6D5LfeD4AOQ4Lc76crlC3IrAK6GcrI4QI0Y5SQNrU+yCelbc7fg4UF";
        public readonly static string public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";
        public readonly static string input_charset = "utf-8";
        public readonly static string sign_type = "RSA";
        public readonly static string publickey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCuPutwUFxo8jnb0smCh2y/2gO9Q1VfRmKt2VKjCormUcKwqDY8NTjtzimsCHEV10+vpLOH+Qpq44r93K+rrhH7uABg
E9JrEtF1LWRUGysLblEEwEYjvxMIxuEAYlpecso4HmkHAUudFxASCbRvYHOnykt5a5YKO99/U9gJRSGtzQIDAQAB";
        
        #endregion

    }
}