<?php
// name: app/public/check_answer.php
// date: 12/13/2025; 4/25/2026
// author: ChatGPT
// description: Accepts JSON {id, choice} and returns whether the answer is correct
header('Content-Type: application/json');
require __DIR__ . '/../db/config.php';

$raw = file_get_contents('php://input');
$input = json_decode($raw, true);
if (!is_array($input) || !isset($input['id']) || !isset($input['choice'])) {
    echo json_encode(['success' => false, 'error' => 'Missing parameters (id, choice)']);
    exit;
}

$id = (int)$input['id'];
$choice = $input['choice'];

$pdo = getPDO();
$stmt = $pdo->prepare('SELECT answer FROM trivia WHERE id = ?');
$stmt->execute([$id]);
$row = $stmt->fetch();
if (!$row) {
    echo json_encode(['success' => false, 'error' => 'Question not found']);
    exit;
}

$correct = ($row['answer'] === $choice);
echo json_encode(['success' => true, 'correct' => $correct, 'correctAnswer' => $row['answer']]);
