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
        public readonly static string seller_email = "jsyk_company@126.com";
        public readonly static string seller_id = "2088021632422534";
        public readonly static string private_key = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAONJrdtyvQKdP4fw
LSjk8AbrN/IoJxIQU0h2QlInheeAcssN4rtSIWa1pZzzUksie+uMWZ9Xe11EBF76
TAoVtw0teP2GM/UTenOOKbRy6sNGp5k3vcHfZX52biRmta6Cje+yb/8l+CohexNe
HjDWzx2e32RyGrvvAwmHnmKS6q+FAgMBAAECgYBJDwPtiEItNvKW9aLuhDiLYMyI
8FMuwUSkBC9pTP8D3QwJLRt2bv4Bj93+R1Bqilke6+xbBbnHzvdAuF/81eISpPu6
IHm0Fl84HDeNgXsNhW/NzehSL3DghB24g10t2SsLDGPq5oyLGfGGM8HVyOrw3wLZ
OLnBSnOWL6vnDNRrgQJBAP8icR74qw/RVfvxkw1KnRg2wQwsiRz3OwQohVeLmR2T
uJw5txaEBjrFTZ4nzNOSfV6NHXJcajMlIYRdbhjxFNUCQQDkDw4jePee827aPN+L
GLAH7R91gA/CtZ9IAOFfixe5NMlbyMYS6WPEh7zjLehqLqENy60CrCOmXvSvNRwh
80fxAkEAzPI41n1AxKsPHBy5WLL4MKxDNOlNl0QOV0/JlUKhU74HTQ+bwG17p5g4
unQUOFxzcxF+dxA/iygnnXGD8GswpQJAZpJu5X1uwcrzPKzMTh9YbPg1gf+LFyPg
892RgAtrLB2VwvZWQANAoA/84KCxO7ClNzM4KU+K6TUkD/lvIcXtwQJBAPYC681h
y5kSmqyuxIFGXVscVYVPeMxwY4vlLSMxI7AB/+tYBuGx5I+mY1mFNSaxyM8+EeMG
792KEe3WUb2d0zk=";
        public readonly static string public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";
        public readonly static string input_charset = "utf-8";
        public readonly static string sign_type = "RSA";
        #endregion

    }
}