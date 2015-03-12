﻿'use strict';

$(document).ready(function () {

    if ($('#idFornecedor').val() !== undefined && $('#idFornecedor').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

$('#resetar').click(function () { $('#idFornecedor').val(''); });

$('#salvar').click(function () {

    if ($('#Nome').val() == '') {
        $('#Nome').closest('.form-group').addClass('has-error');
        $('#Nome').focus();
        return;
    }
    else {
        $('#Nome').closest('.form-group').removeClass('has-error');
    }

    $(this).text('aguarde...');

    var supplier = {
        Id: $('#idFornecedor').val(),
        Nome: $('#Nome').val(),
        Email: $('#Email').val(),
        DDDTelefone: $('#DDDTelefone').val(),
        Telefone: $('#Telefone').val(),
        DDDCelular: $('#DDDCelular').val(),
        Celular: $('#Celular').val(),
        CNPJ: $('#CNPJ').val(),
        IE: $('#Ie').val(),
        Observacoes: $('#Observacoes').val()
    }

    if ($('#idFornecedor').val() == '') { salvar(supplier); } else { atualizar(supplier); }
});

function salvar(supplier) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(supplier),
        url: "http://localhost:60341/api/v1/public/supplier/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idFornecedor').val(data.id);
        $('.bootbox-body').text(data.response.mensagem);
        $('.modal-body').css('background-color', '#fff').css('color', '#393939');
    }).error(function (data) {
        $('.bootbox-body').text(data.responseText);
        $('.modal-body').css('background-color', 'rgb(187, 62, 62)').css('color', 'white');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
        $('.bootbox').modal('show');
    });
}

function atualizar(supplier) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(supplier),
        url: "http://localhost:60341/api/v1/public/supplier/",
        contentType: "application/json"
    }).success(function (data) {
        $('.bootbox-body').text(data.response.mensagem);
        $('.modal-body').css('background-color', '#fff').css('color', '#393939');
    }).error(function (data) {
        $('.bootbox-body').text(data.responseText);
        $('.modal-body').css('background-color', 'rgb(187, 62, 62)').css('color', 'white');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
        $('.bootbox').modal('show');
    });
}

function pesquisar() {

    $.ajax({
        type: "GET",
        data: null,
        url: "http://localhost:60341/api/v1/public/suppliers/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Nome}}", element.nome)
                    .replace("{{Email}}", element.email)
                    .replace("{{Telefone}}", element.dddTelefone + '-' + element.telefone)
                    .replace("{{Celular}}", element.dddCelular + '-' + element.celular)
                    .replace("{{Edicao}}", element.id).replace("{{Exclusao}}", element.id)
                    .replace("{{Edicao-m}}", element.id).replace("{{Exclusao-m}}", element.id)

            $("#corpo").prepend(html);
        });
    });
}

function obter() {

    $.ajax({
        type: "GET",
        url: "http://localhost:60341/api/v1/public/suppliers/" + $('#idFornecedor').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: "http://localhost:60341/api/v1/public/supplier/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
    });
}

function preencherFormulario(dados) {

    $('#idFornecedor').val(dados.id);
    $('#Nome').val(dados.nome);
    $('#Email').val(dados.email);
    $('#DDDTelefone').val(dados.dddTelefone);
    $('#Telefone').val(dados.telefone);
    $('#DDDCelular').val(dados.dddCelular);
    $('#Celular').val(dados.celular);
    $('#CNPJ').val(dados.cnpj);
    $('#Ie').val(dados.ie);
    $('#Observacoes').val(dados.observacoes);
}