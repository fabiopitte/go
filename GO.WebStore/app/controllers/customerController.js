'use strict';

app.controller('customerController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    $scope.customer = {};
    $scope.customers = {};
    $scope.endereco = {};
    $scope.customer.tipoPessoa = 0;
    $scope.loading = false;

    $scope.totalRegistros = 0;
    $scope.urlEndereco = '/app/views/endereco.html';

    if ($routeParams.code != null && window.customer != null) {
        $scope.customer = window.customer;
        $scope.endereco = $scope.customer.endereco;
        window.customer = null;
        window.endereco = null;
    }
    else {
        $scope.urlModal = "/app/views/modalExclusao.html";
        window.customer = null;
        window.endereco = null;
    }

    $scope.pesquisar = function () {
        $scope.loading = true;
        gostoFactory.pesquisarClientes()
            .success(function (data) {
                $scope.customers = data;
                $scope.totalRegistros = data.length;
                $scope.loading = false;
            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    };

    $scope.salvar = function () {

        $scope.$broadcast('show-errors-event');

        if ($scope.formCliente.$invalid) {
            return;
        }

        $scope.customer.endereco = $scope.endereco;

        var id = $scope.customer.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    $scope.editar = function (item) {
        $scope.customer = item;
        window.customer = $scope.customer;
        $scope.endereco = item.endereco;
    };

    $scope.excluir = function (item) {

        $scope.customer = item;

        $scope.CorpoMensagem = "Deseja mesmo excluir o cliente " + item.nome + "?";
        $scope.BtnOk = "Excluir";
        $scope.BtnCancelar = "Cancelar";
        $('#mensagem').modal('show');
    };

    $scope.clickExcluir = function () {

        var customerId = $scope.customer.id;

        gostoFactory.excluirCliente(customerId)
            .success(function (data) {

                $scope.customers.splice($scope.index, 1);
                $scope.totalRegistros = $scope.customers.length;

                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

                $('#mensagem').modal('hide');

            }).error(function (error) {
                mensagem('Aconteceu algum erro', error, 'erro');
            });
    };

    $scope.resetar = function () {
        $scope.customer = null;
        window.customer = null;
    }

    function inserir() {

        var cust = $scope.customer;
        gostoFactory.inserirCliente(cust)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function atualizar() {

        var cust = $scope.customer;

        gostoFactory.atualizarCliente(cust)
            .success(function (data) {
                mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };
}]);