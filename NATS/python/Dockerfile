FROM python:3.6.2-jessie

RUN git clone https://github.com/nats-io/asyncio-nats && cd asyncio-nats && python3.6 setup.py install

WORKDIR /asyncio-nats/

COPY polygon.py .

ENTRYPOINT [ "python3.6", "/asyncio-nats/polygon.py" ]