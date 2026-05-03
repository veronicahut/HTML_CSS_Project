# start
FROM php:8.2-apache

# Copy your project into the web server directory #COPY . /var/www/html/
# Copy ONLY the public folder into the web root
COPY ./AdditionalProjects/sample2/app/public/ /var/www/html/

# Enable Apache rewrite if needed
RUN a2enmod rewrite
