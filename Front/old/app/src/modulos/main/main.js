angular.module('mainApp', ['oc.lazyLoad', 'ui.router', 'ui.bootstrap', 'ngCookies', 'ngAnimate', 'ngSanitize', 'ngToast', 'ui.utils.masks']);

angular.module('mainApp').run(['$rootScope', '$window', '$sce', '$injector', '$timeout', '$state', '$ocLazyLoad', '$templateCache', 'langService', 'msgService', 'itensmenu', 'session', 'ngToast',
    function($rootScope, $window, $sce, $injector, $timeout, $state, $ocLazyLoad, $templateCache, langService, msgService, itensmenu, session, ngToast){
        langService.setLangById('pt');
        $rootScope.msgService = msgService;
        $rootScope.showSplash = false;

        $rootScope.$on('$stateChangeStart', function(e, state){
            $rootScope.showSplash = false;

            if(state.url != '/login' && JSON.stringify(session.getUsuario()) == '{}') {
                e.preventDefault();
                if(state.url != '/') $rootScope.showError('Faça o login.');
                $state.go('login');
            }
            else if(state.url == '/login' && JSON.stringify(session.getUsuario()) != '{}') {
                e.preventDefault();
                $state.go('principal.inicio');
            }
            else if(state.url != '/login'){
                var itemMenu = $rootScope.checkPermissaoPagina(state.name);
                if(itemMenu != null) {
                    e.preventDefault();
                    ngToast.danger({content: 'Você não tem acesso a rotina: "'+ itemMenu.nome +'"'});
                    $state.go('principal.inicio');
                }
                else
                    ngToast.dismiss();
            }
            else{ }
        });

        $window.onbeforeunload = function (e) {
            var message = "Deseja realmente sair desta página?";
            e = e || $window.event;

            // For IE and Firefox
            if (e) { e.returnValue = message; }

            // For Safari
            return message;
        };

        $rootScope.sucessoMsg = function(stringContent){
            ngToastGenerate(stringContent, 'sucesso', stringContent.indexOf('[TOAST_ID]') >= 0);
        };
        $rootScope.erroMsg = function(stringContent){
            ngToastGenerate(stringContent, 'erro', stringContent.indexOf('[TOAST_ID]') >= 0);
        };
        $rootScope.alertaMsg = function(stringContent){
            ngToastGenerate(stringContent, 'alerta', stringContent.indexOf('[TOAST_ID]') >= 0);
        };
        $rootScope.infoMsg = function(stringContent){
            ngToastGenerate(stringContent, 'info', stringContent.indexOf('[TOAST_ID]') >= 0);
        };

        var ngToastGenerate = function(content, tipo, idToastParse){
            var idToast = undefined;

            if($rootScope.scope && content.indexOf($rootScope.scope.controllerName))
                content = content.replace($rootScope.scope.controllerName, 'scope');

            switch (tipo){
                case 'sucesso': { idToast = ngToast.success({ content: $sce.trustAsHtml('<i class="material-icons md-28">done</i> '     + content), compileContent: true }); break; }
                case 'erro':    { idToast = ngToast.danger({ content: $sce.trustAsHtml('<i class="material-icons md-28">clear</i> '     + content), compileContent: true }); break; }
                case 'alerta':  { idToast = ngToast.warning({ content: $sce.trustAsHtml('<i class="material-icons md-28">warning</i> '  + content), compileContent: true }); break; }
                case 'info':    { idToast = ngToast.info({ content: $sce.trustAsHtml('<i class="material-icons md-28">done</i> '        + content), compileContent: true }); break; }
            };

            if(idToastParse){
                switch (tipo){
                    case 'sucesso': { ngToast.update(idToast, { content: $sce.trustAsHtml('<i class="material-icons md-28">done</i> '    + content.replace('[TOAST_ID]', idToast)), compileContent: true }); break; }
                    case 'erro':    { ngToast.update(idToast, { content: $sce.trustAsHtml('<i class="material-icons md-28">clear</i> '   + content.replace('[TOAST_ID]', idToast)), compileContent: true }); break; }
                    case 'alerta':  { ngToast.update(idToast, { content: $sce.trustAsHtml('<i class="material-icons md-28">warning</i> ' + content.replace('[TOAST_ID]', idToast)), compileContent: true }); break; }
                    case 'info':    { ngToast.update(idToast, { content: $sce.trustAsHtml('<i class="material-icons md-28">done</i> '    + content.replace('[TOAST_ID]', idToast)), compileContent: true }); break; }
                };
            }
        };

        $rootScope.loadingDesfazer = false;
        $rootScope.desfazer = function(strWebApi, id, idNgToast, funcScopeAfter, funcEscopeBefore){
            var service = $injector.get(strWebApi);
            if(idNgToast) ngToast.dismiss(idNgToast);

            if(funcEscopeBefore && typeof funcEscopeBefore === 'function') funcEscopeBefore();

            $rootScope.loadingDesfazer = true;
            service.desfazer(
                id,
                function(data){
                    $rootScope.loadingDesfazer = false;
                    if(funcScopeAfter && typeof funcScopeAfter === 'function') funcScopeAfter(data);
                },
                function(erro, status){
                    $rootScope.loadingDesfazer = false;
                    $rootScope.showError(erro, status);
                }
            );
        };

        $rootScope.checkPermissaoPagina = function(state){
            var itemPagina = null;
            itensmenu.forEach(function(item){
                if(item.state == state){
                    itemPagina = item;
                }
                else if(item.submenu && item.submenu.itens && item.submenu.itens.length > 0){
                    item.submenu.itens.forEach(function(subitem){
                        if(subitem.state == state){
                            itemPagina = item;
                            return;
                        }
                    });
                }

                if(itemPagina != null) return;
            });

            if(itemPagina != null){
                var temPermissao = false;
                itemPagina.idPerfis.forEach(function(perfilId){
                    if(session.getUsuario().perfilId == perfilId){
                        temPermissao = true;
                        return;
                    }
                });

                if(!temPermissao) return itemPagina;
            }

            return null;
        };

        $rootScope.showError = function(objError, status){
            if(objError !== null && typeof objError === 'object')
            {
                if('error_description' in objError)
                    ngToast.danger({content: objError.error_description});
                else if('message' in objError){
                    if(status == 401 || status === undefined){
                        if(objError.message !== 'Authorization has been denied for this request.')
                            ngToast.danger({content: objError.message});
                        else{
                            ngToast.danger({content: 'Sua sessão expirou ou o acesso foi negado. Faça o login novamente.'});
                            session.logout(true);
                        }
                    }
                    else
                        ngToast.danger({content: objError.message});
                }
                else
                    ngToast.danger({content: objError});
            }
            else{
                if(typeof objError === 'string')
                    ngToast.danger({content: objError});
                else if(objError === null && status == -1)
                    ngToast.danger({content: 'Não foi possível conectar-se ao servidor.'});
                else
                    ngToast.danger({content: 'Ocorreu um erro ao realizar esta operação. Contate o suporte.'});
            }
        };

        $rootScope.widthContent = 700;
        $rootScope.mobileWidth = 900;
        $rootScope.minMobileWidth = 575;//320;
        $rootScope.mobile = $window.innerWidth <= $rootScope.mobileWidth;
        $rootScope.minMobile = $window.innerWidth <= $rootScope.minMobileWidth;
        $rootScope.window = {
            width: $window.innerWidth,
            height: $window.innerHeight,
            xs: $window.innerWidth <= 575,
            sm: $window.innerWidth > 575 && $window.innerWidth > 768,
            md: $window.innerWidth > 767 && $window.innerWidth < 992,
            lg: $window.innerWidth > 991 && $window.innerWidth < 1200,
            xl: $window.innerWidth > 1199
        };
        $rootScope.heightBody = $rootScope.window.height - 106;
        $rootScope.widthBody = $rootScope.window.width - ($rootScope.mobile ? 0 : 220);
        $rootScope.styleCardBody = {
            'height': $rootScope.heightBody+'px',
            'width': $rootScope.widthBody+'px',
            'border-right':'none',
            'border-bottom':'none'
        };

        angular.element($window).bind('resize', function(){
            $rootScope.mobile = $window.innerWidth <= $rootScope.mobileWidth;
            $rootScope.minMobile = $window.innerWidth <= $rootScope.minMobileWidth;

            $rootScope.window.width = $window.innerWidth;
            $rootScope.window.height = $window.innerHeight;
            $rootScope.window.xs = $window.innerWidth <= 575;
            $rootScope.window.sm = $window.innerWidth > 575 && $window.innerWidth > 768;
            $rootScope.window.md = $window.innerWidth > 767 && $window.innerWidth < 992;
            $rootScope.window.lg = $window.innerWidth > 991 && $window.innerWidth < 1200;
            $rootScope.window.xl = $window.innerWidth > 1199;

            $rootScope.heightBody = $rootScope.window.height - 106;
            $rootScope.widthBody = $rootScope.window.width - ($rootScope.mobile ? 0 : 220);
            $rootScope.styleCardBody = {
                'height': $rootScope.heightBody+'px',
                'width': $rootScope.widthBody+'px',
                'border-right':'none',
                'border-bottom':'none'
            };

            $rootScope.$digest();
        });

        $rootScope.semAcentos = function(actual, expected) {
            if (angular.isObject(actual)) return false;
            function removeAccents(value) {
                return value.toString()
                                .replace(/[á|ã|â]/g, 'a')
                                .replace(/[é|ê]/g, 'e')
                                .replace(/í/g, 'i')
                                .replace(/[ó|õ|ô]/g, 'o')
                                .replace(/ú/g, 'u')
                                .replace(/ñ/g, 'n')
                                .replace(/ç/g, 'c');
            }
            actual = removeAccents(angular.lowercase('' + actual));
            expected = removeAccents(angular.lowercase('' + expected));

            return actual.indexOf(expected) !== -1;
        };

        $templateCache.put('templateCache/myContent.htm', ''+
            '<div class="modal-header">'+
                '<div class="col-xs-12 p-0">'+
                    '<div class="col-xs-8 p-0 pt-05">'+
                        '<h4 class="m-0">{{titulo}}</h4>'+
                    '</div>'+
                    '<div class="col-xs-4 p-0 text-xs-right">'+
                        '<div class="btn-bullet gray-dark float-right" ng-click="cancelar()" title="Cancelar"><i class="material-icons">close</i></div>'+
                    '</div>'+
                '</div>'+
            '</div>'+
            '<div class="modal-body text-center" ng-init="openDialog()">'+
                '<input type="file" id="fileInput" img-cropper-fileread image="cropper.sourceImage" style="display: none" accept="image/*" />'+

                '<div style="display: table; margin: auto">'+
                    '<div style="{{\'display: table; margin: auto; text-align: center; width: \' + width + \'px; height: \' + height + \'px\'}}" ng-if="!cropper.sourceImage">'+
                        '<div style="display: table-cell; vertical-align: middle">'+
                            'Nenhuma imagem selecionada<br/>'+
                            '<button class="btn btn-primary pl-2 pr-2 mt-1" ng-click="openDialog()">Selecionar</button>'+
                        '</div>'+
                    '</div>'+
                    '<canvas ng-if="cropper.sourceImage" width="{{width}}" height="{{height}}" id="canvas" image-cropper image="cropper.sourceImage" cropped-image="cropper.croppedImage" crop-width="widthCrop" crop-height="heightCrop" keep-aspect="true" touch-radius="30"></canvas>'+
                '</div>'+
            '</div>'+
            '<div class="modal-footer">'+
                '<button class="btn btn-primary pl-2 pr-2 float-left" ng-if="cropper.sourceImage" ng-click="openDialog()">Selecionar outra</button>'+
                '<button class="btn btn-none" ng-click="cancelar()">{{cancelarMsg}}</button>'+
                '<button class="btn btn-primary pl-2 pr-2" ng-click="ok()" ng-if="cropper.croppedImage">{{okMsg}}</button>'+
            '</div>'+
        '');
    }
]);
