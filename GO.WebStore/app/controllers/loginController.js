﻿'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    $scope.message = "";
    $scope.loginInvalido = false;
    $scope.authentication = {};
    $scope.executandoLogin = false;

    authService.logOut();

    $scope.login = function () {
        $scope.executandoLogin = true;

        if ($scope.loginForm.$valid) {

            authService.login($scope.user).then(function (response) {
                $scope.authentication.isAuth = authService.authentication.isAuth;
                $scope.token = response.access_token;
                $location.path('/products');

                $scope.executandoLogin = false;
            },
             function (err) {
                 $scope.loginInvalido = true;
                 $scope.message = err.error_description;
                 $scope.executandoLogin = false;
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