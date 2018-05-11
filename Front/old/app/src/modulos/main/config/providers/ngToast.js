angular.module('mainApp').config(['ngToastProvider', function(ngToast) {
    ngToast.configure({
        compileContent: true,
        dismissOnTimeout: true,
        dismissOnClick: false
    });
}])