<?php
session_start();
include "db.php"; // Replace with your actual database connection file

// Set default profile data if not set
$_SESSION['username'] = isset($_SESSION['username']) ? $_SESSION['username'] : "User";
$_SESSION['profile_pic'] = isset($_SESSION['profile_pic']) ? $_SESSION['profile_pic'] : "2.png";
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Image Gallery</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: url('1.jpg') no-repeat center center fixed;
            background-size: cover;
            margin: 0;
            padding: 20px;
        }
        .header {
            display: flex;
            justify-content: center;
            align-items: center;
            position: relative;
            padding: 10px 20px;
            background-color: white;
            border-bottom: 2px solid #007bff;
        }

        .profile-container {
            display: flex;
            align-items: center;
            gap: 10px;
            position: absolute;
            top: 10px;
            left: 10px;
            cursor: pointer;
        }

        .profile-container img {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            border: 2px solid #007bff;
        }

        .profile-dropdown {
            display: none;
            position: absolute;
            top: 50px;
            left: 0;
            background: white;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
            width: 150px;
            text-align: left;
            padding: 5px 0;
            border: 2px solid #007bff;
            z-index: 1000;
        }

        .profile-dropdown a {
            display: block;
            padding: 10px;
            text-decoration: none;
            color: black;
        }

        .profile-dropdown a:hover {
            background: #007bff;
            color: white;
        }

        #searchBox {
            width: 60%;
            max-width: 400px;
            padding: 10px;
            border: 2px solid #007bff;
            border-radius: 5px;
            font-size: 16px;
            text-align: center;
        }

        .gallery-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 50px;
            padding: 50px;
        }

        .image-container {
            position: relative;
            border: 5px solid #ccc;
            padding: 20px;
            width: 250px;
            background: white;
            border-radius: 10px;
            box-shadow: 2px 2px 10px #010aa5;
            text-align: center;
        }

        .image-container img {
            width: 100%;
            height: 250px;
            object-fit: cover;
            border-radius: 5px;
        }

        /* Comment box styling */
        .comment-box {
            display: none;
            margin-top: 10px;
            padding: 10px;
            border-top: 1px solid #ccc;
            background: #f9f9f9;
            width: 100%;
            border-radius: 5px;
            text-align: center;
        }

        .comment-list {
            max-height: 150px;
            overflow-y: auto;
            background: white;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .comment-input {
            width: 90%;
            padding: 5px;
            margin-bottom: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .comment-submit {
            padding: 5px 10px;
            border: none;
            background: #007bff;
            color: white;
            cursor: pointer;
            border-radius: 5px;
        }

        .comment-icon {
            position: absolute;
            bottom: 10px;
            right: 10px;
            font-size: 20px;
            cursor: pointer;
        }
    </style>
</head>
<body>

<div class="header">
    <!-- Profile Section -->
    <div class="profile-container" onclick="toggleProfileDropdown(event)">
        <img src="<?php echo $_SESSION['profile_pic']; ?>" alt="Profile">
        <div class="profile-dropdown" id="profileDropdown">
            <a href="profile.php">👤 <?php echo $_SESSION['username']; ?></a>
            <a href="logout.php">🚪 Logout</a>
        </div>
    </div>

    <input type="text" id="searchBox" placeholder="Search images..." onkeyup="filterImages()">
</div>

<div class="gallery-container" id="gallery">
    <?php
    $images = [
        [
            "src" => "2023-03-13.jpg",
            "title" => "New Government Center",
            "description" => "A newly built government center providing various public services.",
            "apk_link" => "myunityapp://open?scene=gov_center"
        ],
        [
            "src" => "3.png",
            "title" => "Bureau of Internal Revenue (BIR)",
            "description" => "Handles tax collection and enforcement of tax laws in the country.",
            "apk_link" => "myunityapp://open?scene=bir_office"
        ],
        [
            "src" => "4.png",
            "title" => "Social Security System (SSS)",
            "description" => "Provides social security benefits to private sector employees and self-employed individuals.",
            "apk_link" => "myunityapp://open?scene=sss_office"
        ],
        [
            "src" => "5.png",
            "title" => "PhilHealth Office",
            "description" => "Manages the national health insurance program for Filipino citizens.",
            "apk_link" => "myunityapp://open?scene=philhealth"
        ],
        [
            "src" => "6.png",
            "title" => "City Health",
            "description" => "Offers medical and healthcare services for the local community.",
            "apk_link" => "myunityapp://open?scene=city_health"
        ]
    ];

    foreach ($images as $image) {
        echo '<div class="image-container">';
        echo '<a href="' . $image["apk_link"] . '">';
        echo '<img src="' . $image["src"] . '" alt="' . $image["title"] . '">';
        echo '</a>';
        echo '<h3>' . $image["title"] . '</h3>';
        echo '<p>' . $image["description"] . '</p>';
        echo '<span class="comment-icon" onclick="toggleCommentBox(this)">💬</span>';
        echo '<div class="comment-box">';
        echo '<input type="text" class="comment-input" placeholder="Add a comment...">';
        echo '<button class="comment-submit" onclick="submitComment(this)">Post</button>';
        echo '<div class="comment-list"></div>';
        echo '</div>';
        echo '</div>';
    }
    ?>
</div>

<script>
    function toggleCommentBox(icon) {
        let commentBox = icon.nextElementSibling;
        commentBox.style.display = (commentBox.style.display === "none" || commentBox.style.display === "") ? "block" : "none";
    }

    function submitComment(button) {
        let input = button.previousElementSibling;
        let username = "<?php echo $_SESSION['username']; ?>"; // Get username from PHP session
        if (input.value.trim() !== "") {
            let commentList = button.nextElementSibling;
            let newComment = document.createElement("p");
            newComment.innerHTML = `<strong>${username}:</strong> ${input.value}`;
            commentList.appendChild(newComment);
            input.value = "";
        }
    }

    function filterImages() {
        let input = document.getElementById("searchBox").value.toLowerCase();
        let images = document.querySelectorAll(".image-container");

        images.forEach(image => {
            let title = image.querySelector("h3").innerText.toLowerCase();
            image.style.display = title.includes(input) ? "block" : "none";
        });
    }

    function toggleProfileDropdown(event) {
        event.stopPropagation();
        let dropdown = document.getElementById("profileDropdown");
        dropdown.style.display = dropdown.style.display === "block" ? "none" : "block";
    }

    document.addEventListener("click", function() {
        document.getElementById("profileDropdown").style.display = "none";
    });
</script>

</body>
</html>
