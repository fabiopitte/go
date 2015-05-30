'use strict';

app.factory('gostoFactory', ['$http', 'config', function ($http, config) {

    var dataFactory = {};

    //categoria
    dataFactory.pesquisarCategorias = function () {
        return $http.get(config.baseServiceUrl + '/categories');
    };

    dataFactory.inserirCategoria = function (category) {
        return $http.post(config.baseServiceUrl + '/category', JSON.stringify(category));
    }

    dataFactory.atualizarCategoria = function (category) {
        return $http.put(config.baseServiceUrl + '/category', JSON.stringify(category));
    }

    dataFactory.excluirCategoria = function (categoryId) {
        return $http.delete(config.baseServiceUrl + '/category/' + categoryId);
    }

    //marca
    dataFactory.pesquisarMarcas = function () {
        return $http.get(config.baseServiceUrl + '/brandies');
    };

    dataFactory.inserirMarca = function (brand) {
        return $http.post(config.baseServiceUrl + '/brand', JSON.stringify(brand));
    }

    dataFactory.atualizarMarca = function (brand) {
        return $http.put(config.baseServiceUrl + '/brand', JSON.stringify(brand));
    }

    dataFactory.excluirMarca = function (brandId) {
        return $http.delete(config.baseServiceUrl + '/brand/' + brandId);
    }

    //Fornecedor
    dataFactory.pesquisarFornecedores = function () {
        return $http.get(config.baseServiceUrl + '/suppliers');
    };

    dataFactory.inserirFornecedor = function (supplier) {
        return $http.post(config.baseServiceUrl + '/supplier', JSON.stringify(supplier));
    }

    dataFactory.atualizarFornecedor = function (supplier) {
        return $http.put(config.baseServiceUrl + '/supplier', JSON.stringify(supplier));
    }

    dataFactory.excluirFornecedor = function (supplierId) {
        return $http.delete(config.baseServiceUrl + '/supplier/' + supplierId);
    }

    //Cliente
    dataFactory.pesquisarClientes = function () {
        return $http.get(config.baseServiceUrl + '/customers');
    };

    dataFactory.inserirCliente = function (customer) {
        return $http.post(config.baseServiceUrl + '/customer', JSON.stringify(customer));
    }

    dataFactory.atualizarCliente = function (customer) {
        return $http.put(config.baseServiceUrl + '/customer', JSON.stringify(customer));
    }

    dataFactory.excluirCliente = function (customerId) {
        return $http.delete(config.baseServiceUrl + '/customer/' + customerId);
    }

    dataFactory.pesquisarProdutosDoCliente = function (customerId) {
        return $http.get(config.baseServiceUrl + '/customer/products/' + customerId);
    };

    //Produto
    dataFactory.pesquisarProdutos = function () {
        return $http.get(config.baseServiceUrl + '/products');
    };

    dataFactory.inserirProduto = function (product) {

        return $http.post(config.baseServiceUrl + '/product', product);
    }

    dataFactory.atualizarProduto = function (product) {

        return $http.put(config.baseServiceUrl + '/product', product);
    }

    dataFactory.excluirProduto = function (productId) {
        return $http.delete(config.baseServiceUrl + '/product/' + productId);
    }

    dataFactory.excluirPhoto = function (id) {
        return $http.delete(config.baseServiceUrl + '/product/photo/' + id);
    }

    dataFactory.obterFotosDoProduto = function (id) {
        return $http.get(config.baseServiceUrl + '/product/photos/' + id);
    };

    dataFactory.obterFoto = function (url) {
        return $http.get('product/photo/' + url);
    };

    //usuario
    dataFactory.obterUsuario = function (userId) {
        return $http.get(config.baseServiceUrl + '/users/' + userId);
    }

    dataFactory.atualizarUsuario = function (user) {
        return $http.put(config.baseServiceUrl + '/user', JSON.stringify(user));
    }

    //Estilo
    dataFactory.pesquisarEstilos = function () {
        return $http.get(config.baseServiceUrl + '/styles');
    };

    dataFactory.inserirEstilo = function (style) {
        return $http.post(config.baseServiceUrl + '/style', JSON.stringify(style));
    }

    dataFactory.atualizarEstilo = function (style) {
        return $http.put(config.baseServiceUrl + '/style', JSON.stringify(style));
    }

    dataFactory.excluirEstilo = function (styleId) {
        return $http.delete(config.baseServiceUrl + '/style/' + styleId);
    }

    //venda
    dataFactory.pesquisarVendas = function () {
        return $http.get(config.baseServiceUrl + '/sales');
    };

    dataFactory.inserirVenda = function (sale) {
        return $http.post(config.baseServiceUrl + '/sale', JSON.stringify(sale));
    }

    dataFactory.atualizarVenda = function (sale) {
        return $http.put(config.baseServiceUrl + '/sale', JSON.stringify(sale));
    }

    dataFactory.excluirVenda = function (saleId) {
        return $http.delete(config.baseServiceUrl + '/sale/' + saleId);
    }

    dataFactory.obterVenda = function (id) {

        return $http.get(config.baseServiceUrl + '/sale/' + id);
    };

    dataFactory.realizarDevolucao = function (sale) {
        return $http.post(config.baseServiceUrl + '/sale/dispatch', JSON.stringify(sale));
    }

    return dataFactory;
}]);