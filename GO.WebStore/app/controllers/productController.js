'use strict';

app.controller('productController', ['$scope', '$location', '$routeParams', 'gostoFactory', 'config', function ($scope, $location, $routeParams, gostoFactory, config) {

  $scope.urlDropZone = '';
  $scope.dropzoneConfig = null;
  var produtoAterado = false;

  $('#Cost').maskMoney({ thousands: '.', decimal: ',' });
  $('#Price').maskMoney({ thousands: '.', decimal: ',' });

  $('#fuelux-wizard-container')
  .ace_wizard({

  })
  .on('actionclicked.fu.wizard', function (e, info) {
    var formInvalido = true;
    if (info.step == 1) {
      if ($('#Title').val() == '') {
        $('#Title').closest('.form-group').addClass('has-error');
        $('#Title').focus();
        e.preventDefault();
        formInvalido = false;
      }
      else {
        $('#Title').closest('.form-group').removeClass('has-error');
      }

      if ($('#Code').val() == '') {
        $('#Code').closest('.form-group').addClass('has-error');
        $('#Code').focus();
        e.preventDefault();
        formInvalido = false;
      }
      else {
        $('#Code').closest('.form-group').removeClass('has-error');
      }

      if (formInvalido) return;
    }
    else if (info.step == 2) {

      $scope.styles.filter(function (obj) { if (obj.id === $scope.product.styleId) { $('#nome-estilo').text(obj.title); } });
      $scope.categories.filter(function (obj) { if (obj.id === $scope.product.categoryId) { $('#nome-categoria').text(obj.title); } });
      $scope.brandies.filter(function (obj) { if (obj.id === $scope.product.brandId) { $('#nome-marca').text(obj.title); } });
      $scope.suppliers.filter(function (obj) { if (obj.id === $scope.product.supplierId) { $('#nome-fornecedor').text(obj.nome); } });

      salvar();

      $scope.urlDropZone = '/app/views/dropZone.html';

      $scope.dropzoneConfig = {
        'options': {
          'url': function () {
            return config.baseServiceUrl + '/PostFormData/' + sessionStorage.getItem('productID')
          }
        },
        'eventHandlers': {
          'sending': function (file, xhr, formData) {

          },
          'success': function (file, response) {
          }
        }
      };
    }
    else if (info.step == 3) {
      obterFotosDoProduto();
    }
  })
  .on('finished.fu.wizard', function (e) {

    if (!produtoAterado) {
      mensagem('Mensagem de sucesso', 'Ai sim, produto inserido com sucesso!!', 'sucesso');
    }
    else {
      mensagem('Mensagem de sucesso', 'Ai sim, produto alterado com sucesso!!', 'sucesso');
    }

    window.location.href = '#/products';

  }).on('stepclick.fu.wizard', function (e) {
    //e.preventDefault();
  });

  $scope.product = {};
  $scope.products = {};
  $scope.totalRegistros = 0;
  $scope.loading = false;

  function obterFotosDoProduto() {

    var id = $scope.product.id;
    var fotinha = [];

    if (id !== undefined) {
      gostoFactory.obterFotosDoProduto(id)
      .success(function (fotos) {
        angular.forEach(fotos, function (value, key) {
          fotinha.push({ image: value.file, id: value.id });
        });

        $scope.pega = fotinha;

      }).error(function (error) {
        mensagem('Erro ao pesquisar por fotos', error, 'erro');
      });
    }
  }

  function obterFoto(url) {
    gostoFactory.obterFoto(url).success(function (results) {
      return results;
    });
  }

  $scope.pesquisar = function () {
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

  $scope.abrirModalGaleria = function () {
    $('#galeria').modal('show');
  }

  $scope.editar = function (item) {
    $scope.product = item;
    window.product = $scope.product;
  };

  $scope.excluir = function (item) {

    $scope.product = item;

    $scope.CorpoMensagem = "Deseja mesmo excluir o produto " + item.title + "?";
    $scope.BtnOk = "Excluir";
    $scope.BtnCancelar = "Cancelar";
    $('#mensagem').modal('show');
  };

  $scope.openWindow = function (item) {
    window.open(item.url, '_blank');
  }

  $scope.excluirFoto = function (item) {
    angular.forEach($scope.pega, function (ele, i) {
      if (ele.id == item.id) {
        gostoFactory.excluirPhoto(ele.id).success(function (data) {
          $scope.pega.splice($scope.pega.indexOf(ele), 1);
          mensagem('Mensagem de sucesso', 'Foto excluida!', 'sucesso');
        }).error(function (error) {
          mensagem('Aconteceu algum erro', error, 'erro');
        });
      };
    });
  };

  $scope.clickExcluir = function () {

    var productId = $scope.product.id;

    gostoFactory.excluirProduto(productId)
        .success(function (data) {

          $scope.products.splice($scope.products.indexOf($scope.product), 1);

          $scope.totalRegistros = $scope.products.length;

          mensagem('Mensagem de sucesso', data.response.mensagem, 'sucesso');

          $('#mensagem').modal('hide');

        }).error(function (error) {
          mensagem('Aconteceu algum erro', error, 'erro');
        });
  };

  if ($routeParams.code != null && window.product != null) {
    $scope.product = window.product;
  }
  else {
    $scope.urlModal = "/app/views/modalExclusao.html";
    window.product = null;
  }

  if ($location.path() !== '/products') {
    pesquisarEstilos();
    pesquisarCategorias();
    pesquisarFornecedores();
    pesquisarMarcas();
  }

  $scope.resetar = function () {
    $scope.product = null;
    window.product = null;
  }

  function salvar() {

    var id = $scope.product.id;

    if (id == undefined) { id = inserir($scope); } else { atualizar(); }
  };

  function inserir($scope) {

    var cat = $scope.product;

    gostoFactory.inserirProduto(cat)
        .success(function (data) {
          $scope.product.id = data.id;
          sessionStorage.setItem('productID', $scope.product.id);
        }).error(function (error) {
          mensagem('Erro no cadastro', error, 'erro');
        });
  };

  function atualizar() {
    produtoAterado = true;

    var cat = $scope.product;
    sessionStorage.setItem('productID', $scope.product.id);

    gostoFactory.atualizarProduto(cat).error(function (error) {
      mensagem('Erro na atualizacao do produto', error, 'erro');
    });

    $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
  };

  function pesquisarEstilos() {
    gostoFactory.pesquisarEstilos()
        .success(function (data) {
          $scope.styles = data;
        }).error(function (error) {
          $scope.status = 'Aconteceu algum erro ao pesquisar';
        });
  }

  function pesquisarMarcas() {
    gostoFactory.pesquisarMarcas()
        .success(function (data) {
          $scope.brandies = data;
        }).error(function (error) {
          $scope.status = 'Aconteceu algum erro ao pesquisar';
        });
  }

  function pesquisarCategorias() {
    gostoFactory.pesquisarCategorias()
        .success(function (data) {
          $scope.categories = data;
        }).error(function (error) {
          $scope.status = 'Aconteceu algum erro ao pesquisar';
        });
  }

  function pesquisarFornecedores() {
    gostoFactory.pesquisarFornecedores()
        .success(function (data) {
          $scope.suppliers = data;
        }).error(function (error) {
          $scope.status = 'Aconteceu algum erro ao pesquisar';
        });
  }
}]);