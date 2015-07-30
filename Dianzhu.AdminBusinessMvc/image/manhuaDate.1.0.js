/***
 * 漫画Jquery时间插件
 * 编写时间：2012年7月14号
 * version:manhuaDate.1.0.js
***/
$(function() {
	$.fn.manhuaDate = function(options) {
		var defaults = {
			Event : "click",
			Left : 0,
			Top : 22,
			fuhao : "-",
			isTime : false,
			beginY : 1949,
			endY : 2049,
			dataV:""
			
		};
		var options = $.extend(defaults,options);
		var bid = parseInt(Math.random()*100000);	
		$(".calenderdemo").prepend("</div><div class='calender'><div class='calenderContent'><div class='calenderTable'><div class='getyear' id='getdate'><a class='preMonth' id='preMonth"+bid+"'><img src='images/preMoth.jpg' alt='上一月'/></a><span id='month-value'>二月</span><span id='month-id'>二月</span> <span id='year-id'>2015</span><a class='nextMonth' id='nextMonth"+bid+"'><img src='images/nextMoth.jpg' alt='下一月'/></a></div><div class='line'></div><div class='day-class'><span id='day-id'>15</span></div><div class='day-class'><span id='weeken-id'>星期五</span></div><div div class='prompt'><span class='circle'></span><span id='prompt-id'>预约提醒</span></div><div class='tablebg'><table class='calendertb' cellpadding='0' cellspacing='0'><tr class='weekend-tr'><th class='weekend'>日</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th><th class='weekend noborder'>六</th></tr></table><table class='calendertb' id='calender"+bid+"'><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2'></td></tr></table></div></div></div>");	
		var $mhInput = $(this);
		var dayNames = new Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
		var isToday = true;//是否为今天默认为是	
		var date = new Date();//获得时间对象
		var nowYear = date.getFullYear();//获得当前年份
		var nowMonth = date.getMonth() + 1;//获得当前月份
		var nowweeks=date.getDay();
		var today = date.getDate();//获得当前天数
		var nowWeek = new Date(nowYear, nowMonth - 1, 1).getDay();//获得当前星期
		var nowLastday = getMonthNum(nowMonth, nowYear);//获得最后一天
		var data_arr=options.dataV.split(",")
		$("#year-id").html(nowYear);
		$("#month-id").html(nowMonth);
		$("#weeken-id").html(dayNames[nowweeks]);
		mothText(nowMonth);
		
		$("#day-id").html(today);
		
		//年、月下拉框的初始化
		/*
		for(var i=options.beginY; i<=options.endY; i++){
			$("<option value='"+i+"'>"+i+"年</option>").appendTo($("#year"+bid))
		}
		for(var i=1; i<=12; i++){
			$("<option value='"+i+"'>"+i+"月</option>").appendTo($("#month"+bid));
		}
		*/		
		ManhuaDate(nowYear, nowMonth, nowWeek, nowLastday);//初始化为当前日期		
		//上一月绑定点击事件
		$("#preMonth"+bid).click(function() {
			isToday = false;
			var year = parseInt($("#year-id").html());
			var month = parseInt($("#month-value").html());
			mothText(month);	
			month = month - 1;
			if (month < 1) {
				month = 12;
				year = year - 1;
			}
			if(nowYear==year && nowMonth==month){
				isToday = true;
			}
			var week = new Date(year, month - 1, 1).getDay();
			var lastday = getMonthNum(month, year);
			ManhuaDate(year, month, week, lastday);
		});		
		
		//下一个月的点击事件
		 $("#nextMonth"+bid).click(function() {
			isToday = false;
			var year = parseInt($("#year-id").html());
			var month = parseInt($("#month-value").html());
		    mothText(month);
			month = parseInt(month) + 1;
			if (parseInt(month) > 12) {
				month = 1;
				year = parseInt(year) + 1;
			}
			if(nowYear==year && nowMonth==month){
				isToday = true;
			}
			var week = new Date(year, month - 1, 1).getDay();
			var lastday = getMonthNum(month, year);
			ManhuaDate(year, month, week, lastday);
		});
		 
		 //初始化日历
		 function ManhuaDate(year, month, week, lastday) {
			$("#year-id").html(year);
			$("#month-value").html(month)
			mothText(month);
			var table = document.getElementById("calender"+bid);
			var premonthday=getMonthNum(month-1, year);
			var nextmonthday=1
			
			var n = 1;
			for (var j = 0; j < week; j++) {
				table.rows[0].cells[j].className="pre-mon-lastday";
				table.rows[0].cells[j].innerHTML = premonthday-(week-j)+1;
				
			}
			for (var j = week; j < 7; j++) {
				
				table.rows[0].cells[j].innerHTML = n;
				for(dd=0;dd<data_arr.length;dd++){
							if(data_arr[dd]==n){
								table.rows[0].cells[j].innerHTML = n+"<br/><div class='circle2'></div>";
							}
						}
				if (n == today && isToday) {
							
					table.rows[0].cells[j].className="tdtoday";	
					table.rows[0].cells[j].innerHTML = n+"<br/>今天";			
				}else {
					table.rows[0].cells[j].className="";
				}
				n++;
			}
			for (var i = 1; i < 6; i++) {
				for (j = 0; j < 7; j++) {
					if (n > lastday) {
						table.rows[i].cells[j].className="pre-mon-lastday";
						table.rows[i].cells[j].innerHTML = nextmonthday;
						nextmonthday++;
					}
					 else {
						 table.rows[i].cells[j].innerHTML = n;
						if (n == today && isToday) {						
							table.rows[i].cells[j].className="tdtoday";	
							table.rows[i].cells[j].innerHTML = n+"<br/><span style=' font-size:10px; color:#CCC'>今天</span>";					
						}else {
							table.rows[i].cells[j].className="";
						}
						table.rows[i].cells[0].className="stu-sun-day";
						table.rows[i].cells[6].className="stu-sun-day";
						//table.rows[i].cells[j].innerHTML = n+"<br/><div class='circle2'></div>";
						
						for(dd=0;dd<data_arr.length;dd++){
							if(data_arr[dd]==n){
								table.rows[i].cells[j].innerHTML = n+"<br/><div class='circle2'></div>";
								if (n == today && isToday) {						
							table.rows[i].cells[j].className="tdtoday";	
							table.rows[i].cells[j].innerHTML = n+"<br/><span style=' font-size:10px; color:#CCC'>今天</span><br/><div class='circle3'></div>";					
						}
								
							}
						}
						
						n++;
					}
				}
			}
		}
		function mothText(val){
			var monthspan= $("#month-id");
			var mothhtml="一月";
			switch(val){
				case 1:
				  mothhtml="一月";
				break;
				case 2:
				   mothhtml="二月";
				break;
				case 3:
				    mothhtml="三月";
				break;
				case 4:
				    mothhtml="四月";
				break;
				case 5:
				   mothhtml="五月";
				break;
				case 6:
				    mothhtml="六月";
				break;
				case 7:
				     mothhtml="七月";
				break;
				case 8:
				   mothhtml="八月";
				break;
				case 9:
				    mothhtml="九月";
				break;
				case 10:
				     mothhtml="十月";
				break;
				case 11:
				    mothhtml="十一月";
				break;
				case 12:
				    mothhtml="十二月";
				break;
			}
			monthspan.html(mothhtml);
		}
		//获得月份的天数
		function getMonthNum(month, year) {
			month = month - 1;
			var LeapYear = ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? true: false;
			var monthNum;
			switch (parseInt(month)) {
			case 0:
			case 2:
			case 4:
			case 6:
			case 7:
			case 9:
			case 11:
				monthNum = 31;
				break;
			case 3:
			case 5:
			case 8:
			case 10:
				monthNum = 30;
				break;
			case 1:
				monthNum = LeapYear ? 29: 28;
			}
			return monthNum;
		}		
		//每一列的悬挂事件改变当前样式
		/*
		$("#calender"+bid+" td:not(.tdtoday)").hover(function() {
			
			var tdv = $(this).html();
			if (tdv != "&nbsp;"){
				
				$(this).addClass("hover")
			}
			
			
		},function() {
			$(this).removeClass("hover");
		});	
		*/	
		//点击时间列表事件
		$("#calender"+bid+" td").live("click",function() {	
		var targetValue="";
		    if($(this).html().indexOf("<br>")>0){
				var index=$(this).html().indexOf("<br>")
				targetValue=$(this).html().substring(0,index);
				
			}else{
				targetValue=$(this).html();
			}
		
			alert(targetValue);
			var dv = $(this).html();
			$("table td").removeClass("tdtoday");
			$(this).addClass("tdtoday");
			if (dv != "&nbsp;"){
				 var str = "";
				 if (options.isTime){			
					var nd = new Date();
					str = $("#year"+bid).val() + options.fuhao + $("#month"+bid).val() + options.fuhao + dv + " "+ nd.getHours()+":"+nd.getMinutes()+":"+nd.getSeconds();
				 }else{
					str = $("#year"+bid).val() + options.fuhao + $("#month"+bid).val() + options.fuhao + dv;
				}
				//$mhInput.val(str);
			
			}
		});
		
	};
});