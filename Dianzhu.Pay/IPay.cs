using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// 第三方支付接口
/// </summary>
namespace Dianzhu.Pay
{
    /// <summary>
    /// 创建支付链接
    /// </summary>
    public interface IPayRequest
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        decimal PayAmount { get; set; }
        /// <summary>
        /// 支付主题
        /// </summary>
        string PaySubject { get; set;}
        /// <summary>
        /// 支付主题前缀
        /// </summary>
        string PaySubjectPre{ get; set;}
        /// <summary>
        /// 支付备注
        /// </summary>
        string PayMemo { get; set; }
        /// <summary>
        /// 支付项ID
        /// </summary>
        string PaymentId { get; set; }
        //创建支付请求
        string CreatePayRequest();
     
        //创建支付请求字符串
        string CreatePayStr(string str);
    }
    /// <summary>
    /// 接收支付平台回调
    /// </summary>
    public interface IPayCallBack
    {
        /// <summary>
        /// 支付接口回调接口
        /// </summary>
        /// <param name="nvc">回调时的请求参数</param>
        /// <param name="businessOrderId">当初请求的订单ID(支付项ID)</param>
        /// <param name="platformOrderId">平台的订单号</param>
        /// <param name="total_amoun">支付总额</param>
        /// <param name="errMsg">错误消息</param>
        /// <returns>支付是否成功</returns>
        bool PayCallBack(object callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
        //todo: 这两个接口方法要统一成一个.
       // bool PayCallBack(string  callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amoun, out string errMsg);
    }    
    #region 支付宝网页
    /// <summary>
    /// 支付宝实现
    /// </summary>
    public class PayAli : IPayRequest

    {  
        public decimal PayAmount{
            get; set;
        }

        public string PaySubjectPre
        {
            get; set;
             
            
        }

        public string PayMemo
        {
            get; set;
        }

        public string PaymentId
        {
            get; set;
        }

        public string PaySubject
        {
            get; set;
        }

        string payment_type, notify_url, return_url,
              show_url
           ;
        public PayAli( decimal payAmount,string paymentId, string paySubject,
            string payment_type,string notify_url,string return_url,
            string show_url
            )
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.payment_type = payment_type;
            this.notify_url = notify_url;
            this.return_url = return_url;
            this.show_url = show_url;
            this.PaymentId = paymentId;

        }

        log4net.ILog log = log4net.LogManager.GetLogger("Diuanzhu.Pay.PayAli");
        public string CreatePayRequest( )
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", Config.paytype);
            sParaTemp.Add("notify_url", notify_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/notify_url.aspx");
            sParaTemp.Add("return_url", return_url);// Dianzhu.Config.Config.GetAppSetting("PaySite") + "alipay/return_url.aspx");
 
            sParaTemp.Add("out_trade_no", PaymentId);
           
            sParaTemp.Add("subject",PaySubjectPre+ PaySubject);
            sParaTemp.Add("total_fee", string.Format("{0:N2}", PayAmount));
            sParaTemp.Add("show_url",  show_url);
            sParaTemp.Add("body", PayMemo);
            sParaTemp.Add("it_b_pay", string.Empty);
            sParaTemp.Add("extern_token", string.Empty);
            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }

        public string CreatePayStr(string str)
        {
            return string.Empty;
        }
    }
    public class PayCallBackAli : IPayCallBack
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAli");

       

        public bool PayCallBack(object parameters,
            out string businessOrderId,out string platformOrderId,out decimal total_amount
            , out string errMsg)
        {
            NameValueCollection coll = (NameValueCollection)parameters;
               //获取订单号
            string total_fee = coll["total_fee"];            //获取总金额
            string subject = coll["subject"];                //商品名称、订单名称
            string body = coll["body"];                      //商品描述、订单备注、描述
            string buyer_email = coll["buyer_email"];        //买家支付宝账号
                //交易状态   

            string notify_id = coll["notify_id"];
            string sign = coll["sign"];

            SortedDictionary<string, string> sPara = GetRequestGet(coll);

            bool isVerified = new Notify().Verify(sPara, notify_id, sign);
            platformOrderId = businessOrderId =errMsg=  string.Empty;
            
            total_amount = 0m;
              
            if (isVerified)
            {
                platformOrderId = coll["trade_no"];              //支付宝交易号
                businessOrderId = coll["out_trade_no"];

                total_amount = Convert.ToDecimal(coll["total_fee"]);
                string trade_status = coll["trade_status"];
                if (trade_status.ToUpper() == "TRADE_FINISHED" || trade_status.ToUpper() == "TRADE_SUCCESS")
                {
                    return true;
                }
                 
                else
                {
                    errMsg = "支付不成功.支付结果为:" + trade_status;
                    log.Error(errMsg);

                    return false;
                }
            }
            else
            {
                errMsg = "支付结果有误.参数验证失败";
                log.Error(errMsg);
                return false;
            }
            
        }

        private SortedDictionary<string, string> GetRequestGet(NameValueCollection coll)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            //Load Form variables into NameValueCollection variable.
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }

            return sArray;
        }
    }
    #endregion

    #region WePay
    public class PayWeChat : IPayRequest
    {
        public decimal PayAmount { get; set; }

        public string PaySubjectPre { get; set; }

        public string PayMemo { get; set; }

        public string PaymentId { get; set; }

        public string PaySubject { get; set; }

        string  notify_url;

        string nonce_str = Guid.NewGuid().ToString().Replace("-", "");

        public PayWeChat(decimal payAmount, string paymentId, string paySubject,
            string notify_url,string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }

        /// <summary>
        /// 获取prepayID
        /// </summary>
        /// <returns></returns>
        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", "wxd928d1f351b77449");
            sParaTemp.Add("mch_id", "1304996701");
            sParaTemp.Add("nonce_str", nonce_str);
            sParaTemp.Add("body", PaySubjectPre + PaySubject);
            sParaTemp.Add("out_trade_no", PaymentId.Replace("-",""));
            sParaTemp.Add("total_fee", string.Format("{0:0}", PayAmount * 100));
            sParaTemp.Add("spbill_create_ip", "192.168.1.173");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("trade_type", "APP");

            //key=9bc3147c13780388c2a58500966ed564

            string sParaStr = string.Empty;
            string sParStrkey = string.Empty;
            foreach(KeyValuePair<string,string > item in sParaTemp)
            {
                sParaStr += item.Key + "=" + item.Value + "&";
            }
            sParStrkey = sParaStr + "key=9bc3147c13780388c2a58500966ed564";
            MD5 md5Obj = MD5.Create();
            string sParaMd5 = string.Empty;
            byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(sParStrkey));
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] > 16)
                {
                    sParaMd5 += d[i].ToString("X").ToUpper();
                }
                else
                {
                    sParaMd5 += "0" + d[i].ToString("X").ToUpper();
                }
            }

            //sParaTemp.Add("sign", sParaMd5);

            //建立请求
            string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            return sHtmlText;
        }

        /// <summary>
        /// 根据prepayId 构造支付连接,供前端app调用
        /// </summary>
        /// <param name="prepayid"></param>
        /// <returns></returns>
        public string CreatePayStr(string prepayid)
        {
            int times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds;
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("appid", Config.appid_WePay);
            sParaTemp.Add("partnerid", Config.partnerid_WePay);
            sParaTemp.Add("prepayid", prepayid);
            sParaTemp.Add("package", @"Sign=WXPay");
            sParaTemp.Add("noncestr", nonce_str);
            sParaTemp.Add("timestamp", times.ToString());

            string sParaStr = string.Empty;
            string sParStrkey = string.Empty;
            foreach (KeyValuePair<string, string> item in sParaTemp)
            {
                sParaStr += item.Key + "=" + item.Value + "&";
            }
            sParStrkey = sParaStr + "key=9bc3147c13780388c2a58500966ed564";
            MD5 md5Obj = MD5.Create();
            string sParaMd5 = string.Empty;
            byte[] d = md5Obj.ComputeHash(Encoding.GetEncoding("Utf-8").GetBytes(sParStrkey));
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] > 16)
                {
                    sParaMd5 += d[i].ToString("X").ToUpper();
                }
                else
                {
                    sParaMd5 += "0" + d[i].ToString("X").ToUpper();
                }
            }

            string sHtmlText = sParStrkey + "&sign=" + sParaMd5;
            return sHtmlText;
        }

    }
    public class PayCallBackWePay : IPayCallBack
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackWePay");
       
        public bool PayCallBack(object callBackParameters, out string businessOrderId, out string platformOrderId, out decimal total_amount, out string errMsg)
        {
            bool result = false;
            businessOrderId = string.Empty ;
            platformOrderId = string.Empty;
            total_amount =0;
            errMsg = string.Empty;
            
            string json = PHSuit.JsonHelper.Xml2Json((string)callBackParameters, true);

            CallBack callbackParameter = JsonConvert.DeserializeObject<CallBack>(json);
            if (callbackParameter.result_code.ToLower() == "success")
            {
                string result_code = callbackParameter.result_code;
                if (result_code.ToLower() == "success")
                {
                    businessOrderId = callbackParameter.out_trade_no;
                    platformOrderId = callbackParameter.transaction_id;
                    total_amount =Convert.ToDecimal(callbackParameter.total_fee);
                    //更新订单 发送订单状态变更通知
                    return true;

                }
                else if(result_code=="fail") {
                    //
                }
                else {
                    errMsg = "业务操作失败"+callbackParameter.err_code+":"+callbackParameter.err_code_des;
                    log.Error(errMsg);
                    return false;
                }
                businessOrderId = callbackParameter.out_trade_no;
                platformOrderId = callbackParameter.transaction_id;
                total_amount = Convert.ToDecimal(callbackParameter.total_fee) / 100;
                errMsg = callbackParameter.err_code + ":" + callbackParameter.err_code_des;

                return result;
            }
            else if (callbackParameter.result_code.ToLower() == "fail")
            {
                errMsg = "服务器通信失败,返回fail";
                log.Error(errMsg);
                return false;
            }
            else {
                errMsg = "未知的服务器返回参数" + callbackParameter.result_code;
                log.Error(errMsg);
                return false;
                 
                
            }
        }

        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="businessOrderId"></param>
        /// <param name="platformOrderId"></param>
        /// <param name="total_amoun"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool PayCallBack(NameValueCollection nvc, out string businessOrderId, out string platformOrderId, out decimal total_amount, out string errMsg)
        {
            throw new NotImplementedException();

        }
        public class CallBack
        {
 
          public string appid { get; set; }
            public string mch_id { get; set; }
            public string device_info { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            public string openid { get; set; }
            public string is_subscribe { get; set; }
            public string trade_type { get; set; }
            public string bank_type { get; set; }
            public string total_fee { get; set; }
            public string fee_type { get; set; }
            public string cash_fee{ get; set; }
            public string cash_fee_type { get; set; }
            public string coupon_fee { get; set; }
            public string transaction_id { get; set; }
            public string out_trade_no { get; set; }
            public string attach { get; set; }
            public string time_end { get; set; }
 
        }
    }
    #endregion

    #region 支付宝APP
    public class PayAlipayApp : IPayRequest
    {
        public decimal PayAmount { get; set; }
        public string PaySubjectPre { get; set; }
        public string PayMemo { get; set; }
        public string PaymentId { get; set; }
        public string PaySubject { get; set; }

        string notify_url;

        public PayAlipayApp(decimal payAmount, string paymentId, string paySubject, string notify_url, string memo)
        {
            this.PaySubject = paySubject;
            this.PayAmount = payAmount;
            this.PayMemo = memo;
            this.notify_url = notify_url;
            this.PaymentId = paymentId;
        }


        public string CreatePayRequest()
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("out_trade_no", PaymentId.Replace("-", ""));
            sParaTemp.Add("subject", PaySubjectPre + PaySubject);
            sParaTemp.Add("payment_type", Config.paytype);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("body", PayMemo);
            sParaTemp.Add("total_fee", string.Format("{0:F2}", PayAmount));

            //sParaTemp.Add("sign_type", "RSA");
            //sParaTemp.Add("sign", @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDdyOxmayNrwOr821IwUIkLxw2BVVTDDqRD/PRNCnJx/UCCYIVRL7rxXdKMZrSu24m96JNjIYbiUmwEslYnbLMWY3oZr3CGttjiGq10Y2S/tz8FBAvY59ZlxNRF+CMpbii34hHFKkikdC+ave0TN0oqJl3jNYiNN4xA7wqF1bTT4QIDAQAB");

            sParaTemp.Add("app_id", "2016012201112719");

            string sParaStr = "";
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = Submit.BuildRequestApp(sParaTemp);
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sParaStr += temp.Key + "=\"" + temp.Value + "\"&";
            }
            sParaStr = sParaStr.TrimEnd('&');

            //string sParaStr = string.Empty;
            //string sParStrkey = string.Empty;
            //foreach (KeyValuePair<string, string> item in sParaTemp)
            //{
            //    sParaStr += item.Key + "=" + item.Value + "&";
            //}
            //sParaStr = sParaStr.TrimEnd('&');

            //string privateKey = @"MIICXQIBAAKBgQC1to8WnfXSCnxfCP4irQK0+QCETsBcuGwcrCudhqjOWucbTwr7ik8cV4+iMH8InCdO4o9hDPmxXUWys7qDu6P1gx0/k1w+vqvyDpPEBzr8U1u8svfMQT5UvektQMR6vleMoueQ6uaEKM7cFN5wjoKZ4OkaNaQ0i05/4yjeMo+1oQIDAQABAoGACE6UNAB8oGGCVgXfOE6YdRV9BI9lE9gKeTUVuVnSlbxqHEF8ywsDmtQV6OA2rnoVAfIxg8pID+enrAezWxpU4GyVyJizFpCuPMlFXI8+eQcsMbXVWGtJyIZ2q6l+jtEYw8zUYZsxUocABW/Np9JIhwILGPt+EMX8VORwx2PVa8ECQQDruQcPHjgCIRBhOWkir1cd+L6R08n3CLL+P1Z1h/8mj/eWGtx7KowZUDCJjP9V8V0Ij4w27ZRGlZDd79/+rN1NAkEAxVgoPYTW5+GSFRLDxS7/TN2emNj29Op0RQemPum+RXG3bzgpBd8GutrqcslxOeeZl5Jf+VN8CNCc7NAUBlvfpQJBAJmw2ANpZocs26sobX4p6JkoF8io1+PzjhDrZwnWk+umrnz2Io9DnHjcqejlP43fgxMT1Q3zNVwYJI4v2lIIj3kCQDWoCZDofHIhv9Fg/7+uTpX8r/GJFGR1FtXqBYaXkTdaevCPiX/iKvdFLHe3U8TVtsoib1vgGhpfdjthPACanE0CQQCpX4I/rKYxhylrZopsZuj6VWfqT066b5B/wwn5FgEaU3xrT9wbk0c2uXYq4q5QkVar546ddBeBx/AuNmUFVl+r";
            //string sign = GetSign(sParaStr, privateKey, "UTF-8");

            //string publicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC1to8WnfXSCnxfCP4irQK0+QCETsBcuGwcrCudhqjOWucbTwr7ik8cV4+iMH8InCdO4o9hDPmxXUWys7qDu6P1gx0/k1w+vqvyDpPEBzr8U1u8svfMQT5UvektQMR6vleMoueQ6uaEKM7cFN5wjoKZ4OkaNaQ0i05/4yjeMo+1oQIDAQAB";
            //if(Verify(sParaStr,sign, publicKey, "UTF-8"))
            //{
            //    sParStrkey = sParaStr + "&sign_type=RSA&sign=" + sign;
            //}

            return sParaStr;
        }

        public bool PayCallBack(NameValueCollection nvc)
        {
            throw new NotImplementedException();
        }

        public string CreatePayStr(string str)
        {
            return string.Empty;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns></returns>
        public string GetSign(string content, string privateKey, string input_charset)
        {
            Encoding code = Encoding.GetEncoding(input_charset);
            byte[] Data = code.GetBytes(content);
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
            SHA1 sh = new SHA1CryptoServiceProvider();

            byte[] signData = rsa.SignData(Data, sh);
            return Convert.ToBase64String(signData);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns></returns>
        public static bool Verify(string content, string signedString, string publicKey, string input_charset)
        {
            bool result = false;

            Encoding code = Encoding.GetEncoding(input_charset);
            byte[] Data = code.GetBytes(content);
            byte[] data = Convert.FromBase64String(signedString);
            RSAParameters paraPub = ConvertFromPublicKey(publicKey);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);

            SHA1 sh = new SHA1CryptoServiceProvider();
            result = rsaPub.VerifyData(Data, sh, data);
            return result;
        }

        /// <summary>
        /// 解析java生成的pem文件私钥
        /// </summary>
        /// <param name="pemstr"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
        {
            byte[] pkcs8privatekey;
            pkcs8privatekey = Convert.FromBase64String(pemstr);
            if (pkcs8privatekey != null)
            {
                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
                return rsa;
            }
            else
                return null;
        }

        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {

            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;


                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);		//read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)	//expect an Octet string 
                    return null;

                bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)	//version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        #region 解析.net 生成的Pem
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {

            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

        private static RSAParameters ConvertFromPrivateKey(string pemFileConent)
        {
            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 609)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }

            int index = 11;
            byte[] pemModulus = new byte[128];
            Array.Copy(keyData, index, pemModulus, 0, 128);

            index += 128;
            index += 2;//141
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, index, pemPublicExponent, 0, 3);

            index += 3;
            index += 4;//148
            byte[] pemPrivateExponent = new byte[128];
            Array.Copy(keyData, index, pemPrivateExponent, 0, 128);

            index += 128;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//279
            byte[] pemPrime1 = new byte[64];
            Array.Copy(keyData, index, pemPrime1, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//346
            byte[] pemPrime2 = new byte[64];
            Array.Copy(keyData, index, pemPrime2, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//412/413
            byte[] pemExponent1 = new byte[64];
            Array.Copy(keyData, index, pemExponent1, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//479/480
            byte[] pemExponent2 = new byte[64];
            Array.Copy(keyData, index, pemExponent2, 0, 64);

            index += 64;
            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//545/546
            byte[] pemCoefficient = new byte[64];
            Array.Copy(keyData, index, pemCoefficient, 0, 64);

            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            para.D = pemPrivateExponent;
            para.P = pemPrime1;
            para.Q = pemPrime2;
            para.DP = pemExponent1;
            para.DQ = pemExponent2;
            para.InverseQ = pemCoefficient;
            return para;
        }
        #endregion


    }
    #endregion  
}
