/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 80039
 Source Host           : 127.0.0.1:3306
 Source Schema         : cracker_admin

 Target Server Type    : MySQL
 Target Server Version : 80039
 File Encoding         : 65001

 Date: 29/04/2025 21:04:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20250427015233_InitCreated', '8.0.4');

-- ----------------------------
-- Table structure for org_dept
-- ----------------------------
DROP TABLE IF EXISTS `org_dept`;
CREATE TABLE `org_dept`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '部门编号',
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '部门名称',
  `Sort` int NOT NULL COMMENT '排序',
  `Description` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '描述',
  `Status` int NOT NULL COMMENT '状态：1正常2停用',
  `CuratorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '负责人',
  `Email` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Phone` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '电话',
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父ID',
  `ParentIds` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '层级父ID',
  `Layer` int NOT NULL COMMENT '层级',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '部门表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of org_dept
-- ----------------------------

-- ----------------------------
-- Table structure for org_dept_employee
-- ----------------------------
DROP TABLE IF EXISTS `org_dept_employee`;
CREATE TABLE `org_dept_employee`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `EmployeeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '员工ID',
  `DeptId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '部门ID',
  `PositionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '职位ID',
  `IsMain` tinyint(1) NOT NULL COMMENT '是否主职位',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '员工关联部门' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of org_dept_employee
-- ----------------------------

-- ----------------------------
-- Table structure for org_employee
-- ----------------------------
DROP TABLE IF EXISTS `org_employee`;
CREATE TABLE `org_employee`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Code` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '工号',
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '姓名',
  `Sex` int NOT NULL COMMENT '性别',
  `Phone` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '手机号码',
  `IdNo` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '身份证',
  `FrontIdNoUrl` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '身份证正面',
  `BackIdNoUrl` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '身份证背面',
  `BirthDay` datetime(6) NOT NULL COMMENT '生日',
  `Address` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '现住址',
  `Email` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '邮箱',
  `InTime` datetime(6) NOT NULL COMMENT '入职时间',
  `OutTime` datetime(6) NULL DEFAULT NULL COMMENT '离职时间',
  `Status` tinyint(1) NOT NULL COMMENT '是否离职',
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '关联用户ID',
  `DeptId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '部门ID',
  `PositionId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '职位ID',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '员工表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of org_employee
-- ----------------------------

-- ----------------------------
-- Table structure for org_position
-- ----------------------------
DROP TABLE IF EXISTS `org_position`;
CREATE TABLE `org_position`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '职位编号',
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '职位名称',
  `Level` int NOT NULL COMMENT '职级',
  `Status` int NOT NULL COMMENT '状态：1正常2停用',
  `Description` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '描述',
  `GroupId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '职位分组',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '职位表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of org_position
-- ----------------------------
INSERT INTO `org_position` VALUES ('3a19875e-17c5-c32a-8d55-b6692b6a1912', '638813484840313992', '初级前端开发', 1, 1, NULL, '3a198742-cbff-3e27-ecbd-03a422683f6f', '{}', '0e72bec0d90e492492e99798361af9b7', '2025-04-27 11:01:24.072635', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 11:58:08.364273', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 0, NULL, NULL);
INSERT INTO `org_position` VALUES ('3a198766-ec01-e906-9431-edf399a6a218', '638813490626574068', '中级前端开发', 2, 1, NULL, NULL, '{}', 'c6df7a387a354542b750be98c77822d5', '2025-04-27 11:11:02.670104', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for org_positiongroup
-- ----------------------------
DROP TABLE IF EXISTS `org_positiongroup`;
CREATE TABLE `org_positiongroup`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GroupName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '分组名',
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父ID',
  `ParentIds` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '层级父ID',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Sort` int NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '职位分组' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of org_positiongroup
