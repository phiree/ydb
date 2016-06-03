 DROP PROCEDURE IF EXISTS batch_insert;
DELIMITER $$

 CREATE PROCEDURE batch_insert() 
 BEGIN 
    DECLARE MAX INT; 
    DECLARE usertype INT;
    DECLARE username VARCHAR(40);
    DECLARE PASSWORD VARCHAR(40);
    DECLARE plainpassword VARCHAR(40) DEFAULT '123456';
DECLARE rc INT; 
    SET MAX =10; 
    SET rc =1; 
loopl: WHILE rc<=MAX DO 
        SET username=CONCAT('test_user_',rc);
        SET usertype=IF(rc%2=0,1,4);
        #set password=upper(md5(plainpassword));
	INSERT INTO dzmembership(id,username,plainpassword,PASSWORD,usertype) VALUES(UUID(),username,'123456',UPPER(MD5('123456')),usertype) 
	ON DUPLICATE KEY UPDATE    
		username=username,PASSWORD=UPPER(MD5('123456')),plainpassword='123456';
	 
         SELECT CONCAT(username,usertype);
        SET rc=rc+1; 
END WHILE loopl; 
 END$$ 
 DELIMITER ;
 
 
