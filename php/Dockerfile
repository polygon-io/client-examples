FROM composer:latest

WORKDIR /polygon/

COPY composer.json .

RUN composer install

COPY polygon.php .

ENTRYPOINT [ "php", "/polygon/polygon.php" ]