<?php
// Start the session
session_start();

// Check if the user is logged in as admin, otherwise redirect to login page
if (!isset($_SESSION['admin_logged_in'])) {
    // Redirect to login page if not logged in
   
}

// Include necessary files
include("config.php");
include("firebaseRDB.php");

$error = "";
$success = "";

// Firebase reference to retrieve users
$rdb = new firebaseRDB("https://paveway-5ff24-default-rtdb.firebaseio.com/");

// Handle Activating/Deactivating a User
if (isset($_GET['toggle_active'])) {
    $userId = $_GET['toggle_active']; // User ID to be toggled
    $retrieve = $rdb->retrieve("/user/{$userId}");  // Get user data
    $user = json_decode($retrieve, true);

    // Check if user exists
    if (!$user) {
        $error = "User not found.";
    } else {
        // Toggle the 'is_active' field
        $isActive = isset($user['is_active']) && $user['is_active'] == true ? false : true;

        // Update the user's 'is_active' status
        $updateData = [
            "is_active" => $isActive
        ];

        // Perform the update
        $update = $rdb->update("/user/" . $userId, $updateData);
        $result = json_decode($update, true);

        if (isset($result['error'])) {
            $error = "Failed to update user status.";
        } else {
            $success = $isActive ? "User activated successfully!" : "User deactivated successfully!";
        }
    }
}

// Pagination settings
$usersPerPage = 10; // Number of users to show per page
$page = isset($_GET['page']) ? (int)$_GET['page'] : 1; // Get current page from query string (default to page 1)
$offset = ($page - 1) * $usersPerPage; // Calculate the offset for Firebase query

// Retrieve users with pagination
$retrieve = $rdb->retrieve("/user"); // Retrieve all users
$data = json_decode($retrieve, true);

// Pagination logic: Calculate total number of users and total pages
$totalUsers = count($data);
$totalPages = ceil($totalUsers / $usersPerPage);
$users = array_slice($data, $offset, $usersPerPage); // Slice the data for the current page
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        /* General Styles */
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fc;
            margin: 0;
            padding: 0;
        }

        /* Sidebar */
        .sidebar {
            background-color: #4e73df;
            color: white;
            height: 100vh;
            padding-top: 20px;
            position: fixed;
            width: 250px;
        }

        .sidebar .nav-link {
            color: white;
        }

        .sidebar .nav-link:hover {
            background-color: #2e59d9;
        }

        .content-area {
            margin-left: 250px;
            padding: 20px;
        }

        /* Table Styles */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        table, th, td {
            border: 1px solid #ddd;
        }

        th, td {
            padding: 12px;
            text-align: left;
        }

        th {
            background-color: #007BFF;
            color: white;
        }

        td {
            background-color: #f9f9f9;
        }

        tr:nth-child(even) td {
            background-color: #f1f1f1;
        }

        /* Buttons and Action Links */
        .action-buttons a,
        button[type="submit"] {
            background-color: #007BFF;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            text-decoration: none;
            font-size: 14px;
        }

        .action-buttons a:hover,
        button[type="submit"]:hover {
            background-color: #0056b3;
        }

        .action-buttons {
            display: flex;
            gap: 10px;
        }

        .action-buttons a {
            background-color: #FF5722;
        }

        .action-buttons a:hover {
            background-color: #E64A19;
        }

        /* Success and Error Messages */
        .error {
            color: red;
            font-size: 14px;
            margin-top: 10px;
        }

        .success {
            color: green;
            font-size: 14px;
            margin-top: 10px;
        }

        .pagination a {
            margin: 5px;
            text-decoration: none;
            padding: 8px 15px;
            background-color: #007BFF;
            color: white;
            border-radius: 4px;
        }

        .pagination a:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>

    <!-- Sidebar -->
    <div class="sidebar">
        <h2 class="text-center">Admin Dashboard</h2>
        <ul class="nav flex-column">
            <li class="nav-item">
                <a class="nav-link active" href="#">Users</a>
            </li>
        </ul>
    </div>

    <!-- Main Content -->
    <div class="content-area">
        <h2>Registered Users</h2>

        <!-- Display success or error messages -->
        <?php if ($error): ?>
            <p class="error"><?= $error ?></p>
        <?php endif; ?>
        
        <?php if ($success): ?>
            <p class="success"><?= $success ?></p>
        <?php endif; ?>

        <table>
            <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Status</th>
                <th>Actions</th>
            </tr>

            <?php if (!empty($users)): ?>
                <?php foreach ($users as $userId => $user): ?>
                    <tr>
                        <form method="POST" action="">
                            <td><input type="text" name="name" value="<?= $user['name'] ?>" required></td>
                            <td><input type="email" name="email" value="<?= $user['email'] ?>" required></td>
                            <td>
                                <span class="badge <?= $user['is_active'] ? 'bg-success' : 'bg-danger' ?>">
                                    <?= $user['is_active'] ? 'Active' : 'Inactive' ?>
                                </span>
                            </td>
                            <td class="action-buttons">
                                <input type="hidden" name="id" value="<?= $userId ?>">
                                <button type="submit" name="update">Update</button>
                                <a href="admin.php?delete=<?= $userId ?>" onclick="return confirm('Are you sure you want to delete this user?')">Delete</a>
                                <a href="admin.php?toggle_active=<?= $userId ?>" onclick="return confirm('Are you sure you want to toggle the status of this user?')">
                                    <?= $user['is_active'] ? 'Deactivate' : 'Activate' ?>
                                </a>
                            </td>
                        </form>
                    </tr>
                <?php endforeach; ?>
            <?php else: ?>
                <tr><td colspan="4">No users found.</td></tr>
            <?php endif; ?>
        </table>

        <!-- Pagination Links -->
        <div class="pagination">
            <?php if ($page > 1): ?>
                <a href="admin.php?page=<?= $page - 1 ?>">Previous</a>
            <?php endif; ?>
            <?php if ($page < $totalPages): ?>
                <a href="admin.php?page=<?= $page + 1 ?>">Next</a>
            <?php endif; ?>
        </div>
    </div>

    <!-- Bootstrap JS and Popper.js -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js"></script>
</body>
</html>
