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

        /* Removed comment and profile related styling */
    </style>
</head>
<body>
    <div class="header">
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
            echo '</div>';
        }
        ?>
    </div>

    <script>
        function filterImages() {
            let input = document.getElementById("searchBox").value.toLowerCase();
            let images = document.querySelectorAll(".image-container");

            images.forEach(image => {
                let title = image.querySelector("h3").innerText.toLowerCase();
                image.style.display = title.includes(input) ? "block" : "none";
            });
        }
    </script>
</body>
</html>
