'use strict';

$(document).ready(function () {

    if ($('#idUsuario').val() !== undefined && $('#idUsuario').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

$('#resetar').click(function () { $('#idUsuario').val(''); });

$('#salvar').click(function () {

    if ($('#Name').val() == '') {
        $('#Name').closest('.form-group').addClass('has-error');
        $('#Name').focus();
        return;
    }
    else {
        $('#Name').closest('.form-group').removeClass('has-error');
    }

    if ($('#Login').val() == '') {
        $('#Login').closest('.form-group').addClass('has-error');
        $('#Login').focus();
        return;
    }
    else {
        $('#Login').closest('.form-group').removeClass('has-error');
    }

    if ($('#Email').val() == '') {
        $('#Email').closest('.form-group').addClass('has-error');
        $('#Email').focus();
        return;
    }
    else {
        $('#Email').closest('.form-group').removeClass('has-error');
    }

    if ($('#Password').val() !== $('#confirm-password').val()) {
        $('#confirm-password').closest('.form-group').addClass('has-error');
    }
    else {
        $('#confirm-password').closest('.form-group').removeClass('has-error');
    }

    $(this).text('aguarde...');

    var user = {
        Id: $('#idUsuario').val(),
        Name: $('#Name').val(),
        Login: $('#Login').val(),
        Email: $('#Email').val(),
        DDDTelefone: $('#DDDTelefone').val(),
        Telefone: $('#Telefone').val(),
        Password: $('#Password').val()
    }

    if ($('#idUsuario').val() == '') { salvar(user); } else { atualizar(user); }
});

function salvar(user) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(user),
        url: PathService + "/api/v1/public/user/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idUsuario').val(data.id);
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function atualizar(user) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(user),
        url: PathService + "/api/v1/public/user/",
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
        url: PathService + "/api/v1/public/users/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Name}}", element.title)
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
        url: PathService + "/api/v1/public/users/" + $('#idUsuario').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: PathService + "/api/v1/public/user/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Oooooops', data.responseText, 'erro');
    });
}

function preencherFormulario(dados) {
    $('#idUsuario').val(dados.id);
    $('#Name').val(dados.name);
    $('#Login').val(dados.login);
    $('#Email').val(dados.email);
    $('#DDDTelefone').val(dados.dddTelefone);
    $('#Telefone').val(dados.telefone);
}