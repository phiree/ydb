DELIMITER $$

USE `ydb_membership`$$

DROP PROCEDURE IF EXISTS `batch_create_user`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `batch_create_user`(IN  batch_size INT,IN  usertype INT, IN username_prefix VARCHAR(40))
BEGIN 
    
    -- 1: customer 4:customerService
    DECLARE username VARCHAR(40);
    DECLARE PASSWORD VARCHAR(40);
    DECLARE plainpassword VARCHAR(40) DEFAULT '123456';
DECLARE rc INT; 
    
    SET rc =1; 
loopl: WHILE rc<=batch_size DO 
        SET username=CONCAT(username_prefix,rc);
        #set password=upper(md5(plainpassword));
	INSERT INTO dzmembership(id,username,plainpassword,PASSWORD,usertype) VALUES(UUID(),username,'123456',UPPER(MD5('123456')),usertype) 
	ON DUPLICATE KEY UPDATE    
		username=username,PASSWORD=UPPER(MD5('123456')),plainpassword='123456';
	 
         SELECT CONCAT(username);
         SET rc=rc+1; 
END WHILE loopl; 
 END$$

DELIMITER ;