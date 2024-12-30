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

 Date: 29/12/2024 22:13:19
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20241229070339_InitCreated', '8.0.4');

-- ----------------------------
-- Table structure for gentable
-- ----------------------------
DROP TABLE IF EXISTS `gentable`;
CREATE TABLE `gentable`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TableName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '表名',
  `Comment` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '表描述',
  `BusinessName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '业务名',
  `EntityName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '实体名',
  `ModuleName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '模块名',
  `Types` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '实现接口/继承抽象类',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `IX_GenTable_TableName`(`TableName` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '生成表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gentable
-- ----------------------------
INSERT INTO `gentable` VALUES ('3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', '员工表', 'Dept', 'orgdept', 'Developer', NULL, '2024-12-29 20:03:58.345843', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentable` VALUES ('3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', '员工表', 'Employee', 'orgemployee', 'Developer', NULL, '2024-12-29 20:04:47.482314', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);

-- ----------------------------
-- Table structure for gentablecolumn
-- ----------------------------
DROP TABLE IF EXISTS `gentablecolumn`;
CREATE TABLE `gentablecolumn`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GenTableId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TableName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '表名',
  `ColumnName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '列名',
  `CsharpPropName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'C#属性名',
  `JsFieldName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'JS字段名',
  `ColumnType` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '数据库列类型',
  `CsharpType` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'C#类型',
  `JsType` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'JS类型',
  `HtmlType` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'HTML类型',
  `Comment` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '列描述',
  `MaxLength` bigint NULL DEFAULT NULL COMMENT '最大长度',
  `IsNullable` tinyint(1) NOT NULL COMMENT '是否可空',
  `IsInsert` tinyint(1) NOT NULL COMMENT '是否参与新增',
  `IsUpdate` tinyint(1) NOT NULL COMMENT '是否参与修改',
  `IsSearch` tinyint(1) NOT NULL COMMENT '是否参与搜索',
  `SearchType` int NOT NULL COMMENT '搜索类型',
  `IsShow` tinyint(1) NOT NULL COMMENT '是否在表格中显示',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_GenTableColumn_GenTableId`(`GenTableId` ASC) USING BTREE,
  CONSTRAINT `FK_GenTableColumn_GenTable_GenTableId` FOREIGN KEY (`GenTableId`) REFERENCES `gentable` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '生成表的配置列' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of gentablecolumn
