'use strict';

$(document).ready(function () {

    $('#btn-logar').on('click', function () {

        var data = { grant_type: 'password', username: $('#Login').val(), password: $('#Password').val() };

        $.ajax({
            url: PathService + '/api/security/token',
            type: 'post',
            crossDomain : true,
            contentType: "application/x-www-form-urlencoded",
            data: data
        }).done(function (data) {
            localStorage.setItem('token', data.access_token);
            window.location = '/DashBoard/Index';
        }).error(function (data) {
            console.log(data.responseText);
            $('#mensagem').text('Login/Senha inválido');
        });
    });

    $('#btn-request').on('click', function () {
        var token = localStorage.getItem('token');

        $.ajax({
            type: "GET",
            url: PathService + "api/account/values",
            contentType: "application/json",
            headers: { "Authorization": "Bearer " + token }
        }).done(function (data) {
            console.log(data);
        }).error(function (data) {
            console.error('falha na requisição');
        });
    });
});