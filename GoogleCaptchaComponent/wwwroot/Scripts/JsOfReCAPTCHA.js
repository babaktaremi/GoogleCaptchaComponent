function render_recaptcha_v2(dotNetObj, selector, sitekey) {
    setTimeout(function () {
        grecaptcha.ready(function () {
            grecaptcha.render(selector, {
                'sitekey': sitekey,
                'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
                'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); }
            });
        });
    }.bind(this), 100);
};

function render_recaptcha_v3(dotNetObj, sitekey) {
    setTimeout(function () {
            grecaptcha.ready(function () {
                grecaptcha.execute(sitekey).then(function (token) {
                      dotNetObj.invokeMethodAsync('CallbackOnSuccess', token); 
                });
            });
    }.bind(this), 1000);
};


function getResponse(widgetId) {
    return grecaptcha.getResponse(widgetId);
}