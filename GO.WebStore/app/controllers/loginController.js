'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    $scope.message = "";

    $scope.login = function () {

        if ($scope.loginForm.$valid) {

            authService.login($scope.user).then(function (response) {

                $scope.token = response.access_token

                window.location.href = config.baseUrl;
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        }
    };

    $scope.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.authCompletedCB = function (fragment) {

        $scope.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                    $location.path('/orders');

                },
             function (err) {
                 $scope.message = err.error_description;
             });
            }

        });
    }
}]);

//angular.module('gostoWebStoreLogin', ['ngRoute', 'ngCookies','LocalStorageModule', 'angular-loading-bar', 'chart.js', 'ui.bootstrap'])
//    .factory('loginFactory', ['$http',
//  function ($http) {
//      var urlBase = 'http://localhost:60629/api/v1/public';

//      return {
//          postLogin: function (user) {
//              return $http.post(urlBase + "/login", user);
//          }
//      };
//  }]).controller('loginController', ['$scope', '$cookies', '$location', 'loginFactory', function ($scope, $cookies, $location, loginFactory) {
//      $scope.user = {};
//      $scope.executandoLogin = false;
//      $scope.loginInvalido = false;

//      $scope.login = function () {

//          $scope.executandoLogin = true;
//          loginFactory.postLogin($scope.user)
//            .success(function (result, status) {

//                if (status == 200) {

//                    $cookies.usuario = result;
//                    var returnUrl = $location.search().ReturnUrl;
//                    if (returnUrl) {
//                        window.location.href = config.baseUrl.replace(config.baseRoute, '') + $location.search().ReturnUrl;
//                    } else {
//                        window.location.href = config.baseUrl;
//                    }
//                } else {
//                    $scope.executandoLogin = false;
//                    delete $scope.user.senha;
//                    angular.element('#Login').focus();
//                    $scope.loginInvalido = true;
//                }
//            })
//            .error(function (request, status, headers, config) {
//                $scope.executandoLogin = false;
//                $scope.loginInvalido = true;
//                angular.element('#Login').focus();
//                $scope.message = request;
//                console.error(status + ", " + request);
//            });
//      };
//      $scope.$watch(
//        'usuario.login',
//        function (newValue, oldValue) {
//            $scope.loginInvalido = false;
//        });

//      angular.element('#loading').remove();
//  }]);