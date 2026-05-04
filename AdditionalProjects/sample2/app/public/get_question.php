<?php
// name: app/public/get_question.php
// date: 12/13/2025; 5/3/2026
// author: ChatGPT
// description: Fetches a random trivia question from the database and returns it as JSON
// header('Content-Type: application/json');
// //require __DIR__ . '/../db/config.php';
// require '/var/www/html/../db/config.php';
// $pdo = getPDO();
// $stmt = $pdo->query("
//     SELECT id, question
//     FROM trivia
//     ORDER BY RAND()
//     LIMIT 1
// ");
// $q = $stmt->fetch();
// if ($q) {
//     echo json_encode([
//         'success' => true,
//         'data' => $q
//     ]);
// } else {
//     echo json_encode([
//         'success' => false,
//         'error' => 'No questions found'
//     ]);
// }
// Adjust path
require __DIR__ . '/../db/config.php'; 
$pdo = getPDO();
// Changed ORDER BY from RAND() to RANDOM() for Postgres
$stmt = $pdo->query("
    SELECT id, question
    FROM environmentalTrivia
    ORDER BY RANDOM() 
    LIMIT 1
");
$question = $stmt->fetch();
echo json_encode($question);
