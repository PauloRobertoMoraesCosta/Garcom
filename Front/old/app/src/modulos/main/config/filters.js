angular.module('mainApp')
    .filter('perfil', ['perfis',
        function(perfis){
            return function(perfilId){
                var perfil = '';
                perfis.forEach(function(p){
                    if(p.id == perfilId){
                        perfil = p.descricao;
                        return;
                    }
                });
                return perfil;
            }
        }
    ])
    .filter('ativoInativo', function() {
        return function(value) { return (value === true || value === 'T') ? 'Ativo' : 'Inativo'; };
    })
    .filter('cnpj', ['utils', function(utils){
        return function (cnpj){ return utils.cnpjFormat(cnpj); };
    }])
    .filter('simNao', function() {
        return function(value) { return (value === true || value === 'T') ? 'Sim' : 'Não'; };
    })
    .filter('capitalize', [function() {
        /* https://ngmilk.rocks/2015/04/03/angularjs-filter-that-capitalizes-each-word-in-a-string/ */
        return function(input){
            if(!input) return input;
            input = input.toString();

            if(input.indexOf(' ') !== -1){
                var inputPieces, i;

                input = input.toLowerCase();
                inputPieces = input.split(' ');

                for(i = 0; i < inputPieces.length; i++){
                    inputPieces[i] = capitalizeString(inputPieces[i]);
                }

                return inputPieces.toString().replace(/,/g, ' ');
            }
            else {
                input = input.toLowerCase();
                return capitalizeString(input);
            }

            function capitalizeString(inputString){
                if(inputString.match(/^(d[aeo]|e)$/ig)) return inputString;
                return inputString.substring(0,1).toUpperCase() + inputString.substring(1);
            }
        };
    }])
    .filter('cep', [function(){
        return function (s){
            if(!s || (s && s.length !== 8)) return s;
            else return s[0]+s[1]+s[2]+s[3]+s[4]+'-'+s[5]+s[6]+s[7];
        };
    }])
    .filter('cpf', [function(){
        return function (s){
            if(!s || (s && s.length !== 11)) return s;
            else return s[0]+s[1]+s[2]+'.'+s[3]+s[4]+s[5]+'.'+s[6]+s[7]+s[8]+'-'+s[9]+s[10];
        };
    }])
    .filter('cpfCnpj', ['$filter', function($filter){
        return function (s){
            if(!s || (s && (s.length !== 11 && s.length !== 14))) return s;
            if(s.length === 11) return $filter('cpf')(s);
            else if(s.length === 14) return $filter('cnpj')(s);
        };
    }])
    .filter('cns', [function(){
        return function (s){
            if(!s || (s && s.length !== 15)) return s;
            else return s[0]+s[1]+s[2]+' '+s[3]+s[4]+s[5]+s[6]+' '+s[7]+s[8]+s[9]+s[10]+' '+s[11]+s[12]+s[13]+s[14];
        };
    }])

    .filter('removeAccents', [function() {
        return function(input){
            if(!input) return input;
            input = input.toString();

            return input
                .replace(/á/g, 'a').replace(/â/g, 'a').replace(/ã/g, 'a').replace(/à/g, 'a')
                .replace(/é/g, 'e').replace(/ê/g, 'e').replace(/è/g, 'e')
                .replace(/í/g, 'i').replace(/ï/g, 'i')
                .replace(/ó/g, 'o').replace(/ô/g, 'o').replace(/õ/g, 'o')
                .replace(/ú/g, 'u').replace(/ü/g, 'u')
                .replace(/ç/g, 'c')
                .replace(/ñ/g, 'n')
                .replace(/ß/g, 's');
        };
    }])

    .filter('sexo', ['utilsService', function(utilsService){
        return function (sexo){
            return utilsService.getGeneroMedico(sexo);
        };
    }])

    .filter('telefone', [function(){
        return function (s){
            if(s){
                if(s.length === 10) return '('+s[0]+s[1]+') '+s[2]+s[3]+s[4]+s[5]+'-'+s[6]+s[7]+s[8]+s[9];
                else if(s && s.length === 11) return '('+s[0]+s[1]+') '+s[2]+s[3]+s[4]+s[5]+s[6]+'-'+s[7]+s[8]+s[9]+s[10];
            }
            else return s;
        };
    }])

    .filter('trusted', ['$sce', function ($sce) {
        return function(src) {
            if(!src) return;
            return $sce.trustAsResourceUrl(src);
        };
    }])

    .filter('data', ['$filter', function ($filter) {
        return function(data) {
            if(data != '' && data != null){
                if(typeof data == 'string')
                {
                    data = data.replace(/-/g, '/');
                    data = data.replace('T', ' ');
                    data = data.substr(0, 10);
                    var dia = data.split('/')[2];
                    var mes = data.split('/')[1];
                    var ano = data.split('/')[0];

                    return dia + '/' + mes + '/' + ano;
                }
                else
                    return $filter('date')(data, 'dd/MM/yyyy');
            }
            return '';
        };
    }])
    .filter('dataHora', ['$filter', function ($filter) {
        return function(dataHora) {
            if(dataHora != '' && dataHora != null){
                if(typeof dataHora == 'string')
                {
                    dataHora = dataHora.replace(/-/g, '/');
                    dataHora = dataHora.replace('T', ' ');

                    var data = dataHora.split(' ')[0];
                    var hora = dataHora.split(' ')[1];

                    var dia = data.split('/')[2];
                    var mes = data.split('/')[1];
                    var ano = data.split('/')[0];

                    var horas = hora.split(':')[0];
                    var minutos = hora.split(':')[1];

                    return dia + '/' + mes + '/' + ano + ' ' + horas + ':' + minutos;
                }
                else
                    return $filter('date')(data, 'dd/MM/yyyy HH:mm');
            }
            return '';
        };
    }])
    .filter('padLeft', [function () {
        return function(texto, char, qtd) {
            if(typeof texto == 'number') texto = texto.toString();
            if(typeof texto == 'string' && texto.length > 0){
                if(!char) char = '0';
                if(!qtd) qtd = 4;

                if(texto.length < qtd){
                    var concatText = texto;
                    for(var i = 0; i < (qtd - texto.length); i++){
                        concatText = char + concatText;
                    }

                    return concatText;
                }
            }
            return texto;
        };
    }])
    .filter('padRight', [function () {
        return function(texto, char, qtd) {
            if(typeof texto == 'number') texto = texto.toString();
            if(typeof texto == 'string' && texto.length > 0){
                if(!char) char = '0';
                if(!qtd) qtd = 4;

                if(texto.length < qtd){
                    var concatText = texto;
                    for(var i = 0; i < (qtd - texto.length); i++){
                        concatText = concatText + char;
                    }

                    return concatText;
                }
            }
            return texto;
        };
    }])

    .filter('removeIguais', function(){
        return function(lista1, lista2, key1, key2, valorExceto){
            var newList = [];
            lista1.forEach(function(item) {
                var exceto = false;

                if(!valorExceto || (item[key1] !== valorExceto)){
                    lista2.forEach(function(itemExc) {
                            if(!key2 && item[key1] === itemExc[key1]){
                                exceto = true;
                                return;
                            }
                            else if(key2 && item[key1] === itemExc[key2]){
                                exceto = true;
                                return;
                            }
                    });
                }

                if(!exceto) newList.push(item);
            });

            return newList;
        }
    })

    ;