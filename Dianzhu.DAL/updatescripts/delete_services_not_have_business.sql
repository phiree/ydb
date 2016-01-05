/*
说明:
	删除无效外键数据, 防止 nhibernate 自动更新结构时抛出不可见异常,影响性能
使用:
	先手动选中 delete...语句,再执行.
执行记录:

	2016-1-5
	2016-1-4

*/

SELECT *
#delete CashTicketTemplate
FROM CashTicketTemplate LEFT JOIN business_abs b ON business_id=b.`Id`
WHERE b.id IS NULL
 

SELECT *
#delete businessuser
FROM businessuser LEFT JOIN business_abs b ON belongto_id=b.`Id`
WHERE b.id IS NULL;

SELECT CashTicketCreateRecord_id,b.id,b.`Business_id`,c.`Id`
#delete cashticket
 FROM cashticket   INNER JOIN  CashTicketCreateRecord b ON  CashTicketCreateRecord_id= b.`Id`
		   LEFT JOIN business_abs c ON  b.`Business_id`= c.id
 WHERE c.id IS NULL
 

SELECT *
#delete CashTicketCreateRecord
 FROM CashTicketCreateRecord   LEFT JOIN business_abs b ON  Business_id= b.id
WHERE b.id IS NULL


SELECT *
#delete BusinessImage
 FROM BusinessImage   LEFT JOIN business_abs b ON  Business_id= b.id
WHERE b.id IS NULL


SELECT *
#delete staff
 FROM staff   LEFT JOIN business_abs b ON `Belongto_id`=b.`Id`
WHERE b.id IS NULL


SELECT receptionchat_id
#delete 
FROM receptionchatreassign WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )


 );

SELECT receptionchat_id
#delete 
FROM receptionchatmedia WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )


 );

SELECT receptionchat_id
#delete 
FROM receptionchatnotice WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )


 ); 
 
 
 SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )

 

SELECT serviceorder_id
#delete 
FROM paymentlog WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );


SELECT id 
#delete
FROM receptionstatus WHERE order_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );

SELECT id 
#delete
FROM receptionchatdd WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );


SELECT id
# DELETE 

 FROM  serviceorder WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business));

SELECT *
#delete 
 FROM   serviceopentimeforday WHERE `ServiceOpenTime_id` IN (SELECT id 
#delete 
FROM serviceopentime WHERE dzservice_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business))
 );
 
SELECT * 
#delete 
FROM serviceopentime WHERE dzservice_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business));
 
 SELECT *
# delete
 FROM  dzservice WHERE  business_id NOT IN (SELECT id FROM business_abs)
 
