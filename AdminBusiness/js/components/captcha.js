/**
 * Created by chen on 2016/8/26.
 */
;(function($, window, document,undefined) {
    var code = ""; //在全局定义验证码
    var imgUrl = "";
    var codeID;
    //定义Captcha的构造函数
    var Captcha = function(ele, opt) {
        this.$element = ele;
        this.defaults = {
            errorTime: "" || getErrorTime(),
            //生成验证码的包裹层ID
            codeID: "#code"
        };
        this.options = $.extend({}, this.defaults, opt);
    };

    //定义Captcha的方法
    Captcha.prototype = {
        //生成验证码
        createCode: function() {
            var errorTime = this.options.errorTime;
            codeID = this.options.codeID;
            if(errorTime >= 5) {
                //生成input输入框
                $("<input name='captcha' type='text' id='captcha' class='register-input error' placeholder='验证码' aria-required='true' aria-describedby='lblMsg'>").insertBefore(codeID);
                this._createImg();
                if (typeof this.options.callback == "function"){
                    this.options.callback.call(this);
                }
                this._createCodeAgain();
            }
            return this;
        },
        //再次生成
        _createCodeAgain: function() {
            var that = this;
            $(codeID).on("click", function(){
                that._createImg();
            });
        },
        //图片生成
        _createImg: function() {
            code = "";
            imgUrl = "";
            var codeLength = 4;//验证码的长度
            $(codeID).html("");
            var random = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');//随机数
            for (var i = 0; i < codeLength; i++) {//循环操作
                var index = Math.floor(Math.random() * 36);//取得随机数的索引（0~35）
                //console.log(random[index]);
                imgUrl = imgUrl + "<img src='../images/captcha/" + random[index] + ".png'> ";
                code += random[index];//根据索引取得随机数加到code上
            }
            $(codeID).data("code", code);
            $(codeID).html(imgUrl);//把code值赋给验证码
        }
    };
    //在插件中使用Captcha对象
    $.fn.captcha = function(options) {
        return this.each(function() {
            //创建Captcha的实体
            var captcha = new Captcha(this, options);
            captcha.createCode();
            return captcha;
        });

    };
    //获取错误次数
    function getErrorTime() {
        if (document.cookie.length > 0) {
            c_start = document.cookie.indexOf('errorTime' + "=");
            if (c_start != -1) {
                c_start = c_start + 'errorTime'.length + 1;
                c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                //console.log(document.cookie.substring(c_start, c_end));
                return unescape(document.cookie.substring(c_start, c_end));
            }
        }
        return "";
    }
})(jQuery, window, document);
