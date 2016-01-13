using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    public readonly static string AppIDWeChat = "wxd928d1f351b77449";
    public readonly static string AppSecretWeChat = "d4624c36b6795d1d99dcf0547af5443d";

    public readonly static string AppIDWeibo = "wxd928d1f351b77449";
    public readonly static string AppSecretWeibo = "d4624c36b6795d1d99dcf0547af5443d";
}