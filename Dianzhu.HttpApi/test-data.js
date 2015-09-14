var need_to_test=[
//"usm001003","usm001005","usm001007",
//"merm001001","merm001003","merm001005",
"orm001004"
];
var test_data=[
  /***************订单*****************/
    { 
                    "protocol_CODE": "ORM001004", 
                    "ReqData": { 
                        "userID": "1cd5ac25-fcc6-432d-bba0-a4f90129edcf", 
                        "pWord": "123456", 
                        "target": "ALL", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
        {
      
         //订单列表
                    "protocol_CODE": "ORM001003", 
                    "ReqData": { 
                        "userID": "eb2ae597-5adb-4242-b22e-a4f901275654", //13022222222
                        "pWord": "123456", 
                        "target": "ALL", 
                        "pageSize":"10",
                        "pageNum":"1"
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
              
                 
        
       
        /*******************商家******************/
        {//商户注册.
                    "protocol_CODE": "MERM001001", 
                    "ReqData": { 
                                "email": "phiree2@gmail.com", 
                                "pWord": "121212", 
                                }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
         //商户信息修改
                  {
                     "protocol_CODE": "MERM001003", 
                    "ReqData": { 
                    "userID": "d9b54c42-c027-426c-81be-a51301346a9d", //phiree2@gmail.com
                    "pWord": "121212", 
                    "alias": "1805", 
                    "email": "phirea126.com", 
                    "phone": "1999938xxxx", 
                    "password":"121212",
                    
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                },
         //商户信息获取
                  {
                    "protocol_CODE": "MERM001005", 
                    "ReqData": { 
                                "email": "phiree@gmail.com", 
                                "pWord": "121212", 
                                }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
               
                
        /********************用户********************/
                 {  //用户信息获取
                    "protocol_CODE": "USM001005",
                    "ReqData": { 
                                "email": "13022222222", 
                                "pWord": "123456", 
                                }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "USM001001", //注册
                    "ReqData": { 
                                "email": "sdf", 
                                "pWord": 'password', 
                                
                                }, 
                "stamp_TIMES": "1490192929212", 
                "serial_NUMBER": "00147001015869149751" 
                },
                { //信息修改
                    "protocol_CODE": "USM001003", 
                    "ReqData": { 
                    "userID": "eb2ae597-5adb-4242-b22e-a4f901275654", 
                    "pWord": "123456", 
                    "alias": "1805", 
                    "email": "123331@126.com", 
                    "phone": "1999938xxxx", 
                    "password":"123456",
                    "address":"海牙国际大厦20B"
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                },
                //只传入部分字段
                { 
                    "protocol_CODE": "USM001003", 
                    "ReqData": { 
                    "userID": "eb2ae597-5adb-4242-b22e-a4f901275654", 
                    "pWord": "123456", 
                    "address":"海牙国际大厦20A",
                    "phone":"13812341234"
                   
                    }, 
                    "stamp_TIMES": "1490192929222", 
                    "serial_NUMBER": "00147001015869149756" 
                },
                 { 
                    "protocol_CODE": "USM001007",  //上传头像
                    "ReqData": { 
                        "userID": "eb2ae597-5adb-4242-b22e-a4f901275654", 
                        "pWord": "123456", 
                        "imgData":"iVBORw0KGgoAAAANSUhEUgAAAC0AAAAXCAIAAAAQmVEGAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAABXSURBVEhLY3wro8IwCAATlB5oMOoOVDDqDlQw6g5UMOoOVDBY3IEo1+9/eWLy4QeETWtgKiC/g4cVygGD0XhBBaP1LSoYdQcqGHUHKhh1ByoYHO5gYAAAC3gJsxon5CAAAAAASUVORK5CYII="
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                
                
                
                { 
                    "protocol_CODE": "VCM001001", 
                    "ReqData": { 
                        "uid": "24ba4944-660a-4d7e-abd3-a4de00abf040", 
                        "userPWord": "123456", 
                        "vcsTarget": "All", 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                } ,
                { 
                    "protocol_CODE": "VCM001002", 
                    "ReqData": { 
                        "uid": "24ba4944-660a-4d7e-abd3-a4de00abf040", 
                        "userPWord": "123456", 
                        "vcsTarget":"All",
                        "pageSize": "10",
                        "pageNum":"1" 
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                },
                { 
                    "protocol_CODE": "VCM001003", 
                    "ReqData": { 
                        "vcsID": "8ac953df-c658-438e-a308-a4d1010bb4ac", 
                        
                        }, 
                    "stamp_TIMES": "1490192929212", 
                    "serial_NUMBER": "00147001015869149751" 
                } 

            ];