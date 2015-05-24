'use strict';

app.controller("dispatchController", function ($scope, dateFilter, gostoFactory) {

    $scope.dataDevolucao = dateFilter(new Date(), 'dd/MM/yyyy');

    var active_class = 'active';
    $('#simple-table > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
        var th_checked = this.checked;

        $(this).closest('table').find('tbody > tr').each(function () {
            var row = this;
            if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
            else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
        });
    });

    $('#simple-table').on('click', 'td input[type=checkbox]', function () {
        var $row = $(this).closest('tr');
        if (this.checked) $row.addClass(active_class);
        else $row.removeClass(active_class);
    });

    $("#customer").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $("#spinner-customer").removeClass('hide');
            $.getJSON("http://localhost:60629/api/v1/public/customers/" + request.term + '/' + 10,
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

            $("#id-customer").val(ui.item.id);
            $("#customer").val(ui.item.nome);
            $("#customer-cpf").text(ui.item.cpf != null ? 'CPF: ' + ui.item.cpf : '');
            $("#customer-telefone").text(ui.item.telefone != 'null-null' ? 'Celular: ' + ui.item.telefone : '');
            $("#customer-email").text(ui.item.email != null ? 'Email: ' + ui.item.email : '');

            pesquisarProdutosDoCliente(ui.item.id);
            $scope.customerId = ui.item.id;
        }
    });

    function pesquisarProdutosDoCliente(customerId) {
        $scope.loading = true;
        gostoFactory.pesquisarProdutosDoCliente(customerId)
            .success(function (data) {
                $scope.sale = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function (dispatch) {
        $scope.processando = true;
        $('#salvar').text('').prepend('Processando ...');
        inserir(dispatch);
    };

    function inserir(dispatch) {

        var sale = [];

        angular.forEach(dispatch, function (key, value) {

            if (key.checkado !== undefined && key.checkado !== false) {
                sale.push({
                    Id: key.id,
                    itens: [{
                        id: key.itens[0].id,
                        productId: key.itens[0].productId,
                        quantity: key.itens[0].quantity
                    }]
                });
            }
        });

        if (sale[0] !== undefined) {
            gostoFactory.realizarDevolucao(sale)
            .success(function (data) {
                mensagem('Parabens', data.response.mensagem, 'sucesso');

                pesquisarProdutosDoCliente($scope.customerId);

            }).error(function (error) {
                $scope.processando = false;
                mensagem('Aconteceu algum Erro', error, 'erro');
                $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
            });
        } else {
            $scope.processando = false;
            mensagem('Oooops. Faltou alguma coisa hein', 'Acho que voce esta esquecendo de algo, o que acha de checar o item que deseja realizar a baixa?', 'error');
            $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
        }
    };
});