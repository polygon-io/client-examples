<?php

// Require NATS library:
require_once __DIR__.'/vendor/autoload.php';

// Get API key from CLI Args:
$opts = getopt("k:", array( "apikey:" ));
$apikey = $opts["k"] ?: $opts["apikey"];
echo "API KEY:".$apikey;

// Make connection:
$encoder = new \Nats\Encoders\JSONEncoder();
$options = new \Nats\ConnectionOptions();
$options->setHost('nats1.polygon.io')->setPort(30401)->setToken( $apikey );
$client = new \Nats\EncodedConnection($options, $encoder);
$client->connect();

// Simple Subscriber.
$client->subscribe( 'T.*', function ($payload) {
	$symbol = $payload->body['sym'];
	$price = $payload->body['p'];
	$size = $payload->body['s'];
	echo "Trade: \t Symbol:".$symbol." \t Price:".$price." \t Size:".$size."\n\n";

	// Print entire payload:
	// echo json_encode( $payload->body )
});

// Wait for 100 ticks:
$client->wait( 100 );

?>