angular.module('mainApp').factory('httpService', [ '$http', '$q', '$timeout', '$rootScope', 'session',
    function ($http, $q, $timeout, $rootScope, session) {
        //var server = 'http://localhost/GarcomAPI/';
        //var server = 'http://192.168.0.104/GarcomAPI/';
        //var server = 'http://192.168.0.104:50964/';
        var server = 'http://localhost:50964/';

        function requestInit(){
            $rootScope.onRequest = true;
        };

        function requestFinish(){
            $rootScope.onRequest = false;
        };

        function autenticar(login) {
            var objetoAutenticar = 'grant_type=password&username=' + login.login + '&password=' + login.senha;
            var urlAutenticar = server + 'Autenticar';
            var configAutenticar = {headers: {'Content-type': "application/x-www-form-urlencoded"} };
            return $http.post(urlAutenticar, objetoAutenticar, configAutenticar);
        }

        function get(url, sucessoCallback, erroCallback){
            requestInit();
            $http.get(server +'api/'+ url, session.getSession().config).then(
                function(data, status, headers, config, statusText){
                    $timeout(function(){
                    }, 1000);
                    requestFinish();
                    sucessoCallback(data.data, status, headers, config, statusText);
                },
                function(data, status, headers, config, statusText){
                    requestFinish();
                    erroCallback(data.data, status, headers, config, statusText);
                }
            );
        }

        function post(url, data, sucessoCallback, erroCallback){
            $http.post(server +'api/' + url , data, session.getSession().config).then(
                function(data, status, headers, config, statusText){
                    requestFinish();
                    sucessoCallback(data.data, status, headers, config, statusText);
                },
                function(data, status, headers, config, statusText){
                    requestFinish();
                    erroCallback(data.data, status, headers, config, statusText);
                }
            );
        }

        function put(url, data, sucessoCallback, erroCallback){
            $http.put(server +'api/' + url, data, session.getSession().config).then(
                function(data, status, headers, config, statusText){
                    requestFinish();
                    sucessoCallback(data.data, status, headers, config, statusText);
                },
                function(data, status, headers, config, statusText){
                    requestFinish();
                    erroCallback(data.data, status, headers, config, statusText);
                }
            );
        }

        function del(url, data, sucessoCallback, erroCallback){
            $http.delete(server +'api/' + url, data, session.getSession().config).then(
                function(data, status, headers, config, statusText){
                    requestFinish();
                    sucessoCallback(data.data, status, headers, config, statusText);
                },
                function(data, status, headers, config, statusText){
                    requestFinish();
                    erroCallback(data.data, status, headers, config, statusText);
                }
            );
        }

        return {
            get: get,
            post: post,
            put: put,
            del: del,
            autenticar: autenticar
        };
    }
]);