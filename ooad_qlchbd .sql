-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: db
-- Thời gian đã tạo: Th1 13, 2022 lúc 01:32 AM
-- Phiên bản máy phục vụ: 8.0.27
-- Phiên bản PHP: 7.4.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `ooad_qlchbd`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `album`
--

CREATE TABLE `album` (
  `id` bigint NOT NULL,
  `name` varchar(200) NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `album`
--

INSERT INTO `album` (`id`, `name`, `create_time`, `update_time`) VALUES
(1, 'Adventure', '2021-12-13 04:11:39', '2021-12-13 04:11:39'),
(2, 'Horror', '2021-12-13 04:11:39', '2021-12-13 04:11:39'),
(3, 'Comedy', '2021-12-13 04:11:39', '2021-12-13 04:11:39'),
(4, 'Action', '2021-12-13 04:11:39', '2021-12-13 04:11:39');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `disk`
--

CREATE TABLE `disk` (
  `id` bigint NOT NULL,
  `name` varchar(200) NOT NULL,
  `album` bigint NOT NULL,
  `quantity` int NOT NULL,
  `image` varchar(200) NOT NULL,
  `locate` varchar(200) NOT NULL,
  `checked` tinyint(1) NOT NULL,
  `rental_price` int NOT NULL,
  `provider` bigint NOT NULL,
  `id_by_provider` bigint NOT NULL,
  `loss_charges` int NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `update_by` bigint NOT NULL,
  `rented` int NOT NULL,
  `publishing_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `disk`
--

INSERT INTO `disk` (`id`, `name`, `album`, `quantity`, `image`, `locate`, `checked`, `rental_price`, `provider`, `id_by_provider`, `loss_charges`, `create_time`, `update_time`, `create_by`, `update_by`, `rented`, `publishing_date`) VALUES
(4, 'Squid game', 2, 25, 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 'Hàng 5', 1, 5000, 1, 18, 200000, '2021-10-21 06:49:07', '2022-01-12 10:02:56', 1, 1, 9, '2021-10-19 00:00:00'),
(5, 'Squid game part 2', 2, 23, 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 'Hàng 4', 1, 80000, 1, 18, 200000, '2021-10-21 06:49:33', '2021-10-21 06:49:33', 1, 1, 4, '2021-10-19 09:11:36'),
(6, 'Power Ranger', 1, 34, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP1PIgSOWpxk8VGj9_zSuRDaUGpNEIBmozO5ZnEE8Rhhi-GjzfFufjpXt3zTnAsco6-i0&usqp=CAU', 'Hàng 4', 1, 50000, 1, 9, 100000, '2021-10-21 07:09:18', '2021-10-21 07:09:18', 1, 1, 20, '2021-10-19 09:11:36'),
(7, 'Power Ranger part 2', 1, 34, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP1PIgSOWpxk8VGj9_zSuRDaUGpNEIBmozO5ZnEE8Rhhi-GjzfFufjpXt3zTnAsco6-i0&usqp=CAU', 'Hàng 4', 1, 50000, 1, 9, 100000, '2021-10-21 07:09:35', '2021-10-21 07:09:35', 1, 1, 0, '2021-10-19 09:11:36'),
(8, 'Bố già', 1, 34, 'https://media.vov.vn/sites/default/files/styles/large/public/2021-03/290ty.jpg', 'Hàng 5', 1, 50000, 1, 12, 200000, '2021-10-22 10:46:48', '2021-10-22 10:46:48', 1, 1, 0, '2021-10-19 09:11:36'),
(9, 'Neverland', 1, 20, 'https://truyenz.info/wp-content/uploads/2018/07/The-Promised-Neverland-Mi%E1%BB%81n-%C4%90%E1%BA%A5t-H%E1%BB%A9a-1.jpg', 'hàng 7', 1, 20000, 1, 12, 20000, '2021-10-22 13:55:28', '2021-10-22 13:55:28', 1, 1, 10, '2021-10-19 09:11:36'),
(10, 'Us', 2, 15, 'https://www.indiewire.com/wp-content/uploads/2019/12/us-1.jpg?w=758', 'Hàng 8', 1, 50000, 1, 15, 500000, '2021-10-22 19:08:57', '2021-10-22 19:08:57', 1, 1, 0, '2021-10-19 09:11:36'),
(11, 'Courage Dog', 2, 12, 'https://www.indiewire.com/wp-content/uploads/2019/12/us-1.jpg?w=758', 'Hàng 9', 1, 5000, 1, 54, 50000, '2021-10-22 19:10:55', '2021-10-22 19:10:55', 1, 1, 0, '2021-10-19 09:11:36'),
(12, 'Ben 10', 1, 23, 'https://www.indiewire.com/wp-content/uploads/2019/12/us-1.jpg?w=758', 'Ben 10', 1, 12000, 1, 56, 120000, '2021-10-22 19:12:18', '2021-10-22 19:12:18', 1, 1, 0, '2021-10-19 09:11:36'),
(15, 'King of Rings', 1, 11, 'D:/OOAD/QLCHBD-OOAD/QLCHBD-OOAD/Assets/disk_263637147.jpg', 'Hàng 6', 1, 12000, 1, 17, 200000, '2021-10-22 15:29:04', '2022-01-13 01:26:11', 1, 1, 0, '2021-10-19 00:00:00'),
(18, 'viễn phương 2', 3, 7, 'D:/OOAD/QLCHBD-OOAD/QLCHBD-OOAD/Assets/disk_268267121.jpg', '1', 1, 1, 1, 1, 1, '2021-12-20 15:28:17', '2022-01-13 01:26:26', 1, 1, 0, '2021-12-04 00:00:00'),
(24, 'Con hổ rừng tanga', 1, 5, 'https://th.bing.com/th/id/OIP.NO9b4begR7atKn_UUylIUQHaE6?pid=ImgDet&rs=1', 'Tầng 9', 1, 5000, 3, 587, 50000, '2022-01-12 13:52:13', '2022-01-12 13:53:30', 1, 1, 0, '2022-01-12 00:00:00');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `guest`
--

CREATE TABLE `guest` (
  `id` bigint NOT NULL,
  `cmnd_cccd` char(12) NOT NULL,
  `address` varchar(200) NOT NULL,
  `birth_date` date NOT NULL,
  `name` varchar(200) NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL DEFAULT '1',
  `update_by` bigint NOT NULL DEFAULT '1',
  `membership` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `guest`
--

INSERT INTO `guest` (`id`, `cmnd_cccd`, `address`, `birth_date`, `name`, `create_time`, `update_time`, `create_by`, `update_by`, `membership`) VALUES
(1, '012345678910', 'Ấp 7, Tân Bửu, Bến Lức, Long An', '2001-07-13', 'Phạm Minh Tân', '2021-10-12 14:19:58', '2021-10-12 14:19:58', 1, 1, 1),
(2, '012345678911', 'Ấp 7, Tân Bửu, Bến Lức, Tiền Giang', '2001-07-14', 'Phạm Minh Huy', '2021-10-12 14:21:09', '2021-10-12 14:21:09', 1, 1, 1),
(3, '012345678912', '026/B, Long Bình, Long Kim, Cần Thơ', '2001-07-14', 'Phạm Lê Tường Vy', '2021-10-12 14:21:09', '2021-10-12 14:21:09', 1, 1, 1),
(4, '123456789', 'Vĩnh Hưng, Long An', '2004-10-06', 'Bùi Dương Duy Khang', '2021-11-10 19:50:01', '2021-11-10 19:50:01', 1, 1, 1),
(5, '829838301', 'Đường số 4, quận 9, thành phố, Hồ Chí Minh', '1997-08-09', 'Lương Ngọc Anh', '2021-11-11 09:45:31', '2021-11-11 09:45:31', 1, 1, 1),
(6, '187329841', 'Cao Bằng', '2005-01-01', 'Nguyễn Văn A', '2021-11-11 10:02:37', '2021-11-11 10:02:37', 1, 1, 1),
(7, '103847395739', 'Tây Ninh', '2005-01-01', 'Nguyễn Văn B', '2021-11-11 10:04:26', '2021-11-11 10:04:26', 1, 1, 0),
(8, '198376478982', 'Thất sơn bảy núi', '1997-07-11', 'Nguyễn Văn C', '2021-11-11 10:08:25', '2021-11-11 10:08:25', 1, 1, 1),
(9, '198375902', 'Long Thành', '2005-01-01', 'Phạm Minh Huy Hùng', '2021-11-11 10:18:27', '2021-11-11 10:18:27', 1, 1, 0),
(10, '193847038', 'Long Kim', '2004-09-18', 'Phạm Minh Nhật', '2021-11-11 10:19:55', '2021-11-11 10:19:55', 1, 1, 1),
(11, '837294729', 'Tân Bửu, Bến Lức, Long An', '2004-12-02', 'Phạm Quang Nhật', '2021-11-11 10:22:46', '2021-11-11 10:22:46', 1, 1, 1),
(12, '219284720384', 'Cần Thơ', '2005-01-01', 'Nguyễn Văn D', '2021-11-11 10:24:49', '2021-11-11 10:24:49', 1, 1, 1),
(13, '927493829', 'Tây Ninh', '2005-01-01', 'Nguyễn Văn E', '2021-11-11 10:39:00', '2021-11-11 10:39:00', 1, 1, 1),
(22, '123456785', 'Tân Bửu, Long An', '2000-04-07', 'Phạm Minh Tân', '2021-11-14 08:18:22', '2021-11-14 08:18:22', 1, 1, 0),
(23, '325845626', 'Long An', '2005-01-01', 'Bui Duong Duy', '2022-01-12 15:16:48', '2022-01-12 15:16:48', 5, 1, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `import_bill`
--

CREATE TABLE `import_bill` (
  `id` bigint NOT NULL,
  `import_form_id` bigint NOT NULL,
  `provider_name` varchar(200) NOT NULL,
  `payment_date` datetime DEFAULT NULL,
  `sum_amount` int NOT NULL DEFAULT '0',
  `sum_value` int NOT NULL DEFAULT '0',
  `status` enum('PAID','UNPAID') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'UNPAID',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `update_by` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `import_bill`
--

INSERT INTO `import_bill` (`id`, `import_form_id`, `provider_name`, `payment_date`, `sum_amount`, `sum_value`, `status`, `create_time`, `update_time`, `create_by`, `update_by`) VALUES
(1, 1, 'HBO', '2021-12-25 18:44:32', 20, 500000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(2, 2, 'VTV', '2021-12-21 14:30:38', 10, 250000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(3, 3, 'BIDA', '2021-12-21 14:30:38', 20, 500000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(4, 4, 'NetPlix', '2021-12-21 14:30:38', 20, 500000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(5, 5, 'HBO', '2021-12-21 14:30:38', 10, 250000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(6, 6, 'VTV', '2021-12-21 14:30:38', 10, 250000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(7, 7, 'BIDA', '2021-12-21 14:30:38', 10, 250000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(8, 8, 'BIDA', '2021-12-21 14:30:38', 20, 500000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(9, 9, 'NetPlix', '2021-12-21 14:30:38', 20, 500000, 'PAID', '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1),
(297840152, 1565270126, 'HBO', '2022-01-05 10:45:30', 1233, 3123, 'PAID', '2022-01-05 03:31:49', '2022-01-05 03:31:49', 1, 1),
(340793651, 7, 'BIDA', NULL, 0, 100000, 'UNPAID', '2022-01-05 02:55:25', '2022-01-05 02:55:25', 1, 1),
(417862788, 3, 'BIDA', NULL, 1312, 100000, 'UNPAID', '2022-01-05 02:59:11', '2022-01-05 02:59:11', 1, 1),
(814704700, 1028471133, 'HBO', NULL, 345, 123, 'UNPAID', '2022-01-05 03:18:53', '2022-01-05 03:18:53', 1, 1),
(847784185, 210134741, 'HBO', '2022-01-05 10:47:21', 123123, 21313, 'PAID', '2022-01-05 03:23:17', '2022-01-05 03:23:17', 1, 1),
(899864608, 428602130, 'HBO', '2022-01-05 10:44:56', 0, 123123, 'PAID', '2022-01-05 03:35:42', '2022-01-05 03:35:42', 1, 1),
(1032474109, 1565270127, 'BIDA', '2022-01-12 09:59:26', 5, 500000, 'PAID', '2022-01-12 09:59:23', '2022-01-12 09:59:23', 1, 1),
(1136555456, 926023312, 'HBO', NULL, 0, 345, 'UNPAID', '2022-01-05 03:19:11', '2022-01-05 03:19:11', 1, 1),
(1331827671, 5, 'HBO', NULL, 234, 100000, 'UNPAID', '2022-01-05 03:19:51', '2022-01-05 03:19:51', 1, 1),
(1402118795, 6, 'VTV', NULL, 369, 100000, 'UNPAID', '2022-01-05 02:56:24', '2022-01-05 02:56:24', 1, 1),
(1413403462, 1565270126, 'HBO', '2022-01-05 10:45:30', 1234, 3123, 'PAID', '2022-01-05 03:27:21', '2022-01-05 03:27:21', 1, 1),
(1493632629, 1054410771, 'HBO', '2022-01-12 09:57:47', 5, 50000, 'PAID', '2022-01-12 09:57:44', '2022-01-12 09:57:44', 1, 1),
(1588713426, 9, 'NetPlix', NULL, 123, 100000, 'UNPAID', '2022-01-05 03:19:30', '2022-01-05 03:19:30', 1, 1),
(1699200484, 1009729040, 'BIDA', '2022-01-05 10:45:23', 123123, 123132, 'PAID', '2022-01-05 03:20:52', '2022-01-05 03:20:52', 1, 1),
(1831288229, 8, 'BIDA', NULL, 56767, 100000, 'UNPAID', '2022-01-05 03:03:38', '2022-01-05 03:03:38', 1, 1),
(1846690725, 1065655860, 'BIDA', '2022-01-12 13:52:15', 5, 50000, 'PAID', '2022-01-12 13:52:13', '2022-01-12 13:52:13', 1, 1),
(1916359933, 988081386, 'HBO', '2022-01-07 17:31:32', 4444, 346, 'PAID', '2022-01-07 10:31:31', '2022-01-07 10:31:31', 1, 1),
(2034886228, 1413821807, 'HBO', '2022-01-07 17:34:54', 10, 10000, 'PAID', '2022-01-07 10:34:52', '2022-01-07 10:34:52', 1, 1),
(2037805466, 1, 'HBO', '2021-12-25 18:44:32', 3832, 100000, 'PAID', '2021-12-25 11:44:23', '2021-12-25 11:44:23', 1, 1),
(2054822804, 1565270128, 'BIDA', '2022-01-12 10:00:57', 5, 300000, 'PAID', '2022-01-12 10:00:55', '2022-01-12 10:00:55', 1, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `import_billing_item`
--

CREATE TABLE `import_billing_item` (
  `id` bigint NOT NULL,
  `import_bill` bigint NOT NULL,
  `disk_name` varchar(200) NOT NULL,
  `import_price` int NOT NULL,
  `disk_id` bigint DEFAULT NULL,
  `amount` int DEFAULT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `import_billing_item`
--

INSERT INTO `import_billing_item` (`id`, `import_bill`, `disk_name`, `import_price`, `disk_id`, `amount`, `create_time`, `update_time`) VALUES
(2, 2037805466, 'Among Ú', 25000, 51, 0, '2021-12-25 11:44:23', '2021-12-25 11:44:23'),
(3, 2037805466, 'Alibaba And Bicycle', 25000, 52, 3453, '2021-12-25 11:44:23', '2021-12-25 11:44:23'),
(4, 2037805466, 'Alone In The Beach', 25000, 53, 34, '2021-12-25 11:44:23', '2021-12-25 11:44:23'),
(5, 2037805466, 'Living Dev', 25000, 54, 345, '2021-12-25 11:44:23', '2021-12-25 11:44:23'),
(6, 340793651, 'Among Ú', 25000, 121, 0, '2022-01-05 02:55:25', '2022-01-05 02:55:25'),
(7, 340793651, 'Alibaba And Bicycle', 25000, 122, 0, '2022-01-05 02:55:25', '2022-01-05 02:55:25'),
(8, 340793651, 'Alone In The Beach', 25000, 23, 0, '2022-01-05 02:55:25', '2022-01-05 02:55:25'),
(9, 340793651, 'Living Dev', 25000, 24, 0, '2022-01-05 02:55:25', '2022-01-05 02:55:25'),
(10, 1402118795, 'Among Ú', 25000, 111, 123, '2022-01-05 02:56:24', '2022-01-05 02:56:24'),
(11, 1402118795, 'Alibaba And Bicycle', 25000, 112, 123, '2022-01-05 02:56:25', '2022-01-05 02:56:25'),
(12, 1402118795, 'Alone In The Beach', 25000, 13, 0, '2022-01-05 02:56:25', '2022-01-05 02:56:25'),
(13, 1402118795, 'Living Dev', 25000, 14, 123, '2022-01-05 02:56:25', '2022-01-05 02:56:25'),
(14, 417862788, 'Among Ú', 25000, 71, 0, '2022-01-05 02:59:41', '2022-01-05 02:59:41'),
(15, 417862788, 'Alibaba And Bicycle', 25000, 72, 1234, '2022-01-05 03:00:07', '2022-01-05 03:00:07'),
(16, 417862788, 'Alone In The Beach', 25000, 73, 34, '2022-01-05 03:01:34', '2022-01-05 03:01:34'),
(17, 417862788, 'Living Dev', 25000, 74, 44, '2022-01-05 03:01:42', '2022-01-05 03:01:42'),
(18, 1831288229, 'Among Ú', 25000, 121, 0, '2022-01-05 03:03:38', '2022-01-05 03:03:38'),
(19, 1831288229, 'Alibaba And Bicycle', 25000, 122, 0, '2022-01-05 03:03:38', '2022-01-05 03:03:38'),
(20, 1831288229, 'Alone In The Beach', 25000, 123, 56767, '2022-01-05 03:03:38', '2022-01-05 03:03:38'),
(21, 1831288229, 'Living Dev', 25000, 124, 0, '2022-01-05 03:03:38', '2022-01-05 03:03:38'),
(22, 814704700, '123', 123, 123, 345, '2022-01-05 03:18:53', '2022-01-05 03:18:53'),
(23, 1136555456, '3435', 345, 345, 0, '2022-01-05 03:19:11', '2022-01-05 03:19:11'),
(24, 1588713426, 'Among Ú', 25000, 131, 0, '2022-01-05 03:19:30', '2022-01-05 03:19:30'),
(25, 1588713426, 'Alibaba And Bicycle', 25000, 132, 0, '2022-01-05 03:19:30', '2022-01-05 03:19:30'),
(26, 1588713426, 'Alone In The Beach', 25000, 133, 123, '2022-01-05 03:19:30', '2022-01-05 03:19:30'),
(27, 1588713426, 'Living Dev', 25000, 134, 0, '2022-01-05 03:19:30', '2022-01-05 03:19:30'),
(28, 1331827671, 'Among Ú', 25000, 91, 234, '2022-01-05 03:19:51', '2022-01-05 03:19:51'),
(29, 1331827671, 'Alibaba And Bicycle', 25000, 92, 0, '2022-01-05 03:19:54', '2022-01-05 03:19:54'),
(30, 1331827671, 'Alone In The Beach', 25000, 93, 0, '2022-01-05 03:19:54', '2022-01-05 03:19:54'),
(31, 1331827671, 'Living Dev', 25000, 94, 0, '2022-01-05 03:19:54', '2022-01-05 03:19:54'),
(32, 1699200484, '213qweqw', 123132, 123213, 123123, '2022-01-05 03:20:52', '2022-01-05 03:20:52'),
(33, 847784185, 'wqeqw', 21313, 123, 123123, '2022-01-05 03:23:58', '2022-01-05 03:23:58'),
(34, 1413403462, '12312', 3123, 123, 1234, '2022-01-05 03:27:21', '2022-01-05 03:27:21'),
(35, 297840152, '12312', 3123, 123, 1233, '2022-01-05 03:31:49', '2022-01-05 03:31:49'),
(36, 899864608, '123', 123123, 123, 0, '2022-01-05 03:35:42', '2022-01-05 03:35:42'),
(37, 1916359933, 'ewrw', 346, 53225, 4444, '2022-01-07 10:31:31', '2022-01-07 10:31:31'),
(38, 2034886228, '213', 10000, 123, 10, '2022-01-07 10:34:52', '2022-01-07 10:34:52'),
(39, 1493632629, 'Test disk', 50000, 7, 5, '2022-01-12 09:57:44', '2022-01-12 09:57:44'),
(40, 1032474109, 'Squid game', 200000, 18, 0, '2022-01-12 09:59:23', '2022-01-12 09:59:23'),
(41, 1032474109, 'Squid game part 2', 200000, 18, 0, '2022-01-12 09:59:23', '2022-01-12 09:59:23'),
(42, 1032474109, 'Power Ranger', 100000, 9, 5, '2022-01-12 09:59:23', '2022-01-12 09:59:23'),
(43, 2054822804, 'Squid game part 2', 200000, 18, 0, '2022-01-12 10:00:55', '2022-01-12 10:00:55'),
(44, 2054822804, 'Power Ranger', 100000, 9, 5, '2022-01-12 10:00:55', '2022-01-12 10:00:55'),
(45, 1846690725, 'New Disk', 50000, 587, 5, '2022-01-12 13:52:13', '2022-01-12 13:52:13');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `import_form`
--

CREATE TABLE `import_form` (
  `id` bigint NOT NULL,
  `provider_name` varchar(200) NOT NULL,
  `sum_amount` int NOT NULL DEFAULT '0',
  `sum_value` int NOT NULL DEFAULT '0',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `update_by` bigint NOT NULL,
  `status` enum('WATING','DELIVERED','ERROR') NOT NULL DEFAULT 'WATING',
  `image` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `import_form`
--

INSERT INTO `import_form` (`id`, `provider_name`, `sum_amount`, `sum_value`, `create_time`, `update_time`, `create_by`, `update_by`, `status`, `image`) VALUES
(7, 'BIDA', 20, 500000, '2021-12-21 07:30:38', '2021-12-21 07:30:38', 1, 1, 'ERROR', 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Billiard_Balls_and_Rack.jpg/640px-Billiard_Balls_and_Rack.jpg'),
(1054410771, 'HBO', 5, 250000, '2022-01-12 09:57:03', '2022-01-12 09:57:03', 1, 1, 'DELIVERED', '/QLCHBD-OOAD;component/assets/img_noImage.png'),
(1065655860, 'BIDA', 25, 1250000, '2022-01-12 13:44:27', '2022-01-12 13:44:27', 1, 1, 'DELIVERED', '/QLCHBD-OOAD;component/assets/img_noImage.png'),
(1413821807, 'HBO', 20, 200000, '2022-01-07 10:34:34', '2022-01-07 10:34:34', 1, 1, 'DELIVERED', '/QLCHBD-OOAD;component/assets/img_noImage.png'),
(1565270127, 'BIDA', 16, 2600000, '2022-01-12 09:47:56', '2022-01-12 09:47:56', 1, 1, 'DELIVERED', '/QLCHBD-OOAD;component/assets/img_noImage.png'),
(1565270128, 'BIDA', 10, 1500000, '2022-01-12 10:00:20', '2022-01-12 10:00:20', 1, 1, 'DELIVERED', '/QLCHBD-OOAD;component/assets/img_noImage.png');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `import_form_item`
--

CREATE TABLE `import_form_item` (
  `id` bigint NOT NULL,
  `import_form_id` bigint NOT NULL,
  `quantity` int NOT NULL,
  `disk_id` bigint DEFAULT NULL,
  `disk_name` varchar(200) NOT NULL,
  `disk_price` int NOT NULL,
  `id_by_provider` bigint NOT NULL,
  `create_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `import_form_item`
--

INSERT INTO `import_form_item` (`id`, `import_form_id`, `quantity`, `disk_id`, `disk_name`, `disk_price`, `id_by_provider`, `create_time`, `update_time`) VALUES
(25, 7, 5, 100, 'Among Ú', 25000, 121, '2021-12-21 07:33:44', '2021-12-21 07:33:44'),
(26, 7, 5, 101, 'Alibaba And Bicycle', 25000, 122, '2021-12-21 07:33:44', '2021-12-21 07:33:44'),
(27, 7, 5, 102, 'Alone In The Beach', 25000, 23, '2021-12-21 07:33:44', '2021-12-21 07:33:44'),
(28, 7, 5, 103, 'Living Dev', 25000, 24, '2021-12-21 07:33:44', '2021-12-21 07:33:44'),
(45, 1413821807, 20, 123, '213', 10000, 123, '2022-01-07 10:34:34', '2022-01-07 10:34:34'),
(49, 1565270127, 5, 4, 'Squid game', 200000, 18, '2022-01-12 09:47:56', '2022-01-12 09:47:56'),
(50, 1565270127, 5, 5, 'Squid game part 2', 200000, 18, '2022-01-12 09:47:56', '2022-01-12 09:47:56'),
(51, 1565270127, 6, 6, 'Power Ranger', 100000, 9, '2022-01-12 09:47:56', '2022-01-12 09:47:56'),
(52, 1054410771, 5, 7, 'Test disk', 50000, 7, '2022-01-12 09:57:03', '2022-01-12 09:57:03'),
(53, 1565270128, 5, 5, 'Squid game part 2', 200000, 18, '2022-01-12 10:00:21', '2022-01-12 10:00:21'),
(54, 1565270128, 5, 6, 'Power Ranger', 100000, 9, '2022-01-12 10:00:21', '2022-01-12 10:00:21'),
(55, 1065655860, 25, 587, 'New Disk', 50000, 587, '2022-01-12 13:44:27', '2022-01-12 13:44:27');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `provider`
--

CREATE TABLE `provider` (
  `id` bigint NOT NULL,
  `name` varchar(200) NOT NULL,
  `number` int NOT NULL,
  `mail` varchar(200) NOT NULL,
  `address` varchar(300) NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `update_by` bigint NOT NULL,
  `image` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `provider`
--

INSERT INTO `provider` (`id`, `name`, `number`, `mail`, `address`, `create_time`, `update_time`, `create_by`, `update_by`, `image`) VALUES
(1, 'HBO', 123456790, 'abc@gmail.com', 'Sao Hỏa', '2021-12-13 04:11:39', '2022-01-12 14:27:20', 1, 1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/HBO_1972.jpg/640px-HBO_1972.jpg'),
(3, 'BIDA', 123456790, 'abc@gmail.com', 'Sao Thủy', '2021-12-13 04:11:39', '2021-12-13 04:11:39', 1, 1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Billiard_Balls_and_Rack.jpg/640px-Billiard_Balls_and_Rack.jpg'),
(7, 'TKD', 345678766, 'tkd@gmail.com', 'tp HCM', '2022-01-12 09:50:03', '2022-01-12 14:27:38', 1, 1, 'https://farm5.staticflickr.com/4291/buddyicons/3944526@N25_r.jpg?1501081649'),
(123, 'sdfs', 1231, 'weqw@ưeqtrỷt', '123', '2022-01-06 15:26:47', '2022-01-06 15:27:13', 1, 1, 'https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `receipt`
--

CREATE TABLE `receipt` (
  `id` bigint NOT NULL,
  `guess_id` bigint NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint DEFAULT NULL,
  `additional_fee` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `receipt`
--

INSERT INTO `receipt` (`id`, `guess_id`, `create_time`, `create_by`, `additional_fee`) VALUES
(4, 3, '2022-01-06 15:29:28', 1, 0),
(5, 1, '2022-01-06 15:35:40', 1, 0),
(6, 5, '2022-01-12 15:15:57', 5, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `receipt_item`
--

CREATE TABLE `receipt_item` (
  `id` int NOT NULL,
  `receipt` bigint NOT NULL,
  `returned_quantity` int NOT NULL,
  `disk_id` bigint NOT NULL,
  `disk_name` varchar(200) NOT NULL,
  `delay_date` int NOT NULL,
  `lost_quantity` int NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `receipt_item`
--

INSERT INTO `receipt_item` (`id`, `receipt`, `returned_quantity`, `disk_id`, `disk_name`, `delay_date`, `lost_quantity`, `create_time`) VALUES
(4, 4, 10, 2, 'Alone In The Beach', 0, 0, '2022-01-06 15:29:28'),
(5, 4, 9, 4, 'Squid game', 0, 0, '2022-01-06 15:29:28'),
(6, 5, 1, 1, 'Alibaba And Bicycle', 0, 0, '2022-01-06 15:35:40'),
(7, 6, 5, 6, 'Power Ranger', 0, 0, '2022-01-12 15:15:57');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `rental_bill`
--

CREATE TABLE `rental_bill` (
  `id` bigint NOT NULL,
  `guess_id` bigint NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `total_price` int NOT NULL,
  `returned_all` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `rental_bill`
--

INSERT INTO `rental_bill` (`id`, `guess_id`, `create_time`, `create_by`, `total_price`, `returned_all`) VALUES
(83, 3, '2022-01-06 15:29:02', 1, 1441230, 1),
(84, 1, '2022-01-06 15:35:27', 1, 234, 1),
(85, 1, '2022-01-06 15:36:53', 1, 500000, 0),
(86, 4, '2022-01-07 06:30:23', 1, 400000, 0),
(87, 4, '2022-01-07 06:30:32', 1, 80000, 0),
(88, 4, '2022-01-07 06:30:41', 1, 120000, 0),
(89, 3, '2022-01-12 03:02:53', 1, 320000, 0),
(90, 5, '2022-01-12 15:15:43', 5, 500000, 0),
(91, 1, '2022-01-13 01:30:33', 1, 320000, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `rental_bill_item`
--

CREATE TABLE `rental_bill_item` (
  `id` bigint NOT NULL,
  `rental_id` bigint NOT NULL,
  `quantity` int NOT NULL,
  `rental_price` int NOT NULL,
  `disk_name` varchar(200) NOT NULL,
  `disk_image` varchar(200) NOT NULL,
  `disk_id` bigint NOT NULL,
  `due_date` datetime NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `receive_quantity` int NOT NULL,
  `lost_quantity` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `rental_bill_item`
--

INSERT INTO `rental_bill_item` (`id`, `rental_id`, `quantity`, `rental_price`, `disk_name`, `disk_image`, `disk_id`, `due_date`, `create_time`, `update_time`, `receive_quantity`, `lost_quantity`) VALUES
(76, 83, 10, 123, 'Alone In The Beach', '/QLCHBD-OOAD;component/assets/img_noImage.png', 2, '2022-01-07 22:28:02', '2022-01-06 15:29:02', '2022-01-06 15:29:02', 10, 0),
(77, 83, 9, 80000, 'Squid game', 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 4, '2022-01-07 22:28:53', '2022-01-06 15:29:02', '2022-01-06 15:29:02', 9, 0),
(78, 84, 1, 234, 'Alibaba And Bicycle', '/QLCHBD-OOAD;component/assets/img_noImage.png', 1, '2022-01-07 22:35:19', '2022-01-06 15:35:27', '2022-01-06 15:35:27', 1, 0),
(79, 85, 10, 50000, 'Power Ranger', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP1PIgSOWpxk8VGj9_zSuRDaUGpNEIBmozO5ZnEE8Rhhi-GjzfFufjpXt3zTnAsco6-i0&usqp=CAU', 6, '2022-01-07 22:36:46', '2022-01-06 15:36:53', '2022-01-06 15:36:53', 0, 0),
(80, 86, 5, 80000, 'Squid game', 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 4, '2022-01-08 13:30:11', '2022-01-07 06:30:23', '2022-01-07 06:30:23', 0, 0),
(81, 87, 4, 20000, 'Neverland', 'https://truyenz.info/wp-content/uploads/2018/07/The-Promised-Neverland-Mi%E1%BB%81n-%C4%90%E1%BA%A5t-H%E1%BB%A9a-1.jpg', 9, '2022-01-08 13:30:26', '2022-01-07 06:30:32', '2022-01-07 06:30:32', 0, 0),
(82, 88, 6, 20000, 'Neverland', 'https://truyenz.info/wp-content/uploads/2018/07/The-Promised-Neverland-Mi%E1%BB%81n-%C4%90%E1%BA%A5t-H%E1%BB%A9a-1.jpg', 9, '2022-01-08 13:30:34', '2022-01-07 06:30:41', '2022-01-07 06:30:41', 0, 0),
(83, 89, 4, 80000, 'Squid game', 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 4, '2022-01-13 10:02:45', '2022-01-12 03:02:53', '2022-01-12 03:02:53', 0, 0),
(84, 90, 10, 50000, 'Power Ranger', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP1PIgSOWpxk8VGj9_zSuRDaUGpNEIBmozO5ZnEE8Rhhi-GjzfFufjpXt3zTnAsco6-i0&usqp=CAU', 6, '2022-01-13 22:15:38', '2022-01-12 15:15:43', '2022-01-12 15:15:43', 5, 0),
(85, 91, 4, 80000, 'Squid game part 2', 'https://static2.yan.vn/YanNews/2167221/202109/photo11632217845355366010648-ae74414f.jpg', 5, '2022-01-14 08:30:23', '2022-01-13 01:30:33', '2022-01-13 01:30:33', 0, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `report`
--

CREATE TABLE `report` (
  `id` int NOT NULL,
  `create_by` bigint NOT NULL,
  `create_time` datetime NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `file` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `staff`
--

CREATE TABLE `staff` (
  `id` bigint NOT NULL,
  `name` varchar(200) NOT NULL,
  `birth_date` date NOT NULL,
  `image` varchar(200) NOT NULL,
  `user_name` varchar(200) NOT NULL,
  `password` varchar(200) NOT NULL,
  `is_manager` tinyint(1) NOT NULL,
  `cmnd_cccd` char(12) NOT NULL,
  `status` enum('WORKING','FIRED') NOT NULL DEFAULT 'WORKING',
  `is_loged_in` tinyint(1) NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `create_by` bigint NOT NULL,
  `update_by` bigint NOT NULL,
  `salary_coefficient` int NOT NULL DEFAULT '20000'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `staff`
--

INSERT INTO `staff` (`id`, `name`, `birth_date`, `image`, `user_name`, `password`, `is_manager`, `cmnd_cccd`, `status`, `is_loged_in`, `create_time`, `update_time`, `create_by`, `update_by`, `salary_coefficient`) VALUES
(1, 'admin', '2001-10-09', 'image', 'admin', '6Uokiji7hAHmgcxTXWZQxQ==', 1, '012345678910', 'WORKING', 0, '2021-10-09 11:19:54', '2021-10-09 11:19:54', 0, 0, 20000),
(2, 'khang', '2000-02-02', 'none', 'khang', '6Uokiji7hAHmgcxTXWZQxQ==', 0, '123456789', 'WORKING', 0, '2021-12-19 06:50:24', '2021-12-19 06:50:24', 1, 1, 20000),
(3, 'Khang bui duong', '1999-07-09', 'none', 'khangkhang', 'C552woOBwM8FGqmsa+EerQ==', 0, '123456789', 'WORKING', 0, '2021-12-20 07:33:51', '2021-12-20 07:33:51', 1, 1, 20000),
(5, 'Pham Minh Tan', '2001-07-13', 'none', 'pmt', '6Uokiji7hAHmgcxTXWZQxQ==', 0, '305875689', 'WORKING', 0, '2022-01-12 15:14:34', '2022-01-12 15:14:34', 1, 1, 20000);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `album`
--
ALTER TABLE `album`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `disk`
--
ALTER TABLE `disk`
  ADD PRIMARY KEY (`id`),
  ADD KEY `album` (`album`),
  ADD KEY `provider` (`provider`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- Chỉ mục cho bảng `guest`
--
ALTER TABLE `guest`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- Chỉ mục cho bảng `import_bill`
--
ALTER TABLE `import_bill`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- Chỉ mục cho bảng `import_billing_item`
--
ALTER TABLE `import_billing_item`
  ADD PRIMARY KEY (`id`),
  ADD KEY `import_bill` (`import_bill`);

--
-- Chỉ mục cho bảng `import_form`
--
ALTER TABLE `import_form`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- Chỉ mục cho bảng `import_form_item`
--
ALTER TABLE `import_form_item`
  ADD PRIMARY KEY (`id`),
  ADD KEY `import_form_id` (`import_form_id`);

--
-- Chỉ mục cho bảng `provider`
--
ALTER TABLE `provider`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- Chỉ mục cho bảng `receipt`
--
ALTER TABLE `receipt`
  ADD PRIMARY KEY (`id`),
  ADD KEY `guess_id` (`guess_id`),
  ADD KEY `create_by` (`create_by`);

--
-- Chỉ mục cho bảng `receipt_item`
--
ALTER TABLE `receipt_item`
  ADD PRIMARY KEY (`id`),
  ADD KEY `receipt` (`receipt`);

--
-- Chỉ mục cho bảng `rental_bill`
--
ALTER TABLE `rental_bill`
  ADD PRIMARY KEY (`id`),
  ADD KEY `guess_id` (`guess_id`),
  ADD KEY `create_by` (`create_by`);

--
-- Chỉ mục cho bảng `rental_bill_item`
--
ALTER TABLE `rental_bill_item`
  ADD PRIMARY KEY (`id`),
  ADD KEY `rental_id` (`rental_id`);

--
-- Chỉ mục cho bảng `report`
--
ALTER TABLE `report`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`);

--
-- Chỉ mục cho bảng `staff`
--
ALTER TABLE `staff`
  ADD PRIMARY KEY (`id`),
  ADD KEY `create_by` (`create_by`),
  ADD KEY `update_by` (`update_by`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `album`
--
ALTER TABLE `album`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `disk`
--
ALTER TABLE `disk`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT cho bảng `guest`
--
ALTER TABLE `guest`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT cho bảng `import_bill`
--
ALTER TABLE `import_bill`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2054822805;

--
-- AUTO_INCREMENT cho bảng `import_billing_item`
--
ALTER TABLE `import_billing_item`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT cho bảng `import_form`
--
ALTER TABLE `import_form`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1565270129;

--
-- AUTO_INCREMENT cho bảng `import_form_item`
--
ALTER TABLE `import_form_item`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT cho bảng `provider`
--
ALTER TABLE `provider`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=124;

--
-- AUTO_INCREMENT cho bảng `receipt`
--
ALTER TABLE `receipt`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT cho bảng `receipt_item`
--
ALTER TABLE `receipt_item`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT cho bảng `rental_bill`
--
ALTER TABLE `rental_bill`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=92;

--
-- AUTO_INCREMENT cho bảng `rental_bill_item`
--
ALTER TABLE `rental_bill_item`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=86;

--
-- AUTO_INCREMENT cho bảng `report`
--
ALTER TABLE `report`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `staff`
--
ALTER TABLE `staff`
  MODIFY `id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `disk`
--
ALTER TABLE `disk`
  ADD CONSTRAINT `disk_ibfk_1` FOREIGN KEY (`album`) REFERENCES `album` (`id`),
  ADD CONSTRAINT `disk_ibfk_2` FOREIGN KEY (`provider`) REFERENCES `provider` (`id`),
  ADD CONSTRAINT `disk_ibfk_3` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`),
  ADD CONSTRAINT `disk_ibfk_4` FOREIGN KEY (`update_by`) REFERENCES `staff` (`id`);

--
-- Các ràng buộc cho bảng `guest`
--
ALTER TABLE `guest`
  ADD CONSTRAINT `guest_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`),
  ADD CONSTRAINT `guest_ibfk_2` FOREIGN KEY (`update_by`) REFERENCES `staff` (`id`);

--
-- Các ràng buộc cho bảng `import_bill`
--
ALTER TABLE `import_bill`
  ADD CONSTRAINT `import_bill_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`);

--
-- Các ràng buộc cho bảng `import_billing_item`
--
ALTER TABLE `import_billing_item`
  ADD CONSTRAINT `import_billing_item_ibfk_1` FOREIGN KEY (`import_bill`) REFERENCES `import_bill` (`id`);

--
-- Các ràng buộc cho bảng `import_form`
--
ALTER TABLE `import_form`
  ADD CONSTRAINT `import_form_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`),
  ADD CONSTRAINT `import_form_ibfk_2` FOREIGN KEY (`update_by`) REFERENCES `staff` (`id`);

--
-- Các ràng buộc cho bảng `import_form_item`
--
ALTER TABLE `import_form_item`
  ADD CONSTRAINT `import_form_item_ibfk_1` FOREIGN KEY (`import_form_id`) REFERENCES `import_form` (`id`);

--
-- Các ràng buộc cho bảng `receipt`
--
ALTER TABLE `receipt`
  ADD CONSTRAINT `receipt_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`),
  ADD CONSTRAINT `receipt_ibfk_2` FOREIGN KEY (`guess_id`) REFERENCES `guest` (`id`);

--
-- Các ràng buộc cho bảng `receipt_item`
--
ALTER TABLE `receipt_item`
  ADD CONSTRAINT `receipt_item_ibfk_1` FOREIGN KEY (`receipt`) REFERENCES `receipt` (`id`);

--
-- Các ràng buộc cho bảng `rental_bill`
--
ALTER TABLE `rental_bill`
  ADD CONSTRAINT `rental_bill_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`),
  ADD CONSTRAINT `rental_bill_ibfk_2` FOREIGN KEY (`guess_id`) REFERENCES `guest` (`id`);

--
-- Các ràng buộc cho bảng `rental_bill_item`
--
ALTER TABLE `rental_bill_item`
  ADD CONSTRAINT `rental_bill_item_ibfk_1` FOREIGN KEY (`rental_id`) REFERENCES `rental_bill` (`id`);

--
-- Các ràng buộc cho bảng `report`
--
ALTER TABLE `report`
  ADD CONSTRAINT `report_ibfk_1` FOREIGN KEY (`create_by`) REFERENCES `staff` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
