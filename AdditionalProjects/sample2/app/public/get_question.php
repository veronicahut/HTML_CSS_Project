<?php
// name: app/public/get_question.php
// date: 12/13/2025
// author: ChatGPT
// description: Fetches a random trivia question from the database and returns it as JSON
header('Content-Type: application/json');
require __DIR__ . '/../db/config.php';
$pdo = getPDO();
$stmt = $pdo->query("
    SELECT id, question
    FROM trivia
    ORDER BY RAND()
    LIMIT 1
");

$q = $stmt->fetch();

if ($q) {
    echo json_encode([
        'success' => true,
        'data' => $q
    ]);
} else {
    echo json_encode([
        'success' => false,
        'error' => 'No questions found'
    ]);
}