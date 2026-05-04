<?php
// name: app/public/get_question.php
// date: 12/13/2025; 5/3/2026
// author: ChatGPT
// description: Fetches a random trivia question from the database and returns it as JSON

// Adjust path
require __DIR__ . '/includes/config.php'; 

try {
    $pdo = getPDO();
    $stmt = $pdo->query("
        SELECT id, question
        FROM \"environmentalTrivia\"
        ORDER BY RANDOM() 
        LIMIT 1
    ");
    $question = $stmt->fetch();

    if ($question) {
        // Wrap the result so it matches what scripts.js expects
        echo json_encode([
            'success' => true,
            'data' => $question
        ]);
    } else {
        echo json_encode(['success' => false, 'error' => 'No question found']);
    }
} catch (Exception $e) {
    echo json_encode(['success' => false, 'error' => $e->getMessage()]);
}
