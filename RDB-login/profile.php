<?php
session_start();
include "firebaseRDB.php"; // Replace with your actual database connection file

// Handle profile picture update
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    if (isset($_FILES['profile_pic'])) {
        $errors = [];
        $file_name = $_FILES['profile_pic']['name'];
        $file_size = $_FILES['profile_pic']['size'];
        $file_tmp = $_FILES['profile_pic']['tmp_name'];
        $file_type = $_FILES['profile_pic']['type'];
        $file_ext = strtolower(pathinfo($file_name, PATHINFO_EXTENSION));
        
        // Check file type and size
        $allowed_ext = ['jpg', 'jpeg', 'png', 'gif'];
        if (!in_array($file_ext, $allowed_ext)) {
            $errors[] = "Invalid file type. Allowed types: jpg, jpeg, png, gif.";
        }

        if ($file_size > 52428800) { // 2MB limit
            $errors[] = "File size must be under 50MB.";
        }

        if (empty($errors)) {
            // Save the file
            $upload_path = "uploads/" . $_SESSION['username'] . "." . $file_ext;
            if (move_uploaded_file($file_tmp, $upload_path)) {
                // Update session and database with new profile picture
                $_SESSION['profile_pic'] = $upload_path;
                // Optionally update the database with the new file path
                $stmt = $conn->prepare("UPDATE users SET profile_pic = ? WHERE username = ?");
                $stmt->bind_param("ss", $upload_path, $_SESSION['username']);
                $stmt->execute();
                $stmt->close();
            } else {
                $errors[] = "Failed to upload the image.";
            }
        }
    }
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Profile</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .profile-container {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            width: 300px;
            text-align: center;
        }

        .profile-container img {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            border: 2px solid #007bff;
            margin-bottom: 20px;
        }

        input[type="file"] {
            margin-bottom: 20px;
        }

        .button {
            padding: 10px 20px;
            background: #007bff;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            display: inline-block;
            width: 100%; /* Set both buttons to 100% width */
            text-align: center;
        }

        .button:hover {
            background: #0056b3;
        }

        /* Aligning the buttons */
        .button-container {
            text-align: center;
            margin-top: 20px;
        }

        .button-container a {
            text-decoration: none;
        }
    </style>
</head>
<body>

<div class="profile-container">
    <h2>👤 Profile</h2>
    <img src="<?php echo $_SESSION['profile_pic']; ?>" alt="Profile Picture">
    
    <!-- Display errors or success messages -->
    <?php
    if (isset($errors) && !empty($errors)) {
        foreach ($errors as $error) {
            echo "<p style='color:red;'>$error</p>";
        }
    }
    ?>

    <form method="POST" enctype="multipart/form-data">
        <label for="profile_pic">Change Profile Picture</label><br>
        <input type="file" name="profile_pic" accept="image/*" required><br><br>
        <button type="submit" class="button">Upload</button>
    </form>

    <div class="button-container">
        <a href="index.php"><button class="button">Back to Home</button></a>
    </div>
</div>

</body>
</html>
