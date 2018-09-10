
const NATS = require('nats')
const SERVERS = ['nats://nats1.polygon.io:30401', 'nats://nats2.polygon.io:30402', 'nats://nats3.polygon.io:30403']


// Connect to Polygon NATS cluster:
const nats = NATS.connect({
	servers: SERVERS,
	token: 'YourAPIKeyHere'
})


// Subscribing to Currency/FOREX Data...
nats.subscribe('C.*', (msg, reply, subject) => {
	let forex = JSON.parse( msg )
	console.log('FOREX:', JSON.stringify( forex, null, 4 ))
})


// Subscribing to AAPL trades..
nats.subscribe('T.AAPL', (msg, reply, subject) => {
	let trade = JSON.parse( msg )
	console.log('TRADE:', JSON.stringify( trade, null, 4 ))
})


