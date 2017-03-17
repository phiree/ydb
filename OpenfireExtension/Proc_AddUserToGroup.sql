
------------添加组和组内用户----------
DROP PROCEDURE IF EXISTS AddUserToGroup;

DELIMITER //

CREATE PROCEDURE AddUserToGroup(addedGroupName VARCHAR(10),userids TEXT)
BEGIN
  
  
  DECLARE  a INT DEFAULT 0;
  DECLARE idStr VARCHAR(255);
  DELETE FROM ofgroup WHERE groupName=addedGroupName;
  DELETE FROM ofgroupuser WHERE groupName=addedGroupName;
  DELETE FROM ofgroupprop WHERE groupName=addedGroupName;
  
  INSERT INTO ofgroup VALUES(addedGroupName,'');

  id_loop:LOOP
  
	  SET a=a+1;
	  SET idStr=SPLIT_STR(userids,',',a);
	  IF idStr=''  THEN
		LEAVE id_loop;
	  END IF;
	  INSERT INTO ofgroupuser VALUES(addedGroupName,idStr,'');
  END LOOP id_loop;	
  
END
//
DELIMITER ;

-- userage
-- CALL AddUserToGroup('group1','userid1,userid2');

-------------- split 函数 ----------------- 
DELIMITER $$

USE `openfire_3_10_3`$$

DROP FUNCTION IF EXISTS `SPLIT_STR`$$

CREATE DEFINER=`root`@`localhost` FUNCTION `SPLIT_STR`(
  X VARCHAR(60000),
  delim VARCHAR(12),
  pos INT
) RETURNS VARCHAR(255) CHARSET utf8
RETURN REPLACE(SUBSTRING(SUBSTRING_INDEX(X, delim, pos),
       LENGTH(SUBSTRING_INDEX(X, delim, pos -1)) + 1),
       delim, '')$$

DELIMITER ;

