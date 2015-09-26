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
    }
    var showErrorsHandler = function(errorMap, errorList) {
        var errors = this.numberOfInvalids();
        try {
            if (errors) {
                var currentStepErrors = [];
                for ( var i = 0; errorList[ i ]; i++ ) {

                    if ($(errorList[ i].element).parents(".cur-step").length != 0 ){
                        currentStepErrors.push(errorList[ i ]);
                    }
                }

                for ( var i = 0; currentStepErrors[i]; i++ ) {
                    var error = currentStepErrors[i];
                    this.settings.highlight && this.settings.highlight.call( this, error.element, this.settings.errorClass, this.settings.validClass );
                    this.showLabel( error.element, error.message );
                }
                if( currentStepErrors.length ) {
                    this.toShow = this.toShow.add( this.containers );
                }
                if (this.settings.success) {
                    for ( var i = 0; this.successList[i]; i++ ) {
                        this.showLabel( this.successList[i] );
                    }
                }
                if (this.settings.unhighlight) {
                    for ( var i = 0, elements = this.validElements(); elements[i]; i++ ) {
                        this.settings.unhighlight.call( this, elements[i], this.settings.errorClass, this.settings.validClass );
                    }
                }
                this.toHide = this.toHide.not( this.toShow );
                this.hideErrors();
                this.addWrapper( this.toShow ).show();

                if (currentStepErrors.length){
                    if ($(currentStepErrors[0].element).is(":visible")) {
                        $('html, body').css({
                            scrollTop: $(currentStepErrors[0].element).offset().top - $(window).height()/2
                        });
                    }
                    else {
                        $('html, body').css({
                            scrollTop: $($(currentStepErrors[0].element).attr("focusID")).offset().top - $(window).height()/2
                        });
                    } //else
                }

            } //if(errors)
        }
        catch (e){
            this.defaultShowErrors();
            return true;
        }
    }