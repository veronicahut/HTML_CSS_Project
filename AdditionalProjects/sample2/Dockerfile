# start
FROM php:8.2-apache

# Install PostgreSQL client libraries and PHP extensions
RUN apt-get update && apt-get install -y libpq-dev \
    && docker-php-ext-install pdo pdo_pgsql

# Copy your files
COPY ./AdditionalProjects/sample2/app/db/ /var/www/db/
COPY ./AdditionalProjects/sample2/app/public/ /var/www/html/

# run
RUN chown -R www-data:www-data /var/www/
RUN a2enmod rewrite
