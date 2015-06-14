'use strict';

app.controller('saleController', function ($scope, gostoFactory, dateFilter, config) {

    $('#sale-amount').maskMoney({ thousands: '.', decimal: ',' });
    $('#sale-discount').maskMoney({ thousands: '.', decimal: ',' });
    $('#sale-total').maskMoney({ thousands: '.', decimal: ',' });

    $("#listaCliente").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $("#spinner-customer").removeClass('hide');
            $.getJSON(config.baseServiceUrl + "/customers/" + request.term + '/' + 10,
                function (data) {
                    var array = data.error ? [] : $.map(data, function (m) {
                        return {
                            label: m.nome,
                            nome: m.nome,
                            cpf: m.cpf,
                            telefone: m.dddCelular + '-' + m.celular,
                            email: m.email,
                            id: m.id,
                        };
                    });
                    response(array);
                }).complete(function () {
                    $("#spinner-customer").addClass('hide');
                });
        },
        focus: function (event, ui) {
            event.preventDefault();
        },
        select: function (event, ui) {

            event.preventDefault();

            $scope.sale.customerId = ui.item.id;

            $("#id-customer").val(ui.item.id);
            $("#listaCliente").val(ui.item.nome);
            $("#customer-cpf").text(ui.item.cpf != null ? 'CPF: ' + ui.item.cpf : '');
            $("#customer-telefone").text(ui.item.telefone != 'null-null' ? 'Celular: ' + ui.item.telefone : '');
            $("#customer-email").text(ui.item.email != null ? 'Email: ' + ui.item.email : '');
        }
    });

    $('#sale-discount').on('change', function () {
        calcularTotal();
    });

    $scope.sale = {};
    $scope.sale.itens = [];
    $scope.sale.customerId = 0;
    $scope.sale.payment = 1;
    $scope.sale.userId = 1;

    $scope.sale.date = dateFilter(new Date(), 'dd/MM/yyyy');

    var novaData = new Date();
    novaData.setDate(novaData.getDate() + 7);
    $scope.sale.dateDispatch = dateFilter(novaData, 'dd/MM/yyyy');

    var product = { productId: '', price: '', quantity: '' };

    $scope.sale.itens.push(product);

    criarProduto();

    $scope.novoProduto = function () {

        var product = { productId: '', price: '', quantity: '' };

        $scope.sale.itens.push(product);

        criarProduto();
    }

    function calcularTotal() {
        var amount = 0;
        var quantity = 0;

        $.each($scope.sale.itens, function (index, produto) {
            var quantidade_produto = $('#quantidade-produto-' + index).val();
            amount += parseFloat($('#preco-produto-' + index).val().replace(".", "").replace(",", "") * 1).toFixed(2) / 100 * parseFloat(quantidade_produto);
            quantity += parseInt(quantidade_produto);
        });

        $('#sale-amount').val(amount);
        $('#sale-quantity').val(quantity);

        var discount = parseFloat($('#sale-discount').val().replace(".", "").replace(",", "") * 1).toFixed(2) / 100;
        var totalVenda = discount == '' ? parseFloat(amount) : parseFloat(amount) - discount;

        $('#sale-total').val(parseFloat(totalVenda));
    }

    function criarProduto() {

        var template = $("#template-produtos").clone().html();

        var indice = $scope.sale.itens.length - 1;

        var html = template.replace("||indice-id||", indice)
                           .replace("||indice-id-model||", indice)
                           .replace("||indice-nome||", indice)
                           .replace("||indice-label-nome||", indice)
                           .replace("||indice-nome-model||", indice)
                           .replace("||indice-nome-id||", indice)
                           .replace("||indice-price||", indice)
                           .replace("||indice-label-price||", indice)
                           .replace("||indice-price-model||", indice)
                           .replace("||indice-price-id||", indice)
                           .replace("||indice-quantity||", indice)
                           .replace("||indice-label-quantity||", indice)
                           .replace("||indice-quantity-model||", indice)
                           .replace("||indice-quantity-id||", indice)
                           .replace("||indice-spinner||", indice)
                           .replace("||indice-excluir||", indice)
                           .replace("||exibe-botao||", indice == 0 ? 'hide' : '')

        $("#tag-produto").append(html);

        $("#nome-produto-" + indice).bind("autocomplete", addAautoCompleteEvent(indice));

        $("#preco-produto-" + indice).bind("change", function () {

            $scope.sale.itens[indice].price = $(this).val();

            calcularTotal();
        });

        $("#preco-produto-" + indice).bind("maskMoney", $("#preco-produto-" + indice).maskMoney({ thousands: '.', decimal: ',' }));

        $("#quantidade-produto-" + indice).bind("change", function () {

            $scope.sale.itens[indice].quantity = $(this).val();

            calcularTotal();
        });

        $("#excluir-produto-" + indice).bind("click", function (event) {

            //remover o objeto no DOM
            $scope.sale.itens.splice(indice, 1);

            //remover o dom da tela
            $(this).closest('#widget-produto').fadeOut("slow");
            $(this).closest('#widget-produto').remove();

            //recalcular o valor total
            calcularTotal();
        });
    }

    function addAautoCompleteEvent(indice) {

        $("#nome-produto-" + indice).autocomplete({
            minLength: 2,
            source: function (request, response) {
                $("#spinner-" + indice).removeClass('hide');

                $.getJSON(config.baseServiceUrl + "/products/" + request.term + '/' + request.term + '/' + 10,
                    function (data) {
                        var array = data.error ? [] : $.map(data, function (m) {
                            return {
                                label: m.code + ' - ' + m.title + ' R$ ' + m.price,
                                price: m.price,
                                id: m.id,
                                title: m.title
                            };
                        });
                        response(array);
                    }).complete(function () {
                        $("#spinner-" + indice).addClass('hide');
                    });
            },
            focus: function (event, ui) {

                event.preventDefault();
            },
            select: function (event, ui) {

                event.preventDefault();

                $scope.sale.itens[indice].productId = ui.item.id;
                $scope.sale.itens[indice].title = ui.item.title;
                $scope.sale.itens[indice].price = ui.item.price;
                $scope.sale.itens[indice].quantity = 1;

                $("#id-produto-" + indice).val(ui.item.id);
                $("#nome-produto-" + indice).val(ui.item.title);
                $("#preco-produto-" + indice).val(ui.item.price);
                $("#quantidade-produto-" + indice).val(1);

                calcularTotal();
            }
        });
    }

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formVenda.$invalid) {
            return;
        }

        $scope.processando = true;
        $('#salvar').text('').prepend('Processando ...');

        var id = $scope.sale.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    function inserir() {

        var sale = $scope.sale;
        sale.amount = parseFloat($('#sale-amount').val());
        sale.quantity = $('#sale-quantity').val();
        sale.total = parseFloat($('#sale-total').val());

        gostoFactory.inserirVenda(sale)
            .success(function (data) {
                mensagem('Processando...', data.response.mensagem + ' AGUARDE...', 'sucesso');
                setTimeout(function () {
                    $scope.$apply();
                    $('#loading-nota-fiscal').removeClass('hide');
                }, 4000);

                setTimeout(function () {
                    $scope.processando = false;
                    $scope.$apply();
                    window.location = '#/invoice/' + data.id;
                }, 8000);

            }).error(function (error) {
                $scope.processando = false;
                mensagem('Aconteceu algum Erro', error, 'erro');
                $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
            });
    };

    function atualizar() {

        var sale = $scope.sale;
        gostoFactory.atualizarVenda(sale)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });
    };
});