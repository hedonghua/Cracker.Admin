/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 80400
 Source Host           : 127.0.0.1:3306
 Source Schema         : re_admin

 Target Server Type    : MySQL
 Target Server Version : 80400
 File Encoding         : 65001

 Date: 26/07/2024 20:57:58
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

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
INSERT INTO `__EFMigrationsHistory` VALUES ('20240723121224_InitCreate', '8.0.0');
INSERT INTO `__EFMigrationsHistory` VALUES ('20240724113805_UpdateFiles', '8.0.0');

-- ----------------------------
-- Table structure for org_dept
-- ----------------------------
DROP TABLE IF EXISTS `org_dept`;
CREATE TABLE `org_dept`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '部门编号',
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '部门名称',
  `sort` int(0) NOT NULL COMMENT '排序',
  `description` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '描述',
  `status` int(0) NOT NULL COMMENT '状态：1正常2停用',
  `curatorId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '负责人',
  `email` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '邮箱',
  `phone` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '电话',
  `parent_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父ID',
  `parent_ids` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '层级IDS',
  `layer` int(0) NOT NULL COMMENT '层级',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of org_dept
-- ----------------------------
INSERT INTO `org_dept` VALUES ('3a13f1b2-f089-30ce-cedc-18f507cbbe11', '638573626256654133', '华汐科技', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, '{}', 'da8f61b3109548319cf1905c8631f8db', '2024-07-23 20:17:05.749226', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for org_dept_employee
-- ----------------------------
DROP TABLE IF EXISTS `org_dept_employee`;
CREATE TABLE `org_dept_employee`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `employee_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '员工ID',
  `dept_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '部门ID',
  `position_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '职位ID',
  `is_main` tinyint(1) NOT NULL COMMENT '是否主职位',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of org_dept_employee
-- ----------------------------
INSERT INTO `org_dept_employee` VALUES ('3a13f1bf-4b96-53c3-7136-5db867882f4f', '3a13f1bf-4b76-c7ac-0d0c-0dd68462ad5f', '3a13f1b2-f089-30ce-cedc-18f507cbbe11', '3a13f1bc-5c50-2d95-a3ce-9b04170e694d', 1, '{}', '1c01e3aca7154350adc94c7819391a4a', '2024-07-23 20:30:35.429788', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-23 20:44:46.577777', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);

-- ----------------------------
-- Table structure for org_employee
-- ----------------------------
DROP TABLE IF EXISTS `org_employee`;
CREATE TABLE `org_employee`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `code` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '工号',
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '姓名',
  `Sex` int(0) NOT NULL COMMENT '性别',
  `phone` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '手机号码',
  `idno` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '身份证',
  `front_idno_url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '身份证正面',
  `back_idno_url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '身份证背面',
  `birthday` datetime(6) NOT NULL COMMENT '生日',
  `address` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '现住址',
  `email` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '邮箱',
  `in_time` datetime(6) NOT NULL COMMENT '入职时间',
  `out_time` datetime(6) NULL DEFAULT NULL COMMENT '离职时间',
  `is_out` tinyint(1) NOT NULL COMMENT '是否离职',
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '关联用户ID',
  `dept_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '部门ID',
  `position_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '职位ID',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of org_employee
-- ----------------------------
INSERT INTO `org_employee` VALUES ('3a13f1bf-4b76-c7ac-0d0c-0dd68462ad5f', '45001', '张三', 1, '19011111111', '370611201505249976', 'http://localhost:44383/oss/preview/ef8af675-4bdb-4f2a-b37c-6116e17d1235', NULL, '2024-07-24 00:00:00.000000', NULL, NULL, '2024-07-11 00:00:00.000000', NULL, 0, NULL, '3a13f1b2-f089-30ce-cedc-18f507cbbe11', '3a13f1bc-5c50-2d95-a3ce-9b04170e694d', '{}', '774f5eb7cde847a9a7df0b13812007c9', '2024-07-23 20:30:35.412977', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-24 19:42:59.697376', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);

-- ----------------------------
-- Table structure for org_position
-- ----------------------------
DROP TABLE IF EXISTS `org_position`;
CREATE TABLE `org_position`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '职位编号',
  `name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '职位名称',
  `level` int(0) NOT NULL COMMENT '职级',
  `status` int(0) NOT NULL COMMENT '状态：1正常2停用',
  `description` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '描述',
  `group_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '职位分组ID',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of org_position
-- ----------------------------
INSERT INTO `org_position` VALUES ('3a13f1bc-5c50-2d95-a3ce-9b04170e694d', '638573632430878671', '大堂经理', 1, 1, NULL, '3a13f1bc-0ac2-1318-60d9-bbaa444a256c', '{}', '3998b6e7fb434823b4916aa5fb649850', '2024-07-23 20:27:23.129765', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for org_position_group
-- ----------------------------
DROP TABLE IF EXISTS `org_position_group`;
CREATE TABLE `org_position_group`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `group_name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '职位名',
  `remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `parent_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父级ID',
  `parent_ids` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '层级父ID',
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of org_position_group
-- ----------------------------
INSERT INTO `org_position_group` VALUES ('3a13f1bc-0ac2-1318-60d9-bbaa444a256c', '主管', NULL, NULL, NULL, '2024-07-23 20:27:02.243166', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL);

-- ----------------------------
-- Table structure for sys_business_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_business_log`;
CREATE TABLE `sys_business_log`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `action` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作方法，全名',
  `http_method` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'http请求方式',
  `url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '请求地址',
  `os` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '系统',
  `browser` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器',
  `node_name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '操作节点名',
  `ip` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'IP',
  `address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '登录地址',
  `is_success` tinyint(1) NOT NULL COMMENT '是否成功',
  `operation_msg` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '操作信息',
  `mill_seconds` int(0) NOT NULL COMMENT '耗时，单位毫秒',
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_business_log
-- ----------------------------
INSERT INTO `sys_business_log` VALUES (1, 'superadmin', 'Re.Admin.Controllers.System.DictController.AddDictAsync (Re.Admin.HttpApi)', 'POST', '/adm/dict/add', 'Windows', 'Chrome', '新增字典', '::1', '未知', 1, NULL, 1, '2024-07-24 19:46:34.387965', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);
INSERT INTO `sys_business_log` VALUES (2, 'superadmin', 'Re.Admin.Controllers.System.UserController.AddUserAsync (Re.Admin.HttpApi)', 'POST', '/adm/user/add', 'Windows', 'Chrome', '新增用户', '::1', '未知', 1, NULL, 0, '2024-07-24 19:47:42.124391', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);
INSERT INTO `sys_business_log` VALUES (3, 'superadmin', 'Re.Admin.Controllers.System.UserController.AddUserAsync (Re.Admin.HttpApi)', 'POST', '/adm/user/add', 'Windows', 'Chrome', '新增用户', '::1', '未知', 1, NULL, 63, '2024-07-24 19:50:04.552432', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);
INSERT INTO `sys_business_log` VALUES (4, 'superadmin', 'Re.Admin.Controllers.System.UserController.AssignRoleAsync (Re.Admin.HttpApi)', 'POST', '/adm/user/assign-role', 'Windows', 'Chrome', '分配角色', '::1', '未知', 1, NULL, 0, '2024-07-24 19:51:09.503361', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);
INSERT INTO `sys_business_log` VALUES (5, 'superadmin', 'Re.Admin.Controllers.System.UserController.SwitchUserEnabledStatusAsync (Re.Admin.HttpApi)', 'PUT', '/adm/user/change-enabled/3a13f6c0-8fe8-2079-8212-a19eac3bd177', 'Windows', 'Chrome', '切换用户状态', '::1', '未知', 1, NULL, 0, '2024-07-24 21:43:24.201712', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);
INSERT INTO `sys_business_log` VALUES (6, 'superadmin', 'Re.Admin.Controllers.System.UserController.SwitchUserEnabledStatusAsync (Re.Admin.HttpApi)', 'PUT', '/adm/user/change-enabled/3a13f6c0-8fe8-2079-8212-a19eac3bd177', 'Windows', 'Chrome', '切换用户状态', '::1', '未知', 1, NULL, 0, '2024-07-24 21:43:25.193454', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL);

-- ----------------------------
-- Table structure for sys_dict
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict`;
CREATE TABLE `sys_dict`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `key` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '字典键',
  `value` varchar(1024) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '字典值',
  `label` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '显示文本',
  `group_name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '组名',
  `remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `sort` int(0) NOT NULL COMMENT '排序值',
  `is_enabled` tinyint(1) NOT NULL COMMENT '是否开启',
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_dict
-- ----------------------------
INSERT INTO `sys_dict` VALUES ('3a13f6bd-58c2-063f-ecc4-eb2aef2e064c', 'boy', '1', '男', 'sex', NULL, 0, 0, '2024-07-24 19:46:33.892967', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_file
-- ----------------------------
DROP TABLE IF EXISTS `sys_file`;
CREATE TABLE `sys_file`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `unique_name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文件编号/BLOB名',
  `file_name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文件名',
  `size` int(0) NOT NULL COMMENT '单位KB',
  `suffix` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '后缀',
  `relative_url` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '后缀',
  `mime_type` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '媒体类型',
  `extra_path` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '额外地址',
  `upload_type` int(0) NOT NULL COMMENT '上传类型 9:BLOB文件系统',
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_file
-- ----------------------------
INSERT INTO `sys_file` VALUES ('3a13f1dd-75f9-4c45-7b0b-9ae2681d3262', '6684a03f-d800-4655-9aa0-95156d551a44', 'rmzkq4th.pmc.jpg', 31521, '.jpg', 'oss/preview/6684a03f-d800-4655-9aa0-95156d551a44', 'image/jpeg', NULL, 0, '2024-07-23 21:03:32.401884', NULL);
INSERT INTO `sys_file` VALUES ('3a13f1dd-8856-c8cb-958a-4862d13704cd', 'a26cc75b-1fde-4570-b20f-f99bc3da423e', 'etrklwgu.nu4.jpg', 31521, '.jpg', 'oss/preview/a26cc75b-1fde-4570-b20f-f99bc3da423e', 'image/jpeg', NULL, 0, '2024-07-23 21:03:37.061284', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6a1-5e64-e552-4b2d-4bc0451ec03d', '1fdf6bd5-d794-4f78-ad6f-c62dc3f3b5f9', 'x1fr0voo.mnn.jpg', 31521, '.jpg', 'oss/preview/1fdf6bd5-d794-4f78-ad6f-c62dc3f3b5f9', 'image/jpeg', NULL, 0, '2024-07-24 19:16:00.333601', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6a3-3085-16e1-4ce9-1c6d3123c809', 'f77dbf9f-2e71-458a-b914-ad2966549daf', 'egho03us.cym.jpg', 31521, '.jpg', 'oss/preview/f77dbf9f-2e71-458a-b914-ad2966549daf', 'image/jpeg', NULL, 0, '2024-07-24 19:17:59.580349', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6a5-15bf-916b-434f-e3279f64b465', '1f186d74-7602-48ea-9029-af89ff899b5a', 'tmpe4cbq.4od.jpeg', 28543, '.jpeg', 'oss/preview/1f186d74-7602-48ea-9029-af89ff899b5a', 'image/jpeg', NULL, 0, '2024-07-24 19:20:03.787249', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6a8-656b-d56e-b914-ce4a67a2cf37', '1fc78ea2-515b-44f6-a49f-d3b4445af947', '2225pr04.ahh.jpg', 31521, '.jpg', 'oss/preview/1fc78ea2-515b-44f6-a49f-d3b4445af947', 'image/jpeg', NULL, 0, '2024-07-24 19:23:40.794851', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6ac-93bf-2557-7195-a0ed4d63e3aa', '61f859f4-b0de-4d12-a1c0-2e67ae22b730', 'rwplbick.i4k.jpg', 31521, '.jpg', 'oss/preview/61f859f4-b0de-4d12-a1c0-2e67ae22b730', 'image/jpeg', NULL, 0, '2024-07-24 19:28:14.795176', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6ac-d50b-55ad-479e-b9686df8e629', 'd2c50ce9-27cb-489b-b352-2e6a8ff2b32e', 'hqusrrje.wgm.jpg', 31521, '.jpg', 'oss/preview/d2c50ce9-27cb-489b-b352-2e6a8ff2b32e', 'image/jpeg', NULL, 0, '2024-07-24 19:28:31.513242', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6ad-af36-b039-aa09-8f0f538dfbd5', 'f2fe0198-ed42-4837-92b5-47b38cd53e26', 'nm0rwbph.1fc.jpeg', 28543, '.jpeg', 'oss/preview/f2fe0198-ed42-4837-92b5-47b38cd53e26', 'image/jpeg', NULL, 0, '2024-07-24 19:29:27.362341', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6b7-12c1-36d7-e3a1-e08999fd6226', 'ef45de01-41b0-4a78-8274-030af51cca4a', 'g0zhxici.jbh.jpeg', 28543, '.jpeg', 'oss/preview/ef45de01-41b0-4a78-8274-030af51cca4a', 'image/jpeg', NULL, 9, '2024-07-24 19:39:42.693416', NULL);
INSERT INTO `sys_file` VALUES ('3a13f6ba-10b8-e984-b9ab-5c9e1632a686', 'ef8af675-4bdb-4f2a-b37c-6116e17d1235', 'kiksbyal.iox.jpeg', 28543, '.jpeg', 'oss/preview/ef8af675-4bdb-4f2a-b37c-6116e17d1235', 'image/jpeg', NULL, 9, '2024-07-24 19:42:58.760508', NULL);

-- ----------------------------
-- Table structure for sys_login_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_login_log`;
CREATE TABLE `sys_login_log`  (
  `id` int(0) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `ip` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'IP',
  `address` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '登录地址',
  `os` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '系统',
  `browser` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '浏览器',
  `operation_msg` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '操作信息',
  `is_success` tinyint(1) NOT NULL COMMENT '是否成功',
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 30 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_login_log
-- ----------------------------
INSERT INTO `sys_login_log` VALUES (1, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:15:46.026104', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (2, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:15:51.860779', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (3, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:39:23.094971', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (4, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:51:20.892018', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (5, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:51:32.556451', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (6, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:51:44.549311', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (7, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:51:59.280747', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (8, 'admin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 19:57:01.979562', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (9, 'admin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 19:57:05.306543', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (10, 'admin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 19:57:08.040860', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (11, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:57:15.233946', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (12, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 19:58:15.948035', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (13, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:00:00.781078', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (14, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:00:16.335698', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (15, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:00:36.119372', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (16, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:25:32.591050', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (17, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:36:30.865916', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (18, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:43:35.809269', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (19, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 20:50:49.565100', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (20, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:13:52.694635', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (21, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:24:29.594404', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (22, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:34:14.854316', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (23, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:35:12.958030', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (24, 'superadmin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 21:35:31.288176', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (25, 'superadmin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 21:35:33.995213', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (26, 'superadmin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 21:35:36.377102', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (27, 'superadmin', '::1', '未知', 'Windows', 'Chrome', '密码错误', 0, '2024-07-24 21:35:37.061180', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (28, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:35:52.655951', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (29, 'superadmin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 21:36:17.262665', NULL, NULL, NULL);
INSERT INTO `sys_login_log` VALUES (30, 'admin', '::1', '未知', 'Windows', 'Chrome', NULL, 1, '2024-07-24 22:15:21.840522', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `title` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '显示标题/名称',
  `name` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '组件名',
  `icon` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '图标',
  `path` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '路由/地址',
  `function_type` int(0) NOT NULL COMMENT '功能类型',
  `permission` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '授权码',
  `parent_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '父级ID',
  `sort` int(0) NOT NULL COMMENT '排序',
  `hidden` tinyint(1) NOT NULL COMMENT '是否隐藏',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES ('3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', '系统管理', NULL, 'ant-design:setting-filled', '/system', 1, NULL, NULL, 2, 0, '{}', '0bfb8102d95c4abfafb1ecf150626aac', '2024-06-15 15:49:13.507249', '3a132908-ca06-34de-164e-21c96505a036', '2024-07-08 22:48:53.115413', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a132d16-df35-09cb-9f50-0a83e8290575', '用户管理', NULL, '', '/system/user', 1, '', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 1, 0, '{}', '24d8c968541b4b0db505dfdf6e49988b', '2024-06-15 16:01:03.300935', '3a132908-ca06-34de-164e-21c96505a036', '2024-07-08 22:56:35.333303', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a132d1f-2026-432a-885f-bf6b10bec15c', '角色管理', NULL, NULL, '/system/roles', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 2, 0, '{}', '824ab5eb475348d39d2217e824c826b0', '2024-06-15 16:10:04.215040', '3a132908-ca06-34de-164e-21c96505a036', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a132d1f-e2dd-7447-ac4b-2250201a9bad', '菜单管理', NULL, NULL, '/system/menus', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 3, 0, '{}', 'be3c510a07494b09a27b2c4b2d07c0cd', '2024-06-15 16:10:54.046247', '3a132908-ca06-34de-164e-21c96505a036', '2024-06-15 17:34:09.717457', '3a132908-ca06-34de-164e-21c96505a036', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a1356be-ac7a-f1a9-e45e-397a7e841149', '数据字典', NULL, NULL, '/system/dict', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 4, 0, '{}', '4d8490425be14b57bb82e6808b7d6d96', '2024-06-23 18:08:46.203010', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135caa-6050-b8fa-ba75-6aaf548a7683', '新增', NULL, NULL, NULL, 2, 'admin_system_user_add', '3a132d16-df35-09cb-9f50-0a83e8290575', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135caa-b115-4de4-3be5-4b3cc477d8f4', '查询', NULL, NULL, NULL, 2, 'admin_system_user_list', '3a132d16-df35-09cb-9f50-0a83e8290575', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-3200-f6de-41f9-948404a81884', '删除', NULL, NULL, NULL, 2, 'admin_system_user_delete', '3a132d16-df35-09cb-9f50-0a83e8290575', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-7acb-41cf-30c4-720eea400b2c', '分配角色', NULL, NULL, NULL, 2, 'admin_system_user_assignrole', '3a132d16-df35-09cb-9f50-0a83e8290575', 4, 0, '{}', 'cfc407a5c15f4186bcaf76c94479bace', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135cab-fcbb-1f19-8416-25c218db4279', '启用/禁用', NULL, NULL, NULL, 2, 'admin_system_user_changeenabled', '3a132d16-df35-09cb-9f50-0a83e8290575', 1, 0, '{}', 'a4a5bdbcc4d8405798476a17ec123c53', '2024-06-24 21:46:04.859652', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135cb0-3753-ae33-82fd-603c3622f661', '编辑', NULL, NULL, NULL, 2, 'admin_system_role_update', '3a132d1f-2026-432a-885f-bf6b10bec15c', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a135cc6-eea2-5551-e3a2-245ff43fcf01', '刷新缓存', NULL, NULL, NULL, 2, 'admin_system_dict_refresh', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 5, 0, '{}', 'aff5b83d52e440da8a2a636560eaac82', '2024-06-24 22:15:30.725052', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a138b11-d573-3e37-298a-c67a014e430b', '日志管理', NULL, NULL, '/logManagement', 1, NULL, '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', 5, 0, '{}', '361b2b7c73804b2f845979b4b9780bdd', '2024-07-03 21:59:51.413081', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-03 22:00:02.918234', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a138b12-93b5-e723-1539-adaeb17a2ae1', '登录日志', NULL, NULL, '/logManagement/loginLogs', 1, 'admin_system_loginlog_list', '3a138b11-d573-3e37-298a-c67a014e430b', 1, 0, '{}', '6127c487e28f48eb846badcb8d45a370', '2024-07-03 22:00:40.118092', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-24 19:59:42.559353', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a138b13-fccd-7b4a-0bb5-435a9d9c4172', '业务日志', NULL, NULL, '/logManagement/businessLogs', 1, 'admin_system_businesslog_list', '3a138b11-d573-3e37-298a-c67a014e430b', 2, 0, '{}', '3e60c8ee4742449a81eb2042635d1596', '2024-07-03 22:02:12.559269', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-07-24 19:59:52.333522', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13a4fe-6f74-733b-a628-6125c0325481', '组织架构', NULL, 'ri:user-fill', '/organization', 1, NULL, NULL, 1, 0, '{}', '93e9172908db4ada8fc847d6e2ecad2c', '2024-07-08 22:48:47.742259', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', '2024-07-08 22:54:10.348666', '3a13a4f2-568e-41fe-55e7-210cc37b6d8a', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcf2-3701-be8e-4ec8-ad5f77536101', '职位分组', NULL, NULL, '/organization/positionGroup', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 1, 0, '{}', '2da10e75eec746a8be0485fd27961cdb', '2024-07-13 14:26:20.046127', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 14:26:29.336149', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfc-f11b-7dce-a264-0f34b21085f1', '新增', NULL, NULL, NULL, 2, 'admin_system_positiongroup_add', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '9a826b054fb34f9ab7fa17c6faf98027', '2024-07-13 14:38:03.055512', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-52bb-db4a-d508-eea8536c8bdc', '查询', NULL, NULL, NULL, 2, 'admin_system_positiongroup_list', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 2, 0, '{}', '89c73d12d4094672932cec8c676f9dfb', '2024-07-13 14:38:28.028042', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-90b2-02ef-4440-8062594e3d79', '编辑', NULL, NULL, NULL, 2, 'admin_system_positiongroup_update', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '624f6ff8e265403db40c9f3ece0ab41a', '2024-07-13 14:38:43.893385', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bcfd-c549-8f84-1941-1d6baccfd6b4', '删除', '', NULL, NULL, 2, 'admin_system_positiongroup_delete', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', 1, 0, '{}', '978521f1290347b991792b181b2a8d6a', '2024-07-13 14:38:57.354973', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 14:39:02.834469', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', '职位管理', NULL, NULL, '/organization/position', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 2, 0, '{}', 'c0e89e402eb747f4bc920eea1221c810', '2024-07-13 17:52:45.803191', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb0-d210-fbaf-ce01-6d658b1b7ec9', '新增', NULL, NULL, NULL, 2, 'admin_system_position_add', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 1, 0, '{}', '57f606dc1d164ee585fef364d6fd735f', '2024-07-13 17:54:31.568625', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb1-26a1-79e7-6920-b40e50de0bbe', '查询', '', NULL, NULL, 2, 'admin_system_position_list', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 2, 0, '{}', '52bc36543df24f6dac1714131f5219d5', '2024-07-13 17:54:53.219392', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 17:55:17.296224', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb1-742f-1ff2-87d1-b07a807c791f', '编辑', NULL, NULL, NULL, 2, 'admin_system_position_update', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 3, 0, '{}', '1e9dd90dcc14442d8fdb70e6e73c644b', '2024-07-13 17:55:13.072355', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13bdb2-213b-d9fa-d067-287801312ea1', '删除', NULL, NULL, NULL, 2, 'admin_system_position_delete', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', 4, 0, '{}', '90b12c1a10f84bf4aee0e6e0784d063a', '2024-07-13 17:55:57.372951', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be18-7fe2-2163-01ba-4a86ca6a7c40', '部门管理', NULL, NULL, '/organization/dept', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 3, 0, '{}', '538e75fe37fd4cc39ae6ae5df7e18990', '2024-07-13 19:47:46.294491', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-204c-f634-f95f-ef8b7dcd9117', '新增', '', NULL, NULL, 2, 'admin_system_dept_add', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 1, 0, '{}', 'fd9e76e3f569464db3ce31cb234c05f3', '2024-07-13 19:48:27.341268', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-6b83-eaa2-0194-a9f17b786109', '查询', NULL, NULL, NULL, 2, 'admin_system_dept_list', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 2, 0, '{}', '9090f8ff34df441190f26b250e3d54ae', '2024-07-13 19:48:46.596368', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-a679-8375-aa20-5ab28ad85890', '编辑', NULL, NULL, NULL, 2, 'admin_system_dept_update', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 3, 0, '{}', 'a2794573859e4943874670b579b342f2', '2024-07-13 19:49:01.689374', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be19-d4fe-0a05-3dd1-25310c7bd52a', '删除', '', NULL, NULL, 2, 'admin_system_dept_delete', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', 1, 0, '{}', '1d5ef8e9b846479cbc55bf73d20f5fa3', '2024-07-13 19:49:13.598822', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be49-5f19-8ebd-5dda-1cf390060a09', '员工列表', NULL, NULL, '/organization/employee', 1, NULL, '3a13a4fe-6f74-733b-a628-6125c0325481', 4, 0, '{}', 'cecdbe7cd75749248782f450379b2c13', '2024-07-13 20:41:09.171061', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4b-3b01-f611-e5da-8b3a3dc41cfc', '新增', NULL, NULL, NULL, 2, 'admin_system_employee_add', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 1, 0, '{}', 'a676ba69e17248ed9153710e3e5a8f55', '2024-07-13 20:43:10.978954', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', '2024-07-13 20:44:33.952170', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4b-8355-c505-5dd3-15fbe89e2639', '查询', NULL, NULL, NULL, 2, 'admin_system_employee_list', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 2, 0, '{}', '990c7640f41843cfa8faee786190ec74', '2024-07-13 20:43:29.495014', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4c-2e0e-af01-4432-a4892ff622ab', '编辑', NULL, NULL, NULL, 2, 'admin_system_employee_update', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 3, 0, '{}', '38bd3f28295645c4ab5590251a4b7eae', '2024-07-13 20:44:13.199159', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('3a13be4c-d335-53c6-15e1-b2a16d9f94e4', '删除', NULL, NULL, NULL, 2, 'admin_system_employee_delete', '3a13be49-5f19-8ebd-5dda-1cf390060a09', 4, 0, '{}', '66d5b6682fda443c818c3e7b2c846432', '2024-07-13 20:44:55.479201', '3a13bc48-e3c9-4c0b-0cc4-b6fc4e606741', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('5c74b782-3231-11ef-afb3-0242ac110003', '编辑', NULL, NULL, NULL, 2, 'admin_system_menu_update', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('5c7548d7-3231-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_menu_add', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('5c75c0d0-3231-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_menu_list', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('5c767046-3231-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_menu_delete', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('7e1b4c13-3231-11ef-afb3-0242ac110003', '编辑', NULL, NULL, NULL, 2, 'admin_system_dict_update', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 4, 0, '{}', '67972f6dc1fd490aa2f3fdad2bac6883', '2024-06-24 21:50:42.004144', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:03.142409', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('7e1cbcbf-3231-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_dict_add', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('7e1d3cd0-3231-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_dict_list', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('7e1db553-3231-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_dict_delete', '3a1356be-ac7a-f1a9-e45e-397a7e841149', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('87cd2f63-3230-11ef-afb3-0242ac110003', '新增', NULL, NULL, NULL, 2, 'admin_system_role_add', '3a132d1f-2026-432a-885f-bf6b10bec15c', 1, 0, '{}', 'df39e7ca822c488b9da1be5b28e6c408', '2024-06-24 21:44:19.284329', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('9a844856-3230-11ef-afb3-0242ac110003', '查询', NULL, NULL, NULL, 2, 'admin_system_role_list', '3a132d1f-2026-432a-885f-bf6b10bec15c', 2, 0, '{}', '6ce2bf9e4d9742259743f3dc953ba92f', '2024-06-24 21:44:39.957889', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:44:48.686151', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('9a84d205-3230-11ef-afb3-0242ac110003', '删除', NULL, NULL, NULL, 2, 'admin_system_role_delete', '3a132d1f-2026-432a-885f-bf6b10bec15c', 3, 0, '{}', '2f4a0fc588ca44d58c446bbf5e21fcf4', '2024-06-24 21:45:12.961588', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:45:36.981105', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);
INSERT INTO `sys_menu` VALUES ('9a8546e3-3230-11ef-afb3-0242ac110003', '分配菜单', NULL, NULL, NULL, 2, 'admin_system_role_assignmenu', '3a132d1f-2026-432a-885f-bf6b10bec15c', 5, 0, '{}', '3a37914cba334a039eaabcef9d88260a', '2024-06-24 21:45:31.597928', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', '2024-06-24 21:51:05.971863', '3a1356b8-6f63-a393-1f8d-4ab9dc4914f4', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `role_name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '角色名',
  `remark` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('3a13f1b0-671a-f50f-44db-a39d52e7eaf8', '系统管理员', '系统默认创建，拥有所有权限', '{}', '4289462eefc5467dbd1db2d4f2e82c26', '2024-07-23 20:14:19.490384', NULL, NULL, NULL, 0, NULL, NULL);
INSERT INTO `sys_role` VALUES ('3a13f6c1-440e-3653-5cb0-92ccb4c856da', '演示管理员', '只读所有权限', '{}', '02d98b8e79804098966b671c0ff9918d', '2024-07-24 19:50:50.656691', '3a13f1b0-682b-e853-f288-73a653778a85', NULL, NULL, 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_role_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_menu`;
CREATE TABLE `sys_role_menu`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `menu_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '菜单ID',
  `role_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role_menu
-- ----------------------------
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-02a7-2c1a-bf865597e3b3', '5c74b782-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-1a53-63d5-40bfdfba6a0e', '7e1d3cd0-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-20f7-01a4-d0c449a89b97', '3a13bcfd-52bb-db4a-d508-eea8536c8bdc', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-2373-0240-5e57770cd61d', '3a13bcf2-3701-be8e-4ec8-ad5f77536101', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-2b9f-3897-bd8ae412410f', '7e1db553-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-34df-f805-08547157029f', '3a13bdb2-213b-d9fa-d067-287801312ea1', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-375c-f9f5-c7491f9d5ff8', '3a135cb0-3753-ae33-82fd-603c3622f661', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-3ccc-676c-df969192f081', '7e1cbcbf-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-461c-58a3-a2ab167ec1b8', '3a13bdaf-34ea-bf3c-c7eb-1d1cfd91d361', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-4625-76ae-f32625febf1a', '3a13bdb1-26a1-79e7-6920-b40e50de0bbe', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-4e3f-84bf-a1e16380c44e', '3a138b12-93b5-e723-1539-adaeb17a2ae1', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-508e-c139-01923efc99b6', '3a13be19-204c-f634-f95f-ef8b7dcd9117', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-51e7-880d-40cbb76f53d0', '3a13bdb1-742f-1ff2-87d1-b07a807c791f', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-586c-53fb-f04ceeacf7ee', '3a13be18-7fe2-2163-01ba-4a86ca6a7c40', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-5b44-c1a4-cc7d12ca351e', '3a135cc6-eea2-5551-e3a2-245ff43fcf01', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6142-8492-41b1a9573c50', '3a138b13-fccd-7b4a-0bb5-435a9d9c4172', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-62e6-5a9e-311e4d0160d4', '3a13bdb0-d210-fbaf-ce01-6d658b1b7ec9', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6444-88a3-7f5bf1e6a032', '3a135cab-3200-f6de-41f9-948404a81884', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6520-2c9f-4af59ebe98cd', '7e1b4c13-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-671f-ec8c-5eb7c7e0f6d1', '3a135caa-6050-b8fa-ba75-6aaf548a7683', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6acd-7b56-36760ce7abe5', '3a138b11-d573-3e37-298a-c67a014e430b', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6b48-c691-ecb6cf4b50c2', '3a1356be-ac7a-f1a9-e45e-397a7e841149', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6cdf-268b-77958d880909', '9a84d205-3230-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-6e97-2e1b-1580324ace84', '5c767046-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-769a-fdc4-65fc050b552f', '3a13a4fe-6f74-733b-a628-6125c0325481', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-788c-8f04-d444aad44631', '3a13bcfd-c549-8f84-1941-1d6baccfd6b4', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-8574-9828-f3e647a915b4', '3a13be49-5f19-8ebd-5dda-1cf390060a09', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-8622-9d87-cab4a814e135', '3a132d0c-0a70-b4c5-1ffd-1088c23ae02a', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-8b13-ded1-216e54a3a405', '3a13be4b-3b01-f611-e5da-8b3a3dc41cfc', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-91e4-767a-cdf278e3d917', '3a13bcfc-f11b-7dce-a264-0f34b21085f1', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-925b-3bff-85d83d8dd5b9', '3a135cab-7acb-41cf-30c4-720eea400b2c', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-9a56-3d04-21fe9151fea6', '3a132d1f-e2dd-7447-ac4b-2250201a9bad', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-9e94-3043-2c1e8fc6f389', '3a13be19-6b83-eaa2-0194-a9f17b786109', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-a3ea-a154-c3c3ff6a848b', '3a13be19-d4fe-0a05-3dd1-25310c7bd52a', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-ab0a-f682-f160a9dd06d1', '3a13bcfd-90b2-02ef-4440-8062594e3d79', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-bea2-2ee1-9fc3a8be91a6', '5c7548d7-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-c047-613b-3712254ed506', '3a135caa-b115-4de4-3be5-4b3cc477d8f4', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-c7df-bd3f-00501daad2cd', '3a13be4c-d335-53c6-15e1-b2a16d9f94e4', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-cfab-5dfa-108c1fa6a039', '87cd2f63-3230-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-d2c7-8770-afb8f4de4cef', '3a13be4c-2e0e-af01-4432-a4892ff622ab', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-dabe-7bcc-4ffab6b86bd7', '5c75c0d0-3231-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-dbfe-2899-c45c8aa107e3', '3a132d16-df35-09cb-9f50-0a83e8290575', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-e34f-ab83-447f740648cd', '9a8546e3-3230-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-e44b-3e8e-f5405a5eb4cf', '3a132d1f-2026-432a-885f-bf6b10bec15c', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-e643-1562-d6d5b4469b72', '3a13be4b-8355-c505-5dd3-15fbe89e2639', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-ebba-79f3-3e18ffbdc810', '3a135cab-fcbb-1f19-8416-25c218db4279', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-f23e-b5a3-506f789d4f87', '9a844856-3230-11ef-afb3-0242ac110003', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');
INSERT INTO `sys_role_menu` VALUES ('3a13f6ca-14dc-fe15-6cb7-9f30e0ee3a60', '3a13be19-a679-8375-aa20-5ab28ad85890', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `username` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '用户名',
  `password` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '密码',
  `password_salt` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '密码盐',
  `avatar` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '头像',
  `nickname` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '昵称',
  `sex` int(0) NOT NULL COMMENT '性别',
  `is_enabled` tinyint(1) NOT NULL COMMENT '是否启用',
  `extra_properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `concurrency_stamp` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `creation_time` datetime(6) NOT NULL,
  `creator_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `last_modification_time` datetime(6) NULL DEFAULT NULL,
  `last_modifier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0,
  `deleter_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL,
  `deletion_time` datetime(6) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('3a13f1b0-682b-e853-f288-73a653778a85', 'superadmin', 'e5af5c9a0f0b42e2db99c56989478783', '2F/DTAcEcGtJ8Ijq5Rql6Q==', 'http://localhost:44383/oss/preview/1f186d74-7602-48ea-9029-af89ff899b5a', 'superadmin', 1, 1, '{}', '285062d675f44212b6ab65d7e637eda8', '2024-07-23 20:14:19.739005', NULL, '2024-07-24 19:20:05.303399', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);
INSERT INTO `sys_user` VALUES ('3a13f6c0-8fe8-2079-8212-a19eac3bd177', 'admin', '3857ecf0dfa27fbea666f9cbe623a245', 'Odf2NiHk40CGp9Uq6x3jmg==', 'http://localhost:44383/images/boy.png', '演示管理01', 1, 1, '{}', '0ad29be1a0dc4c4fa6da6f4692213f63', '2024-07-24 19:50:04.531371', '3a13f1b0-682b-e853-f288-73a653778a85', '2024-07-24 21:43:25.183505', '3a13f1b0-682b-e853-f288-73a653778a85', 0, NULL, NULL);

-- ----------------------------
-- Table structure for sys_user_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`  (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '用户ID',
  `role_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user_role
-- ----------------------------
INSERT INTO `sys_user_role` VALUES ('3a13f1b0-688f-0d28-2539-9e09b623f589', '3a13f1b0-682b-e853-f288-73a653778a85', '3a13f1b0-671a-f50f-44db-a39d52e7eaf8');
INSERT INTO `sys_user_role` VALUES ('3a13f6c1-8da8-15ee-91ee-582eef5125fa', '3a13f6c0-8fe8-2079-8212-a19eac3bd177', '3a13f6c1-440e-3653-5cb0-92ccb4c856da');

SET FOREIGN_KEY_CHECKS = 1;
