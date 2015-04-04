'use strict';

app.controller('saleController', function ($scope, gostoFactory, dateFilter) {

    $("#customer").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.getJSON("http://localhost:60629/api/v1/public/customers", {
                q: request.term,
                page_limit: 10
            }, function (data) {
                var array = data.error ? [] : $.map(data, function (m) {
                    return {
                        label: m.nome,
                        nome: m.nome,
                        id: m.id,
                    };
                });
                response(array);
            });
        },
        focus: function (event, ui) {
            event.preventDefault();
        },
        select: function (event, ui) {

            console.log(ui.item);

            event.preventDefault();
            $("#id-customer").val(ui.item.id);
            $("#customer").val(ui.item.nome);
        }
    });

    $('.valor-produtos, .quantidade-produto, #sale-discount').on('change', function () {
        calcularTotal();
    });

    $scope.sale = {};
    $scope.sale.product = [];
    $scope.sale.pay = 1;
    $scope.sale.dataVenda = dateFilter(new Date(), 'dd/MM/yyyy');

    var product = { id: '', price: '', quantity: '', excluir: false };

    $scope.sale.product.push(product);

    criarProduto();

    $scope.novoProduto = function () {

        var product = { id: '', price: '', quantity: '', excluir: true };

        $scope.sale.product.push(product);

        criarProduto();
    }

    function calcularTotal() {

        var amount = 0;
        var quantity = 0;

        $.each($scope.sale.product, function (index, produto) {
            //obtem o valor de cada produto vezes a quantidade
            var preco_produto = $('#preco-produto-' + index).val();
            var quantidade_produto = $('#quantidade-produto-' + index).val();

            amount += preco_produto * quantidade_produto;
            quantity += parseInt(quantidade_produto);
        });

        $('#sale-amount').val(amount);
        $('#sale-quantity').val(quantity);

        var discount = $('#sale-discount').val();
        var totalVenda = parseInt(amount - discount);
        $('#sale-total').val(totalVenda);
    }

    function criarProduto() {

        var template = $("#template-produtos").clone().html();

        var indice = $scope.sale.product.length - 1;

        var html = template.replace("||indice-id||", indice)
                           .replace("||indice-id-model||", indice)
                           .replace("||indice-nome||", indice)
                           .replace("||indice-nome-model||", indice)
                           .replace("||indice-nome-id||", indice)
                           .replace("||indice-price||", indice)
                           .replace("||indice-price-model||", indice)
                           .replace("||indice-price-id||", indice)
                           .replace("||indice-quantity||", indice)
                           .replace("||indice-quantity-model||", indice)
                           .replace("||indice-quantity-id||", indice)
                           .replace("||indice-excluir||", indice)

        $("#tag-produto").append(html);

        $("#nome-produto-" + indice).bind("autocomplete", addAautoCompleteEvent(indice));
        $("#preco-produto-" + indice).bind("change", calcularTotal);
        $("#quantidade-produto-" + indice).bind("change", calcularTotal);
    }

    function addAautoCompleteEvent(indice) {

        $("#nome-produto-" + indice).autocomplete({
            minLength: 2,
            source: function (request, response) {
                $.getJSON("http://localhost:60629/api/v1/public/products", {
                    q: request.term,
                    page_limit: 10
                }, function (data) {
                    var array = data.error ? [] : $.map(data, function (m) {
                        return {
                            label: '(' + m.code + ') ' + m.title + ' R$ ' + m.price,
                            price: m.price,
                            id: m.id,
                            title: m.title
                        };
                    });
                    response(array);
                });
            },
            focus: function (event, ui) {

                event.preventDefault();
            },
            select: function (event, ui) {

                event.preventDefault();

                $("#id-produto-" + indice).val(ui.item.id);
                $("#nome-produto-" + indice).val(ui.item.title);
                $("#preco-produto-" + indice).val(ui.item.price);
                $("#quantidade-produto-" + indice).val(1);

                calcularTotal();
            }
        });
    }
});