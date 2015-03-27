'use strict';

app.controller('saleController', function ($scope, gostoFactory) {

    // Instantiate the Bloodhound suggestion engine
    var clientes = new Bloodhound({
        datumTokenizer: function (datum) {
            return Bloodhound.tokenizers.whitespace(datum.value);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: 'http://localhost:60629/api/v1/public/customers',
            filter: function (clientes) {
                // Map the remote source JSON array to a JavaScript object array
                return $.map(clientes, function (cliente) {
                    return {
                        value: cliente.nome,
                        id: cliente.id
                    };
                });
            }
        }
    });

    // Initialize the Bloodhound suggestion engine
    clientes.initialize();

    // Instantiate the Typeahead UI
    $('#typeahead').typeahead(null, {
        minLength: 3,
        displayKey: function (key) {
            return key.value
        },
        source: clientes.ttAdapter()
    }).on('typeahead:selected', function (obj, datum) {
        console.log(datum.id);
        //armazena o cliente
    });
});