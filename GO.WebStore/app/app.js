var app = angular.module('gostoWebStore', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'chart.js', 'ui.bootstrap']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", { controller: 'dashboardController', templateUrl: "/app/views/dashboard/index.html" });

    $routeProvider.when("/dashboard", { controller: 'dashboardController', templateUrl: "/app/views/dashboard/index.html" });

    $routeProvider.when("/user", { controller: 'userController', templateUrl: "/app/views/user/index.html" });

    $routeProvider.when("/sale", { controller: "saleController", templateUrl: "/app/views/sale/index.html" });

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

    //$routeProvider.otherwise(function () {
    //    return window.location.href = "/login.html";
    //});

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