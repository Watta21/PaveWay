<?php
// Initialize session and include necessary files
session_start();
include("config.php");
include("firebaseRDB.php");

$error = "";
$success = "";

// Form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Retrieve form data
    $name = trim($_POST['name']);
    $email = trim($_POST['email']);
    $password = trim($_POST['password']);

    // Validate input fields
    if (empty($name)) {
        $error = "Name is required";
    } elseif (empty($email)) {
        $error = "Email is required";
    } elseif (empty($password)) {
        $error = "Password is required";
    } else {
        // Firebase reference to check if the email already exists
        $rdb = new firebaseRDB("https://paveway-5ff24-default-rtdb.firebaseio.com/");
        $retrieve = $rdb->retrieve("/user", "email", "EQUAL", $email);
        $data = json_decode($retrieve, true);

        // Check if the email is already registered
        if (!empty($data)) {
            $error = "Email is already used";
        } else {
            // Insert the new user into Firebase with the plain password (not hashed)
            $insert = $rdb->insert("/user", [
                "name" => $name,
                "email" => $email,
                "password" => $password // Store plain password (not hashed)
            ]);

            $result = json_decode($insert, true);

            // Check if the insert was successful
            if (isset($result['name'])) {
                $success = "Signup successful! Please log in.";
            } else {
                $error = "Signup failed";
            }
        }
    }
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign Up Page</title>
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

        /* Center the sign up form */
        .signup-container {
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
        input[type="submit"] {
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

        input[type="submit"]:hover {
            background: darkblue;
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
            .signup-container {
                width: 90%;
            }
        }

        /* Added space for the 'Already have an account?' text */
        .login-text {
            margin-top: 25px;
            font-size: 16px;
            color: #555;
        }
    </style>
</head>
<body>

<div class="signup-container">
    <h2>Sign Up</h2>

    <!-- Display error or success messages -->
    <?php if ($error): ?>
        <p class="error"><?= $error ?></p>
    <?php endif; ?>
    
    <?php if ($success): ?>
        <p class="success"><?= $success ?></p>
    <?php endif; ?>

    <form method="post" action="">
        <input type="text" name="name" placeholder="Name" required><br>
        <input type="text" name="email" placeholder="Email" required><br>
        <input type="password" name="password" placeholder="Password" required><br><br>

        <input type="submit" value="SIGN UP"><br><br>

        <div class="login-text">
            Already have an account? <a href="login.php">Login</a>
        </div>
    </form>
</div>

</body>
</html>
