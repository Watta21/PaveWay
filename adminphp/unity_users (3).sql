-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Mar 09, 2026 at 07:53 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unity_users`
--

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE `comments` (
  `id` int(11) NOT NULL,
  `image_name` varchar(100) NOT NULL,
  `comment` text NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `username` varchar(255) DEFAULT NULL,
  `archived` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comments`
--

INSERT INTO `comments` (`id`, `image_name`, `comment`, `created_at`, `username`, `archived`) VALUES
(174, 'img1', 'comment1', '2025-11-19 15:52:10', 'user1', 0),
(176, 'img3', 'comment3', '2025-11-19 15:52:10', 'user3', 0);

-- --------------------------------------------------------

--
-- Table structure for table `ratings`
--

CREATE TABLE `ratings` (
  `id` int(11) NOT NULL,
  `image_name` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ratings`
--

INSERT INTO `ratings` (`id`, `image_name`, `username`, `rating`) VALUES
(14, 'NGC', 'GianM', 1),
(15, 'BIR', 'GianM', 5),
(27, NULL, NULL, 5),
(39, NULL, NULL, 5),
(40, NULL, NULL, 5),
(41, NULL, NULL, 5),
(42, NULL, NULL, 5),
(43, NULL, NULL, 5),
(44, NULL, NULL, 5),
(82, NULL, NULL, 5),
(83, NULL, NULL, 5),
(84, 'NGC', 'user01', 3),
(85, 'NGC', 'user02', 3),
(86, 'NGC', 'user03', 3),
(87, 'NGC', 'user04', 3),
(88, 'NGC', 'user05', 3),
(89, 'NGC', 'user06', 3),
(90, 'NGC', 'user07', 3),
(91, 'NGC', 'user08', 3),
(92, 'NGC', 'user09', 3),
(93, 'NGC', 'user10', 3),
(94, 'NGC', 'user11', 3),
(95, 'NGC', 'user12', 3),
(96, 'NGC', 'user13', 3),
(97, 'NGC', 'user14', 3),
(98, 'NGC', 'user15', 3),
(99, 'NGC', 'user16', 3),
(100, 'NGC', 'user17', 3),
(101, 'NGC', 'user18', 3),
(102, 'NGC', 'user19', 3),
(103, 'NGC', 'user20', 3),
(104, 'NGC', 'user21', 3),
(105, 'NGC', 'user22', 3),
(106, 'NGC', 'user23', 3),
(107, 'NGC', 'user24', 3),
(108, 'NGC', 'user25', 3),
(109, 'NGC', 'user26', 3),
(110, 'NGC', 'user27', 3),
(111, 'NGC', 'user28', 3),
(112, 'NGC', 'user29', 3),
(113, 'NGC', 'user30', 5),
(129, 'PHILHEALTH', 'GianM', 5),
(130, 'CITYHEALTH', 'GianM', 5);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `profile_pic` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  `status` varchar(50) DEFAULT NULL,
  `last_seen` datetime DEFAULT NULL,
  `last_activity` datetime DEFAULT NULL,
  `archived` tinyint(1) DEFAULT 0,
  `reset_token` varchar(255) DEFAULT NULL,
  `reset_expires` datetime DEFAULT NULL,
  `reset_token_expires` datetime DEFAULT NULL,
  `role` varchar(50) DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `email`, `password`, `profile_pic`, `created_at`, `status`, `last_seen`, `last_activity`, `archived`, `reset_token`, `reset_expires`, `reset_token_expires`, `role`) VALUES
(13, 'hi', 'hi@gmail.com', '$2y$10$.AqJ30W3MGYpSTZF0l.dt.xD0G3Sf7MvgNKbfyKA4iHH.QbB2HZeG', NULL, '2025-08-25 23:00:18', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(14, 'hiw', 'hiw@gmail.com', '$2y$10$ikBk7Txt7Dx4gJ3iNJE1guECQQNTP5F4v6jNpuJYzzh2PBFjHavsK', NULL, '2025-08-25 23:14:51', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(15, 'mee', 'mee@gmail.com', '$2y$10$KbkBEtQ1j5u0ItH.4R0fzOuNlAT..c4mQhXEz14M1JU/iSsJb7pzW', NULL, '2025-08-26 00:14:39', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(16, 'wt', 'wt@gmail.com', '$2y$10$/UxVWqAtWK9Z/BulHQO79OuLlBJFVSam4bI7gzymObB6aa6RUXtE6', NULL, '2025-08-26 15:05:23', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(17, 'wd', 'wd@gmail.com', '$2y$10$yA4jwn3RVI/zph5sfeRO1ew0n1aYGL4Lw9/bV1HloFtzlJfx5Aope', NULL, '2025-08-26 15:06:57', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(18, 'me', 'me@gmail.com', '$2y$10$7X4ousOEX2uYcW.VLjIdKexZooWvsIcbYCHhP5T9w0Lta65n2DMqC', NULL, '2025-08-26 17:54:21', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(19, 'bahobuli', 'bahu@gmail.com', '$2y$10$CdCkrOxUK1JUMECU/ezpau1ANXYKJ80YT/gO60J36zHhiSRWIQJUK', NULL, '2025-08-26 17:54:49', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(20, 'mer', 'mer@gmai.com', '$2y$10$zs3BqKPeN9kgoxij44PGOe62d08oDvF.PvsWO6ZrN5c1fYx6PXye2', NULL, '2025-09-09 16:58:39', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(21, 'mers', 'mers@gmail.com', '$2y$10$C.0GaL/cOOLRFc8lfaAvfuZSSHKmGLlpcSU1k6VDeE6QOO1N1kdCi', NULL, '2025-09-09 16:59:21', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(22, 'sheena', 'sheena@gmail.com', '$2y$10$rnH9DGMTDfzxeJSk9UiSe.SSxE4k6w0pXEUyKLFRY/W2bz5sD7AKW', NULL, '2025-09-10 10:18:19', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(23, 'mww', 'mww@gmail.com', '$2y$10$FVg4qASSjGLe0ZV9Uu11BOrWD94Vsxrp7qBD3BxM3XE//TF4Eng36', NULL, '2025-09-14 15:30:02', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(24, 'mera@gmail.com', 'mera@gmail.com', '$2y$10$JT26MKbrIw6okFs2IiesRufeJSbQAV/E3aLO3Juld4LD5SlQBUXYa', NULL, '2025-09-14 16:07:45', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(25, 'era', 'era@gmail.com', '$2y$10$N8PUjLwDDMYetx2jyGENf.bF.bzvwMQVjX.9wMhAInxcf0g/KxUTK', NULL, '2025-09-14 16:27:01', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(26, 'erass', 'erass@gmail.com', '$2y$10$1tK6rUC.UxSatZqWGEwaC.tboW4QUuoHa/ITC0lZONiR7lP3WpV..', NULL, '2025-09-14 16:35:54', NULL, NULL, '2025-09-18 01:15:55', 1, NULL, NULL, NULL, 'user'),
(27, 'carasmall', 'carasmall@gmail.com', '$2y$10$WdIZDJsNgOceJwLfc/eBEOtkwz8Tk68VpAdkR5wwgcKpetdsU3mmS', NULL, '2025-09-14 19:10:50', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(28, 'jam', 'jam@gmail.com', '$2y$10$pOcIvkQtE1sZ21zSQjFdGu5wvKMYAqHQvTaD1u8x8Q2NREpx8BBZC', NULL, '2025-09-14 19:35:22', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(29, 'ako', 'ako@gmail.com', '$2y$10$mZ4atbZIqasz0cZnbAhWCeMgPME2vQF2UTHoXUTz0L83/J9/d4rrG', NULL, '2025-09-15 21:19:09', NULL, NULL, NULL, 1, '7f0ea861945b9f8c14bd5ea5aa4ce16378acbc674540d39f10f3c7f4990ec3ce', NULL, '2025-09-17 19:29:39', 'user'),
(30, 'qwert', 'qwert@gmail.com', '$2y$10$a7aQxOgETvAJdq55XCwaMOk1qf7YNibpOzRbpHGoWVNuE11VQjF8.', NULL, '2025-09-17 15:39:19', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(31, 'akos', 'akos@gmail.com', '$2y$10$.75cOtQC0FfOE9fpIQ2toeAi5hus7F7NJ.xwYC8f.y4Ccb733AbBu', NULL, '2025-09-17 20:38:07', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(32, 'where', 'where@gmail.com', '$2y$10$s15tWNhhZ4iUrsWnfb/YdeRGvB7EM6athMAlYeVqRQxM3pyQLVyHi', NULL, '2025-09-17 20:40:26', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(33, 'Admin', 'admin@gmail.com', '$2y$10$mV.HbjUeP9cSDUlJRYjRDeUMBVtqHVzuYloFEi0.6Eo0KNC9JCKqS', NULL, '2025-09-18 00:43:31', 'active', NULL, NULL, 1, NULL, NULL, NULL, 'admin'),
(34, 'Admina', 'admina@gmail.com', '$2y$10$fZTk8W/EgwWvYz3c8dQebeaWWOosG5HDr35rFK3fp4HcPNiPfQkIC', NULL, '2025-09-18 00:44:23', 'active', NULL, NULL, 1, NULL, NULL, NULL, 'admin'),
(35, 'akoo', 'akoo@gmail.com', '$2y$10$aJR9p3SPEjqrguYZcp3jc.w/GNDAxzw9ildKKHdom/v1CX6mEPD/G', NULL, '2025-09-18 00:45:43', 'active', NULL, '2025-09-18 01:24:43', 1, 'f0b2b55542aa0757b99eeceeba2ae65980fe81dab5d8b52d75ec13a5058c69df', NULL, '2025-11-19 18:55:27', 'admin'),
(36, 'qw', 'qw@gmail.com', '$2y$10$37capGfMI.S0tI0Ygy/RdOt51mDd8o/hd17wBqGeSm7I29UX6tsl.', NULL, '2025-09-18 01:34:56', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(37, 'as', 'as@gmail.com', '$2y$10$ELf12rE9hmHyqcDS3sdgueoIvdBIggN1fmeYkuGCv3Wwpc4B5auw2', NULL, '2025-09-18 11:49:01', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(40, 'JAY', 'soberanobejau@gmail.com', '$2y$10$6tdSnSizZTnBy7k5tOFIfO5TK1pby1i1BqbN3o5ki77fAVtBnZF9e', NULL, '2025-09-18 17:29:04', NULL, NULL, NULL, 1, '586651a8a4d32c07fe41343d2a39dd438f215e4f0737f5e608d094421dd3a793', NULL, '2025-09-18 12:29:54', 'user'),
(46, 'di', 'hahaha@gmail.com', '$2y$10$7vQANrPCE8Ezbkka13QTLO2qT/dzXL4m4rfOfQwndOCYZMZpYfYEC', NULL, '2025-09-18 18:40:33', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(47, 'ggg', 'gg@gmail.com', '$2y$10$yZTFJIDyRrI/iYbi9ERnOOhQTNazXbtZuaMPMBZEUgi63vtKl3GoG', NULL, '2025-09-18 18:42:32', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(48, 'ff', 'ff@gmail.com', '$2y$10$d5Zso6C.1T8I2yVsBV/y5.2Xe1iofyOAIc5mJZAo7ubl2VRvsqraq', NULL, '2025-09-18 18:43:42', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(49, 'ffd', 'fffd@gmail.com', '$2y$10$saUxSKjKv.EqdDMTwdhoGetON7DtCr7s4mA1MgY8.qGLGu7QYvVj.', NULL, '2025-09-18 18:45:14', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(50, 'ffdd', 'fffdd@gmail.com', '$2y$10$Ngj5OGcB36qT6VM52FOXDedQysxU9QQcIZhKM81za5dUG.RsO/61C', NULL, '2025-09-18 18:45:34', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(51, 'ffddd', 'fffddd@gmail.com', '$2y$10$Wx.tS2s0LjGe1LU8hLLWFeR1YTBZL7164uqpdM957YMY.7zwlWCWe', NULL, '2025-09-18 18:46:04', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(52, 'fd', 'fd@gmail.com', '$2y$10$z/L7Mxka9yBfaVNbEgcen.lamgbFv48OIkanmV1l6BqSCgKElUJ3W', NULL, '2025-09-18 18:49:00', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(53, 'sof', 'sof@gmail.com', '$2y$10$5Sy2CFp4HMmlYrmYbt63RuUGUDcbfsWMpt2VaIsHl8FH7eVl6qk6W', NULL, '2025-09-18 18:49:40', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(55, 'f', 'f@gmail.com', '$2y$10$GTB6r6eObXoM0ID8f0/zKeR3ePOII.CYumKMfk./9HFzOsGI//n/y', NULL, '2025-09-18 18:58:51', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(56, 'col', 'cool4cool456@gmail.com', '$2y$10$SnMU..pT934VtxxMAU00QOqkgo8M8rogBRj0Y.k5qCdsc0MgwFISK', NULL, '2025-09-18 19:00:29', NULL, NULL, NULL, 0, 'e0460b8266287ac3293c59fca46809d9fc64ee509f519281da90c2a0d7302bbe', NULL, '2025-11-19 18:55:40', 'user'),
(57, 'sheena16', 'sheena16@gmail.com', '$2y$10$HtsGhPLIzMy0t9MfktEf3uNZc/.qHBCobl/kODF6vapsUSyJDhbZe', NULL, '2025-09-22 11:23:07', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(58, 'abogandagian', 'abogandagian0@gmail.com', '$2y$10$Tbd12mCknbvp5kBI5uGY8O93ZVLlydth5GIP9/Ef5ZvnvhWP1xUH2', NULL, '2025-10-13 21:55:54', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(59, 'Admin', 'akoosa@gmail.com', '$2y$10$eNTEagW1J9YdBJeaTrGf5uogDQchYfT2XFADJ3NRXIb80XWm/0sCq', NULL, '2025-10-17 01:11:33', 'active', NULL, NULL, 0, NULL, NULL, NULL, 'admin'),
(60, 'czcz', 'zczc', '', NULL, '2025-10-17 01:20:58', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(61, 'czczc', 'czc', '', NULL, '2025-10-17 01:21:38', NULL, NULL, NULL, 0, 'sczs', NULL, NULL, 'user'),
(62, '', '', '', NULL, '2025-10-17 01:21:38', NULL, NULL, NULL, 0, 'czsc', NULL, NULL, 'user'),
(63, 'dqdqd', 'qdqd', 'dqwd', 'qdq', '2025-10-17 01:30:29', NULL, NULL, NULL, 0, 'dqwdq', NULL, NULL, 'user'),
(64, 'dqwdq', '', '', NULL, '2025-10-17 01:30:29', NULL, NULL, NULL, 0, 'wdqd', NULL, NULL, 'user'),
(65, 'dqdq', '', 'dqwdd', NULL, '2025-10-17 01:31:14', NULL, NULL, NULL, 1, 'dqd', NULL, NULL, 'user'),
(66, '', 'dqd', '', NULL, '2025-10-17 01:31:14', NULL, NULL, NULL, 1, 'dqd', NULL, NULL, 'user'),
(67, 'sdfsdfs', '', 'sfsff', NULL, '2025-10-17 01:39:15', NULL, NULL, NULL, 0, 'fsfs', NULL, NULL, 'user'),
(68, 'fs', '', '', NULL, '2025-10-17 01:39:15', NULL, NULL, NULL, 0, 'sff', NULL, NULL, 'user'),
(69, 'hirt', 'badillesjodilyn@gmail.com', '$2y$10$zravhCiafoROEgN.lsf33uqTeAY5MoG8oSR.hfskmhXgJvjWCc2/q', NULL, '2025-10-18 19:38:49', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(70, 'Admin', 'bulimo@gmail.com', '$2y$10$tg36RPuVifG86p3F9fQRfeCVfaAsDfyUXgnUSwjQ/cnMQqNJeMl.W', NULL, '2025-10-25 00:33:00', 'active', NULL, NULL, 0, NULL, NULL, NULL, 'admin'),
(71, 'que', 'que@gmail.com', '$2y$10$R4liOfDa1mOlg6n2pUwG4O52hyuTWKgwdd./a9QisBmoTHBp5ScTq', NULL, '2025-10-26 00:27:48', 'active', NULL, '2025-11-13 17:00:46', 1, 'db46a14dab340c9f4709d948525312276bbb1556760ac189cbcb3c779b13e783', NULL, '2025-11-19 18:31:20', 'admin'),
(72, 'aboganda', 'abogandaka@gmail.com', '$2y$10$6fKJe57EGvD34ZpUEJPfi.xpg865uM8asuY7Wkn7WOwUP.QT.TtIm', NULL, '2025-10-26 18:07:19', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(73, 'dewata', 'dewata@gmail.com', '$2y$10$T39ZSvWS/kk9dMxP.MmZuOf3E7/76FIrx5FC0SkAjQfrd1LRg4flC', NULL, '2025-10-26 19:45:01', NULL, NULL, '2025-10-26 20:24:34', 1, 'ddfdc3473c1ab01b7a403a8b2603f1772fc0b7fa8c8ba8a0936e40e85fa957b4', NULL, '2025-10-26 14:08:54', 'user'),
(74, 'meyou', 'meeyou@outlook.com', '$2y$10$fjnvwzSFwlHKacO53co/Q.YSPFMyBRzAvv0TPmlrZcLcF4ZIZPQPm', NULL, '2025-10-26 20:10:51', NULL, NULL, NULL, 0, NULL, NULL, NULL, 'user'),
(75, 'FYVYGVYH', 'VH', 'J', 'H', '2025-11-14 19:31:42', NULL, NULL, NULL, 1, NULL, NULL, NULL, 'user'),
(76, 'akoka', 'yushanen@paveway.com', '$2y$10$zGKXqzM4DFF9soPh00PI1OSY6PAvVbhRUc5uOEwCeq5qy3TJ5M8aK', NULL, '2025-11-20 00:03:03', 'active', NULL, NULL, 0, NULL, NULL, NULL, 'admin'),
(77, 'akoka', 'yushanena@paveway.com', '$2y$10$Cah9MfxutFnF2klomOvOguOYJEZDbi2aNnSFNQ8rkygVMAACbePYW', NULL, '2025-11-20 00:03:56', 'active', NULL, NULL, 0, NULL, NULL, NULL, 'admin'),
(78, 'akokaka', 'yushanenn@paveway.com', '$2y$10$bxd4PqSOKbHAA3o7iVRBf.xOFPHivGYRGrkTVPE9k7Cx.rvUmrApG', NULL, '2025-11-20 00:11:01', 'active', NULL, NULL, 0, NULL, NULL, NULL, 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `user_image_state`
--

CREATE TABLE `user_image_state` (
  `id` int(11) NOT NULL,
  `username` varchar(100) NOT NULL,
  `image_id` varchar(100) NOT NULL,
  `visible_step` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `comments`
--
ALTER TABLE `comments`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `ratings`
--
ALTER TABLE `ratings`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `unique_user_image` (`image_name`,`username`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user_image_state`
--
ALTER TABLE `user_image_state`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `comments`
--
ALTER TABLE `comments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=204;

--
-- AUTO_INCREMENT for table `ratings`
--
ALTER TABLE `ratings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=131;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=79;

--
-- AUTO_INCREMENT for table `user_image_state`
--
ALTER TABLE `user_image_state`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
