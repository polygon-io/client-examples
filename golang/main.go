
package main
import (
	"fmt"
	"nats"
)

func main(){

	servers := "nats://nats1.polygon.io:30401, nats://nats2.polygon.io:30402, nats://nats3.polygon.io:30403"
	nc, _ := nats.Connect(servers, nats.Token("YourAPIKeyHere"))
	
	// Subscribe to all Currency/FOREX data
	nc.Subscribe("C.*", func(m *nats.Msg){
		fmt.Printf("[FOREX] Received: %s\n", string(m.Data))
	})

	// Subscribe to AAPL trades
	nc.Subscribe("T.AAPL", func(m *nats.Msg){
		fmt.Printf("[TRADE] Received: %s\n", string(m.Data))
	})

}