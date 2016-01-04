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
 
