<?php
session_start(); // Start session

include("config.php");
include("firebaseRDB.php");

if (isset($_SESSION['user'])) {
    unset($_SESSION['user']); // Remove user session
    session_destroy(); // Destroy all session data
    header("Location: login.php"); // Redirect to login page
    exit(); // Stop further execution
} else {
    header("Location: login.php"); // If no user session, still redirect
    exit();
}
?>
 