var invalidHandler = function (form, validator) {
    var errors = validator.numberOfInvalids();
    if (errors) {
        if ($(validator.errorList[0].element).is(":visible")) {
            $('html, body').animate({
                scrollTop: $(validator.errorList[0].element).offset().top - $(window).height()/2
            }, 1000).stop(false,true);
        }
        else {
            $('html, body').animate({
                scrollTop: $("#" + $(validator.errorList[0].element).attr("focusID")).offset().top - $(window).height()/2
            }, 1000).stop(false,true);
        } //else
    } //if(errors)
}