# start
FROM php:8.2-apache

RUN apt-get update && apt-get install -y libpq-dev \
    && docker-php-ext-install pdo pdo_pgsql

# copy the main portfolio files (index.html, assets, etc.)
COPY . /var/www/html/

# note: the sample2 files are already included in the copy above 

# run
RUN a2enmod rewrite
