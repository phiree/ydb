 #CALL batch_insert();
 
 DELETE s
#select m.username,s.*

FROM receptionstatus s 
INNER JOIN   dzmembership m ON s.customer_id=m.`Id`

WHERE m.username LIKE 'test_user_%'

ORDER BY ordercreated DESC ;
 
DELETE s
#select m.username,s.*

FROM serviceorder s 
INNER JOIN   dzmembership m ON s.customer_id=m.`Id`

WHERE m.username LIKE 'test_user_%'

ORDER BY ordercreated DESC ;

#select * 
#delete 

FROM dzmembership WHERE username LIKE 'test_user_%'
 
 
 