function setHeaderParam(ojb) {
    //var elementHead = ojb.parentElement.parentElement.parentElement.parentElement;
    //var elementHead = $('#resource_' + ojb.parentId);
    var objOperation = $('#'+ojb.parentId + '_' + ojb.nickname); //$(elementHead);
    var objHead = objOperation.find('.heading');
    var strPath = objHead.find('.path').find('a').html();
    var strToken="";
    if (strPath != "/api/v1/authorization" && strPath != "/api/v1/customers" && strPath != "/api/v1/merchants" && strPath != "/api/v1/customer3rds")
    {
        var objAuthorization = $('#resource__Authorization');
        var objResponseBody = objAuthorization.find('.response_body');
        if (objResponseBody.html() != "")
        {
            var arrSpan = objResponseBody.find('span');
            if (arrSpan.length == 4 && $(arrSpan[0]).text() == "token")
            {
                strToken = $(arrSpan[1]).text();
            }
        }
    }
    var arrTrParam = objOperation.find('.operation-params').find('tr');
    var queryParam = "";
    var contentParam = "";
    var inputTime;
    var inputSign;
    var inputToken;
    var inputAppName;
    for (var i = 0; i < arrTrParam.length; i++) {
        var arrTdParam = $(arrTrParam[i]).find('td');
        var paramType = $(arrTdParam[3]).html();
        var paramControl = $(arrTdParam[1]).find('input');
        var inputValue = $(paramControl[0]).val();
        if (paramControl.length == 0) {
            paramControl = $(arrTdParam[1]).find('textarea');
            inputValue = $(paramControl[0]).val();
        }
        if (paramControl.length == 0)
        {
            paramControl = $(arrTdParam[1]).find('select');
            inputValue = $(paramControl[0]).find("option:selected").text();
        }
        if (paramType == "path") {
            strPath = strPath.replace('{' + $(arrTdParam[0]).text() + '}', inputValue);
        }
        if (paramType == "body") {
            contentParam = inputValue;
        }
        if (paramType == "query" && inputValue != "") {
            var arrParamUri = $(arrTdParam[0]).text().split(".");
            queryParam = queryParam + arrParamUri[arrParamUri.length-1] + "=" + inputValue + "&";
        }
        if (paramType == "header" && $(arrTdParam[0]).text() == "stamp_TIMES")
        {
            inputTime = $(paramControl[0]);
        }
        if (paramType == "header" && $(arrTdParam[0]).text() == "sign") {
            inputSign = $(paramControl[0]);
        }
        if (paramType == "header" && $(arrTdParam[0]).text() == "token") {
            inputToken = $(paramControl[0]);
        }
        if (paramType == "header" && $(arrTdParam[0]).text() == "appName") {
            inputAppName = $(paramControl[0]);
        }
    }
    inputToken.val(strToken.substring(1,strToken.length-1));
    if (queryParam != "")
    {
        strPath = strPath + "?" + queryParam.substring(0, queryParam.length - 1);
    }
    if (contentParam.trim() == "")
    {
        contentParam = "''";
    }

    var settings = {
        async: false,
        url: "/AjaxService/GetSignAndTime.ashx",
        type: "POST",
        headers: {
            "content-type": "application/json",
            "appName": inputAppName.val()
        },
        dataType: "JSON",
        data: "{\n    \"apiurl\":\"" + strPath + "\",\n    \"token\":" + strToken + ",\n\"content\":" + contentParam + "\n}",
        success: function (data) {
            console.log(data)
            inputTime.val(data.data.TimeStamp);
            inputSign.val(data.data.Signature);
        },
        error: function (xhr, arg2, arg3) {
            //console.log(xhr)
            //console.log(arg2)
            //console.log(arg3)
            return;
        }
    }

    $.ajax(settings).done(function (response){
       //console.log(response);
    });


    //$('#resource__Authorization').find('.operation-params').find('tr')
    //alert(strPath);
}