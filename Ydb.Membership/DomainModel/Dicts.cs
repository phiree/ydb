using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///Dicts 的摘要说明
/// </summary>
public static class Dicts
{
    public readonly static string[] ProtocolCode = {

                                                   "USM001001", //用户登录验证
                                                   "USM001002",//用户注册
                                                   "USM001003",
                                                   "SVM001001",
                                                   "SVM001002",
                                                   "SVM001003",
                                                   "SVM002001",
                                                   "VCM001001",
                                                   "VCM001002",
                                                   "VCM001003"

                                                   };
    public readonly static string[] StateCode = {
                                            "009000",//正常 
                                            "009001",//未知数据类型
                                            "009002",//数据库访问错误
                                            "009003",//违反数据唯一性约束
                                            "009004",// 5 数据库返回值 数量错误

                                            "009005",//数据资源忙
                                            "009006",//数据超出范围
                                            "009007",//8 提交过于频繁

                                            "001001",//用户认证错误
                                            "001002",//用户密码错误
                                            "001003",//密码错误次数超过限定被锁定
                                            "001004",//12 外部系统IP被拒绝
                                            

                                            };
    //public readonly static string AppIDWeChat = "wxd928d1f351b77449";
    //public readonly static string AppSecretWeChat = "d4624c36b6795d1d99dcf0547af5443d";

    public readonly static string AppIDWeChat = "wx11ee76c3c2104b41";
    public readonly static string AppSecretWeChat = "8fab3036ffc61804c0c06803ffeeada4";
    

    public readonly static string AppIDWeibo = "1213986121";
    public readonly static string AppSecretWeibo = "d624e12a4fbbd23ab234d5c7ae7fbd0f";

    public readonly static string AppIDQQIos = "1105009517";
    public readonly static string AppSecretQQIos = "5THVAcWOU43Th2Vy";

    public readonly static string AppIDQQAndroid = "1105109568";
    public readonly static string AppSecretQQAndroid = "LhUndFD35p7wIPw1";

    //alipay支付宝相应参数，加*的表示不可为空的
    public readonly static string service = "mobile.securitypay.pay";   // * 接口名称
    public readonly static string partner = "2088021632422534";         // * 合作者身份ID:签约的支付宝账号对应的支付宝唯一用户号。以2088开头的16位纯数字组成。
    public readonly static string _input_charset = "utf-8";             // * 商户网站使用的编码格式，固定为utf-8。
    public readonly static string sign_type = "RSA";                    // * 签名类型，目前仅支持RSA。
    public readonly static string sign = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDdyOxmayNrwOr821IwUIkLxw2BVVTDDqRD/PRNCnJx/UCCYIVRL7rxXdKMZrSu24m96JNjIYbiUmwEslYnbLMWY3oZr3CGttjiGq10Y2S/tz8FBAvY59ZlxNRF+CMpbii34hHFKkikdC+ave0TN0oqJl3jNYiNN4xA7wqF1bTT4QIDAQAB";        // * 签名

    public readonly static string sign_public = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDI6d306Q8fIfCOaTXyiUeJHkrIvYISRcc73s3vF1ZT7XN8RNPwJxo8pWaJMmvyTn9N4HQ632qJBVHf8sxHi/fEsraprwCtzvzQETrNRwVxLO5jVmRGi60j8Ue1efIlzPXV9je9mkjzOmdssymZkh2QhUrCmZYI/FCEa3/cNMW0QIDAQAB";//支付宝公钥

    public readonly static string notify_url = "";                      // * 服务器异步通知页面路径:支付宝服务器主动通知商户网站里指定的页面http路径。
    public readonly static string app_id = "2016012201112719";          //   客户端号
    public readonly static string appenv = "";                          //   客户端来源:标识客户端来源。参数值内容约定如下：appenv=”system=客户端平台名^version=业务系统版本”
    public readonly static string out_trade_no = "";                    // * 商户网站唯一订单号:支付宝合作商户网站唯一订单号。
    public readonly static string subject = "";                         // * 商品名称:商品的标题/交易标题/订单标题/订单关键字等。该参数最长为128个汉字。
    public readonly static string payment_type = "1";                   // * 支付类型。默认值为：1（商品购买）。
    public readonly static string seller_id = "jsyk_company@126.com";  // * 卖家支付宝账号（邮箱或手机号码格式）或其对应的支付宝唯一用户号（以2088开头的纯16位数字）。
    public readonly static string total_fee = "";                       // * 该笔订单的资金总额，单位为RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
    public readonly static string body = "";                            // * 对一笔交易的具体描述信息。如果是多种商品，请将商品描述字符串累加传给body。
    public readonly static string goods_type = "1";                     //   商品类型:具体区分本地交易的商品类型。1：实物交易；0：虚拟交易。默认为1（实物交易）。
    public readonly static string rn_check = "T";                       //   是否发起实名校验:T：发起实名校验；F：不发起实名校验。
    public readonly static string out_context = "";                     //   业务扩展参数，支付宝特定的业务需要添加该字段，json格式。 商户接入时和支付宝协商确定。
    /// <summary>
    /// 可空
    /// 未付款交易的超时时间:设置未付款交易的超时时间，一旦超时，该笔交易就会自动被关闭。
    /// 当用户输入支付密码、点击确认付款后（即创建支付宝交易后）开始计时。
    /// 取值范围：1m～15d，或者使用绝对时间（示例格式：2014-06-13 16:00:00）。
    /// m-分钟，h-小时，d-天，1c-当天（1c-当天的情况下，无论交易何时创建，都在0点关闭）。
    /// 该参数数值不接受小数点，如1.5h，可转换为90m。
    /// </summary>
    public readonly static string it_b_pay = "30m";
    /// <summary>
    /// 可空
    /// 授权令牌:	开放平台返回的包含账户信息的token（授权令牌，商户在一定时间内对支付宝某些服务的访问权限）。
    /// 通过授权登录后获取的alipay_open_id，作为该参数的value，登录授权账户即会为支付账户。
    /// </summary>
    public readonly static string extern_token = "";
}