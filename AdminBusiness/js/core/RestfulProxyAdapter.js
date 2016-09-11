(function (root, $) {

    var RestfulProxyAdapter = {};

    // Map from CRUD to HTTP
    //GET = 0,
    //POST = 1,
    //PUT = 2,
    //DELETE = 3,
    //HEAD = 4,
    //OPTIONS = 5,
    //PATCH = 6,
    //MERGE = 7
    var methodMap = {
            'read': 'GET',
            'create': 'POST',
            'update': 'PUT',
            'delete': 'DELETE',
            'patch': 'PATCH'
        },
        methodMapCode = {
            'GET': 0,
            'POST': 1,
            'PUT': 2,
            'DELETE': 3,
            'HEAD': 4,
            'OPTIONS': 5,
            'PATCH': 6,
            'MERGE': 7
        };

    RestfulProxyAdapter.sync = function (method, model, option) {
        var type = methodMap[method], data;
        var params = {
            url: option.url || urlError("url"),
            type: "POST",
            contentType: "application/json",
            processData: false
        };
        var error = option.error;
        var success = option.success;

        data = {
            apiurl: option.apiurl || urlError("apiurl"),
            method: methodMapCode[type],
            token: option.token || YDBan.cookie.getCookie("api_token"),
            content : ""
        };

        // 只有是合适的请求时，才给content赋值
        if ( option.data == null && model && (method === "create" || method === "update" || method === "patch")){
            data.content = model ? model.attribute : {};
        }

        params.data = JSON.stringify(data);

        option.error = function (xhr, textStatus, errorThrown) {
            //如果为unlogin错误，则返回登陆页面
            if ( JSON.parse(xhr.responseText).msg === "unlogin" ) {
                window.location.href = "/login.aspx"
            }

            if ( error ) {
                error(xhr, textStatus, errorThrown);
            }
        };

        option.success = function (data, textStatus, jqXHR) {
            var model;
            if ( data.result === "True" ) {
                model = data.data;
            }
            if ( success ) {
                success(model, textStatus, jqXHR)
            }
        };

        return $.ajax($.extend(params, option))

    };

    function urlError(url) {
        throw new Error('A ' + url + ' property or function must be specified');
    }

    root.RestfulProxyAdapter = RestfulProxyAdapter;

})(window, $);