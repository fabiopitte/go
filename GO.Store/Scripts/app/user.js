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

    $(this).text('aguarde...');

    var user = {
        Id: $('#idUsuario').val(),
        Name: $('#Name').val()
    }

    if ($('#idUsuario').val() == '') { salvar(user); } else { atualizar(user); }
});

function salvar(user) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(user),
        url: "http://localhost:60341/api/v1/public/user/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idUsuario').val(data.id);
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

function atualizar(user) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(user),
        url: "http://localhost:60341/api/v1/public/user/",
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
        url: "http://localhost:60341/api/v1/public/users/",
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
    });
}

function obter() {

    $.ajax({
        type: "GET",
        url: "http://localhost:60341/api/v1/public/users/" + $('#idUsuario').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: "http://localhost:60341/api/v1/public/user/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
    });
}

function preencherFormulario(dados) {
    $('#idUsuario').val(dados.id);
    $('#Name').val(dados.name);
}