var app = angular.module('gostoWebStore', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", { controller: "homeController", templateUrl: "/app/views/home.html" });

    $routeProvider.when("/sale", { controller: "saleController", templateUrl: "/app/views/sale.html" });

    $routeProvider.when("/category", { controller: "categoryController", templateUrl: "/app/views/category/index.html" });
    $routeProvider.when("/category/:code", { controller: "categoryController", templateUrl: "/app/views/category/index.html" });
    $routeProvider.when("/categories", { controller: "categoryController", templateUrl: "/app/views/category/list.html" });

    $routeProvider.when("/brand", { controller: "brandController", templateUrl: "/app/views/brand/index.html" });
    $routeProvider.when("/brand/:code", { controller: "brandController", templateUrl: "/app/views/brand/index.html" });
    $routeProvider.when("/brandies", { controller: "brandController", templateUrl: "/app/views/brand/list.html" });

    $routeProvider.when("/supplier", { controller: "supplierController", templateUrl: "/app/views/supplier/index.html" });
    $routeProvider.when("/supplier/:code", { controller: "supplierController", templateUrl: "/app/views/supplier/index.html" });
    $routeProvider.when("/suppliers", { controller: "supplierController", templateUrl: "/app/views/supplier/list.html" });

    $routeProvider.when("/customer/list", { controller: "customerController", templateUrl: "/app/views/customer.html" });

    $routeProvider.when("/customer/index", { controller: "customerController", templateUrl: "/app/views/customer/index.html" });

    $routeProvider.when("/product/list", { controller: "productController", templateUrl: "/app/views/product.html" });

    $routeProvider.when("/product/index", { controller: "productController", templateUrl: "/app/views/product/index.html" });

    $routeProvider.when("/tokens", { controller: "tokensManagerController", templateUrl: "/app/views/tokens.html" });

    $routeProvider.otherwise({ redirectTo: "/" });

    //$locationProvider.html5Mode(true);
});

var serviceBase = 'http://localhost:60629/';
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