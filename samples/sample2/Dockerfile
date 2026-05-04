# start
FROM php:8.2-apache

RUN apt-get update && apt-get install -y libpq-dev \
    && docker-php-ext-install pdo pdo_pgsql

# copy the entire app folder directly to the server root
COPY ./samples/sample2/ /var/www/html/

# run
RUN a2enmod rewrite
