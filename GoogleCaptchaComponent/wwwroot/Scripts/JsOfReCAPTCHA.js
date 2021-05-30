function render_recaptcha(dotNetObj, selector, sitekey) {
    setTimeout(function () {
        if (typeof grecaptcha === 'undefined' || typeof grecaptcha.render === 'undefined') {
            this.render_recaptcha();
        } else {
            return grecaptcha.render(selector, {
                'sitekey': sitekey,
                'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
                'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); }
            });
        }
    }.bind(this), 100);
};

function getResponse(widgetId) {
    return grecaptcha.getResponse(widgetId);
}