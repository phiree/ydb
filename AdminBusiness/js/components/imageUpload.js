/**
 * imageUpload.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * image upload plugin with limit and local preview.
 */
(function (factory) {
    if ( typeof define === "function" && define.amd ) {
        define(['jquery'], function ($) {
            factory($);
        });
    } else {
        factory(jQuery);
    }
}(function ($) {
    var pluginName = "imageUpload";

    var ImageUpload = (function () {

        function clearFileInput(file){
            var form=document.createElement('form');
            var pos = file.nextSibling;

            document.body.appendChild(form);
            form.appendChild(file);
            form.reset();
            pos.parentNode.insertBefore(file, pos);
            document.body.removeChild(form);
        }

        function _ImageUpload(element, options) {
            this.el = element;
            this.$el = $(element);
            this.options = $.extend({}, _ImageUpload.DEFAULTS, options);
            this.limit = this.$el.attr("data-limit") && parseInt(this.$el.attr("data-limit")) || this.options.limit;
            this.size = this.$el.attr("data-size") && parseInt(this.$el.attr("data-size")) * 1024 * 1024 || this.options.size;
            this.params = this.$el.attr("data-params") && JSON.parse(this.$el.attr("data-params")) || this.options.params;
            this.single = typeof this.$el.attr("data-single") === "string" && this.$el.attr("data-single") === "true" || this.options.single;
            this.preview = typeof this.$el.attr("data-preview") === "string" && this.$el.attr("data-preview") || this.options.params;
            this.local = typeof this.$el.attr("data-local") === "string" && this.$el.attr("data-local") === "true" || this.options.local;
            this.$list = this.$el.attr("data-list") &&  $(this.$el.attr("data-list")) || this.options.list || this.$el.parent();


            this.init();
        }

        _ImageUpload.DEFAULTS = {
            limit : 2,
            size : 2 * 1024 * 1024,
            createNew : true,
            single : false,
            local : false
        };

        _ImageUpload.prototype.init = function(){
            // 如果指定列表里的input-file元素大于limit,则直接返回
            if ( this.$list.find('[data-count="input-file"]').length >= this.limit) {
                this.$el.remove();
                return
            }

            this._buildDOM();
            this._delegate();
        };

        /**
         * 构建input DOM
         * @private
         */
        _ImageUpload.prototype._buildDOM = function () {
            var $inputBox = $("<div></div>").addClass("input-file-box").attr("data-count", "input-file"),
                $inputPreview = $("<img/>").addClass("input-file-pre").attr("src", this.preview),
                $inputMark = $("<div></div>").addClass("input-file-mark"),
                $inputFg = $("<div></div>").addClass("input-file-fg"),
                $inputBg = $("<div></div>").addClass("input-file-bg");

            this.$el.wrap($inputBox);

            this.$inputBox = $inputBox = this.$el.parent();
            $inputBox.append($inputPreview)
                .append($inputMark)
                .append($inputFg)
                .append($inputBg);

            this.$preview = $inputPreview;
            this.$mark = $inputMark;
        };

        /**
         * 绑定事件
         * @private
         */
        _ImageUpload.prototype._delegate = function () {
            var _this = this;
            // 在图片文件改变是触发upload
            this.$el.change(function(){
                var ele = this;
                return _this._upload(ele);
            });
        };

        /**
         * 图片上传
         * @private
         */
        _ImageUpload.prototype._upload = function(){
            var ele = this.el;

            if ( typeof ele.files !== "undefined" && ele.files.length ){
                if ( !/image\//.test(ele.files[0].type) ){
                    alert("请选择(png,jpg,gif)格式的图片文件");
                    return;
                }

                if ( ele.files[0].size > this.size ){
                    clearFileInput(ele);
                    alert("图片大小应小于4M");
                    return;
                }
            }

            if ( this.local ){
                this._setPreview();
                return;
            }

            this._uploadForm();
        };

        /**
         * 为了兼容ASP.NET使用的方法。
         * @description 由于ASP.NET webForm只支持一个form，因此异步提交图片，采用在
         * form外新建另外一个form上传完毕后删除的方法，来源于网络。
         * @private
         */
        _ImageUpload.prototype._uploadForm = function () {
            var $form,
                $inputId,
                $inputType,
                _this = this;

            $form = $("<form></form>").css({
                "height" : "0px",
                "overflow" : "hidden",
                "position" : "relative"
            });
            $inputId = $("<input>");
            $inputType = $("<input>");


            $form.attr({
                "action" : "/AjaxService/FileUploader.ashx",
                "method" : "post",
                "enctype" : "multipart/form-data"
            });

            $inputId.attr({
                "name" : "businessId",
                "value" : this.params.businessId
            });

            $inputType.attr({
                "name" : "imageType",
                "value" : this.params.imageType
            });

            this.$el.attr("name", "upload_file");
            $form.append($inputId).append($inputType).append(this.$el);
            $('body').append($form);


            _this.$mark.show();

            $form.ajaxSubmit({
                success: function (data) {
                    _this._setPreview();
                    _this.$mark.hide();

                    if ( _this.single ){
                        _this.$inputBox.prepend(_this.$el);
                    }

                    if ( !_this.single && _this.options.createNew) {
                        // 新建另外一个新的文件控件
                        _this.createNew();
                    }
                    $form.remove();
                },
                error: function () {
                    // 若上传错误， 重置上传控件
                    _this._reset();
                    _this.$preview.get(0).src = "";
                    _this.$mark.hide();
                    $form.remove();
                    alert("上次失败，请重新上传");
                }
            })
        };

        /**
         * 上传图片的本地预览
         * @private
         */
        _ImageUpload.prototype._setPreview = function(){
            var preview = this.$preview.get(0);
            var ele = this.el;

            // DOM files属性支持检测
            if ( typeof ele.files !== "undefined" && ele.files.length ){
                preview.src = window.URL.createObjectURL(ele.files[0]);
            } else {
                // 不支持files的IE用AlphaImageLoader来获取图片路径
                var imageSrc;
                ele.select();
                ele.blur();
                imageSrc = document.selection.createRange().text;

                try {
                    preview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    preview.filter.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imageSrc;
                } catch (e){
                    return;
                }
                document.selection.empty();
            }
        };

        _ImageUpload.prototype._reset = function(){
            this.$inputBox.prepend(this.$el);
            clearFileInput(this.el);
        };

        _ImageUpload.prototype.createNew = function(){
            if ( this.$list.find('[data-count="input-file"]').length + 1 > this.limit ) {
                return
            }
            this.$inputBox.after(this.$el);
            this._buildDOM();
        };

        return _ImageUpload;
    })();

    $.fn[pluginName] = function (option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data("ImageUpload");
            var options = ( typeof option === "object" && option );

            if ( !data ) {
                $this.data("imageUpload", ( data = new ImageUpload(this, options)))
            }
        });
    };

}));