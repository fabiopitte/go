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

function abrirModalUpload() {
    $('#upload').modal('show');
}

function abrirModalGaleria() {
    $('#galeria').modal('show');
}

function pesquisarCategorias() {

    $.ajax({
        type: "GET",
        data: null,
        url: PathService + "/api/v1/public/categories/",
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
        url: PathService + "/api/v1/public/suppliers/",
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
        url: PathService + "/api/v1/public/brandies/",
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
        url: PathService + "/api/v1/public/product/",
        contentType: "application/json"
    }).success(function (data) {
        $('#idProduto').val(data.id);
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Erro no cadastro', data.responseText, 'erro');
    }).complete(function () {
        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    });
}

function atualizar(product) {
    $.ajax({
        type: "PUT",
        data: JSON.stringify(product),
        url: PathService + "/api/v1/public/product/",
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
        url: PathService + "/api/v1/public/products/",
        contentType: "application/json"
    }).success(function (data) {

        var template = $("#template-listagem").clone().html();

        $(data).each(function (index, element) {

            var html =
                template
                    .replace("{{Quantity}}", element.quantity == 1 ? '<strong class="red">1</strong> <span class="red ace-icon fa fa-flag bigger"></span>' : element.quantity)
                    .replace("{{Title}}", element.title)
                    .replace("{{Brand}}", element.brand.title)
                    .replace("{{Price}}", element.price)
                    .replace("{{Cost}}", element.cost)
                    .replace("{{Product}}", element.id).replace("{{Product-m}}", element.id)
                    .replace("{{Out}}", element.id).replace("{{Out-m}}", element.id)
                    .replace("{{Edit}}", element.id).replace("{{Edit-m}}", element.id)
            $("#corpo").prepend(html);
        });
    }).complete(function () {
        $('#load').addClass('hide');
    });
}

function obter() {

    $.ajax({
        type: "GET",
        url: PathService + "/api/v1/public/products/" + $('#idProduto').val(),
        contentType: "application/json"
    }).success(function (data) {
        preencherFormulario(data);
    });
}

function excluir(obj) {

    var id = $(obj).data('id');

    $.ajax({
        type: "DELETE",
        url: PathService + "/api/v1/public/product/" + id,
        contentType: "application/json"
    }).success(function (data) {
        $(obj).closest('tr').remove();
        mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');
    }).error(function (data) {
        mensagem('Oooooops', data.responseText, 'erro');
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