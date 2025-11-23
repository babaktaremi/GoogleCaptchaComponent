function render_recaptcha_v2(dotNetObj, selector, sitekey,theme,language) {
    setTimeout(function () {
        grecaptcha.enterprise.ready(function () {
            grecaptcha.enterprise.render(selector, {
                'sitekey': sitekey,
                'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
                'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); },
                'theme': theme.replace(/"/g, "'"),
                'hl': language
            });
        });
    }.bind(this), 1000);
};


function execute_recaptcha_v3(dotNetObj, sitekey, action) {
    grecaptcha.enterprise.ready(function () {
        grecaptcha.enterprise.execute(sitekey, {action: action}).then(function (token) {
            dotNetObj.invokeMethodAsync('CallbackOnSuccess', token);
        });
    });
};

function render_recaptcha_v3(dotNetObj, sitekey, action) {
    setTimeout(function () {
        execute_recaptcha_v3(dotNetObj, sitekey, action);
    }.bind(this), 1000);
};


function reloadCaptcha() {

    grecaptcha.enterprise.reset();
}

function getResponse(widgetId) {
    return grecaptcha.enterprise.getResponse(widgetId);
}