# start
FROM php:8.2-apache

# 1. Copy the entire app directory (includes db and public)
# This places 'db' at /var/www/db and 'public' at /var/www/html
COPY ./AdditionalProjects/sample2/app/db/ /var/www/db/
COPY ./AdditionalProjects/sample2/app/public/ /var/www/html/

# 2. Set permissions so PHP can read the files
RUN chown -R www-data:www-data /var/www/

# 3. Enable Apache rewrite
RUN a2enmod rewrite
