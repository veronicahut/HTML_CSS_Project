<?php
// name: app/public/check_answer.php
// date: 12/13/2025; 5/3/2026
// author: ChatGPT
// description: Accepts JSON {id, choice} and returns whether the answer is correct

header('Content-Type: application/json');

// updated path to match the new 'includes' folder
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
    // updated table name to match the Postgres "environmentalTrivia"
    // Use double quotes for the capital 'T' in pgAdmin
    $stmt = $pdo->prepare('SELECT answer FROM \"environmentalTrivia\" WHERE id = ?');
    $stmt->execute([$id]);
    $row = $stmt->fetch();

    if (!$row) {
        echo json_encode(['success' => false, 'error' => 'Answer not found']);
        exit;
    }

    // use simple comparison logic
    $correct = (trim($row['answer']) === trim($choice));
    echo json_encode([
        'success' => true, 
        'correct' => $correct, 
        'correctAnswer' => $row['answer']
    ]);

} catch (Exception $e) {
    echo json_encode(['success' => false, 'error' => 'Server error']);
}
