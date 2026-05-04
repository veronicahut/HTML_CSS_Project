<?php
// name: app/public/check_answer.php
// date: 12/13/2025; 5/3/2026
// author: ChatGPT
// description: Accepts JSON {id, choice} and returns whether the answer is correct
//header('Content-Type: application/json');
//require __DIR__ . '/../db/config.php';
// require '/var/www/html/../db/config.php';
// $raw = file_get_contents('php://input');
// $input = json_decode($raw, true);
// if (!is_array($input) || !isset($input['id']) || !isset($input['choice'])) {
//     echo json_encode(['success' => false, 'error' => 'Missing parameters (id, choice)']);
//     exit;
// }
// $id = (int)$input['id'];
// $choice = $input['choice'];
// $pdo = getPDO();
// $stmt = $pdo->prepare('SELECT answer FROM trivia WHERE id = ?');
// $stmt->execute([$id]);
// $row = $stmt->fetch();
// if (!$row) {
//     echo json_encode(['success' => false, 'error' => 'Question not found']);
//     exit;
// }
// $correct = ($row['answer'] === $choice);
// echo json_encode(['success' => true, 'correct' => $correct, 'correctAnswer' => $row['answer']]);
// header('Content-Type: application/json');
//     UPDATES
// 1. Updated path to match your new 'includes' folder
require __DIR__ . '/includes/config.php';

$raw = file_get_contents('php://input');
$input = json_decode($raw, true);

if (!is_array($input) || !isset($input['id']) || !isset($input['choice'])) {
    echo json_encode(['success' => false, 'error' => 'Missing parameters (id, choice)']);
    exit;
}

$id = (int)$input['id'];
$choice = $input['choice'];

try {
    $pdo = getPDO();
    // 2. Updated table name to match your Postgres "environmentalTrivia"
    // Use double quotes if you kept the capital 'T' in pgAdmin
    $stmt = $pdo->prepare('SELECT answer FROM "environmentalTrivia" WHERE id = ?');
    $stmt->execute([$id]);
    $row = $stmt->fetch();

    if (!$row) {
        echo json_encode(['success' => false, 'error' => 'Question not found']);
        exit;
    }

    // 3. Simple comparison logic
    $correct = (trim($row['answer']) === trim($choice));
    echo json_encode([
        'success' => true, 
        'correct' => $correct, 
        'correctAnswer' => $row['answer']
    ]);

} catch (Exception $e) {
    echo json_encode(['success' => false, 'error' => 'Server error']);
}
