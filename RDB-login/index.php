<?php
session_start(); // Always start session

include("config.php");

if (!isset($_SESSION['user'])) {  // Removed the incorrect semicolon
    header("Location: login.php");
    // Stop further script execution after redirect
} else {
    header("Location: dashboard.php");
   
}
?>
