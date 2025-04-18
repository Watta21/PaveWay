<?php
session_start();
include("config.php");
include("firebaseRDB.php");

$error = "";

// Form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $email = trim($_POST['email']);
    $password = trim($_POST['password']); 

    // Firebase login
    if (filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $rdb = new firebaseRDB("https://paveway-5ff24-default-rtdb.firebaseio.com/");
        $retrieve = $rdb->retrieve("/user", "email", "EQUAL", $email);
        $data = json_decode($retrieve, true);

        if (!$data || count($data) == 0) {
            $error = "⚠️ Email not registered";
        } else {
            $id = array_keys($data)[0];  // Get the unique user ID
            if ($data[$id]['password'] === $password) {
                // Store user data in session (including name and profile picture)
                $_SESSION['user'] = $data[$id]; // This stores the entire user data
                $_SESSION['username'] = $data[$id]['name'];  // Store full name as 'username'
                $_SESSION['profile_pic'] = $data[$id]['profile_pic'] ?? "2.png"; // Store profile picture

                header("Location: index.php");
                exit();
            } else {
                $error = "❌ Incorrect password.";
            }
        }
    } else {
        $error = "⚠️ Invalid email format.";
    }
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <style>
        /* General page styling */
        body {
            font-family: Arial, sans-serif;
            background: url('2023-03-13.jpg') no-repeat center center fixed;
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        /* Center the login/signup form */
        .login-container {
            background: white;
            padding: 20px;
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            width: 300px;
            text-align: center;
        }

        /* Style form inputs */
        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 10px;
            margin: 8px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }

        /* Style buttons */
        button {
            width: 100%;
            padding: 10px;
            background: blue;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-size: 16px;
            margin-top: 10px;
        }

        button:hover {
            background: darkblue;
        }

        /* Password container for checkbox functionality */
        .password-container {
            position: relative;
            display: flex;
            align-items: center;
        }

        .password-container input {
            width: 100%;
        }

        /* Checkbox styling */
        .checkbox-container {
            display: flex;
            justify-content: flex-start;
            margin-top: 10px;
        }

        /* Error message styling */
        .error {
            color: red;
            font-size: 14px;
            margin-top: 10px;
        }

        /* Success message styling */
        .success {
            color: green;
            margin-top: 10px;
        }

        /* Responsive */
        @media (max-width: 400px) {
            .login-container {
                width: 90%;
            }
        }

        /* Added space between Quick View and Sign Up buttons */
        .button-group {
            margin-top: 3px;  
        }

        .button-group a {
            text-decoration: none;
            margin-top: 2px;
            display: block;
        }

        /* Added space for the 'Create an Account' text */
        .create-account-text {
            margin-top: 25px;
            font-size: 16px;
            color: #555;
        }
    </style>
</head>
<body>

<div class="login-container">
    <h2>Login</h2>
    
    <?php if ($error): ?>
        <p class="error"><?= $error ?></p>
    <?php endif; ?>

    <form method="post">
        <input type="text" name="email" placeholder="Email" required><br>

        <div class="password-container">
            <input type="password" name="password" id="password" placeholder="Password" required>
        </div>

        <!-- Checkbox to toggle password visibility -->
        <div class="checkbox-container">
            <input type="checkbox" id="showPassword" onclick="togglePasswordVisibility()"> 
            <label for="showPassword">Show Password</label>
        </div>

        <button type="submit">Login</button>
    </form>

    <!-- Create Account text (just plain text) -->
    <div class="create-account-text">
        Don't have an account? Create one now!
    </div>

    <div class="button-group">
        <a href="signup.php"><button>Sign Up</button></a>
        <!-- Change Quick View link to dashboard.php -->
        <a href="quickview.php"><button>Quick View</button></a>
    </div>
</div>

<script>
function togglePasswordVisibility() {
    let passwordField = document.getElementById("password");
    let checkbox = document.getElementById("showPassword");

    // If checkbox is checked, show password
    passwordField.type = checkbox.checked ? "text" : "password";
}
</script>

</body>
</html>
