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
    <script src="test-data.js"></script>
    <script>
 
    var apiTest={
    requestArray:test_data,
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
