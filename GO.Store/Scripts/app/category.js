'use strict';

$(document).ready(function () {

    if ($('#idCategoria').val() !== undefined && $('#idCategoria').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

$('#resetar').click(function () {
    $('#idCategoria').val('');
});

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

    var category = {
        Id: $('#idCategoria').val(),
        Title: $('#Title').val()
    }

    if ($('#idCategoria').val() == '') { salvar(category); } else { atualizar(category); }
});

function salvar(category) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(category),
        url: PathService + "/api/v1/public/category/",
        contentType: "application/json"
    }).success(function (data) {
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
        $('#idCategoria').val(data.id);
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function atualizar(category) {

    $.ajax({
        type: "PUT",
        data: JSON.stringify(category),
        url: PathService + "/api/v1/public/category/",
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
        url: PathService + "/api/v1/public/categories/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            console.log(element);

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
        url: PathService + "/api/v1/public/categories/" + $('#idCategoria').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: PathService + "/api/v1/public/category/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Oooooops', data.responseText, 'erro');
    });
}

function preencherFormulario(dados) {
    $('#idCategoria').val(dados.id);
    $('#Title').val(dados.title);
}