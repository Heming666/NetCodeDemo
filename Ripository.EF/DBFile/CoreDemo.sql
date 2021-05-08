/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80024
 Source Host           : localhost:3306
 Source Schema         : CoreDemo

 Target Server Type    : MySQL
 Target Server Version : 80024
 File Encoding         : 65001

 Date: 09/05/2021 00:33:50
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
DROP TABLE IF EXISTS `Base_Department`;
CREATE TABLE `Base_Department`  (
  `DeptId` int NOT NULL AUTO_INCREMENT,
  `DeptName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DeptCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) NOT NULL,
  PRIMARY KEY (`DeptId`) USING BTREE,
  INDEX `Index_ID`(`DeptId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
INSERT INTO `Base_Department` VALUES (1, '系统', '001', '2021-05-08 23:44:00.000000', '2021-05-08 23:44:00.000000');

-- ----------------------------
-- Table structure for Base_UserInfo
-- ----------------------------
DROP TABLE IF EXISTS `Base_UserInfo`;
CREATE TABLE `Base_UserInfo`  (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Account` varchar(24) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账户',
  `PassWord` varchar(24) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `UserName` varchar(24) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户昵称',
  `Gender` int NULL DEFAULT NULL COMMENT '性别',
  `Photo` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '照片',
  `Phone` varchar(11) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号',
  `CreateDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) NOT NULL,
  `DeptId` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `IX_Base_UserInfo_Account`(`Account`) USING BTREE,
  UNIQUE INDEX `IX_Base_UserInfo_UserName`(`UserName`) USING BTREE,
  INDEX `Index_Account`(`Account`) USING BTREE,
  INDEX `IX_Base_UserInfo_DeptId`(`DeptId`) USING BTREE,
  CONSTRAINT `FK_Base_UserInfo_Base_Department_DeptId` FOREIGN KEY (`DeptId`) REFERENCES `Base_Department` (`DeptId`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Base_UserInfo
-- ----------------------------
INSERT INTO `Base_UserInfo` VALUES (1, 'System', 'hm940817', '管理员', 0, NULL, '13407112119', '2021-05-08 23:44:00.000000', '2021-05-08 23:44:00.000000', 1);

-- ----------------------------
-- Table structure for User_ConsumeEntity
-- ----------------------------
DROP TABLE IF EXISTS `User_ConsumeEntity`;
CREATE TABLE `User_ConsumeEntity`  (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ConsumeName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '消费名称',
  `Amount` decimal(8, 2) NOT NULL COMMENT '金额',
  `Place` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消费地点',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `Classify` int NOT NULL COMMENT '分类',
  `CreateTime` datetime(6) NOT NULL COMMENT '创建时间',
  `LogTime` datetime(6) NOT NULL COMMENT '消费时间',
  `UserId` int NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `Index_ID1`(`ID`) USING BTREE,
  INDEX `IX_User_ConsumeEntity_UserId`(`UserId`) USING BTREE,
  CONSTRAINT `FK_User_ConsumeEntity_Base_UserInfo_UserId` FOREIGN KEY (`UserId`) REFERENCES `Base_UserInfo` (`ID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '消费支出明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of User_ConsumeEntity
-- ----------------------------
INSERT INTO `User_ConsumeEntity` VALUES (2, '中餐', 15.00, '公司', '外卖', 0, '2021-05-09 00:12:09.000000', '0001-01-07 12:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (3, '中餐', 15.50, '软件园食堂', NULL, 0, '2021-05-09 00:15:09.000000', '2021-05-08 12:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (4, '米', 54.90, '中百超市', '传媒学院店', 0, '2021-05-09 00:16:09.000000', '2021-05-08 18:30:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (5, '肠粉', 5.00, '公司', '早餐', 0, '2021-05-09 00:18:09.000000', '2021-05-08 08:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (6, '槟榔', 37.80, '公司楼下', NULL, 2, '2021-05-09 00:18:09.000000', '2021-05-07 00:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (7, '河南烩面', 18.00, '公司', '晚餐', 0, '2021-05-09 00:19:09.000000', '2021-05-07 19:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (8, '早餐', 12.90, '公司楼下', NULL, 0, '2021-05-09 00:20:09.000000', '2021-05-07 08:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (9, '麻辣香锅', 58.50, '华罗利广场', '晚餐', 0, '2021-05-09 00:20:09.000000', '2021-05-06 18:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (10, '公司插板', 63.90, '沃尔玛', '公司', 2, '2021-05-09 00:21:09.000000', '2021-05-06 14:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (11, '午餐', 17.00, '公司', '外卖', 0, '2021-05-09 00:22:09.000000', '2021-05-06 12:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (12, '早餐', 20.00, '公司', '早餐', 0, '2021-05-09 00:23:09.000000', '2021-05-06 08:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (13, '高速服务区消费', 47.00, '宜昌回武汉', NULL, 4, '2021-05-09 00:23:09.000000', '0001-01-05 12:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (14, '油费', 185.00, '宜昌高速回武汉', NULL, 4, '2021-05-09 00:24:09.000000', '2021-05-05 09:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (15, '零食', 95.30, '长阳', NULL, 4, '2021-05-09 00:25:09.000000', '2021-05-05 08:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (16, '长阳旅游日用', 93.00, '长阳', NULL, 4, '2021-05-09 00:26:09.000000', '2021-05-04 10:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (17, '油费', 157.89, '鄂州', '去宜昌', 4, '2021-05-09 00:27:09.000000', '2021-05-03 07:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (18, '五一在家消费', 221.50, '鄂州', NULL, 0, '2021-05-09 00:28:09.000000', '2021-05-01 09:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (19, '蛋白粉', 597.00, '京东', NULL, 6, '2021-05-09 00:29:09.000000', '2021-05-08 00:00:00.000000', 1);

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20210508154207_mysql', '5.0.5');

SET FOREIGN_KEY_CHECKS = 1;
