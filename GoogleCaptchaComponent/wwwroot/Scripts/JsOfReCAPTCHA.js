function render_recaptcha_v2(dotNetObj, selector, sitekey,theme,language) {
    setTimeout(function () {
        grecaptcha.ready(function () {
            grecaptcha.render(selector, {
                'sitekey': sitekey,
                'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
                'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); },
                'theme': theme.replace(/"/g, "'"),
                'hl': language
            });
        });
    }.bind(this), 1000);
};

function render_recaptcha_v3(dotNetObj, sitekey, theme, action) {
    setTimeout(function () {
        grecaptcha.ready(function () {
            grecaptcha.execute(sitekey, {action: action}).then(function (token) {
                dotNetObj.invokeMethodAsync('CallbackOnSuccess', token);
            });
        });
    }.bind(this), 1000);
};


function reloadCaptcha() {

    grecaptcha.reset();
}

function getResponse(widgetId) {
    return grecaptcha.getResponse(widgetId);
}