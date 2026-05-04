<?php
// name: samples/sample2/check_answer.php
// date: 12/13/2025; 5/3/2026
// author: ChatGPT, Google Gemini, Veronica Hutchins
// description: Accepts JSON {id, choice} and returns whether the answer is correct

header('Content-Type: application/json');
require __DIR__ . '/includes/config.php';

$raw = file_get_contents('php://input');
$input = json_decode($raw, true);

if (!is_array($input) || !isset($input['id']) || !isset($input['choice'])) {
    echo json_encode(['success' => false, 'error' => 'Missing parameters (id, choice)']);
    exit;
}

try {
    $pdo = getPDO();
    $id = (int)$input['id'];
    $choice = $input['choice'];

    // Ensure the table name matches the one in get_question.php
    $stmt = $pdo->prepare('SELECT answer FROM "environmentalTrivia" WHERE id = ?');
    $stmt->execute([$id]);
    $row = $stmt->fetch();

    if (!$row) {
        echo json_encode(['success' => false, 'error' => 'Answer not found in database']);
        exit;
    }

    // Use trim() to avoid issues with hidden spaces in the database
    $isCorrect = (trim($row['answer']) === trim($choice));

    echo json_encode([
        'success' => true,
        'correct' => $isCorrect,
        'correctAnswer' => $row['answer']
    ]);

} catch (PDOException $e) {
    // This catches database errors (e.g., table not found, bad credentials)
    echo json_encode([
        'success' => false, 
        'error' => 'Database error: ' . $e->getMessage()
    ]);
} catch (Exception $e) {
    // This catches everything else
    echo json_encode([
        'success' => false, 
        'error' => 'General error: ' . $e->getMessage()
    ]);
}
