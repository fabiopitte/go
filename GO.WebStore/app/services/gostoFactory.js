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

    //Cliente
    dataFactory.pesquisarClientes = function () {
        return $http.get(urlBase + '/customers');
    };

    dataFactory.inserirCliente = function (customer) {
        return $http.post(urlBase + '/customer', JSON.stringify(customer));
    }

    dataFactory.atualizarCliente = function (customer) {
        return $http.put(urlBase + '/customer', JSON.stringify(customer));
    }

    dataFactory.excluirCliente = function (customerId) {
        return $http.delete(urlBase + '/customer/' + customerId);
    }

    dataFactory.pesquisarProdutosDoCliente = function (customerId) {
        return $http.get(urlBase + '/customer/products/' + customerId);
    };

    //Produto
    dataFactory.pesquisarProdutos = function () {
        return $http.get(urlBase + '/products');
    };

    dataFactory.inserirProduto = function (product) {

        return $http.post(urlBase + '/product', product);
    }

    dataFactory.atualizarProduto = function (product) {

        return $http.put(urlBase + '/product', product);
    }

    dataFactory.excluirProduto = function (productId) {
        return $http.delete(urlBase + '/product/' + productId);
    }

    dataFactory.excluirPhoto = function (id) {
        return $http.delete(urlBase + '/product/photo/' + id);
    }

    dataFactory.obterFotosDoProduto = function (id) {
        return $http.get(urlBase + '/product/photos/' + id);
    };

    dataFactory.obterFoto = function (url) {
        return $http.get('product/photo/' + url);
    };

    //usuario
    dataFactory.obterUsuario = function (userId) {
        return $http.get(urlBase + '/users/' + userId);
    }

    dataFactory.atualizarUsuario = function (user) {
        return $http.put(urlBase + '/user', JSON.stringify(user));
    }

    //Estilo
    dataFactory.pesquisarEstilos = function () {
        return $http.get(urlBase + '/styles');
    };

    dataFactory.inserirEstilo = function (style) {
        return $http.post(urlBase + '/style', JSON.stringify(style));
    }

    dataFactory.atualizarEstilo = function (style) {
        return $http.put(urlBase + '/style', JSON.stringify(style));
    }

    dataFactory.excluirEstilo = function (styleId) {
        return $http.delete(urlBase + '/style/' + styleId);
    }

    //venda
    dataFactory.pesquisarVendas = function () {
        return $http.get(urlBase + '/sales');
    };

    dataFactory.inserirVenda = function (sale) {
        return $http.post(urlBase + '/sale', JSON.stringify(sale));
    }

    dataFactory.atualizarVenda = function (sale) {
        return $http.put(urlBase + '/sale', JSON.stringify(sale));
    }

    dataFactory.excluirVenda = function (saleId) {
        return $http.delete(urlBase + '/sale/' + saleId);
    }

    dataFactory.obterVenda = function (id) {

        return $http.get(urlBase + '/sale/' + id);
    };

    dataFactory.realizarDevolucao = function (sale) {
        return $http.post(urlBase + '/sale/dispatch', JSON.stringify(sale));
    }

    return dataFactory;
}]);