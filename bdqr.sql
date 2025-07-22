/*
 Navicat Premium Data Transfer

 Source Server         : ServerLocal
 Source Server Type    : MySQL
 Source Server Version : 100432
 Source Host           : localhost:3306
 Source Schema         : bdqr

 Target Server Type    : MySQL
 Target Server Version : 100432
 File Encoding         : 65001

 Date: 22/07/2025 15:57:56
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for archivosqrs
-- ----------------------------
DROP TABLE IF EXISTS `archivosqrs`;
CREATE TABLE `archivosqrs`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NombreArchivo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RutaHtml` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Fecha` datetime NOT NULL,
  `Anio` int NULL DEFAULT NULL,
  `Usuario` int NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of archivosqrs
-- ----------------------------
INSERT INTO `archivosqrs` VALUES (1, 'EF-69', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);
INSERT INTO `archivosqrs` VALUES (3, 'EF-69', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);
INSERT INTO `archivosqrs` VALUES (4, 'EF-69', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);
INSERT INTO `archivosqrs` VALUES (5, 'EF-69', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);
INSERT INTO `archivosqrs` VALUES (6, 'EF-69', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);
INSERT INTO `archivosqrs` VALUES (7, 'EF-70', 'http://cardenas.gob.mx/EF-69.pdf', '2025-05-09 00:00:00', 2025, 0);

-- ----------------------------
-- Table structure for personal
-- ----------------------------
DROP TABLE IF EXISTS `personal`;
CREATE TABLE `personal`  (
  `id` int NOT NULL,
  `Nombre` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `APaterno` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AMaterno` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Correo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Fecha` date NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of personal
-- ----------------------------

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `id` int NOT NULL,
  `IdPersona` int NULL DEFAULT NULL,
  `NombreUser` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IdPersona`(`IdPersona` ASC) USING BTREE,
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`IdPersona`) REFERENCES `personal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
