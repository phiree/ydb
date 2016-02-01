/*
说明:
	删除无效外键数据, 防止 nhibernate 自动更新结构时抛出不可见异常,影响性能
使用:
	 
执行记录:

	2016-1-5
	2016-1-4

*/

DELETE cashticket
FROM cashticket
INNER JOIN CashTicketTemplate ct ON ct.`Id`=  CashTicketTemplate_id
LEFT JOIN business_abs b ON b.`Id`=ct.`Business_id`
WHERE b.id IS NULL;

DELETE cashticketcreaterecord
FROM cashticketcreaterecord
INNER JOIN CashTicketTemplate ct ON ct.`Id`=  CashTicketTemplate_id
LEFT JOIN business_abs b ON b.`Id`=ct.`Business_id`
WHERE b.id IS NULL;

DELETE CashTicketTemplate
FROM CashTicketTemplate LEFT JOIN business_abs b ON business_id=b.`Id`
WHERE b.id IS NULL;
 

DELETE businessuser
FROM businessuser LEFT JOIN business_abs b ON belongto_id=b.`Id`
WHERE b.id IS NULL;


 DELETE CashTicketCreateRecord
 FROM CashTicketCreateRecord   LEFT JOIN business_abs b ON  Business_id= b.id
WHERE b.id IS NULL;


DELETE BusinessImage
 FROM BusinessImage   LEFT JOIN business_abs b ON  Business_id= b.id
WHERE b.id IS NULL;


DELETE staff
 FROM staff   LEFT JOIN business_abs b ON `Belongto_id`=b.`Id`
WHERE b.id IS NULL;



DELETE 
FROM receptionchatreassign WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )

 );

DELETE 
FROM receptionchatmedia WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )


 );

DELETE 
FROM receptionchatnotice WHERE receptionchat_id IN ( 
SELECT id
#delete 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )


 ); 
 
 
DELETE 
FROM receptionchat WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) )
;
 

DELETE paymentlog

FROM paymentlog WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );

;
DELETE receptionstatus
FROM receptionstatus WHERE order_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );
;
DELETE receptionchatdd
FROM receptionchatdd WHERE serviceorder_id IN ( SELECT id FROM  serviceorder  WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business)) );
;

 DELETE 

 FROM  serviceorder WHERE service_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business));
;
DELETE 
 FROM   serviceopentimeforday WHERE `ServiceOpenTime_id` IN (SELECT id 
FROM serviceopentime WHERE dzservice_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business))
 );
 
DELETE 
FROM serviceopentime WHERE dzservice_id IN(SELECT id FROM dzservice s WHERE s.business_id NOT IN (SELECT id FROM business));
 ;
DELETE
 FROM  dzservice WHERE  business_id NOT IN (SELECT id FROM business_abs)
 
;