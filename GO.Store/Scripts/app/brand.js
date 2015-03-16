﻿'use strict';

$(document).ready(function () {

    if ($('#idMarca').val() !== undefined && $('#idMarca').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

$('#resetar').click(function () { $('#idMarca').val(''); });

$('#salvar').click(function () {

    if ($('#Title').val() == '') {
        $('#Title').closest('.form-group').addClass('has-error');
        $('#Title').focus();
        return;
    }
    else {
        $('#Title').closest('.form-group').removeClass('has-error');
    }

    $(this).text('aguarde...');

    var brand = {
        Id: $('#idMarca').val(),
        Title: $('#Title').val()
    }

    if ($('#idMarca').val() == '') { salvar(brand); } else { atualizar(brand); }
});

function salvar(brand) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(brand),
        url: PathService + "/api/v1/public/brand/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idMarca').val(data.id);
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function atualizar(brand) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(brand),
        url: PathService + "/api/v1/public/brand/",
        contentType: "application/json"
    }).success(function (data) {
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function pesquisar() {

    $('#load').removeClass('hide');

    $.ajax({
        type: "GET",
        data: null,
        url: PathService + "/api/v1/public/brandies/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Title}}", element.title)
                    .replace("{{Edicao}}", element.id).replace("{{Exclusao}}", element.id)
                    .replace("{{Edicao-m}}", element.id).replace("{{Exclusao-m}}", element.id)

            $("#corpo").prepend(html);
        });
    }).complete(function () {
        $('#load').addClass('hide');
    });
}

function obter() {

    $.ajax({
        type: "GET",
        url: PathService + "/api/v1/public/brandies/" + $('#idMarca').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: PathService + "/api/v1/public/brand/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
    }).error(function (data) {
        mensagem('Oooooops', data.responseText, 'erro');
    });
}

function preencherFormulario(dados) {
    $('#idMarca').val(dados.id);
    $('#Title').val(dados.title);
}