<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/json2.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>

    <form id="form1" runat="server">
    <div> 
        <div id="dvResults">
        </div>
    </div>
    </form>
    <script>
 
    var apiTest={
    requestArray:[
                { 
                    "protocol_CODE": "USM001001", 
                    "ReqData": { 
                                "userEmail": "issumao@126.com", 
                                "userPWord": "password", 
                                }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "USM001002", 
                    "ReqData": { 
                                "userEmail": "issumao@126.com", 
                                "userPWord": 'password', 
                                
                                }, 
                "stamp_TIMES": "1490192929212", 
                "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "USM001003", 
                    "ReqData": { 
                    "uid": "a8a3d8aad5e04f478425a4ce00bf0bd1", 
                    "userPWord": "password", 
                    "alias": "*%+", 
                    "email": "12333@126.com", 
                    "phone": "1999938xxxx", 
                    "password":"password",
                    "address":"海牙国际大厦20B"
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                },
                //只传入部分字段
                { 
                    "protocol_CODE": "USM001003", 
                    "ReqData": { 
                    "uid": "a8a3d8aad5e04f478425a4ce00bf0bd1", 
                    "userPWord": "password", 
                    "address":"海牙国际大厦20A"
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                },
                { 
                    "protocol_CODE": "USM001004", 
                    "ReqData": { 
                        "uid": "a8a3d8aad5e04f478425a4ce00bf0bd1", 
                        "appName": "IOS", 
                        "appToken": "326a866223956ceb2705d8b88758dc77e6420c3ff7ee3cab2388352a461c7b47", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "SVM001001", 
                    "ReqData": { 
                        "uid": "9343d2583fd34de0adc4a4c700a47f0e", 
                        "userPWord": "123456", 
                        "srvTarget": "ALL", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "SVM001001", 
                    "ReqData": { 
                        "uid": "9343d2583fd34de0adc4a4c700a47f0e", 
                        "userPWord": "123456", 
                        "srvTarget": "Nt", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                }
                ,
                { 
                    "protocol_CODE": "SVM001001", 
                    "ReqData": { 
                        "uid": "9343d2583fd34de0adc4a4c700a47f0e", 
                        "userPWord": "123456", 
                        "srvTarget": "De", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                }

            ],
    begin:function(){
            for(var  i=0;i<this.requestArray.length;i++)
            {
                    var data=this.requestArray[i];
                    var data_str=JSON.stringify(data);
                    $.ajax({
                            url:"DianzhuApi.ashx",
                            method:"POST",
                            data:data_str, 
                            async:false,
                            success:function (result) { 
                               apiTest.writelog("请求Code:"+data.protocol_CODE+ ":<br/>------返回值"+JSON.stringify(result));
                                },//success
                            error:function(errmsg)
                            {
                                
                            },//error
                            complete:function(result){}//complete
                        });//ajax
                }//for
       },//begin
    writelog:function(msg){
        $("#dvResults").append(msg+"<br/>");
    }//writelog
};//apitest
 
 apiTest.begin();
    </script>
</body>
</html>
