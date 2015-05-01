'use strict';

app.controller('productController', ['$scope', '$location', '$routeParams', 'gostoFactory', function ($scope, $location, $routeParams, gostoFactory) {

    var produtoAterado = false;

    //var $overflow = '';
    //var colorbox_params = {
    //    rel: 'colorbox',
    //    reposition: true,
    //    scalePhotos: true,
    //    scrolling: false,
    //    previous: '<i class="ace-icon fa fa-arrow-left"></i>',
    //    next: '<i class="ace-icon fa fa-arrow-right"></i>',
    //    close: '&times;',
    //    current: '{current} de {total}',
    //    maxWidth: '100%',
    //    maxHeight: '100%',
    //    onOpen: function () {
    //        $overflow = document.body.style.overflow;
    //        document.body.style.overflow = 'hidden';
    //    },
    //    onClosed: function () {
    //        document.body.style.overflow = $overflow;
    //    },
    //    onComplete: function () {
    //        $.colorbox.resize();
    //    }
    //};

    //$('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
    //$("#cboxLoadingGraphic").html("<i class='ace-icon fa fa-spinner orange fa-spin'></i>");

    //$(document).one('ajaxloadstart.page', function (e) {
    //    $('#colorbox, #cboxOverlay').remove();
    //});

    $scope.dropzoneConfig = {
        'options': { // passed into the Dropzone constructor
            'url': 'http://localhost:60629/api/v1/public/PostFormData/1'
        },
        'eventHandlers': {
            'sending': function (file, xhr, formData) {

            },
            'success': function (file, response) {

            }
        }
    };

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

            if (formInvalido) {
                return;
            }
        }
        else if (info.step == 2) {
            $('#nome-estilo').text($('#Style').data('title'));
            $('#nome-categoria').text($('#Category').data('title'));
            $('#nome-marca').text($('#Brand').data('title'));
            $('#nome-fornecedor').text($('#Supplier').data('title'));

            salvar();
        }
        else if (info.step == 3) {
            obterFotosDoProduto();
        }
    })
    .on('finished.fu.wizard', function (e) {

        if (!produtoAterado) {
            mensagem('Mensagem de sucesso', 'Parabéns, produto inserido com sucesso!!', 'sucesso');
        }
        else {
            mensagem('Mensagem de sucesso', 'Parabéns, produto alterado com sucesso!!', 'sucesso');
        }

        window.location.href = '#/products';

    }).on('stepclick.fu.wizard', function (e) {
        //e.preventDefault();//this will prevent clicking and selecting steps
    });

    if ($location.path() !== '/products') {
        pesquisarEstilos();
        pesquisarCategorias();
        pesquisarFornecedores();
        pesquisarMarcas();
    }

    $scope.product = {};
    $scope.products = {};
    $scope.totalRegistros = 0;
    $scope.loading = false;

    function obterFotosDoProduto() {

        var id = $scope.product.id;

        console.log(id);

        if (id !== undefined) {
            gostoFactory.obterFotosDoProduto(id)
            .success(function (data) {

                $scope.product.photos = data;
            }).error(function (error) {
                mensagem('Erro ao pesquisar por fotos', error, 'erro');
            });
        }
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

    $scope.clickExcluir = function () {

        var productId = $scope.product.id;

        gostoFactory.excluirProduto(productId)
            .success(function (data) {

                $scope.products.splice($scope.index - 1, 1);
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

    $scope.resetar = function () {
        $scope.product = null;
        window.product = null;
    }

    function salvar() {

        var id = $scope.product.id;
        if (id == undefined) { inserir(); } else { atualizar(); }
    };

    function inserir() {

        var cat = $scope.product;

        gostoFactory.inserirProduto(cat)
            .success(function (data) {
                $scope.product.id = data.id;
            }).error(function (error) {
                mensagem('Erro no cadastro', error, 'erro');
            });
    };

    function atualizar() {
        produtoAterado = true;

        var cat = $scope.product;

        gostoFactory.atualizarProduto(cat).error(function (error) {
            mensagem('Erro no cadastro', error, 'erro');
        });

        $('#salvar').text('').prepend('Salvar <i class="ace-icon fa fa-arrow-right icon-on-right bigger-110"></i>');
    };

    function pesquisarEstilos() {
        gostoFactory.pesquisarEstilos()
            .success(function (data) {
                $('#Style').empty();
                $('#Style').append($('<option/>').attr("value", "").text(""));
                $.each(data, function (i, option) {
                    $('#Style').append($('<option/>').attr("value", option.id).text(option.title)).data("title", option.title);
                });

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    }

    function pesquisarMarcas() {
        gostoFactory.pesquisarMarcas()
            .success(function (data) {
                $('#Brand').empty();
                $('#Brand').append($('<option/>').attr("value", "").text(""));
                $.each(data, function (i, option) {
                    $('#Brand').append($('<option/>').attr("value", option.id).text(option.title)).data("title", option.title);
                });

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    }

    function pesquisarCategorias() {
        gostoFactory.pesquisarCategorias()
            .success(function (data) {
                $('#Category').empty();
                $('#Category').append($('<option/>').attr("value", "").text(""));
                $.each(data, function (i, option) {
                    $('#Category').append($('<option/>').attr("value", option.id).text(option.title)).data("title", option.title);
                });

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    }

    function pesquisarFornecedores() {
        gostoFactory.pesquisarFornecedores()
            .success(function (data) {
                $('#Supplier').empty();
                $('#Supplier').append($('<option/>').attr("value", "").text(""));
                $.each(data, function (i, option) {
                    $('#Supplier').append($('<option/>').attr("value", option.id).text(option.nome)).data("title", option.title);
                });

            }).error(function (error) {
                $scope.status = 'Aconteceu algum erro ao pesquisar';
            });
    }
}]);