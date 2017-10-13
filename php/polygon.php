<?php

// Require NATS library:
require_once __DIR__.'/vendor/autoload.php';

// Get API key from CLI Args:
$opts = getopt("k:", array( "apikey:" ));
$apikey = $opts["k"] ?: $opts["apikey"];
echo "API KEY:".$apikey."\n";

// Make connection:
$encoder = new \Nats\Encoders\JSONEncoder();
$options = new \Nats\ConnectionOptions();
$options->setHost('nats1.polygon.io')->setPort(30401)->setToken( $apikey );
$client = new \Nats\EncodedConnection($options, $encoder);
$client->connect();

// Simple Subscriber.
$client->subscribe( 'C.*', function ($payload) {
	$pair = $payload->body['p'];
	$ask = $payload->body['a'];
	$bid = $payload->body['b'];
	echo "Forex Quote: \t Pair:".$pair." \t Ask:".$ask." \t Bid:".$bid."\n";

	// Print entire payload:
	// echo json_encode( $payload->body );
});

// Wait for 100 ticks:
$client->wait( 100 );

?>