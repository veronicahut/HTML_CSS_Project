# create
FROM php:8.2-apache

# Copy your project into the web server directory
#COPY . /var/www/html/
COPY . /HTML_CSS_Project/

# Enable Apache rewrite if needed
RUN a2enmod rewrite
