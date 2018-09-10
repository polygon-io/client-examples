
package main

import (
	"fmt"
	nats "github.com/nats-io/nats"
)

func main(){

	servers := "nats://nats1.polygon.io:30401, nats://nats2.polygon.io:30402, nats://nats3.polygon.io:30403"
	nc, _ := nats.Connect(servers, nats.Token("YourAPIKeyHere"))


	messages := make(chan *nats.Msg, 1000000) // Plenty of buffer room

	go printMessages( messages )
	
	// Subscribe to Quotes
	nc.Subscribe("Q.*", func(m *nats.Msg){
		messages <- m
	})

	// Subscribe to Trades:
	nc.Subscribe("T.*", func(m *nats.Msg){
		// Do not print to console here because it will block
		messages <- m
	})

	var input string
	fmt.Scanln( &input )

}


func printMessages( messages chan *nats.Msg ){
	for {
		m := <- messages
		fmt.Printf("[MSG] Received: %s\n", string(m.Data))
	}
}
