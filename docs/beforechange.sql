/*
SQLyog 企业版 - MySQL GUI v8.14 
MySQL - 5.6.21 
*********************************************************************
*/
/*!40101 SET NAMES utf8 */;

create table `ofproperty_copy_before_changed` (
	`name` varchar (300),
	`propValue` blob 
); 
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('admin.authorizedJIDs','550700860||qq.com@20141220-pc');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('adminConsole.port','9090');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('adminConsole.securePort','9091');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('connectionProvider.className','org.jivesoftware.database.DefaultConnectionProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.connectionTimeout','1.0');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.driver','com.mysql.jdbc.Driver');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.maxConnections','25');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.minConnections','5');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.password','b1bba1329a38edb222d94d1bda1691686d03bd5ae3728814');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.serverURL','jdbc:mysql://localhost:3306/openfire3_10_2?rewriteBatchedStatements=true');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.testAfterUse','false');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.testBeforeUse','false');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.testSQL','select 1');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('database.defaultProvider.username','4735ce306ad7096ad470e8f92b9c0f449193af798c6822f3');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcAuthProvider.passwordSQL','SELECT plainPassword FROM dzmembership WHERE usernameforopenfire= ?');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcAuthProvider.passwordType','plain');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcProvider.connectionString','jdbc:mysql://localhost:3306/dianzhu_dev?user=root&password=root');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcProvider.driver','com.mysql.jdbc.Driver');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.allUsersSQL','SELECT usernameforopenfire FROM dzmembership');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.emailField','email');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.loadUserSQL','SELECT usernameforopenfire AS NAME,usernameforopenfire FROM dzmembership WHERE usernameforopenfire=?');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.nameField','usernameforopenfire');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.searchSQL','SELECT usernameforopenfire FROM dzmembership WHERE');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.userCountSQL','SELECT COUNT(*) FROM dzmembership');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('jdbcUserProvider.usernameField','usernameforopenfire');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('locale','en');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('passwordKey','uo8r3Wvl7t9fDV3');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.admin.className','org.jivesoftware.openfire.admin.DefaultAdminProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.auth.className','org.jivesoftware.openfire.auth.JDBCAuthProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.group.className','org.jivesoftware.openfire.group.DefaultGroupProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.lockout.className','org.jivesoftware.openfire.lockout.DefaultLockOutProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.securityAudit.className','org.jivesoftware.openfire.security.DefaultSecurityAuditProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.user.className','org.jivesoftware.openfire.user.JDBCUserProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('provider.vcard.className','org.jivesoftware.openfire.vcard.DefaultVCardProvider');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('setup','true');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('update.lastCheck','1442647407546');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('xmpp.auth.anonymous','true');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('xmpp.domain','20141220-pc');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('xmpp.session.conflict-limit','0');
insert into `ofproperty_copy_before_changed` (`name`, `propValue`) values('xmpp.socket.ssl.active','true');
