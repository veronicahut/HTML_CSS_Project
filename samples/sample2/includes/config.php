<?php
// name: includes/config.php
// date: 12/13/2025; 5/4/2026
// author: ChatGPT, Veronica Hutchins
// description: Database configuration file for Trivia Game application
// notes: PDO helper for connecting to the MySQL database.
// Configure via environment variables or edit the defaults below.
// $DB_HOST = getenv('DB_HOST') ?: 'localhost';
// $DB_NAME = getenv('DB_NAME') ?: 'trivia_db';
// $DB_USER = getenv('DB_USER') ?: 'root';
// $DB_PASS = getenv('DB_PASS') ?: '';
// function getPDO()
// {
//     global $DB_HOST, $DB_NAME, $DB_USER, $DB_PASS;
//     $dsn = "mysql:host={$DB_HOST};dbname={$DB_NAME};charset=utf8mb4";
//     $options = [
//         PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
//         PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
//         PDO::ATTR_EMULATE_PREPARES => false,
//     ];
//     try {
//         return new PDO($dsn, $DB_USER, $DB_PASS, $options);
//     } catch (PDOException $e) {
//         http_response_code(500);
//         header('Content-Type: application/json');
//         echo json_encode(['success' => false, 'error' => 'Database connection failed']);
//         exit;
//     }
// }

// config.php file; Get the connection string from Render/Railway (postgresql) environment
$databaseUrl = getenv('DATABASE_URL');

if ($databaseUrl) {
    // Parse the URL (postgresql://user:pass@host:port/dbname)
    $dbopts = parse_url($databaseUrl);

    $DB_HOST = $dbopts['host'];
    $DB_PORT = $dbopts['port'] ?? 5432;
    $DB_USER = $dbopts['user'];
    $DB_PASS = $dbopts['pass'];
    $DB_NAME = ltrim($dbopts['path'], '/');
}

function getPDO() {
    global $DB_HOST, $DB_NAME, $DB_USER, $DB_PASS, $DB_PORT;
    try {
        // Note: use 'pgsql' driver instead of 'mysql'
        $dsn = "pgsql:host=$DB_HOST;port=$DB_PORT;dbname=$DB_NAME";
        return new PDO($dsn, $DB_USER, $DB_PASS, [
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
        ]);
    } catch (PDOException $e) {
        die("Connection failed: " . $e->getMessage());
    }
}
