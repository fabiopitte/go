'use strict';

//Directive for showing an alert on click
app.directive("alert", function () {
    return function (scope, element, attrs) {
        element.bind("click", function () {
            console.log(attrs);
            alert("This is alert #" + attrs.alert);
        });
    };
});

app.directive('productTypeAhead', function ($compile, config) {
    return {
        restrict: 'E',
        template: '<input id="productTypeahead" ng-required="true" class="form-control" />',
        controller: function ($scope, $element) {

            $scope.criar = function () {


                // Instantiate the Bloodhound suggestion engine
                var produtos = new Bloodhound({
                    datumTokenizer: function (datum) {
                        return Bloodhound.tokenizers.whitespace(datum.value);
                    },
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: {
                        url: config.baseServiceUrl + '/products',
                        filter: function (produtos) {
                            // Map the remote source JSON array to a JavaScript object array
                            return $.map(produtos, function (produto) {
                                return {
                                    title: produto.title,
                                    id: produto.id,
                                    price: produto.price,
                                    code: produto.code,
                                    quantity: produto.quantity
                                };
                            });
                        }
                    }
                });

                produtos.initialize();

                // Instantiate the Typeahead UI
                $('.productTypeahead').typeahead(null, {
                    name: 'produto',
                    displayKey: 'title',
                    templates: {
                        suggestion: function (produto) {
                            return '<p>' + produto.title + '<span class="pull-right">R$ ' + produto.price + '</p>';
                        }
                    },
                    source: produtos.ttAdapter()
                }).on('typeahead:selected', function (obj, datum) {

                    var product = {
                        quantity: datum.quantity,
                        price: datum.price,
                        title: datum.title
                    };

                    $scope.sale.product.push(product);

                    window.sales = $scope.sale;

                    $('#price').val(product.price);
                    $('#quantity').focus();
                });

            }

        },
        link: function ($scope, $element, $attrs) {
            $element.on('bind', function (e) {
                $scope.produto = e.target.value;
                console.log('aaaaa');
            });
        }
    }
});

app.directive("addProduct", function () {
    return {
        restrict: "E",
        template: "<a class='btn btn-link pull-left' add-campo>Adicionar mais item</a><div class='clearfix'></div>"
    }
});

app.directive("addCampo", function ($compile) {
    return function (scope, element, attrs) {
        element.bind("click", function (item) {

            scope.sale = item;

            var el = $compile("<product-type-ahead></product-type-ahead>")(scope);
            element.parent().append(el);

            //var template = $("#template-produtos").clone().html();

            //var html = template.replace("||indice-preco||", 1)
            //                   .replace("||indice-quantidade||", 1)

            //$("#tag-produto").append($compile('<product-type-ahead></product-type-ahead>')(scope));

            //angular.element(document.getElementById('tag-produto'))
            //    .append($compile(html)(scope));

        });
    };
});

app.directive("addFields", function ($compile) {
    return function (scope, element, attrs) {
        element.bind("click", function () {
            scope.count++;
            angular.element(document.getElementById('space-for-buttons')).append($compile("<div><button class='btn btn-default' data-alert=" + scope.count + ">Show alert #" + scope.count + "</button></div>")(scope));
        });
    };
});



app.directive('autocomplete', function ($compile, gostoFactory) {
    return {
        restrict: 'E',
        //scope: { itemName: '@' },
        replace: true,
        template: "<input type='text' name='sale' ng-model='sale' typeahead='sale as sale.product for sale in getItems() | filter:$viewValue | limitTo:4' typeahead-on-select='updateItemInputValues(sale, " + 3 + ")' class='inputStr form-control'>",
        controller: function ($scope, $element) {
            $scope.getItems = function () {
                return gostoFactory.pesquisarCategorias()
                    .success(function (data) {
                        return data;
                    });
            };

            $scope.updateItemInputValues = function (item, itemNumber) {
                $('#itemCost' + itemNumber).val(item.cost.toFixed(2));
                $('#itemAmount' + itemNumber).val(item.amount);
            }
        }
    }
});

app.directive('addautobtn', function ($compile, gostoFactory) {
    var numItems;
    return {
        restrict: 'E',
        scope: { text: '@' },
        template: "<input type='button' class='btn btn-primary btn-sm' value='New Item' ng-click='add()'>",
        controller: function ($scope, $element) {
            $scope.getItems = function () {
                return gostoFactory.pesquisarCategorias()
                    .success(function (data) {
                        return data;
                    });
            };

            $scope.updateItemInputValues = function (item, itemNumber) {
                $('#itemCost' + itemNumber).val(item.cost.toFixed(2));
                $('#itemAmount' + itemNumber).val(item.amount);
            }

            $scope.add = function () {
                numItems++;
                var itemRow = document.createElement('div');
                itemRow.setAttribute('id', 'item' + (numItems));
                itemRow.setAttribute('class', 'row itemRow');

                var itemColTitle = document.createElement('div');
                itemColTitle.setAttribute('class', 'col-md-1');

                var itemColName = document.createElement('div');
                itemColName.setAttribute('class', 'col-md-3 itemCol');
                var itemNameInput = $compile("<input type='text' name='sale' ng-model='sale' typeahead='sale as sale.product for sale in getItems() | filter:$viewValue | limitTo:4' typeahead-on-select='updateItemInputValues(sale, " + 3 + ")' class='inputStr form-control'>")($scope);

                $(itemColName).append(itemNameInput);

                var cost = document.createElement('input');
                cost.setAttribute('id', 'cost');
                cost.setAttribute('class', 'form-control');
                cost.appendChild(document.createTextNode('123'));

                var itemColCost = document.createElement('div');
                itemColCost.setAttribute('class', 'col-md-2 itemCol');
                itemColCost.appendChild(cost);

                var amount = document.createElement('input');
                amount.setAttribute('id', 'amount');
                amount.setAttribute('class', 'form-control');
                amount.appendChild(document.createTextNode('12'));

                var itemColAmount = document.createElement('div');
                itemColAmount.setAttribute('class', 'col-md-2 itemCol');
                itemColAmount.appendChild(amount);

                var deleteCol = document.createElement('div');
                deleteCol.setAttribute('id', 'deleteItem' + numItems);
                deleteCol.setAttribute('class', 'col-md-1 deleteCol');
                deleteCol.setAttribute('onclick', 'deleteItem(' + numItems + ')');
                var deleteLink = document.createElement('a');
                deleteLink.setAttribute('class', 'btn btn-danger btn-xs');
                var deleteIcon = document.createElement('i');
                deleteIcon.setAttribute('class', 'fa fa-ban');
                deleteLink.appendChild(deleteIcon);
                deleteCol.appendChild(deleteLink);

                itemRow.appendChild(itemColTitle);
                itemRow.appendChild(itemColName);
                itemRow.appendChild(itemColCost);
                itemRow.appendChild(itemColAmount);
                itemRow.appendChild(deleteCol);

                $("#item").after(itemRow);
            };
        }
    };
});