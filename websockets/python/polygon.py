# Be sure to pip install websocket-client
# Details: https://pypi.org/project/websocket-client/

import websocket
try:
	import thread
except ImportError:
	import _thread as thread
import time

def on_message(ws, message):
	print(message)

def on_error(ws, error):
	print(error)

def on_close(ws):
	print("### closed ###")

def on_open(ws):
	ws.send('{"action":"auth","params":"YOUR_API_KEY"}')
	ws.send('{"action":"subscribe","params":"C.AUD/USD,C.USD/EUR,C.USD/JPY"}')

if __name__ == "__main__":
	# websocket.enableTrace(True)
	ws = websocket.WebSocketApp("wss://socket.polygon.io/forex",
							  on_message = on_message,
							  on_error = on_error,
							  on_close = on_close)
	ws.on_open = on_open
	ws.run_forever()

