/**
 * imgLocalPrev by licdream@126.com
 * 2015-8-18
 *
 * introduce:--------------------------------
 * a simple method to preview a upload img.
 *
 * feature:----------------------------------
 * show a preview pic before real img file upload.
 *
 * html structure:---------------------------
 * <div>
 *      <input class="input-file-btn" />
 *      <img class="input-file-pre" />
 * <div>
 *
 * usage:------------------------------------
 * $("input-file-btn").imgLocalPrev();
 */

$.fn.imgLocalPrev = function(options){
    var defaults = {
        previewImg : "input-file-pre"
    };
    return this.each(function(){
        var opts = $.extend({}, defaults, options);
        var _this = this;
        var $this = $(this);

        $this.change(function(){

            var imgObjPreview = $this.siblings("." + opts.previewImg).get(0);

            if ( _this.files && _this.files[0]) {
                imgObjPreview.src = window.URL.createObjectURL(_this.files[0]);
            } else {
                _this.select();
                _this.blur();
                var imgSrc = document.selection.createRange().text;

                try {
                    imgObjPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    imgObjPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    return false;
                }
                document.selection.empty();
            }
        });
    });

}