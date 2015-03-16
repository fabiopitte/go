'use strict';

$(document).ready(function () {

    if ($('#idVenda').val() !== undefined && $('#idVenda').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

$('#resetar').click(function () { $('#idVenda').val(''); });

$('#salvar').click(function () {

    if ($('#Amount').val() == '') {
        $('#Amount').closest('.form-group').addClass('has-error');
        $('#Amount').focus();
        return;
    }
    else {
        $('#Amount').closest('.form-group').removeClass('has-error');
    }

    $(this).text('aguarde...');

    var sale = {
        Id: $('#idVenda').val(),
        Amount: $('#Amount').val()
    }

    if ($('#idVenda').val() == '') { salvar(sale); } else { atualizar(sale); }
});

function salvar(sale) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(sale),
        url: PathService + "/api/v1/public/sale/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idVenda').val(data.id);
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function atualizar(sale) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(sale),
        url: PathService + "/api/v1/public/sale/",
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
        url: PathService + "/api/v1/public/sales/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Amount}}", element.title)
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
        url: PathService + "/api/v1/public/sales/" + $('#idVenda').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: PathService + "/api/v1/public/sale/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
    }).error(function (data) {
        mensagem('Oooooops', data.responseText, 'erro');
    });
}

function preencherFormulario(dados) {
    $('#idVenda').val(dados.id);
    $('#Amount').val(dados.amount);
}