var http = require('http');

var server = http.createServer(function (req, res) {
	res.writeHead(200,{'Content-type':'text/plain'});
	res.end('functiona');
});

server.listen(3000, function () {
	console.log('escutando a porta 3000');
})