-- ----------------------------
INSERT INTO `org_positiongroup` VALUES ('3a198730-43cd-526b-9be9-7e6b19f956f5', '互联网AI', NULL, NULL, NULL, '2025-04-27 10:11:20.677333', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 10:36:17.748678', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 2);
INSERT INTO `org_positiongroup` VALUES ('3a198730-cb81-f6a3-33a9-55e36e5f599e', '后端开发', NULL, '3a198730-43cd-526b-9be9-7e6b19f956f5', '3a198730-43cd-526b-9be9-7e6b19f956f5', '2025-04-27 10:11:55.407338', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 10:29:51.553458', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 0);
INSERT INTO `org_positiongroup` VALUES ('3a198741-d91f-9324-26a8-aeac08833a4a', '.NET', NULL, '3a198730-cb81-f6a3-33a9-55e36e5f599e', '3a198730-43cd-526b-9be9-7e6b19f956f5,3a198730-cb81-f6a3-33a9-55e36e5f599e', '2025-04-27 10:30:33.004661', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198742-070d-e79f-1f2d-c4e5ac96ebf2', 'Java', NULL, '3a198730-cb81-f6a3-33a9-55e36e5f599e', '3a198730-43cd-526b-9be9-7e6b19f956f5,3a198730-cb81-f6a3-33a9-55e36e5f599e', '2025-04-27 10:30:44.749706', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198742-836f-135d-6d69-8e2b3803bf7b', '全栈工程师', NULL, '3a198730-cb81-f6a3-33a9-55e36e5f599e', '3a198730-43cd-526b-9be9-7e6b19f956f5,3a198730-cb81-f6a3-33a9-55e36e5f599e', '2025-04-27 10:31:16.592032', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198742-cbff-3e27-ecbd-03a422683f6f', '前端/移动开发', NULL, '3a198730-43cd-526b-9be9-7e6b19f956f5', '3a198730-43cd-526b-9be9-7e6b19f956f5', '2025-04-27 10:31:35.167651', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198742-fd07-872f-c5d5-39e4e8360389', 'Android', NULL, '3a198742-cbff-3e27-ecbd-03a422683f6f', '3a198730-43cd-526b-9be9-7e6b19f956f5,3a198742-cbff-3e27-ecbd-03a422683f6f', '2025-04-27 10:31:47.719705', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198743-1f2a-d737-a1d8-6d72c6c96f11', '产品', NULL, NULL, NULL, '2025-04-27 10:31:56.469128', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 10:36:14.182083', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 1);
INSERT INTO `org_positiongroup` VALUES ('3a198743-3f18-c493-a628-70aec59fdeb5', '产品经理', NULL, '3a198743-1f2a-d737-a1d8-6d72c6c96f11', '3a198743-1f2a-d737-a1d8-6d72c6c96f11', '2025-04-27 10:32:04.632649', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);
INSERT INTO `org_positiongroup` VALUES ('3a198743-94e5-f444-8044-f1d68b956e7b', '用户研究', NULL, '3a198743-1f2a-d737-a1d8-6d72c6c96f11', '3a198743-1f2a-d737-a1d8-6d72c6c96f11', '2025-04-27 10:32:26.597891', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, 0);

-- ----------------------------
-- Table structure for sys_businesslog
-- ----------------------------
DROP TABLE IF EXISTS `sys_businesslog`;
CREATE TABLE `sys_businesslog`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `Action` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作方法，全名',
  `HttpMethod` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'http请求方式',
  `Url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '请求地址',
  `Os` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '系统',
  `Browser` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器',
  `NodeName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作节点名',
  `Ip` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'IP',
  `Address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '登录地址',
  `IsSuccess` tinyint(1) NOT NULL COMMENT '是否成功',
  `OperationMsg` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '操作信息',
  `MillSeconds` int NOT NULL COMMENT '耗时，单位毫秒',
  `RequestId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '请求跟踪ID',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '业务日志' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_businesslog
-- ----------------------------

-- ----------------------------
-- Table structure for sys_config
-- ----------------------------
DROP TABLE IF EXISTS `sys_config`;
CREATE TABLE `sys_config`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '配置名称',
  `Key` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '配置键名',
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '配置键值',
  `GroupKey` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_sys_config_Key`(`Key` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '系统配置' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_config
-- ----------------------------

-- ----------------------------
-- Table structure for sys_dict_data
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict_data`;
CREATE TABLE `sys_dict_data`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Key` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Label` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DictType` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sort` int NOT NULL,
  `IsEnabled` tinyint(1) NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_sys_dict_data_Key`(`Key` ASC) USING BTREE,
  INDEX `IX_sys_dict_data_DictType`(`DictType` ASC) USING BTREE,
  CONSTRAINT `FK_sys_dict_data_sys_dict_type_DictType` FOREIGN KEY (`DictType`) REFERENCES `sys_dict_type` (`DictType`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_dict_data
-- ----------------------------
INSERT INTO `sys_dict_data` VALUES ('3a198725-0204-b13f-471d-cc65b728e6b8', '1', '1', 'L1', 'PositionLevel', NULL, 1, 1, '2025-04-27 09:59:02.935551', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 09:59:25.117401', '3a172a37-55d5-ee9b-dc92-e07386eadc7c');
INSERT INTO `sys_dict_data` VALUES ('3a198725-34f8-c4e9-e0c4-c4fa8bdc6dee', '2', '2', 'L2', 'PositionLevel', NULL, 2, 1, '2025-04-27 09:59:15.961386', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 09:59:28.794558', '3a172a37-55d5-ee9b-dc92-e07386eadc7c');
INSERT INTO `sys_dict_data` VALUES ('3a198727-b85f-adf1-41ac-cd4acf174dca', '3', '3', 'L3', 'PositionLevel', NULL, 3, 1, '2025-04-27 10:02:00.672236', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198727-e500-f543-9313-5c10a9942365', '4', '4', 'L4', 'PositionLevel', NULL, 4, 1, '2025-04-27 10:02:12.097157', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198728-43b7-9ce7-7ecd-ce19567739c9', '5', '5', 'L5', 'PositionLevel', NULL, 5, 1, '2025-04-27 10:02:36.343760', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198728-62f6-bc4f-80b8-ad479fed40fc', '6', '6', 'L6', 'PositionLevel', NULL, 6, 1, '2025-04-27 10:02:44.343000', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198728-8a5c-5e42-1ab7-45699bb76645', '7', '7', 'L7', 'PositionLevel', NULL, 7, 1, '2025-04-27 10:02:54.428784', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198728-a914-cc7a-89f6-2aa4af035728', '8', '8', 'L8', 'PositionLevel', NULL, 8, 1, '2025-04-27 10:03:02.293143', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a198728-c5f9-54cf-9563-33ac0faf39c4', '9', '9', 'L9', 'PositionLevel', NULL, 9, 1, '2025-04-27 10:03:09.689281', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);
INSERT INTO `sys_dict_data` VALUES ('3a19883e-6066-0997-6c67-aad9901d4ba2', 'boy', '1', '男', 'Sex', NULL, 1, 1, '2025-04-27 15:06:22.729186', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 15:06:45.960769', '3a172a37-55d5-ee9b-dc92-e07386eadc7c');
INSERT INTO `sys_dict_data` VALUES ('3a19883e-a8aa-f431-f3ca-b31f2d489df1', 'girl', '2', '女', 'Sex', NULL, 2, 1, '2025-04-27 15:06:41.195444', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 15:06:49.561167', '3a172a37-55d5-ee9b-dc92-e07386eadc7c');

-- ----------------------------
-- Table structure for sys_dict_type
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict_type`;
CREATE TABLE `sys_dict_type`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DictType` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsEnabled` tinyint(1) NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `AK_sys_dict_type_DictType`(`DictType` ASC) USING BTREE,
  UNIQUE INDEX `IX_sys_dict_type_DictType`(`DictType` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_dict_type
-- ----------------------------
INSERT INTO `sys_dict_type` VALUES ('3a198723-edb0-21c4-c987-a4008204aea3', '职级', 'PositionLevel', '', 1, '2025-04-27 09:57:52.280499', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-27 11:38:06.630709', '3a172a37-55d5-ee9b-dc92-e07386eadc7c');
INSERT INTO `sys_dict_type` VALUES ('3a19883b-390e-ff06-2c89-7bcd90b06c7c', '性别', 'Sex', NULL, 1, '2025-04-27 15:02:56.048677', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL);

-- ----------------------------
-- Table structure for sys_loginlog
-- ----------------------------
DROP TABLE IF EXISTS `sys_loginlog`;
CREATE TABLE `sys_loginlog`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `Ip` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'IP',
  `Address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '登录地址',
  `Os` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '系统',
  `Browser` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器',
  `OperationMsg` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器操作信息',
  `IsSuccess` tinyint(1) NOT NULL COMMENT '是否成功',
  `SessionId` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '会话ID',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 54 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '登录日志' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_loginlog
-- ----------------------------

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Title` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '显示标题/名称',
  `Name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '组件名',
  `Icon` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '图标',
  `Path` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '路由/地址',
  `FunctionType` int NOT NULL COMMENT '功能类型',
  `Permission` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '授权码',
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父级ID',
  `Sort` int NOT NULL COMMENT '排序',
  `Hidden` tinyint(1) NOT NULL COMMENT '是否隐藏',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `Component` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '菜单表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES ('3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', '系统管理', NULL, 'antd:SettingOutlined', '/system', 1, 'System', NULL, 2, 0, '{}', '9794086f941941f7be26cc8ae202b24c', '2024-06-15 15:49:13.507249', '3a132908-ca06-34de-164e-21c96505a036', '2025-04-28 17:01:06.659173', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a132d16-df35-09cb-9f50-0a83e8290575', '用户管理', NULL, '', '/system/user', 1, 'Sys:User', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 1, 0, '{}', '38cae97c400e4423a0e0f09d06b0f856', '2024-06-15 16:01:03.300935', '3a132908-ca06-34de-164e-21c96505a036', '2025-04-29 20:36:44.432771', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/User');
INSERT INTO `sys_menu` VALUES ('3a132d1f-2026-432a-885f-bf6b10bec15c', '角色管理', NULL, NULL, '/system/role', 1, 'Sys:Role', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 2, 0, '{}', '9f5abe94c55b4f1b89e72fa6352691d2', '2024-06-15 16:10:04.215040', '3a132908-ca06-34de-164e-21c96505a036', '2025-04-29 20:36:50.353568', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Role');
INSERT INTO `sys_menu` VALUES ('3a132d1f-e2dd-7447-ac4b-2250201a9bad', '菜单管理', NULL, NULL, '/system/menu', 1, 'Sys:Menu', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 3, 0, '{}', '4395e566c69c4460b3deafe2813707d9', '2024-06-15 16:10:54.046247', '3a132908-ca06-34de-164e-21c96505a036', '2025-04-29 20:36:55.993564', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Menu');
INSERT INTO `sys_menu` VALUES ('3a135caa-6050-b8fa-ba75-6aaf548a7683', '新增', NULL, NULL, NULL, 2, 'Sys.User.Add', '3a132d16-df35-09cb-9f50-0a83e8290575', 1, 0, '{}', 'a0b5b53309dc48b3bb9c52944f0f7b39', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:51:07.324256', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a135caa-b115-4de4-3be5-4b3cc477d8f4', '查询', NULL, NULL, NULL, 2, 'Sys.User.List', '3a132d16-df35-09cb-9f50-0a83e8290575', 2, 0, '{}', '31d71a1d5cb64ea7917ab70e0b5fdfa4', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:51:17.770903', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-3200-f6de-41f9-948404a81884', '删除', NULL, NULL, NULL, 2, 'Sys.User.Delete', '3a132d16-df35-09cb-9f50-0a83e8290575', 3, 0, '{}', '39ed8009e53d46ccbba51b4af63eeefe', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:52:03.506303', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-7acb-41cf-30c4-720eea400b2c', '分配角色', NULL, NULL, NULL, 2, 'Sys.User.AssignRole', '3a132d16-df35-09cb-9f50-0a83e8290575', 4, 0, '{}', '6966861b046341759a952fdb9234d3a0', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:52:13.678473', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-fcbb-1f19-8416-25c218db4279', '启用/禁用', NULL, NULL, NULL, 2, 'Sys.User.SwitchEnabledStatus', '3a132d16-df35-09cb-9f50-0a83e8290575', 5, 0, '{}', '389974c17e114f28a571f79ef3696aaa', '2024-06-24 21:46:04.859652', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:51:41.139399', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a135cb0-3753-ae33-82fd-603c3622f661', '编辑', NULL, NULL, NULL, 2, 'Sys.Role.Update', '3a132d1f-2026-432a-885f-bf6b10bec15c', 3, 0, '{}', 'ffd9777ffbfc4e01b9dc54ded78b0c53', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:53:16.859118', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a138b11-d573-3e37-298a-c67a014e430b', '日志管理', NULL, NULL, '/system/log', 1, 'Sys:Log', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 6, 0, '{}', 'f21d561c09cc464e8bca12a692772a4d', '2024-07-03 21:59:51.413081', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:37:18.078221', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a138b12-93b5-e723-1539-adaeb17a2ae1', '登录日志', NULL, NULL, '/system/log/login', 1, 'Sys:Log:Login', '3a138b11-d573-3e37-298a-c67a014e430b', 1, 0, '{}', 'f3ece4b4f9fe4ec3b069b04672f8368c', '2024-07-03 22:00:40.118092', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:37:35.345770', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Log/LoginLog');
INSERT INTO `sys_menu` VALUES ('3a138b13-fccd-7b4a-0bb5-435a9d9c4172', '业务日志', NULL, NULL, '/system/log/business', 1, 'Sys:Log:Business', '3a138b11-d573-3e37-298a-c67a014e430b', 2, 0, '{}', 'c7e05ffb2b7a4571ac20f785be896cf5', '2024-07-03 22:02:12.559269', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:37:42.404550', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Log/BusinessLog');
INSERT INTO `sys_menu` VALUES ('3a13a4fe-6f74-733b-a628-6125c0325481', '组织架构', NULL, 'antd:TeamOutlined', '/org', 1, 'Org', NULL, 1, 0, '{}', 'e23212cc082f44d9883bddb89cb8522e', '2024-07-08 22:48:47.742259', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', '2025-04-28 17:00:59.620913', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcf2-3701-be8e-4ec8-ad5f77536101', '职位分组', NULL, NULL, '/org/position-group', 1, 'Org:PositionGroup', '3a13a4fe-6f74-733b-a628-6125c0325481', 1, 0, '{}', 'c931e2376246445e99ee07e978b8f60d', '2024-07-13 14:26:20.046127', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:36:13.199232', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './Org/PositionGroup');
INSERT INTO `sys_menu` VALUES ('3a13bcfc-f11b-7dce-a264-0f34b21085f1', '新增', NULL, NULL, NULL, 2, 'Org.PositionGroup.Add', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '248ba847b0244d46aa9c575939485195', '2024-07-13 14:38:03.055512', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:45:39.341082', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-52bb-db4a-d508-eea8536c8bdc', '查询', NULL, NULL, NULL, 2, 'Org.PositionGroup.List', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 2, 0, '{}', '389c6208807648789c8af475bcf93336', '2024-07-13 14:38:28.028042', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:45:50.604507', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-90b2-02ef-4440-8062594e3d79', '编辑', NULL, NULL, NULL, 2, 'Org.PositionGroup.Update', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 3, 0, '{}', 'd1405aaba9cc4f889d23b52369cb0e77', '2024-07-13 14:38:43.893385', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:46:23.289169', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-c549-8f84-1941-1d6baccfd6b4', '删除', '', NULL, NULL, 2, 'Org.PositionGroup.Delete', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 4, 0, '{}', '1e47c444c102435facaa6a0caa4ed317', '2024-07-13 14:38:57.354973', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:46:41.331844', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', '职位管理', NULL, NULL, '/org/position', 1, 'Org:Position', '3a13a4fe-6f74-733b-a628-6125c0325481', 2, 0, '{}', '1ee1548ca01e4ef4b3ce681a632353ac', '2024-07-13 17:52:45.803191', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:36:20.582159', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './Org/Position');
INSERT INTO `sys_menu` VALUES ('3a13bdb0-d210-fbaf-ce01-6d658b1b7ec9', '新增', NULL, NULL, NULL, 2, 'Org.Position.Add', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 1, 0, '{}', 'bf7d1e73fc3246aca097e4dbe03051fb', '2024-07-13 17:54:31.568625', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:46:59.032454', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb1-26a1-79e7-6920-b40e50de0bbe', '查询', '', NULL, NULL, 2, 'Org.Position.List', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 2, 0, '{}', '6b1bf89db81e4323ab4d80d949e6292e', '2024-07-13 17:54:53.219392', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:47:09.010474', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb1-742f-1ff2-87d1-b07a807c791f', '编辑', NULL, NULL, NULL, 2, 'Org.Position.Update', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 3, 0, '{}', '5de3b7d0e0404512852633ce747bf47c', '2024-07-13 17:55:13.072355', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:47:21.211547', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb2-213b-d9fa-d067-287801312ea1', '删除', NULL, NULL, NULL, 2, 'Org.Position.Delete', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 4, 0, '{}', '7729f55fffda458a92949d164419168a', '2024-07-13 17:55:57.372951', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:47:31.856772', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be18-7fe2-2163-01ba-4a86ca6a7c40', '部门管理', NULL, NULL, '/org/dept', 1, 'Org:Department', '3a13a4fe-6f74-733b-a628-6125c0325481', 3, 0, '{}', '9eebf993836645a8a1ce41403037a8d8', '2024-07-13 19:47:46.294491', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:36:27.150538', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './Org/Department');
INSERT INTO `sys_menu` VALUES ('3a13be19-204c-f634-f95f-ef8b7dcd9117', '新增', '', NULL, NULL, 2, 'Org.Dept.Add', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 1, 0, '{}', '6d009e7401204af695b5c88447281362', '2024-07-13 19:48:27.341268', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:48:59.893869', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-6b83-eaa2-0194-a9f17b786109', '查询', NULL, NULL, NULL, 2, 'Org.Dept.List', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 2, 0, '{}', '3be3331527094f5db07b09325fe4dff0', '2024-07-13 19:48:46.596368', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:49:23.612289', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-a679-8375-aa20-5ab28ad85890', '编辑', NULL, NULL, NULL, 2, 'Org.Dept.Update', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 3, 0, '{}', '1f2d92eb3e3c4149a89781c1fc56d280', '2024-07-13 19:49:01.689374', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:49:33.553887', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-d4fe-0a05-3dd1-25310c7bd52a', '删除', '', NULL, NULL, 2, 'Org.Dept.Delete', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 4, 0, '{}', '892c10c9223c4bc4aa92a1b998bf961b', '2024-07-13 19:49:13.598822', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:49:51.518226', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be49-5f19-8ebd-5dda-1cf390060a09', '员工列表', NULL, NULL, '/org/employee', 1, 'Org:Employee', '3a13a4fe-6f74-733b-a628-6125c0325481', 4, 0, '{}', '61de35900d1941ab83e173903b48185e', '2024-07-13 20:41:09.171061', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:36:32.587813', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './Org/Employee');
INSERT INTO `sys_menu` VALUES ('3a13be4b-3b01-f611-e5da-8b3a3dc41cfc', '新增', NULL, NULL, NULL, 2, 'Org.Employee.Add', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 1, 0, '{}', '6654d0519e374bb686ae53f5c9f89bb1', '2024-07-13 20:43:10.978954', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:47:54.367167', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4b-8355-c505-5dd3-15fbe89e2639', '查询', NULL, NULL, NULL, 2, 'Org.Employee.List', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 2, 0, '{}', '16c475a8816941c7969963a4dd44b9d5', '2024-07-13 20:43:29.495014', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:48:06.234935', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4c-2e0e-af01-4432-a4892ff622ab', '编辑', NULL, NULL, NULL, 2, 'Org.Employee.Update', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 3, 0, '{}', 'b6130cee13374966aeacc7fa81e630a4', '2024-07-13 20:44:13.199159', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:48:17.084888', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4c-d335-53c6-15e1-b2a16d9f94e4', '删除', NULL, NULL, NULL, 2, 'Org.Employee.Delete', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 4, 0, '{}', 'c5e13e092d4445b999a21ad455e9638c', '2024-07-13 20:44:55.479201', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2025-04-29 20:48:28.036373', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a174174-857e-2328-55e6-395fcffb3774', '系统监控', NULL, 'antd:FundOutlined', '/monitor', 1, 'Monitor', NULL, 3, 0, '{}', '6417b79624f247bb988c9a03c5f6b1a9', '2025-01-04 11:06:54.207263', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-28 17:01:14.281866', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a174175-1893-a38e-c4a2-837cd49e79f6', '在线用户', NULL, NULL, '/monitor/online-user', 1, 'Monitor:OnlineUser', '3a174174-857e-2328-55e6-395fcffb3774', 1, 0, '{}', 'c7eaa7d2d9ec4dfc9c0d3c2532653c1c', '2025-01-04 11:07:31.859814', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 20:37:53.890983', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './Monitor/OnlineUser');
INSERT INTO `sys_menu` VALUES ('3a198c3b-bf80-dce3-f433-f9f221339227', '数据字典', NULL, NULL, '/system/dict', 1, 'Sys:Dict', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 4, 0, '{}', 'd02724bf21bf493687e8b59f71f0dd33', '2025-04-28 09:41:59.312747', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 20:37:01.689127', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Dict/DictType');
INSERT INTO `sys_menu` VALUES ('3a198d86-8791-5c15-2dac-dada4eeda0fd', '字典项', NULL, NULL, '/system/dict-item/:dictType', 1, 'Sys:DictData', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 5, 1, '{}', '72ece5f2492344d78c835ded620d2d9f', '2025-04-28 15:43:17.394293', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 20:37:12.475837', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', './System/Dict/DictData');
INSERT INTO `sys_menu` VALUES ('3a1993c1-445d-b8d0-32e8-d0dfeff83a05', '注销', NULL, NULL, NULL, 2, 'Monitor.Logout', '3a174175-1893-a38e-c4a2-837cd49e79f6', 1, 0, '{}', '08926cd1a43840318719093cfe85b98c', '2025-04-29 20:45:10.110740', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993cc-dce1-53b9-c08b-9b77b244263a', '新增', NULL, NULL, NULL, 2, 'Sys.DictType.Add', '3a198c3b-bf80-dce3-f433-f9f221339227', 1, 0, '{}', '4cff9bc7e6b245de8f2bda4d462848e5', '2025-04-29 20:57:50.052293', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 20:58:03.675259', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a1993ce-9de3-0234-d9b4-3c7012f7e361', '查询', NULL, NULL, NULL, 2, 'Sys.DictType.List', '3a198c3b-bf80-dce3-f433-f9f221339227', 2, 0, '{}', '1c8950c615534e58a31faf9b3e47d1ee', '2025-04-29 20:59:45.015479', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993cf-45ce-6733-402a-63161432073c', '编辑', NULL, NULL, NULL, 2, 'Sys.DictType.Update', '3a198c3b-bf80-dce3-f433-f9f221339227', 3, 0, '{}', 'a5ad9e6261e2419fa844419e65f4ba9b', '2025-04-29 21:00:27.983718', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993d0-7b90-8d33-94c7-f9138a5711c0', '删除', NULL, NULL, '', 2, 'Sys.DictType.Delete', '3a198c3b-bf80-dce3-f433-f9f221339227', 4, 0, '{}', 'e65323fba3044bf4a90abad1788e10ed', '2025-04-29 21:01:47.281134', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993d0-db1b-1ff9-1ce2-85ca968e7e64', '新增', NULL, NULL, NULL, 2, 'Sys.DictData.Add', '3a198d86-8791-5c15-2dac-dada4eeda0fd', 1, 0, '{}', '29f767b493844e6fac8e8de889844a66', '2025-04-29 21:02:11.740137', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993d1-2120-d4d2-e8e9-51b29d3c5cd8', '查询', NULL, NULL, NULL, 2, 'Sys.DictData.List', '3a198d86-8791-5c15-2dac-dada4eeda0fd', 2, 0, '{}', 'b1955ccab2b14c229035dff5f3307c45', '2025-04-29 21:02:29.664810', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 21:02:39.750796', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('3a1993d2-1130-66f0-ca47-f3d73f9fb857', '编辑', NULL, NULL, NULL, 2, 'Sys.DictData.Update', '3a198d86-8791-5c15-2dac-dada4eeda0fd', 3, 0, '{}', '58f41cc48545497cb199c85bcd5865b6', '2025-04-29 21:03:31.121607', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1993d2-62aa-5257-9d47-0d2b1548f5f1', '删除', NULL, NULL, NULL, 2, 'Sys.DictData.Delete', '3a198d86-8791-5c15-2dac-dada4eeda0fd', 1, 0, '{}', 'f108060513d9498689aab8f33fabc762', '2025-04-29 21:03:51.978444', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('5c74b782-3231-11ef-afb3-0242ac110003', '编辑', NULL, NULL, NULL, 2, 'Sys.Menu.Update', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 3, 0, '{}', '313054bc35ac431b8eadd009c8c61da2', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:55:24.956527', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('5c7548d7-3231-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'Sys.Menu.Add', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 1, 0, '{}', '141da654183445569777157a86c66f87', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:54:56.609943', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('5c75c0d0-3231-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'Sys.Menu.List', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 2, 0, '{}', '936661f6595449a5842cee9023b750a1', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:55:11.073904', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('5c767046-3231-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'Sys.Menu.Delete', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 4, 0, '{}', 'a3ac6d5af0db49cc91e1b3583d7c1bb2', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:55:35.173080', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('87cd2f63-3230-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'Sys.Role.Add', '3a132d1f-2026-432a-885f-bf6b10bec15c', 1, 0, '{}', 'ef11ebd27eed4b55bf5991ecb8bd3b90', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:52:43.815643', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('9a844856-3230-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'Sys.Role.List', '3a132d1f-2026-432a-885f-bf6b10bec15c', 2, 0, '{}', 'f3ce283cd40546fbbd67f62582301c11', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:52:56.276398', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('9a84d205-3230-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'Sys.Role.Delete', '3a132d1f-2026-432a-885f-bf6b10bec15c', 4, 0, '{}', 'af013d985288474daeb518dfa508ebfc', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:53:34.572696', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);
INSERT INTO `sys_menu` VALUES ('9a8546e3-3230-11ef-afb3-0242ac110003', '分配菜单', NULL, NULL, NULL, 2, 'Sys.Role.AssignMenu', '3a132d1f-2026-432a-885f-bf6b10bec15c', 5, 0, '{}', 'fdce7f69ec6c4d84b0da999479220992', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2025-04-29 20:53:58.447933', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', NULL);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `RoleName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '角色名',
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '角色表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('3a172369-28a4-e37e-b78a-8c3eaec17359', '系统管理员', '系统默认创建，拥有最高权限', '{}', '5862c3d6e35f4ac3afe9dee4914acd47', '2024-12-29 15:05:53.128472', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_role` VALUES ('3a197cba-ed3a-6ee1-9473-09a7a7e25daa', '121212', '121212', '{}', 'eed03c6bee3147ba8182de328082a699', '2025-04-25 09:26:58.618609', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-25 09:27:03.286007', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 1, '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-25 09:27:03.282872');
INSERT INTO `sys_role` VALUES ('3a197d12-8029-fce1-561f-031c40698e18', '部门主管', NULL, '{}', '943598dd18744ebe927d5d75182279ae', '2025-04-25 11:02:37.866353', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-28 10:49:02.032214', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `sys_rolemenu`;
CREATE TABLE `sys_rolemenu`  (
  `MenuId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '菜单ID',
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`RoleId`, `MenuId`) USING BTREE,
  INDEX `IX_sys_rolemenu_MenuId`(`MenuId` ASC) USING BTREE,
  CONSTRAINT `FK_sys_rolemenu_sys_menu_MenuId` FOREIGN KEY (`MenuId`) REFERENCES `sys_menu` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_sys_rolemenu_sys_role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_rolemenu
-- ----------------------------
INSERT INTO `sys_rolemenu` VALUES ('3a13a4fe-6f74-733b-a628-6125c0325481', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bcf2-3701-be8e-4ec8-ad5f77536101', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bcfc-f11b-7dce-a264-0f34b21085f1', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bcfd-52bb-db4a-d508-eea8536c8bdc', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bdb0-d210-fbaf-ce01-6d658b1b7ec9', '3a197d12-8029-fce1-561f-031c40698e18');
INSERT INTO `sys_rolemenu` VALUES ('3a13bdb1-26a1-79e7-6920-b40e50de0bbe', '3a197d12-8029-fce1-561f-031c40698e18');

-- ----------------------------
-- Table structure for sys_tenant
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenant`;
CREATE TABLE `sys_tenant`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '租户名称',
  `ConnectionString` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '连接字符串',
  `RedisConnection` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Redis连接',
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '租户表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_tenant
-- ----------------------------

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '用户名',
  `Password` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '密码',
  `PasswordSalt` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '密码盐',
  `Avatar` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '头像',
  `NickName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '昵称',
  `Sex` int NOT NULL COMMENT '性别',
  `IsEnabled` tinyint(1) NOT NULL COMMENT '是否启用',
  `ExtraProperties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConcurrencyStamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('3a172a37-55d5-ee9b-dc92-e07386eadc7c', 'admin', '07adb07c44884292e13ffeb4dea8668f', 'ehdOJOf/roWvyWn/GddGwQ==', 'http://localhost:5000/file/default/a74bb0c10e2048c5a4c63cac59aba808.jpg', 'admin', 2, 1, '{}', 'ad77eedb27c64a59ba633a8a200db4ae', '2024-12-30 22:48:48.457636', NULL, '2025-04-28 10:46:53.382563', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 0, NULL, NULL);
INSERT INTO `sys_user` VALUES ('3a198c7a-47c3-7d79-13b8-282b3dd60b05', 'cracker', 'c45e5d9b0971c47b92144a15afc8c951', '1vLpyt4/YkX+2zCvImTaTg==', 'http://localhost:5000/file/avatar/male.png', 'cracker', 1, 1, '{}', '8a31646f98044c58ac50c4a187fbf33b', '2025-04-28 10:50:17.412826', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', '2025-04-29 20:41:29.281355', '3a172a37-55d5-ee9b-dc92-e07386eadc7c', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_userrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_userrole`;
CREATE TABLE `sys_userrole`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '用户ID',
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE,
  INDEX `IX_sys_userrole_RoleId`(`RoleId` ASC) USING BTREE,
  CONSTRAINT `FK_sys_userrole_sys_role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_sys_userrole_sys_user_UserId` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户角色关联表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of sys_userrole
-- ----------------------------
INSERT INTO `sys_userrole` VALUES ('3a172a37-55d5-ee9b-dc92-e07386eadc7c', '3a172369-28a4-e37e-b78a-8c3eaec17359');
INSERT INTO `sys_userrole` VALUES ('3a198c7a-47c3-7d79-13b8-282b3dd60b05', '3a197d12-8029-fce1-561f-031c40698e18');

SET FOREIGN_KEY_CHECKS = 1;
