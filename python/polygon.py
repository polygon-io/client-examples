#!/usr/bin/python3.6

import asyncio
import os
import signal
import sys
from nats.aio.client import Client as NATS

# Be sure to: pip install asyncio-nats-client

print("API KEY: {0}".format(sys.argv[1]))

def run(loop):
    nc = NATS()

    @asyncio.coroutine
    def closed_cb():
        print("Connection to NATS is closed.")
        yield from asyncio.sleep(0.1, loop=loop)
        loop.stop()

    options = {
        "servers": [
            "nats://{0}@nats1.polygon.io:30401".format(sys.argv[1]),
            "nats://{0}@nats2.polygon.io:30402".format(sys.argv[1]),
            "nats://{0}@nats3.polygon.io:30403".format(sys.argv[1])
        ],
        "io_loop": loop,
        "closed_cb": closed_cb
    }
    print( str(options) )

    yield from nc.connect(**options)
    print("Connected to NATS at {}...".format(nc.connected_url.netloc))

    @asyncio.coroutine
    def subscribe_handler(msg):
        subject = msg.subject
        reply = msg.reply
        data = msg.data.decode()
        print("Received a message on '{subject} {reply}': {data}".format(
          subject=subject, reply=reply, data=data))

    # Basic subscription to receive all published messages
    # which are being sent to a single topic 'discover'
    yield from nc.subscribe("C.*", cb=subscribe_handler)

    def signal_handler():
        if nc.is_closed:
            return
        print("Disconnecting...")
        loop.create_task(nc.close())

    for sig in ('SIGINT', 'SIGTERM'):
        loop.add_signal_handler(getattr(signal, sig), signal_handler)

if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    loop.run_until_complete(run(loop))
    try:
        loop.run_forever()
    finally:
        loop.close()