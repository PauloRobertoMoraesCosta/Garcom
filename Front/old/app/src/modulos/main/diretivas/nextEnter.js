angular.module('mainApp')
    .directive('nextEnter', [function() {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                var canNext = scope.$eval(attrs.nextEnter);
                if(canNext === undefined) canNext = true;

                element.on('focus', function()
                {
                    element.bind('keydown', function(evt) {
                        if (evt.which == 13) {
                            evt.preventDefault(); // Doesn't work at all
                            window.stop(); // Works in all browsers but IE
                            document.execCommand("Stop"); // Works in IE

                            if(canNext)
                            {
                                try{
                                    if(scope.frm && scope.frm.$$element[0].length)
                                    {
                                        var elements = scope.frm.$$element[0];
                                        var next = null;
                                        var finded = false;
                                        for(var i = 0; i < elements.length; i++)
                                        {
                                            if(!finded && !('skip' in elements[0].attributes) && element[0] == elements[i])
                                                finded = true;
                                            else if(finded && !('skip' in elements[i].attributes))
                                            {
                                                next = i;
                                                break;
                                            }
                                        }

                                        if(next)
                                        {
                                            var input = scope.frm.$$element[0][next];
                                            input.focus();
                                            if(input.tagName == 'SELECT')
                                                $(input).click();
                                        }
                                    }
                                }
                                catch(e)
                                { }
                            }

                            return false;
                        }
                    });
                });

                element.on('blur', function()
                {
                    element.unbind('keydown');
                });
            }
        };
    }]);
