
<?php
$host = "localhost";
$user = "root";
$pass = "12345678";
$db = "mygame";

$conn = new mysqli($host, $user, $pass, $db);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>