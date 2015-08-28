/*
SQLyog v10.2 
MySQL - 5.6.24 : Database - openfire2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`openfire2` /*!40100 DEFAULT CHARACTER SET utf8 */;

/*Table structure for table `ofextcomponentconf` */

DROP TABLE IF EXISTS `ofextcomponentconf`;

CREATE TABLE `ofextcomponentconf` (
  `subdomain` varchar(255) NOT NULL,
  `wildcard` tinyint(4) NOT NULL,
  `secret` varchar(255) DEFAULT NULL,
  `permission` varchar(10) NOT NULL,
  PRIMARY KEY (`subdomain`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofextcomponentconf` */

/*Table structure for table `ofgroup` */

DROP TABLE IF EXISTS `ofgroup`;

CREATE TABLE `ofgroup` (
  `groupName` varchar(50) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`groupName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofgroup` */

/*Table structure for table `ofgroupprop` */

DROP TABLE IF EXISTS `ofgroupprop`;

CREATE TABLE `ofgroupprop` (
  `groupName` varchar(50) NOT NULL,
  `name` varchar(100) NOT NULL,
  `propValue` text NOT NULL,
  PRIMARY KEY (`groupName`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofgroupprop` */

/*Table structure for table `ofgroupuser` */

DROP TABLE IF EXISTS `ofgroupuser`;

CREATE TABLE `ofgroupuser` (
  `groupName` varchar(50) NOT NULL,
  `username` varchar(100) NOT NULL,
  `administrator` tinyint(4) NOT NULL,
  PRIMARY KEY (`groupName`,`username`,`administrator`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofgroupuser` */

/*Table structure for table `ofid` */

DROP TABLE IF EXISTS `ofid`;

CREATE TABLE `ofid` (
  `idType` int(11) NOT NULL,
  `id` bigint(20) NOT NULL,
  PRIMARY KEY (`idType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofid` */

insert  into `ofid`(`idType`,`id`) values (18,1),(19,51),(23,1),(25,3),(26,2);

/*Table structure for table `ofmucaffiliation` */

DROP TABLE IF EXISTS `ofmucaffiliation`;

CREATE TABLE `ofmucaffiliation` (
  `roomID` bigint(20) NOT NULL,
  `jid` text NOT NULL,
  `affiliation` tinyint(4) NOT NULL,
  PRIMARY KEY (`roomID`,`jid`(70))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucaffiliation` */

/*Table structure for table `ofmucconversationlog` */

DROP TABLE IF EXISTS `ofmucconversationlog`;

CREATE TABLE `ofmucconversationlog` (
  `roomID` bigint(20) NOT NULL,
  `sender` text NOT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `logTime` char(15) NOT NULL,
  `subject` varchar(255) DEFAULT NULL,
  `body` text,
  KEY `ofMucConversationLog_time_idx` (`logTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucconversationlog` */

/*Table structure for table `ofmucmember` */

DROP TABLE IF EXISTS `ofmucmember`;

CREATE TABLE `ofmucmember` (
  `roomID` bigint(20) NOT NULL,
  `jid` text NOT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `firstName` varchar(100) DEFAULT NULL,
  `lastName` varchar(100) DEFAULT NULL,
  `url` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `faqentry` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`roomID`,`jid`(70))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucmember` */

/*Table structure for table `ofmucroom` */

DROP TABLE IF EXISTS `ofmucroom`;

CREATE TABLE `ofmucroom` (
  `serviceID` bigint(20) NOT NULL,
  `roomID` bigint(20) NOT NULL,
  `creationDate` char(15) NOT NULL,
  `modificationDate` char(15) NOT NULL,
  `name` varchar(50) NOT NULL,
  `naturalName` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `lockedDate` char(15) NOT NULL,
  `emptyDate` char(15) DEFAULT NULL,
  `canChangeSubject` tinyint(4) NOT NULL,
  `maxUsers` int(11) NOT NULL,
  `publicRoom` tinyint(4) NOT NULL,
  `moderated` tinyint(4) NOT NULL,
  `membersOnly` tinyint(4) NOT NULL,
  `canInvite` tinyint(4) NOT NULL,
  `roomPassword` varchar(50) DEFAULT NULL,
  `canDiscoverJID` tinyint(4) NOT NULL,
  `logEnabled` tinyint(4) NOT NULL,
  `subject` varchar(100) DEFAULT NULL,
  `rolesToBroadcast` tinyint(4) NOT NULL,
  `useReservedNick` tinyint(4) NOT NULL,
  `canChangeNick` tinyint(4) NOT NULL,
  `canRegister` tinyint(4) NOT NULL,
  PRIMARY KEY (`serviceID`,`name`),
  KEY `ofMucRoom_roomid_idx` (`roomID`),
  KEY `ofMucRoom_serviceid_idx` (`serviceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucroom` */

/*Table structure for table `ofmucroomprop` */

DROP TABLE IF EXISTS `ofmucroomprop`;

CREATE TABLE `ofmucroomprop` (
  `roomID` bigint(20) NOT NULL,
  `name` varchar(100) NOT NULL,
  `propValue` text NOT NULL,
  PRIMARY KEY (`roomID`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucroomprop` */

/*Table structure for table `ofmucservice` */

DROP TABLE IF EXISTS `ofmucservice`;

CREATE TABLE `ofmucservice` (
  `serviceID` bigint(20) NOT NULL,
  `subdomain` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `isHidden` tinyint(4) NOT NULL,
  PRIMARY KEY (`subdomain`),
  KEY `ofMucService_serviceid_idx` (`serviceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucservice` */

insert  into `ofmucservice`(`serviceID`,`subdomain`,`description`,`isHidden`) values (1,'conference',NULL,0);

/*Table structure for table `ofmucserviceprop` */

DROP TABLE IF EXISTS `ofmucserviceprop`;

CREATE TABLE `ofmucserviceprop` (
  `serviceID` bigint(20) NOT NULL,
  `name` varchar(100) NOT NULL,
  `propValue` text NOT NULL,
  PRIMARY KEY (`serviceID`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofmucserviceprop` */

/*Table structure for table `ofoffline` */

DROP TABLE IF EXISTS `ofoffline`;

CREATE TABLE `ofoffline` (
  `username` varchar(64) NOT NULL,
  `messageID` bigint(20) NOT NULL,
  `creationDate` char(15) NOT NULL,
  `messageSize` int(11) NOT NULL,
  `stanza` text NOT NULL,
  PRIMARY KEY (`username`,`messageID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofoffline` */

/*Table structure for table `ofpresence` */

DROP TABLE IF EXISTS `ofpresence`;

CREATE TABLE `ofpresence` (
  `username` varchar(64) NOT NULL,
  `offlinePresence` text,
  `offlineDate` char(15) NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpresence` */

insert  into `ofpresence`(`username`,`offlinePresence`,`offlineDate`) values ('13012341234',NULL,'001439972349201'),('13012341235',NULL,'001439972349201'),('13022222222',NULL,'001440159157790'),('13033333333',NULL,'001439983902081'),('17092089640',NULL,'001440036436751'),('18308988180',NULL,'001440147006087'),('4||4.4',NULL,'001439980131167'),('666',NULL,'001439984993412'),('aa||aa.aa',NULL,'001439984998384'),('e||e.e',NULL,'001440725574810'),('f||f.f',NULL,'001440725547083'),('g||g.g',NULL,'001439980912483');

/*Table structure for table `ofprivacylist` */

DROP TABLE IF EXISTS `ofprivacylist`;

CREATE TABLE `ofprivacylist` (
  `username` varchar(64) NOT NULL,
  `name` varchar(100) NOT NULL,
  `isDefault` tinyint(4) NOT NULL,
  `list` text NOT NULL,
  PRIMARY KEY (`username`,`name`),
  KEY `ofPrivacyList_default_idx` (`username`,`isDefault`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofprivacylist` */

/*Table structure for table `ofprivate` */

DROP TABLE IF EXISTS `ofprivate`;

CREATE TABLE `ofprivate` (
  `username` varchar(64) NOT NULL,
  `name` varchar(100) NOT NULL,
  `namespace` varchar(200) NOT NULL,
  `privateData` text NOT NULL,
  PRIMARY KEY (`username`,`name`,`namespace`(100))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofprivate` */

/*Table structure for table `ofproperty` */

DROP TABLE IF EXISTS `ofproperty`;

CREATE TABLE `ofproperty` (
  `name` varchar(100) NOT NULL,
  `propValue` text NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofproperty` */

insert  into `ofproperty`(`name`,`propValue`) values ('admin.authorizedJIDs','17092089640@192.168.1.140'),('adminConsole.port','9090'),('adminConsole.securePort','9091'),('connectionProvider.className','org.jivesoftware.database.DefaultConnectionProvider'),('database.defaultProvider.connectionTimeout','1.0'),('database.defaultProvider.driver','com.mysql.jdbc.Driver'),('database.defaultProvider.maxConnections','25'),('database.defaultProvider.minConnections','5'),('database.defaultProvider.password','8c476db728f6ce744d27a7530c7d85156e810be338d895f2'),('database.defaultProvider.serverURL','jdbc:mysql://192.168.1.140:3306/openfire2?rewriteBatchedStatements=true'),('database.defaultProvider.testAfterUse','false'),('database.defaultProvider.testBeforeUse','false'),('database.defaultProvider.testSQL','select 1'),('database.defaultProvider.username','b010212a02a40e387980edd466d526f140f494c930f47fef'),('jdbcAuthProvider.passwordSQL','SELECT plainpassword FROM dzmembership WHERE usernameforopenfire=?'),('jdbcAuthProvider.passwordType','plain'),('jdbcProvider.connectionString','jdbc:mysql://192.168.1.140:3306/dianzhu_dev?user=root&password=root'),('jdbcProvider.driver','com.mysql.jdbc.Driver'),('jdbcUserProvider.allUsersSQL','SELECT usernameforopenfire FROM dzmembership'),('jdbcUserProvider.emailField','email'),('jdbcUserProvider.loadUserSQL','SELECT usernameforopenfire AS NAME,usernameforopenfire FROM dzmembership WHERE usernameforopenfire=?'),('jdbcUserProvider.nameField','username'),('jdbcUserProvider.searchSQL','SELECT usernameforopenfire FROM dzmembership WHERE'),('jdbcUserProvider.userCountSQL','SELECT COUNT(*) FROM dzmembership'),('jdbcUserProvider.usernameField','usernameforopenfire'),('locale','zh_CN'),('passwordKey','1fjrxAQQV80G6n0'),('provider.admin.className','org.jivesoftware.openfire.admin.DefaultAdminProvider'),('provider.auth.className','org.jivesoftware.openfire.auth.JDBCAuthProvider'),('provider.group.className','org.jivesoftware.openfire.group.DefaultGroupProvider'),('provider.lockout.className','org.jivesoftware.openfire.lockout.DefaultLockOutProvider'),('provider.securityAudit.className','org.jivesoftware.openfire.security.DefaultSecurityAuditProvider'),('provider.user.className','org.jivesoftware.openfire.user.JDBCUserProvider'),('provider.vcard.className','org.jivesoftware.openfire.vcard.DefaultVCardProvider'),('setup','true'),('update.lastCheck','1440655372617'),('xmpp.auth.anonymous','true'),('xmpp.domain','192.168.1.140'),('xmpp.session.conflict-limit','0'),('xmpp.socket.ssl.active','true');

/*Table structure for table `ofpubsubaffiliation` */

DROP TABLE IF EXISTS `ofpubsubaffiliation`;

CREATE TABLE `ofpubsubaffiliation` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `jid` varchar(255) NOT NULL,
  `affiliation` varchar(10) NOT NULL,
  PRIMARY KEY (`serviceID`,`nodeID`,`jid`(70))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubaffiliation` */

insert  into `ofpubsubaffiliation`(`serviceID`,`nodeID`,`jid`,`affiliation`) values ('pubsub','','yuanfei-pc','owner');

/*Table structure for table `ofpubsubdefaultconf` */

DROP TABLE IF EXISTS `ofpubsubdefaultconf`;

CREATE TABLE `ofpubsubdefaultconf` (
  `serviceID` varchar(100) NOT NULL,
  `leaf` tinyint(4) NOT NULL,
  `deliverPayloads` tinyint(4) NOT NULL,
  `maxPayloadSize` int(11) NOT NULL,
  `persistItems` tinyint(4) NOT NULL,
  `maxItems` int(11) NOT NULL,
  `notifyConfigChanges` tinyint(4) NOT NULL,
  `notifyDelete` tinyint(4) NOT NULL,
  `notifyRetract` tinyint(4) NOT NULL,
  `presenceBased` tinyint(4) NOT NULL,
  `sendItemSubscribe` tinyint(4) NOT NULL,
  `publisherModel` varchar(15) NOT NULL,
  `subscriptionEnabled` tinyint(4) NOT NULL,
  `accessModel` varchar(10) NOT NULL,
  `language` varchar(255) DEFAULT NULL,
  `replyPolicy` varchar(15) DEFAULT NULL,
  `associationPolicy` varchar(15) NOT NULL,
  `maxLeafNodes` int(11) NOT NULL,
  PRIMARY KEY (`serviceID`,`leaf`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubdefaultconf` */

insert  into `ofpubsubdefaultconf`(`serviceID`,`leaf`,`deliverPayloads`,`maxPayloadSize`,`persistItems`,`maxItems`,`notifyConfigChanges`,`notifyDelete`,`notifyRetract`,`presenceBased`,`sendItemSubscribe`,`publisherModel`,`subscriptionEnabled`,`accessModel`,`language`,`replyPolicy`,`associationPolicy`,`maxLeafNodes`) values ('pubsub',0,0,0,0,0,1,1,1,0,0,'publishers',1,'open','English',NULL,'all',-1),('pubsub',1,1,5120,0,-1,1,1,1,0,1,'publishers',1,'open','English',NULL,'all',-1);

/*Table structure for table `ofpubsubitem` */

DROP TABLE IF EXISTS `ofpubsubitem`;

CREATE TABLE `ofpubsubitem` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `id` varchar(100) NOT NULL,
  `jid` varchar(255) NOT NULL,
  `creationDate` char(15) NOT NULL,
  `payload` mediumtext,
  PRIMARY KEY (`serviceID`,`nodeID`,`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubitem` */

/*Table structure for table `ofpubsubnode` */

DROP TABLE IF EXISTS `ofpubsubnode`;

CREATE TABLE `ofpubsubnode` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `leaf` tinyint(4) NOT NULL,
  `creationDate` char(15) NOT NULL,
  `modificationDate` char(15) NOT NULL,
  `parent` varchar(100) DEFAULT NULL,
  `deliverPayloads` tinyint(4) NOT NULL,
  `maxPayloadSize` int(11) DEFAULT NULL,
  `persistItems` tinyint(4) DEFAULT NULL,
  `maxItems` int(11) DEFAULT NULL,
  `notifyConfigChanges` tinyint(4) NOT NULL,
  `notifyDelete` tinyint(4) NOT NULL,
  `notifyRetract` tinyint(4) NOT NULL,
  `presenceBased` tinyint(4) NOT NULL,
  `sendItemSubscribe` tinyint(4) NOT NULL,
  `publisherModel` varchar(15) NOT NULL,
  `subscriptionEnabled` tinyint(4) NOT NULL,
  `configSubscription` tinyint(4) NOT NULL,
  `accessModel` varchar(10) NOT NULL,
  `payloadType` varchar(100) DEFAULT NULL,
  `bodyXSLT` varchar(100) DEFAULT NULL,
  `dataformXSLT` varchar(100) DEFAULT NULL,
  `creator` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `language` varchar(255) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `replyPolicy` varchar(15) DEFAULT NULL,
  `associationPolicy` varchar(15) DEFAULT NULL,
  `maxLeafNodes` int(11) DEFAULT NULL,
  PRIMARY KEY (`serviceID`,`nodeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubnode` */

insert  into `ofpubsubnode`(`serviceID`,`nodeID`,`leaf`,`creationDate`,`modificationDate`,`parent`,`deliverPayloads`,`maxPayloadSize`,`persistItems`,`maxItems`,`notifyConfigChanges`,`notifyDelete`,`notifyRetract`,`presenceBased`,`sendItemSubscribe`,`publisherModel`,`subscriptionEnabled`,`configSubscription`,`accessModel`,`payloadType`,`bodyXSLT`,`dataformXSLT`,`creator`,`description`,`language`,`name`,`replyPolicy`,`associationPolicy`,`maxLeafNodes`) values ('pubsub','',0,'001439204237907','001439204237907',NULL,0,0,0,0,1,1,1,0,0,'publishers',1,0,'open','','','','yuanfei-pc','','English','',NULL,'all',-1);

/*Table structure for table `ofpubsubnodegroups` */

DROP TABLE IF EXISTS `ofpubsubnodegroups`;

CREATE TABLE `ofpubsubnodegroups` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `rosterGroup` varchar(100) NOT NULL,
  KEY `ofPubsubNodeGroups_idx` (`serviceID`,`nodeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubnodegroups` */

/*Table structure for table `ofpubsubnodejids` */

DROP TABLE IF EXISTS `ofpubsubnodejids`;

CREATE TABLE `ofpubsubnodejids` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `jid` varchar(255) NOT NULL,
  `associationType` varchar(20) NOT NULL,
  PRIMARY KEY (`serviceID`,`nodeID`,`jid`(70))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubnodejids` */

/*Table structure for table `ofpubsubsubscription` */

DROP TABLE IF EXISTS `ofpubsubsubscription`;

CREATE TABLE `ofpubsubsubscription` (
  `serviceID` varchar(100) NOT NULL,
  `nodeID` varchar(100) NOT NULL,
  `id` varchar(100) NOT NULL,
  `jid` varchar(255) NOT NULL,
  `owner` varchar(255) NOT NULL,
  `state` varchar(15) NOT NULL,
  `deliver` tinyint(4) NOT NULL,
  `digest` tinyint(4) NOT NULL,
  `digest_frequency` int(11) NOT NULL,
  `expire` char(15) DEFAULT NULL,
  `includeBody` tinyint(4) NOT NULL,
  `showValues` varchar(30) DEFAULT NULL,
  `subscriptionType` varchar(10) NOT NULL,
  `subscriptionDepth` tinyint(4) NOT NULL,
  `keyword` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`serviceID`,`nodeID`,`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofpubsubsubscription` */

/*Table structure for table `ofremoteserverconf` */

DROP TABLE IF EXISTS `ofremoteserverconf`;

CREATE TABLE `ofremoteserverconf` (
  `xmppDomain` varchar(255) NOT NULL,
  `remotePort` int(11) DEFAULT NULL,
  `permission` varchar(10) NOT NULL,
  PRIMARY KEY (`xmppDomain`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofremoteserverconf` */

/*Table structure for table `ofroster` */

DROP TABLE IF EXISTS `ofroster`;

CREATE TABLE `ofroster` (
  `rosterID` bigint(20) NOT NULL,
  `username` varchar(64) NOT NULL,
  `jid` varchar(1024) NOT NULL,
  `sub` tinyint(4) NOT NULL,
  `ask` tinyint(4) NOT NULL,
  `recv` tinyint(4) NOT NULL,
  `nick` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`rosterID`),
  KEY `ofRoster_unameid_idx` (`username`),
  KEY `ofRoster_jid_idx` (`jid`(255))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofroster` */

/*Table structure for table `ofrostergroups` */

DROP TABLE IF EXISTS `ofrostergroups`;

CREATE TABLE `ofrostergroups` (
  `rosterID` bigint(20) NOT NULL,
  `rank` tinyint(4) NOT NULL,
  `groupName` varchar(255) NOT NULL,
  PRIMARY KEY (`rosterID`,`rank`),
  KEY `ofRosterGroup_rosterid_idx` (`rosterID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofrostergroups` */

/*Table structure for table `ofsaslauthorized` */

DROP TABLE IF EXISTS `ofsaslauthorized`;

CREATE TABLE `ofsaslauthorized` (
  `username` varchar(64) NOT NULL,
  `principal` text NOT NULL,
  PRIMARY KEY (`username`,`principal`(200))
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofsaslauthorized` */

/*Table structure for table `ofsecurityauditlog` */

DROP TABLE IF EXISTS `ofsecurityauditlog`;

CREATE TABLE `ofsecurityauditlog` (
  `msgID` bigint(20) NOT NULL,
  `username` varchar(64) NOT NULL,
  `entryStamp` bigint(20) NOT NULL,
  `summary` varchar(255) NOT NULL,
  `node` varchar(255) NOT NULL,
  `details` text,
  PRIMARY KEY (`msgID`),
  KEY `ofSecurityAuditLog_tstamp_idx` (`entryStamp`),
  KEY `ofSecurityAuditLog_uname_idx` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofsecurityauditlog` */

insert  into `ofsecurityauditlog`(`msgID`,`username`,`entryStamp`,`summary`,`node`,`details`) values (1,'17092089640',1439277180443,'set server property xmpp.domain','YuanFei-PC','xmpp.domain = 192.168.1.140'),(2,'17092089640',1439280190715,'closed session for address 17092089640@192.168.1.140/agsXMPP','YuanFei-PC',NULL);

/*Table structure for table `ofuser` */

DROP TABLE IF EXISTS `ofuser`;

CREATE TABLE `ofuser` (
  `username` varchar(64) NOT NULL,
  `plainPassword` varchar(32) DEFAULT NULL,
  `encryptedPassword` varchar(255) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `creationDate` char(15) NOT NULL,
  `modificationDate` char(15) NOT NULL,
  PRIMARY KEY (`username`),
  KEY `ofUser_cDate_idx` (`creationDate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofuser` */

insert  into `ofuser`(`username`,`plainPassword`,`encryptedPassword`,`name`,`email`,`creationDate`,`modificationDate`) values ('admin',NULL,'c9a56fff15254b024ba7ee5485e1b122265a7ff69b7e25a2','Administrator','phiree@gmail.com','001439204229009','0');

/*Table structure for table `ofuserflag` */

DROP TABLE IF EXISTS `ofuserflag`;

CREATE TABLE `ofuserflag` (
  `username` varchar(64) NOT NULL,
  `name` varchar(100) NOT NULL,
  `startTime` char(15) DEFAULT NULL,
  `endTime` char(15) DEFAULT NULL,
  PRIMARY KEY (`username`,`name`),
  KEY `ofUserFlag_sTime_idx` (`startTime`),
  KEY `ofUserFlag_eTime_idx` (`endTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofuserflag` */

/*Table structure for table `ofuserprop` */

DROP TABLE IF EXISTS `ofuserprop`;

CREATE TABLE `ofuserprop` (
  `username` varchar(64) NOT NULL,
  `name` varchar(100) NOT NULL,
  `propValue` text NOT NULL,
  PRIMARY KEY (`username`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofuserprop` */

insert  into `ofuserprop`(`username`,`name`,`propValue`) values ('17092089640','console.refresh','session-summary=0'),('17092089640','console.rows_per_page','session-summary=25');

/*Table structure for table `ofvcard` */

DROP TABLE IF EXISTS `ofvcard`;

CREATE TABLE `ofvcard` (
  `username` varchar(64) NOT NULL,
  `vcard` mediumtext NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofvcard` */

/*Table structure for table `ofversion` */

DROP TABLE IF EXISTS `ofversion`;

CREATE TABLE `ofversion` (
  `name` varchar(50) NOT NULL,
  `version` int(11) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ofversion` */

insert  into `ofversion`(`name`,`version`) values ('openfire',21);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
