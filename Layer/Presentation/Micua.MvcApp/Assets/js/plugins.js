/// <reference path="../../jquery/jquery-2.1.3.js" />

/*
 * A jQuery plugin for bootstrap alert
 */
; (function ($) {
    "use strict";

    var alertTmpl = '<div class="alert alert-{0} fade in" role="alert">{1}{2}{3}{4}</div>';
    var titleTmpl = '<strong>{0}</strong> ';
    var iconTmpl = '<span class="glyphicon glyphicon-{0}" aria-hidden="true"></span>';
    var closeButton = '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>';

    $.fn.alert = function (content, option) {
        var config = {
            title: '',
            level: 'info',
            icon: '',
            timeout: 0,
            showClose: false
        };

        if (option) jQuery.extend(config, option);

        this.empty();

        var alertElement = alertTmpl
            .replace('{0}', config.level)
            .replace('{1}', config.icon ? iconTmpl.replace('{0}', config.icon) : '')
            .replace('{2}', config.title ? titleTmpl.replace('{0}', config.title) : '')
            .replace('{3}', content)
            .replace('{4}', config.showClose ? closeButton : '')
        var $elements = $(alertElement).appendTo(this).fadeIn();
        //this.each(function () {
        //    // element-specific code here
        //    this.append(alertElement);

        //});
        if (config.timeout) {
            setTimeout(function () {
                $elements.fadeOut(function () { $elements.remove(); });
            }, config.timeout);
        }
        return this;
    };
})(jQuery);

/*
 * A jQuery plugin for shake
 */
; (function ($) {
    "use strict";

    var animates = ['bounce', 'flash', 'pulse', 'rubberBand', 'shake', 'swing', 'tada', 'wobble', 'bounceIn', 'bounceInDown', 'bounceInLeft', 'bounceInRight', 'bounceInUp', 'bounceOut', 'bounceOutDown', 'bounceOutLeft', 'bounceOutRight', 'bounceOutUp', 'fadeIn', 'fadeInDown', 'fadeInDownBig', 'fadeInLeft', 'fadeInLeftBig', 'fadeInRight', 'fadeInRightBig', 'fadeInUp', 'fadeInUpBig', 'fadeOut', 'fadeOutDown', 'fadeOutDownBig', 'fadeOutLeft', 'fadeOutLeftBig', 'fadeOutRight', 'fadeOutRightBig', 'fadeOutUp', 'fadeOutUpBig', 'flip', 'flipInX', 'flipInY', 'flipOutX', 'flipOutY', 'lightSpeedIn', 'lightSpeedOut', 'rotateIn', 'rotateInDownLeft', 'rotateInDownRight', 'rotateInUpLeft', 'rotateInUpRight', 'rotateOut', 'rotateOutDownLeft', 'rotateOutDownRight', 'rotateOutUpLeft', 'rotateOutUpRight', 'slideInUp', 'slideInDown', 'slideInLeft', 'slideInRight', 'slideOutUp', 'slideOutDown', 'slideOutLeft', 'slideOutRight', 'zoomIn', 'zoomInDown', 'zoomInLeft', 'zoomInRight', 'zoomInUp', 'zoomOut', 'zoomOutDown', 'zoomOutLeft', 'zoomOutRight', 'zoomOutUp', 'hinge', 'rollIn', 'rollOut'];

    $.fn.cssAnimate = function (animate) {
        if (this.hasClass('animated')) {
            this.removeClass('animated');
            for (var i = 0; i < animates.length; i++) {
                this.removeClass(animates[i]);
            }
        }
        this.addClass(animate + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass(animate + ' animated');
        });

        return this;
    };
})(jQuery);