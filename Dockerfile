# # start
# FROM php:8.2-apache
# # Install PostgreSQL client libraries and PHP extensions
# RUN apt-get update && apt-get install -y libpq-dev \
#     && docker-php-ext-install pdo pdo_pgsql
# # Copy your files
# COPY ./AdditionalProjects/sample2/app/db/ /var/www/db/
# COPY ./AdditionalProjects/sample2/app/public/ /var/www/html/
# # run
# RUN chown -R www-data:www-data /var/www/
# RUN a2enmod rewrite

# start
FROM php:8.2-apache

RUN apt-get update && apt-get install -y libpq-dev \
    && docker-php-ext-install pdo pdo_pgsql

# copy the entire app folder directly to the server root
COPY ./samples/sample2/ /var/www/html/

# run
RUN a2enmod rewrite
