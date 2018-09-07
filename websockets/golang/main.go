package main

import (
	"fmt"
	websocket "github.com/gorilla/websocket"
)

const APIKEY = "YOUR_API_KEY"
const CHANNELS = "C.AUD/USD,C.USD/EUR,C.USD/JPY"


func readLoop( c *websocket.Conn ){
	defer c.Close()
	var msg interface{}
	for {
		err := c.ReadJSON( &msg )
		if err != nil {
			panic( err )
		}
		fmt.Println( "Message:", msg )
	}
}


func main(){
	c, _, err := websocket.DefaultDialer.Dial("wss://socket.polygon.io/forex", nil )
	if err != nil {
		panic( err )
	}
	go readLoop( c )

	_ = c.WriteMessage( websocket.TextMessage, []byte( fmt.Sprintf("{\"action\":\"auth\",\"params\":\"%s\"}", APIKEY) ) )
	_ = c.WriteMessage( websocket.TextMessage, []byte( fmt.Sprintf("{\"action\":\"subscribe\",\"params\":\"%s\"}", CHANNELS) ) )

	done := make(chan struct{})
	for {
		select {
		case <-done:
			return
		}
	}
}
