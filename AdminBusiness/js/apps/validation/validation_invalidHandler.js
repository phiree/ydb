    var invalidHandler = function (form, validator) {
        var errors = validator.numberOfInvalids();
        try {
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
        catch (e){
            return true;
        }
    };

    var showErrorsHandler = function(errorMap, errorList) {

        try {
            this.defaultShowErrors();

            if ( errorList.length > 0 ){
                var $firstError = $(errorList[0].element);

                if ($firstError.is(":visible")) {
                    $('html, body').css({
                        scrollTop: $firstError.offset().top
                    });
                } else {
                    $('html, body').css({
                        scrollTop: $($firstError.attr("focusID")).offset().top
                    });
                } //else
            }
        }
        catch (e){
            return true;
        }
    }
