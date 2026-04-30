-- SQL to create the Trivia table and insert sample rows
-- Edit the database name below or run in your chosen database/schema

-- Create database if you need it (optional)
-- CREATE DATABASE IF NOT EXISTS trivia_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE trivia_db;

-- create table
CREATE TABLE IF NOT EXISTS trivia (
  id INT AUTO_INCREMENT PRIMARY KEY,
  question TEXT NOT NULL,
  answer ENUM('True','False') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- add data
-- INSERT INTO trivia (question, answer) VALUES ('The largest state in the United States by land area is Alaska', 'True'),
-- 	('The Sahara Desert is located in South America', 'False'),
-- 	('There are 13 stripes on the US flag', 'True'),
-- 	('The human bodys largest organ is the small intestine', 'False'),
-- 	('The name of the international group formed to maintain world peace after World War I was the League of Nations', 'True'),
-- 	('The country that has the most islands in the world is Japan', 'False'),
-- 	('Every odd number contains the letter e in its spelling', 'True'),
-- 	('The only planet in our solar system to rotate clockwise is Earth', 'False'),
-- 	('The word “hundred” comes from the old Norse term, “hundrath”, which actually means 120 and not 100', 'True'),
-- 	('Snapchat is the startup, acquired by Facebook for $1 billion in 2012, that became the fourth most downloaded app in the 2010s and is known for the posting of pictures', 'False'),
-- 	('Founded in 1946, the IWC is an international organization that aims to "provide for the proper conservation" of Whales', 'True'),
-- 	('A major potential in efforts to engineer environmental sustainability is attempts to breed bacteria that can eat what glass materials that are difficult to recycle', 'False'),
-- 	('IMO is the organization responsible for setting standards for the safety, security, and environmental performance of ships and stands for International Maritime Organization', 'True'),
-- 	('A password that is limited strictly to numeric characters, or a PIN, is short for Personal Identification Number', 'True'),
-- 	('Windows is the popular operating system, launched in 1991, and has its own mascot, Tux the penguin', 'False'),
-- 	('A Virtual Private Network provides a secure connection to a different network than public Wi-Fi', 'True'),
-- 	('While building magnetrons at MIT in the 1940s, Percy Spencer noticed a melting chocolate bar in his pocket, which led to the invention of chocolate syrup', 'False'),
-- 	('GUI is short for Graphical User Interface', 'True'),
-- 	('Google first introduced their Chromebook hardware in 1911', 'False'),
-- 	('An abacus is a calculating tool that uses beans or stones that are moved to perform calculations', 'True'),
-- 	('The Samsung Galactica was the first Android-powered device from Samsung Mobile that also became the first in a long-running product line for the company', 'False'),
-- 	('As the IoT gained commercial prominence throughout the 2010s, the rise of cybersecurity occurred alongside it as there were more potential attack surfaces', 'True'),
-- 	('ClamBox is private electronics accessory company based in Fort Collins, CO, and is most well-known for a series of phone cases that are water-resistant, shock-resistant, and drop-resistant', 'False'),
-- 	('The iPhone was announced in 2007 with the following slogan: This is only the beginning.', 'True'),
-- 	('Google Aide is the Google software, debuting in 2016, that offers vocal help to Google mobile and smart home device owners', 'False');

-- verify rows
-- SELECT * FROM trivia LIMIT 10;

