<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

require 'connect.php';

$p_name = $_POST['p_name'] ?? '';
$username = $_POST['username'] ?? '';
$password = $_POST['password'] ?? '';

// ตรวจสอบค่าว่าง
if ($p_name == '' || $username == '' || $password == '') {
    echo "status:0"; // ส่งกลับให้ Unity รู้ว่า error
    exit();
}

// ตรวจสอบว่ามี username นี้อยู่แล้วหรือยัง
$check = "SELECT * FROM player WHERE username = '$username'";
$result = mysqli_query($conn, $check);

if (mysqli_num_rows($result) > 0) {
    echo "status:1"; // มีชื่อผู้ใช้งานแล้ว
    exit();
}

// ถ้ายังไม่มีก็ insert ลงฐานข้อมูล
$sql = "INSERT INTO player (p_name, username, password) VALUES ('$p_name', '$username', '$password')";
if (mysqli_query($conn, $sql)) {
    echo "status:success";
} else {
    echo "status:error";
}

mysqli_close($conn);
?>