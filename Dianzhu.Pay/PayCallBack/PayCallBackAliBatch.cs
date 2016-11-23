using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace Dianzhu.Pay
{
    public class PayCallBackAliBatch : IPayCallBack
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Pay.PayCallBackAliBatch");

        public static Dictionary<string, string> TradeStatus = new Dictionary<string, string>()
        {
            { "BATCH_TRANS_NOTIFY","转账成功" },
            { "UPLOAD_FILE_NOT_FOUND","非常抱歉，找不到上传的文件!" },
            { "UPLOAD_FILE_NAME_ERROR","上传文件名不能为空" },
            { "UPLOAD_USERID_ERROR","上传用户ID不能为空" },
            { "UPLOAD_ISCONFIRM_ERROR","复核参数错误" },
            { "UPLOAD_XTEND_NAME_ERROR","抱歉，上传文件的格式不正确，文件扩展名必须是xls、csv" },
            { "UPLOAD_CN_FILE_NAME_ERROR","抱歉，上传文件的文件名中不能有乱码" },
            { "UPLOAD_FILE_NAME_LENGTH_ERROR","抱歉，上传文件的文件名长度不能超过64个字节" },
            { "UPLOAD_FILE_NAME_DUPLICATE","抱歉，您上传文件的文件名不能和以前上传过的有重复" },
            { "EMAIL_ACCOUNT_LOCKED","您暂时无法使用此功能，请立即补全您的认证信息" },
            { "BATCH_OUT_BIZ_NO_DUPLICATE","业务唯一校验失败" },
            { "BATCH_OUT_BIZ_NO_LIMIT_ERROR","抱歉，上传文件的批次号必须为11~32位的数字、字母或数字与字母的组合" },
            { "AMOUNT_FORMAT_ERROR","抱歉，您上传的文件中，第二行第五列的金额不正确。格式必须为半角的数字，最高精确到分，金额必须大于0" },
            { "PAYER_FORMAT_ERROR","您上传的文件中付款账户格式错误" },
            { "PAYER_IS_NULL","抱歉，您上传的文件中付款账户不能为空" },
            { "FILE_CONTENT_NULL","抱歉，您上传的文件内容不能为空" },
            { "FILE_CONTENT_LIMIT","抱歉，您上传的文件付款笔数超过了最大限制" },
            { "PAYER_USERINFO_NOT_EXIST","抱歉，您上传文件中的付款账户，与其所对应的账户信息不匹配或状态异常" },
            { "DAILY_QUOTA_LIMIT_EXCEED","日限额超限" },
            { "FILE_SUMMARY_NOT_MATCH","抱歉，您填入的信息与上传文件中的数据不一致" },
            { "ILLEGAL_CONTENT_FORMAT","文件内容格式非法" },
            { "DETAIL_OUT_BIZ_NO_REPEATE","同一批次中商户流水号重复" },
            { "TOTAL_COUNT_NOT_MATCH","总笔数与明细汇总笔数不一致" },
            { "TOTAL_AMOUNT_NOT_MATCH","总金额与明细汇总金额不一致" },
            { "PAYER_ACCOUNT_IS_RELEASED","付款账户名与他人重复，无法进行收付款。为保障资金安全，建议及时修改账户名" },
            { "PAYEE_ACCOUNT_IS_RELEASED","收款账户名与他人重复，无法进行收付款" },
            { "ERROR_INVALID_UPLOAD_FILE","抱歉，您上传的文件无效！请确认文件是否存在，并且是有效的文件格式。" },
            { "FILE_SAVE_ERROR","文件上传到服务器失败，请确认您是否已关闭待上传的文件" },
            { "ERROR_FILE_NAME_DUPLICATE","上传的文件名不能重复" },
            { "BATCH_ID_NULL","批次明细查询时批次ID为空" },
            { "BATCH_NO_NULL","批次号为空" },
            { "PARSE_DATE_ERROR","到帐户批次查询日期格式错误" },
            { "USER_NOT_UPLOADER","用户查询不是其上传的批次信息" },
            { "ERROR_ACCESS_DATA","无权访问该数据" },
            { "ILLEGAL_FILE_NAME","文件名不合法，只允许为数字、英文（半角）、中文、点以及下划线" },
            { "ERROR_FILE_EMPTY","非常抱歉，找不到上传的文件或文件内容为空!" },
            { "ERROR_FILE_NAME_SURFFIX","错误的文件后缀名" },
            { "ERROR_FILE_NAME_LENGTH","过长的文件名" },
            { "ERROR_SEARCH_DATE","付款记录的查询时间段跨度不能超过15天" },
            { "ERROR_BALANCE_NULL","用户余额不存在" },
            { "ERROR_USER_INFO_NULL","用户信息为空" },
            { "ERROR_USER_ID_NULL","用户名为空" },
            { "ERROR_BATCH_ID_NULL","批次ID为空" },
            { "ERROR_BATCH_NO_NULL","批次号为空" },
            { "STATUS_NOT_VALID","请等待该批次明细校验完成后再下载" },
            { "USER_SERIAL_NO_ERROR","商户流水号的长度不正确，不能为空或必须小于等于32个字符" },
            { "USER_SERIAL_NO_REPEATE","同一批次中商户流水号重复" },
            { "RECEIVE_EMAIL_ERROR","收款人EMAIL的长度不正确，不能为空或必须小于等于100个字符" },
            { "RECEIVE_NAME_ERROR","收款人姓名的长度不正确，不能为空或必须小于等于128个字符" },
            { "RECEIVE_MONEY_ERROR","收款金额格式必须为半角的数字，最高精确到分，金额必须大于0" },
            { "RECEIVE_ACCOUNT_ERROR","收款账户有误或不存在" },
            { "RECEIVE_SINGLE_MONEY_ERROR","收款金额超限" },
            { "LINE_LENGTH_ERROR","流水列数不正确，流水必须等于5列" },
            { "SYSTEM_DISUSE_FILE","用户逾期15天未复核，批次失败" },
            { "MERCHANT_DISUSE_FILE","用户复核不通过，批次失败" },
            { "TRANSFER_AMOUNT_NOT_ENOUGH","转账余额不足，批次失败" },
            { "RECEIVE_USER_NOT_EXIST","收款用户不存在" },
            { "ILLEGAL_USER_STATUS","用户状态不正确" },
            { "ACCOUN_NAME_NOT_MATCH","用户姓名和收款名称不匹配" },
            { "ERROR_OTHER_CERTIFY_LEVEL_LIMIT","收款账户实名认证信息不完整，无法收款" },
            { "ERROR_OTHER_NOT_REALNAMED","收款账户尚未实名认证，无法收款" },
            { "用户撤销","用户撤销" },
            { "USER_NOT_EXIST","用户不存在" },
            { "PERMIT_NON_BANK_LIMIT_PAYEE_L0_FORBIDDEN","根据监管部门的要求，对方未完善身份信息，无法收款" },
            { "PERMIT_PAYER_L1_FORBIDDEN","根据监管部门的要求，当前余额支付额度仅剩XX元，请尽快完善身份信息提升额度" },
            { "PERMIT_PAYER_L2_L3_FORBIDDEN","根据监管部门的要求，您今日余额支付额度仅剩XX元，今年余额支付额度仅剩XX元" },
            { "PERMIT_CHECK_PERM_AML_DATA_INCOMPLETE","由于您的资料不全，付款受限" },
            { "PERMIT_CHECK_PERM_AML_CERT_EXPIRED","由于您的证件过期，付款受限" },
            { "PERMIT_CHECK_PERM_AML_CERT_MISS_4_TRANS_LIMIT","您的账户收付款额度超限" },
            { "PERMIT_CHECK_PERM_AML_CERT_MISS_4_ACC_LIMIT","为了保证您的资金安全，请尽快完成信息补全" },
            { "PERMIT_CHECK_PERM_IDENTITY_THEFT","您的账户存在身份冒用风险，请进行身份核实解除限制" },
            { "PERMIT_CHECK_PERM_LIMITED_BY_SUPERIOR","根据监管部门的要求，请补全您的身份信息解除限制" },
            { "PERMIT_CHECK_PERM_ACCOUNT_NOT_EXIST","根据监管部门的要求，请补全您的身份信息，开立余额账户" },
            { "PERMIT_CHECK_PERM_INSUFFICIENT_ACC_LEVEL","根据监管部门的要求，请完善身份信息解除限制" }
        };

        public string PayCallBack(object parameters,
            out string businessOrderId, out string platformOrderId, out decimal total_amount
            , out string errMsg)
        {
            NameValueCollection coll = new NameValueCollection();

            string[] parameters_str = parameters.ToString().Split('&');
            string[] item;
            for (int i = 0; i < parameters_str.Count(); i++)
            {
                item = parameters_str[i].Split('=');
                coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
            }
            SortedDictionary<string, string> sPara = GetAliRequestGet.GetRequestGet(coll);

            //获取订单号
            //string total_fee = coll["total_fee"];            //获取总金额
            //string subject = coll["subject"];                //商品名称、订单名称
            //string body = coll["body"];                      //商品描述、订单备注、描述
            //string buyer_email = coll["buyer_email"];        //买家支付宝账号
                                                             //交易状态   

            string notify_id = coll["notify_id"];
            string sign = coll["sign"];

            bool isVerified = new Notify().Verify(sPara, notify_id, sign);
            log.Debug("参数验证结果:" + isVerified);
            platformOrderId = businessOrderId = errMsg = string.Empty;

            total_amount = 0m;

            if (isVerified)
            {
                //platformOrderId = coll["trade_no"];              //支付宝交易号
                //businessOrderId = coll["out_trade_no"];
                //total_amount = Convert.ToDecimal(coll["total_fee"]);
                string trade_status = coll["trade_status"].ToUpper();
                log.Debug("交易结果:" + trade_status);
                log.Debug("结果说明:" + TradeStatus[trade_status]);
                return trade_status;
                //if (trade_status.ToUpper() == "TRADE_SUCCESS")
                //{
                //    return "TRADE_SUCCESS";
                //}
                //else if (trade_status.ToUpper() == "WAIT_BUYER_PAY")
                //{
                //    log.Debug("交易已创建，等待买家付款.支付结果为:" + trade_status);
                //    return "WAIT_BUYER_PAY";
                //}
                //else if (trade_status.ToUpper() == "TRADE_FINISHED")
                //{
                //    log.Debug("交易成功且结束，不可再做任何操作.支付结果为:" + trade_status);
                //    return "TRADE_FINISHED";
                //}
                //else
                //{
                //    errMsg = "在指定时间段内未支付.支付结果为:" + trade_status;
                //    log.Error(errMsg);

                //    return "TRADE_CLOSED";
                //}
                //Response.Write("success");
            }
            else
            {
                errMsg = "支付结果有误.参数验证失败";
                log.Error(errMsg);
                return "ERROR";
            }

        }

    }
}
