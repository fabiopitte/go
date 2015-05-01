'use strict';

app.controller("dispatchController", function ($scope, dateFilter, gostoFactory) {

    $scope.dataDevolucao = dateFilter(new Date(), 'dd/MM/yyyy');

    //select/deselect all rows according to table header checkbox
    var active_class = 'active';
    $('#simple-table > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
        var th_checked = this.checked;//checkbox inside "TH" table header

        $(this).closest('table').find('tbody > tr').each(function () {
            var row = this;
            if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
            else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
        });
    });

    //select/deselect a row when the checkbox is checked/unchecked
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
                            cpf: m.cnpj,
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
            $("#customer-cpf").text('CPF: ' + ui.item.cpf);
            $("#customer-telefone").text('Celular: ' + ui.item.telefone);
            $("#customer-email").text('Email: ' + ui.item.email);

            pesquisar();
        }
    });

    function pesquisar() {
        $scope.loading = true;
        gostoFactory.pesquisarProdutos()
            .success(function (data) {
                $scope.products = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

});