-- ----------------------------
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114c-0777-3ecb-c2af317d8107', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Id', 'Id', 'id', 'char', 'Guid', 'string', 'text', '', 36, 0, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.448393', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114c-49e5-d9c7-43c675172de6', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Code', 'Code', 'code', 'varchar', 'string', 'string', 'text', '部门编号', 32, 0, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.449087', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114c-6a44-fdc7-ee7d45854703', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Name', 'Name', 'name', 'varchar', 'string', 'string', 'text', '部门名称', 64, 0, 1, 1, 1, 2, 1, '2024-12-29 20:03:58.450806', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-1cb0-5e07-4f708dd0c0e3', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Status', 'Status', 'status', 'int', 'int', 'number', 'text', '状态：1正常2停用', NULL, 0, 1, 1, 1, 2, 1, '2024-12-29 20:03:58.451278', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-204a-264a-5960dbf4f5bd', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'CuratorId', 'Curatorid', 'curatorid', 'char', 'Guid?', 'string?', 'text', '负责人', 36, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451343', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-2b0a-546b-ce3a477fd1ea', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'DeletionTime', 'Deletiontime', 'deletiontime', 'datetime', 'DateTime?', 'Date?', 'datetime', '', NULL, 1, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.452271', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-37c7-50a6-b17f34b25004', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'LastModifierId', 'Lastmodifierid', 'lastmodifierid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.452098', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-433f-e2ff-a963bf505bfd', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'IsDeleted', 'Isdeleted', 'isdeleted', 'tinyint', 'sbyte', 'number', 'text', '', NULL, 0, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.452155', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-4ec8-012b-b1d50eb48b35', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Layer', 'Layer', 'layer', 'int', 'int', 'number', 'text', '层级', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451723', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-51a3-eec5-ddf99f8d0d31', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'ExtraProperties', 'Extraproperties', 'extraproperties', 'longtext', 'string', 'string', 'text', '', 4294967295, 0, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.451798', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-5314-ca0c-422808c45072', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Description', 'Description', 'description', 'varchar', 'string?', 'string?', 'text', '描述', 512, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451204', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-5e56-1406-ee3b11b18f00', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Sort', 'Sort', 'sort', 'int', 'int', 'number', 'text', '排序', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451130', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-b12f-4b78-c8e66feded38', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'ParentIds', 'Parentids', 'parentids', 'varchar', 'string?', 'string?', 'text', '层级父ID', 1024, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451652', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-b6e2-a2d9-4ec2823baf37', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'DeleterId', 'Deleterid', 'deleterid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.452213', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-bebb-1643-acccb41194cc', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Phone', 'Phone', 'phone', 'varchar', 'string?', 'string?', 'text', '电话', 64, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451517', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-d1ea-455a-5e4baa299cbd', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'ConcurrencyStamp', 'Concurrencystamp', 'concurrencystamp', 'varchar', 'string', 'string', 'text', '', 40, 0, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.451859', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-dfbb-e068-dfdf84b45955', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'CreatorId', 'Creatorid', 'creatorid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.451979', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-e1ff-d31a-e6af23c91deb', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'Email', 'Email', 'email', 'varchar', 'string?', 'string?', 'text', '邮箱', 64, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451445', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-f18a-3378-cffe5876aaae', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'LastModificationTime', 'Lastmodificationtime', 'lastmodificationtime', 'datetime', 'DateTime?', 'Date?', 'datetime', '', NULL, 1, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.452037', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-f6de-83e4-7c59cf55e788', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'CreationTime', 'Creationtime', 'creationtime', 'datetime', 'DateTime', 'Date', 'datetime', '', NULL, 0, 0, 0, 0, 2, 0, '2024-12-29 20:03:58.451922', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-114d-fafe-a7ea-d06e13af0054', '3a17247a-10f7-f4ed-21d6-dc5b5492c2e3', 'orgdept', 'ParentId', 'Parentid', 'parentid', 'char', 'Guid?', 'string?', 'text', '父ID', 36, 1, 1, 1, 0, 2, 1, '2024-12-29 20:03:58.451581', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d109-7d86-a0e9-71fe552b8a88', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Id', 'Id', 'id', 'char', 'Guid', 'string', 'text', '', 36, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498453', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-0053-bd6c-c79c7687a3ea', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'ConcurrencyStamp', 'Concurrencystamp', 'concurrencystamp', 'varchar', 'string', 'string', 'text', '', 40, 0, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499415', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-0059-e608-80028e423577', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'LastModifierId', 'Lastmodifierid', 'lastmodifierid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499583', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-0d0a-2222-91f0fda6ff2a', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'ExtraProperties', 'Extraproperties', 'extraproperties', 'longtext', 'string', 'string', 'text', '', 4294967295, 0, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499374', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-0d6c-745f-06b0c05f8a76', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Address', 'Address', 'address', 'varchar', 'string?', 'string?', 'text', '现住址', 512, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499010', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-10b0-c5df-c8e422aa951f', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'OutTime', 'Outtime', 'outtime', 'datetime', 'DateTime?', 'Date?', 'datetime', '离职时间', NULL, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499155', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-1cff-c596-f63c0aefb679', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'IsDeleted', 'Isdeleted', 'isdeleted', 'tinyint', 'sbyte', 'number', 'text', '', NULL, 0, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499626', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-330e-ab07-3d3970e7ce21', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'DeptId', 'Deptid', 'deptid', 'char', 'Guid', 'string', 'text', '部门ID', 36, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499289', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-5382-7c0f-d75159cc0a26', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'UserId', 'Userid', 'userid', 'char', 'Guid?', 'string?', 'text', '关联用户ID', 36, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499247', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-6854-1879-93c1c3d8f5f9', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Code', 'Code', 'code', 'varchar', 'string', 'string', 'text', '工号', 64, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498548', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-7d1a-1923-3be8ada56c53', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'IdNo', 'Idno', 'idno', 'varchar', 'string', 'string', 'text', '身份证', 32, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498771', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-7e1a-9ec4-aa6d6be27187', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'PositionId', 'Positionid', 'positionid', 'char', 'Guid', 'string', 'text', '职位ID', 36, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499331', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-8a07-47dc-ba0547ec5e80', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'BirthDay', 'Birthday', 'birthday', 'datetime', 'DateTime', 'Date', 'datetime', '生日', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498967', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-8cbb-88dc-fa7b5742ebdb', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Name', 'Name', 'name', 'varchar', 'string', 'string', 'text', '姓名', 64, 0, 1, 1, 1, 2, 1, '2024-12-29 20:04:47.498615', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-8cd6-3819-4c0058b4476c', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'DeletionTime', 'Deletiontime', 'deletiontime', 'datetime', 'DateTime?', 'Date?', 'datetime', '', NULL, 1, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499714', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-8e2e-cdc5-443e22f102f1', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Phone', 'Phone', 'phone', 'varchar', 'string', 'string', 'text', '手机号码', 16, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498719', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-9141-24e0-61afb64ccf26', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'BackIdNoUrl', 'Backidnourl', 'backidnourl', 'varchar', 'string?', 'string?', 'text', '身份证背面', 512, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498917', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-a766-0732-d50bfa3a852a', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'DeleterId', 'Deleterid', 'deleterid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499671', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-a98e-1e66-6397246cb31c', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'CreatorId', 'Creatorid', 'creatorid', 'char', 'Guid?', 'string?', 'text', '', 36, 1, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499499', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-b59d-4382-82a9408def87', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Email', 'Email', 'email', 'varchar', 'string?', 'string?', 'text', '邮箱', 64, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499051', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-cc4e-05ef-238937d4b12c', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'InTime', 'Intime', 'intime', 'datetime', 'DateTime', 'Date', 'datetime', '入职时间', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499096', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-d38f-a1ff-3ab0dc0d1d9f', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'Sex', 'Sex', 'sex', 'int', 'int', 'number', 'text', '性别', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498670', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-d457-ccc5-534f9ac5c750', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'LastModificationTime', 'Lastmodificationtime', 'lastmodificationtime', 'datetime', 'DateTime?', 'Date?', 'datetime', '', NULL, 1, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499541', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-d806-e193-f3351a9fe880', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'FrontIdNoUrl', 'Frontidnourl', 'frontidnourl', 'varchar', 'string?', 'string?', 'text', '身份证正面', 512, 1, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.498859', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-ebd5-017e-1e50135aaf0c', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'IsOut', 'Isout', 'isout', 'tinyint', 'sbyte', 'number', 'text', '是否离职', NULL, 0, 1, 1, 0, 2, 1, '2024-12-29 20:04:47.499198', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);
INSERT INTO `gentablecolumn` VALUES ('3a17247a-d10a-f4a8-1266-d873f9fba526', '3a17247a-d0f9-559b-6cf4-ff5644780a4c', 'orgemployee', 'CreationTime', 'Creationtime', 'creationtime', 'datetime', 'DateTime', 'Date', 'datetime', '', NULL, 0, 0, 0, 0, 2, 0, '2024-12-29 20:04:47.499456', '3a172369-2963-bc18-82bc-b3d0da8c574f', NULL, NULL);

