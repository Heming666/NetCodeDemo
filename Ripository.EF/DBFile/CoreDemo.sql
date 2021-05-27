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

 Date: 24/05/2021 23:00:58
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
) ENGINE = InnoDB AUTO_INCREMENT = 57 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '消费支出明细表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of User_ConsumeEntity
-- ----------------------------
INSERT INTO `User_ConsumeEntity` VALUES (2, '中餐', 15.00, '公司', '外卖', 0, '2021-05-09 00:12:09.000000', '2021-05-09 00:00:38.000000', 1);
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
INSERT INTO `User_ConsumeEntity` VALUES (20, '药', 119.00, '京东', NULL, 6, '2021-05-09 12:48:09.000000', '2021-05-09 00:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (21, '饮食', 49.00, '梁山头', NULL, 0, '2021-05-09 12:48:09.000000', '2021-05-09 08:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (22, '停车费', 500.00, '梁山头', '5月9到11月9', 4, '2021-05-09 23:22:09.000000', '2021-05-09 23:23:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (23, '拖鞋', 19.00, '名创优品', '传媒学院店', 2, '2021-05-09 23:24:09.000000', '2021-05-09 00:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (24, '晚餐', 30.00, '传媒学院', NULL, 0, '2021-05-09 23:24:09.000000', '2021-05-09 00:00:00.000000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (25, '夏天的衣服', 831.80, '京东', NULL, 1, '2021-05-10 00:15:10.000000', '2021-05-10 00:15:40.855000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (26, '午饭', 13.50, '公司', '外卖', 0, '2021-05-10 22:40:10.000000', '2021-05-10 12:40:12.034000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (27, '电扇', 85.00, '拼多多', NULL, 2, '2021-05-10 22:40:10.000000', '2021-05-10 22:40:40.729000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (28, '名创优品', 70.40, NULL, NULL, 2, '2021-05-11 21:28:11.000000', '2021-05-11 21:28:09.829000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (29, '吃饭', 15.00, NULL, NULL, 0, '2021-05-11 21:29:11.000000', '2021-05-11 21:29:14.145000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (30, '零食', 111.00, '京东', '三只松鼠', 0, '2021-05-12 21:34:12.000000', '2021-05-12 21:34:32.351000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (31, '买菜', 4.80, NULL, NULL, 0, '2021-05-12 21:34:12.000000', '2021-05-12 21:34:53.892000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (32, '槟榔', 35.00, NULL, NULL, 0, '2021-05-12 21:35:12.000000', '2021-05-12 21:35:13.470000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (33, '雨伞', 29.00, '雄楚一号有家', NULL, 2, '2021-05-14 23:56:14.000000', '2021-05-14 23:56:23.967000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (34, '荣儿去我家买的日用品', 28.30, '月火里超市', NULL, 2, '2021-05-14 23:57:14.000000', '2021-05-14 23:57:36.347000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (35, '桥头排骨', 20.00, '雄楚一号', NULL, 0, '2021-05-14 23:58:14.000000', '2021-05-14 23:58:27.094000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (36, '龙门花甲', 46.00, '雄楚一号', NULL, 0, '2021-05-14 23:58:14.000000', '2021-05-14 23:58:47.727000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (37, '面包', 11.80, '雄楚一号', NULL, 0, '2021-05-14 23:59:14.000000', '2021-05-14 23:59:00.415000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (38, '硬红楼', 200.00, '梁山头', NULL, 0, '2021-05-14 23:59:14.000000', '2021-05-12 20:59:19.008000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (39, '早餐', 11.00, NULL, NULL, 0, '2021-05-16 22:03:16.000000', '2021-05-15 22:03:35.841000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (40, '给妹妹买衣服', 277.72, NULL, NULL, 2, '2021-05-16 22:04:16.000000', '2021-05-15 22:04:41.717000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (41, '鄂州消费', 130.00, NULL, NULL, 0, '2021-05-16 22:05:16.000000', '2021-05-15 22:05:10.352000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (42, '槟榔', 35.00, NULL, NULL, 0, '2021-05-16 22:06:16.000000', '2021-05-15 22:06:09.990000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (43, '给舅舅买车票', 168.00, NULL, NULL, 4, '2021-05-16 22:06:16.000000', '2021-05-16 22:06:46.834000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (44, '电视', 850.00, NULL, NULL, 2, '2021-05-16 22:17:16.000000', '2021-05-16 22:17:48.544000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (45, '房租', 746.00, NULL, NULL, 3, '2021-05-19 23:28:19.000000', '2021-05-17 23:28:05.950000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (46, '水果', 9.60, '关西小区', NULL, 0, '2021-05-19 23:28:19.000000', '2021-05-18 23:28:26.769000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (47, '晚餐', 74.00, NULL, '光谷天地-加班', 0, '2021-05-19 23:29:19.000000', '2021-05-19 23:29:05.490000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (48, '点痣膏', 56.26, '淘宝', NULL, 6, '2021-05-19 23:29:19.000000', '2021-05-19 23:29:50.061000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (49, '饮料-可乐', 6.00, '公司', NULL, 0, '2021-05-19 23:30:19.000000', '2021-05-18 23:30:13.044000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (50, '一条红楼', 200.00, NULL, NULL, 0, '2021-05-20 22:26:20.000000', '2021-05-20 22:26:52.549000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (51, '槟榔', 35.00, NULL, NULL, 0, '2021-05-20 22:27:20.000000', '2021-05-20 22:27:08.877000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (52, '520买菜', 49.00, '八一花园', NULL, 0, '2021-05-20 22:28:20.000000', '2021-05-20 22:28:14.523000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (53, '鸡公煲', 50.00, '八一花园', NULL, 0, '2021-05-23 18:20:23.000000', '2021-05-21 18:20:37.142000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (54, '周末消费', 87.00, '鄂州', NULL, 0, '2021-05-23 18:21:23.000000', '2021-05-23 18:21:00.471000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (55, '麦克风', 150.00, '京东', NULL, 3, '2021-05-24 22:58:24.000000', '2021-05-24 22:58:48.282000', 1);
INSERT INTO `User_ConsumeEntity` VALUES (56, '吃饭', 20.00, NULL, NULL, 0, '2021-05-24 22:59:24.000000', '2021-05-24 22:59:31.979000', 1);

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
