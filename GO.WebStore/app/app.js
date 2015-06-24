var app = angular.module('gostoWebStore', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'chart.js', 'ui.bootstrap', 'dropzone']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/dashboard", { controller: 'dashboardController', templateUrl: "/app/views/dashboard/index.html" });

    $routeProvider.when("/user", { controller: 'userController', templateUrl: "/app/views/user/index.html" });

    $routeProvider.when("/invoice/:code", {
        controller: function ($scope, $routeParams, gostoFactory) {

            if ($routeParams.code !== null) {
                var sale = $routeParams.code;
                getInvoice(sale);
            }

            function getInvoice(sale) {
                gostoFactory.obterVenda(sale).success(function (venda) {
                    $scope.sale = venda;
                    var total = 0;
                    for (var i = 0; i < venda.itens.length; i++) { total = parseFloat(total) + parseFloat(venda.itens[i].price.replace(".", "").replace(",", "") * 1).toFixed(2) / 100 * parseFloat(venda.itens[i].quantity); }
                    $scope.totalDaVenda = total.toFixed(2);
                });
            }

            $scope.getTotal = function (item, index) {
                var total = parseFloat(item.price.replace(".", "").replace(",", "") * 1).toFixed(2) / 100 * parseFloat(item.quantity);

                $scope.sale.itens[index].total = total;

                return total.toFixed(2).toString().replace(".", ",");
            }
        },
        templateUrl: "/app/views/sale/invoice.html"
    });

    $routeProvider.when("/sale", { controller: "saleController", templateUrl: "/app/views/sale/index.html" });
    $routeProvider.when("/sale/:code", { controller: "saleController", templateUrl: "/app/views/sale/index.html" });

    $routeProvider.when("/dispatch", { controller: "dispatchController", templateUrl: "/app/views/dispatch/index.html" });
    $routeProvider.when("/dispatch/:code", { controller: "dispatchController", templateUrl: "/app/views/dispatch/index.html" });

    $routeProvider.when("/category", { controller: "categoryController", templateUrl: "/app/views/category/index.html" });
    $routeProvider.when("/category/:code", { controller: "categoryController", templateUrl: "/app/views/category/index.html" });
    $routeProvider.when("/categories", { controller: "categoryController", templateUrl: "/app/views/category/list.html" });

    $routeProvider.when("/style", { controller: "styleController", templateUrl: "/app/views/style/index.html" });
    $routeProvider.when("/style/:code", { controller: "styleController", templateUrl: "/app/views/style/index.html" });
    $routeProvider.when("/styles", { controller: "styleController", templateUrl: "/app/views/style/list.html" });

    $routeProvider.when("/brand", { controller: "brandController", templateUrl: "/app/views/brand/index.html" });
    $routeProvider.when("/brand/:code", { controller: "brandController", templateUrl: "/app/views/brand/index.html" });
    $routeProvider.when("/brandies", { controller: "brandController", templateUrl: "/app/views/brand/list.html" });

    $routeProvider.when("/customer", { controller: "customerController", templateUrl: "/app/views/customer/index.html" });
    $routeProvider.when("/customer/:code", { controller: "customerController", templateUrl: "/app/views/customer/index.html" });
    $routeProvider.when("/customers", { controller: "customerController", templateUrl: "/app/views/customer/list.html" });

    $routeProvider.when("/supplier", { controller: "supplierController", templateUrl: "/app/views/supplier/index.html" });
    $routeProvider.when("/supplier/:code", { controller: "supplierController", templateUrl: "/app/views/supplier/index.html" });
    $routeProvider.when("/suppliers", { controller: "supplierController", templateUrl: "/app/views/supplier/list.html" });

    $routeProvider.when("/product", { controller: "productController", templateUrl: "/app/views/product/index.html" });
    $routeProvider.when("/product/:code", { controller: "productController", templateUrl: "/app/views/product/index.html" });
    $routeProvider.when("/products", { controller: "productController", templateUrl: "/app/views/product/list.html" });

    $routeProvider.when("/tokens", { controller: "tokensManagerController", templateUrl: "/app/views/tokens.html" });

    $routeProvider.when("/login", { controller: "loginController", templateUrl: "/app/views/login.html" });

    $routeProvider.otherwise('/login');
});

var serviceBase = 'http://sevicelamariee.azurewebsites.net/';
//var serviceBase = 'http://localhost:60629/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

var config = {
    baseUrl: 'http://localhost:65368/',
    baseRoute: window.location.href
};

angular.element('#loading').remove();