-- ----------------------------
-- Table structure for orgdept
-- ----------------------------
DROP TABLE IF EXISTS `orgdept`;
CREATE TABLE `orgdept`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '员工表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orgdept
-- ----------------------------

-- ----------------------------
-- Table structure for orgdeptemployee
-- ----------------------------
DROP TABLE IF EXISTS `orgdeptemployee`;
CREATE TABLE `orgdeptemployee`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '员工关联部门' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orgdeptemployee
-- ----------------------------

-- ----------------------------
-- Table structure for orgemployee
-- ----------------------------
DROP TABLE IF EXISTS `orgemployee`;
CREATE TABLE `orgemployee`  (
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
  `IsOut` tinyint(1) NOT NULL COMMENT '是否离职',
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '员工表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orgemployee
-- ----------------------------

-- ----------------------------
-- Table structure for orgposition
-- ----------------------------
DROP TABLE IF EXISTS `orgposition`;
CREATE TABLE `orgposition`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '职位表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orgposition
-- ----------------------------

-- ----------------------------
-- Table structure for orgpositiongroup
-- ----------------------------
DROP TABLE IF EXISTS `orgpositiongroup`;
CREATE TABLE `orgpositiongroup`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GroupName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '分组名',
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ParentId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父ID',
  `ParentIds` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '层级父ID',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '职位分组' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orgpositiongroup
-- ----------------------------

-- ----------------------------
-- Table structure for sysbusinesslog
-- ----------------------------
DROP TABLE IF EXISTS `sysbusinesslog`;
CREATE TABLE `sysbusinesslog`  (
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
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '业务日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysbusinesslog
-- ----------------------------

-- ----------------------------
-- Table structure for sysdict
-- ----------------------------
DROP TABLE IF EXISTS `sysdict`;
CREATE TABLE `sysdict`  (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Key` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Label` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `GroupName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sort` int NOT NULL,
  `IsEnabled` tinyint(1) NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `LastModificationTime` datetime(6) NULL DEFAULT NULL,
  `LastModifierId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysdict
-- ----------------------------

-- ----------------------------
-- Table structure for sysloginlog
-- ----------------------------
DROP TABLE IF EXISTS `sysloginlog`;
CREATE TABLE `sysloginlog`  (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `Ip` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'IP',
  `Address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '登录地址',
  `Os` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '系统',
  `Browser` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器',
  `OperationMsg` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器操作信息',
  `IsSuccess` tinyint(1) NOT NULL COMMENT '是否成功',
  `CreationTime` datetime(6) NOT NULL,
  `CreatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '登录日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysloginlog
-- ----------------------------

-- ----------------------------
-- Table structure for sysmenu
-- ----------------------------
DROP TABLE IF EXISTS `sysmenu`;
CREATE TABLE `sysmenu`  (
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
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  `DeleterId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `DeletionTime` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '菜单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysmenu
-- ----------------------------
INSERT INTO `sysmenu` VALUES ('3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', '系统管理', NULL, 'ant-design:setting-filled', '/system', 1, NULL, NULL, 2, 0, '{}', '0bfb8102d95c4abfafb1ecf150626aac', '2024-06-15 15:49:13.507249', '3a132908-ca06-34de-164e-21c96505a036', '2024-07-08 22:48:53.115413', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a132d16-df35-09cb-9f50-0a83e8290575', '用户管理', NULL, '', '/system/user', 1, '', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 1, 0, '{}', '24d8c968541b4b0db505dfdf6e49988b', '2024-06-15 16:01:03.300935', '3a132908-ca06-34de-164e-21c96505a036', '2024-07-08 22:56:35.333303', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a132d1f-2026-432a-885f-bf6b10bec15c', '角色管理', NULL, NULL, '/system/roles', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 2, 0, '{}', '824ab5eb475348d39d2217e824c826b0', '2024-06-15 16:10:04.215040', '3a132908-ca06-34de-164e-21c96505a036', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a132d1f-e2dd-7447-ac4b-2250201a9bad', '菜单管理', NULL, NULL, '/system/menus', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 3, 0, '{}', 'be3c510a07494b09a27b2c4b2d07c0cd', '2024-06-15 16:10:54.046247', '3a132908-ca06-34de-164e-21c96505a036', '2024-06-15 17:34:09.717457', '3a132908-ca06-34de-164e-21c96505a036', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a1356be-ac7a-f1a9-e45e-397a7e841149', '数据字典', NULL, NULL, '/system/dict', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 4, 0, '{}', '4d8490425be14b57bb82e6808b7d6d96', '2024-06-23 18:08:46.203010', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135caa-6050-b8fa-ba75-6aaf548a7683', '新增', NULL, NULL, NULL, 2, 'admin_system_user_add', '3a132d16-df35-09cb-9f50-0a83e8290575', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135caa-b115-4de4-3be5-4b3cc477d8f4', '查询', NULL, NULL, NULL, 2, 'admin_system_user_list', '3a132d16-df35-09cb-9f50-0a83e8290575', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135cab-3200-f6de-41f9-948404a81884', '删除', NULL, NULL, NULL, 2, 'admin_system_user_delete', '3a132d16-df35-09cb-9f50-0a83e8290575', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135cab-7acb-41cf-30c4-720eea400b2c', '分配角色', NULL, NULL, NULL, 2, 'admin_system_user_assignrole', '3a132d16-df35-09cb-9f50-0a83e8290575', 4, 0, '{}', 'cfc407a5c15f4186bcaf76c94479bace', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135cab-fcbb-1f19-8416-25c218db4279', '启用/禁用', NULL, NULL, NULL, 2, 'admin_system_user_changeenabled', '3a132d16-df35-09cb-9f50-0a83e8290575', 1, 0, '{}', 'a4a5bdbcc4d8405798476a17ec123c53', '2024-06-24 21:46:04.859652', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135cb0-3753-ae33-82fd-603c3622f661', '编辑', NULL, NULL, NULL, 2, 'admin_system_role_update', '3a132d1f-2026-432a-885f-bf6b10bec15c', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a135cc6-eea2-5551-e3a2-245ff43fcf01', '刷新缓存', NULL, NULL, NULL, 2, 'admin_system_dict_refresh', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 5, 0, '{}', 'aff5b83d52e440da8a2a636560eaac82', '2024-06-24 22:15:30.725052', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a138b11-d573-3e37-298a-c67a014e430b', '日志管理', NULL, NULL, '/logManagement', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 5, 0, '{}', '361b2b7c73804b2f845979b4b9780bdd', '2024-07-03 21:59:51.413081', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-03 22:00:02.918234', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a138b12-93b5-e723-1539-adaeb17a2ae1', '登录日志', NULL, NULL, '/logManagement/loginLogs', 1, 'admin_system_loginlog_list', '3a138b11-d573-3e37-298a-c67a014e430b', 1, 0, '{}', '6127c487e28f48eb846badcb8d45a370', '2024-07-03 22:00:40.118092', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-24 19:59:42.559353', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a138b13-fccd-7b4a-0bb5-435a9d9c4172', '业务日志', NULL, NULL, '/logManagement/businessLogs', 1, 'admin_system_businesslog_list', '3a138b11-d573-3e37-298a-c67a014e430b', 2, 0, '{}', '3e60c8ee4742449a81eb2042635d1596', '2024-07-03 22:02:12.559269', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-24 19:59:52.333522', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13a4fe-6f74-733b-a628-6125c0325481', '组织架构', NULL, 'ri:user-fill', '/organization', 1, NULL, NULL, 1, 0, '{}', '93e9172908db4ada8fc847d6e2ecad2c', '2024-07-08 22:48:47.742259', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', '2024-07-08 22:54:10.348666', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bcf2-3701-be8e-4ec8-ad5f77536101', '职位分组', NULL, NULL, '/organization/positionGroup', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 1, 0, '{}', '2da10e75eec746a8be0485fd27961cdb', '2024-07-13 14:26:20.046127', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 14:26:29.336149', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bcfc-f11b-7dce-a264-0f34b21085f1', '新增', NULL, NULL, NULL, 2, 'admin_system_positiongroup_add', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '9a826b054fb34f9ab7fa17c6faf98027', '2024-07-13 14:38:03.055512', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bcfd-52bb-db4a-d508-eea8536c8bdc', '查询', NULL, NULL, NULL, 2, 'admin_system_positiongroup_list', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 2, 0, '{}', '89c73d12d4094672932cec8c676f9dfb', '2024-07-13 14:38:28.028042', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bcfd-90b2-02ef-4440-8062594e3d79', '编辑', NULL, NULL, NULL, 2, 'admin_system_positiongroup_update', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '624f6ff8e265403db40c9f3ece0ab41a', '2024-07-13 14:38:43.893385', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bcfd-c549-8f84-1941-1d6baccfd6b4', '删除', '', NULL, NULL, 2, 'admin_system_positiongroup_delete', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '978521f1290347b991792b181b2a8d6a', '2024-07-13 14:38:57.354973', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 14:39:02.834469', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', '职位管理', NULL, NULL, '/organization/position', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 2, 0, '{}', 'c0e89e402eb747f4bc920eea1221c810', '2024-07-13 17:52:45.803191', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bdb0-d210-fbaf-ce01-6d658b1b7ec9', '新增', NULL, NULL, NULL, 2, 'admin_system_position_add', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 1, 0, '{}', '57f606dc1d164ee585fef364d6fd735f', '2024-07-13 17:54:31.568625', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bdb1-26a1-79e7-6920-b40e50de0bbe', '查询', '', NULL, NULL, 2, 'admin_system_position_list', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 2, 0, '{}', '52bc36543df24f6dac1714131f5219d5', '2024-07-13 17:54:53.219392', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 17:55:17.296224', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bdb1-742f-1ff2-87d1-b07a807c791f', '编辑', NULL, NULL, NULL, 2, 'admin_system_position_update', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 3, 0, '{}', '1e9dd90dcc14442d8fdb70e6e73c644b', '2024-07-13 17:55:13.072355', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13bdb2-213b-d9fa-d067-287801312ea1', '删除', NULL, NULL, NULL, 2, 'admin_system_position_delete', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 4, 0, '{}', '90b12c1a10f84bf4aee0e6e0784d063a', '2024-07-13 17:55:57.372951', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be18-7fe2-2163-01ba-4a86ca6a7c40', '部门管理', NULL, NULL, '/organization/dept', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 3, 0, '{}', '538e75fe37fd4cc39ae6ae5df7e18990', '2024-07-13 19:47:46.294491', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be19-204c-f634-f95f-ef8b7dcd9117', '新增', '', NULL, NULL, 2, 'admin_system_dept_add', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 1, 0, '{}', 'fd9e76e3f569464db3ce31cb234c05f3', '2024-07-13 19:48:27.341268', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be19-6b83-eaa2-0194-a9f17b786109', '查询', NULL, NULL, NULL, 2, 'admin_system_dept_list', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 2, 0, '{}', '9090f8ff34df441190f26b250e3d54ae', '2024-07-13 19:48:46.596368', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be19-a679-8375-aa20-5ab28ad85890', '编辑', NULL, NULL, NULL, 2, 'admin_system_dept_update', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 3, 0, '{}', 'a2794573859e4943874670b579b342f2', '2024-07-13 19:49:01.689374', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be19-d4fe-0a05-3dd1-25310c7bd52a', '删除', '', NULL, NULL, 2, 'admin_system_dept_delete', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 1, 0, '{}', '1d5ef8e9b846479cbc55bf73d20f5fa3', '2024-07-13 19:49:13.598822', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be49-5f19-8ebd-5dda-1cf390060a09', '员工列表', NULL, NULL, '/organization/employee', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 4, 0, '{}', 'cecdbe7cd75749248782f450379b2c13', '2024-07-13 20:41:09.171061', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be4b-3b01-f611-e5da-8b3a3dc41cfc', '新增', NULL, NULL, NULL, 2, 'admin_system_employee_add', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 1, 0, '{}', 'a676ba69e17248ed9153710e3e5a8f55', '2024-07-13 20:43:10.978954', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 20:44:33.952170', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be4b-8355-c505-5dd3-15fbe89e2639', '查询', NULL, NULL, NULL, 2, 'admin_system_employee_list', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 2, 0, '{}', '990c7640f41843cfa8faee786190ec74', '2024-07-13 20:43:29.495014', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be4c-2e0e-af01-4432-a4892ff622ab', '编辑', NULL, NULL, NULL, 2, 'admin_system_employee_update', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 3, 0, '{}', '38bd3f28295645c4ab5590251a4b7eae', '2024-07-13 20:44:13.199159', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a13be4c-d335-53c6-15e1-b2a16d9f94e4', '删除', NULL, NULL, NULL, 2, 'admin_system_employee_delete', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 4, 0, '{}', '66d5b6682fda443c818c3e7b2c846432', '2024-07-13 20:44:55.479201', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a1723a7-da37-9340-e106-341f0e17fe6e', '开发工具', NULL, 'mingcute:code-fill', 'developer', 1, NULL, NULL, 3, 0, '{}', '34ecd14b0b0941039f3b19b5c5068d36', '2024-12-29 16:14:21.752625', '3a172369-2963-bc18-82bc-b3d0da8c574f', '2024-12-29 16:16:21.087105', '3a172369-2963-bc18-82bc-b3d0da8c574f', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('3a1723b7-5702-dc80-434f-f639ffcb66ef', '代码生成', NULL, NULL, '/develop/genTable', 1, NULL, '3a1723a7-da37-9340-e106-341f0e17fe6e', 1, 0, '{}', '02295c18cc1d4eaaba0ff3689146d697', '2024-12-29 16:31:16.739378', '3a172369-2963-bc18-82bc-b3d0da8c574f', '2024-12-29 16:33:57.053851', '3a172369-2963-bc18-82bc-b3d0da8c574f', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('5c74b782-3231-11ef-afb3-0242ac110003', '编辑', NULL, NULL, NULL, 2, 'admin_system_menu_update', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('5c7548d7-3231-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_menu_add', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('5c75c0d0-3231-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_menu_list', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('5c767046-3231-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_menu_delete', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('7e1b4c13-3231-11ef-afb3-0242ac110003', '编辑', NULL, NULL, NULL, 2, 'admin_system_dict_update', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('7e1cbcbf-3231-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_dict_add', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('7e1d3cd0-3231-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_dict_list', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('7e1db553-3231-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_dict_delete', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('87cd2f63-3230-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_role_add', '3a132d1f-2026-432a-885f-bf6b10bec15c', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('9a844856-3230-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_role_list', '3a132d1f-2026-432a-885f-bf6b10bec15c', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('9a84d205-3230-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_role_delete', '3a132d1f-2026-432a-885f-bf6b10bec15c', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sysmenu` VALUES ('9a8546e3-3230-11ef-afb3-0242ac110003', '分配菜单', NULL, NULL, NULL, 2, 'admin_system_role_assignmenu', '3a132d1f-2026-432a-885f-bf6b10bec15c', 5, 0, '{}', '3a37914cba334a039eaabcef9d88260a', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:05.971863', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sysrole
-- ----------------------------
DROP TABLE IF EXISTS `sysrole`;
CREATE TABLE `sysrole`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysrole
-- ----------------------------
INSERT INTO `sysrole` VALUES ('3a172369-28a4-e37e-b78a-8c3eaec17359', '系统管理员', '系统默认创建，拥有最高权限', '{}', '5862c3d6e35f4ac3afe9dee4914acd47', '2024-12-29 15:05:53.128472', NULL, NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sysrolemenu
-- ----------------------------
DROP TABLE IF EXISTS `sysrolemenu`;
CREATE TABLE `sysrolemenu`  (
  `MenuId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '菜单ID',
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`RoleId`, `MenuId`) USING BTREE,
  INDEX `IX_SysRoleMenu_MenuId`(`MenuId` ASC) USING BTREE,
  CONSTRAINT `FK_SysRoleMenu_SysMenu_MenuId` FOREIGN KEY (`MenuId`) REFERENCES `sysmenu` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_SysRoleMenu_SysRole_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sysrole` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysrolemenu
-- ----------------------------

-- ----------------------------
-- Table structure for sysuser
-- ----------------------------
DROP TABLE IF EXISTS `sysuser`;
CREATE TABLE `sysuser`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysuser
-- ----------------------------
INSERT INTO `sysuser` VALUES ('3a172369-2963-bc18-82bc-b3d0da8c574f', 'admin', '71e024f12c38ff5198e3ee4f60e2f1f9', 'iqbglILuEZHo/sSWGijq/w==', 'avatar/female.png', 'admin', 0, 1, '{}', 'a020bc0d79bc4527895b78d0eb82681e', '2024-12-29 15:05:53.280210', NULL, NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sysuserrole
-- ----------------------------
DROP TABLE IF EXISTS `sysuserrole`;
CREATE TABLE `sysuserrole`  (
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '用户ID',
  `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE,
  INDEX `IX_SysUserRole_RoleId`(`RoleId` ASC) USING BTREE,
  CONSTRAINT `FK_SysUserRole_SysRole_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `sysrole` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_SysUserRole_SysUser_UserId` FOREIGN KEY (`UserId`) REFERENCES `sysuser` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户角色关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysuserrole
-- ----------------------------
INSERT INTO `sysuserrole` VALUES ('3a172369-2963-bc18-82bc-b3d0da8c574f', '3a172369-28a4-e37e-b78a-8c3eaec17359');

SET FOREIGN_KEY_CHECKS = 1;
