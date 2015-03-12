'use strict';

$(document).ready(function () {

    if ($('#idProduto').val() !== undefined) {
        pesquisarCategorias();
        pesquisarFornecedores();
        pesquisarMarcas();
    }

    if ($('#idProduto').val() !== undefined && $('#idProduto').val() !== '') {
        obter();
    } else if ($('#template-listagem').val() !== undefined) {
        pesquisar();
    }
});

function pesquisarCategorias() {

    $.ajax({
        type: "GET",
        data: null,
        url: "http://localhost:60341/api/v1/public/categories/",
        contentType: "application/json"
    }).success(function (data) {

        $('#Category').empty();

        $('#Category').append($('<option/>').attr("value", "").text("-- Escolha uma categoria --"));

        $.each(data, function (i, option) {
            $('#Category').append($('<option/>').attr("value", option.id).text(option.title));
        });
    });
}

function pesquisarFornecedores() {

    $.ajax({
        type: "GET",
        data: null,
        url: "http://localhost:60341/api/v1/public/suppliers/",
        contentType: "application/json"
    }).success(function (data) {

        $('#Supplier').empty();

        $('#Supplier').append($('<option/>').attr("value", "").text("-- Escolha um fornecedor --"));

        $.each(data, function (i, option) {
            $('#Supplier').append($('<option/>').attr("value", option.id).text(option.nome));
        });
    });
}

function pesquisarMarcas() {

    $.ajax({
        type: "GET",
        data: null,
        url: "http://localhost:60341/api/v1/public/brandies/",
        contentType: "application/json"
    }).success(function (data) {

        $('#Brand').empty();

        $('#Brand').append($('<option/>').attr("value", "").text("-- Escolha uma marca --"));

        $.each(data, function (i, option) {
            $('#Brand').append($('<option/>').attr("value", option.id).text(option.title));
        });
    });
}

$('#resetar').click(function () { $('#idProduto').val(''); });

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

    var product = {
        Id: $('#idProduto').val(),
        Title: $('#Title').val(),
        Description: $('#Description').val(),
        Style: $('#Style').val(),
        Size: $('#Size').val(),
        Measure: $('#Measure').val(),
        Color: $('#Color').val(),
        Model: $('#Model').val(),
        BrandId: $('#Brand').val(),
        SupplierId: $('#Supplier').val(),
        CategoryId: $('#Category').val(),
        Quantity: $('#Quantity').val(),
        Cost: $('#Cost').val(),
        Price: $('#Price').val()
    }

    if ($('#idProduto').val() == '') { salvar(product); } else { atualizar(product); }
});

function salvar(product) {
    $.ajax({
        type: "POST",
        data: JSON.stringify(product),
        url: "http://localhost:60341/api/v1/public/product/",
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

function atualizar(product) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(product),
        url: "http://localhost:60341/api/v1/public/product/",
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
        url: "http://localhost:60341/api/v1/public/products/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Title}}", element.title)
                    .replace("{{Cost}}", element.cost)
                    .replace("{{Price}}", element.price)
                    .replace("{{Quantity}}", element.quantity == 1 ? '1 <span class="red ace-icon fa fa-flag bigger"></span>' : element.quantity)
                    .replace("{{Total}}", element.price * element.quantity)
                    .replace("{{Edicao}}", element.id).replace("{{Exclusao}}", element.id)

            $("#corpo").prepend(html);
        });
    });
}

function obter() {

    $.ajax({
        type: "GET",
        url: "http://localhost:60341/api/v1/public/products/" + $('#idProduto').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: "http://localhost:60341/api/v1/public/product/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
    });
}

function preencherFormulario(dados) {

    $('#idProduto').val(dados.id);
    $('#Title').val(dados.title);
    $('#Description').val(dados.description);
    $('#Style').val(dados.style);
    $('#Size').val(dados.size);
    $('#Measure').val(dados.measure);
    $('#Color').val(dados.color);
    $('#Model').val(dados.model);
    $('#Brand').val(dados.brandId);
    $('#Supplier').val(dados.supplierId);
    $('#Category').val(dados.categoryId);

    $('#Quantity').val(dados.quantity);
    $('#Cost').val(dados.cost);
    $('#Price').val(dados.price);
}