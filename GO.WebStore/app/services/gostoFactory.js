'use strict';

app.factory('gostoFactory', ['$http', function ($http) {

    var urlBase = 'http://localhost:60629/api/v1/public';
    var dataFactory = {};

    //categoria
    dataFactory.pesquisarCategorias = function () {
        return $http.get(urlBase + '/categories');
    };

    dataFactory.inserirCategoria = function (category) {
        return $http.post(urlBase + '/category', JSON.stringify(category));
    }

    dataFactory.atualizarCategoria = function (category) {
        return $http.put(urlBase + '/category', JSON.stringify(category));
    }

    dataFactory.excluirCategoria = function (categoryId) {
        return $http.delete(urlBase + '/category/' + categoryId);
    }

    //marca
    dataFactory.pesquisarMarcas = function () {
        return $http.get(urlBase + '/brandies');
    };

    dataFactory.inserirMarca = function (brand) {
        return $http.post(urlBase + '/brand', JSON.stringify(brand));
    }

    dataFactory.atualizarMarca = function (brand) {
        return $http.put(urlBase + '/brand', JSON.stringify(brand));
    }

    dataFactory.excluirMarca = function (brandId) {
        return $http.delete(urlBase + '/brand/' + brandId);
    }

    //Fornecedor
    dataFactory.pesquisarFornecedores = function () {
        return $http.get(urlBase + '/suppliers');
    };

    dataFactory.inserirFornecedor = function (supplier) {
        return $http.post(urlBase + '/supplier', JSON.stringify(supplier));
    }

    dataFactory.atualizarFornecedor = function (supplier) {
        return $http.put(urlBase + '/supplier', JSON.stringify(supplier));
    }

    dataFactory.excluirFornecedor = function (supplierId) {
        return $http.delete(urlBase + '/supplier/' + supplierId);
    }

    return dataFactory;
}]);