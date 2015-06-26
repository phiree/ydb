var clickName="";//点击名ID的变量
var hiddenName="";//提交的服务器的id变量
var serTimetxt="";//服务时间变量
var serTimetxt2="";//服务时段变量
var hiddenSerTime1="";//提交的服务器的id
var hiddenSerTime2="";//提交的服务器的id2
function saveInputVal(){
	var val=$("#rightInputName").val();
	if(val!=""){
		if(clickName=="#loginpwd-txt"){
			
		
		}else{
			
		}
		$(clickName).text(val);
		$(hiddenName).val(val);
		$("#rightInputName").val("");
	
	}
}
function saveprice() {
    $("#spanUnitPrice").text($("tbxUnitPrice").val());
    $("#spanChargeUnit").text(
     $('input[name=rblChargeUnit]:checked').val()
    );
}
function goToRightPanel(iName,tName){
	
	hiddenName=iName;
	var txt=$(tName).text();
	
	$("#rightInputName").val(txt);
	clickName=tName;
	
}


function saveInputVal2(){
	var val=$("#rightInputName2").val();
	if(val!=""){	
		$(hiddenName).val(val);
		alert(val);
		$("#rightInputName2").val("");
	
	}
}
function goToRightPanel2(iName,tName){
	
	hiddenName=iName;
	clickName=tName;
	
}



function saveInputVal3(){
	var val=$("#serTime").val();
	var val2=$("#serTime2").val();
	if(val!=""){
		
		$(serTimetxt).text(val);
		$(hiddenSerTime1).val(val);
		$("#serTime").val("");
	
	}
	if(val2!=""){
		
		$(serTimetxt2).text(val2);
		$(hiddenSerTime2).val(val2);
		$("#serTime2").val("");
	
	}
}
function goToRightPanel3(iName1,iName2,tName1,tName2){
	
	hiddenSerTime1=iName1;
	hiddenSerTime2=iName2;
	serTimetxt=tName1;
	serTimetxt2=tName2;
	var txt=$(tName1).text();
	var txt2=$(tName2).text();
	$("#serTime").val(txt);
	$("#serTime2").val(txt2);
	
	
}

function getSelectVal(selectID,targetID){
	
	var txt=$(selectID+" option:selected").text();

	$(targetID).text(txt);

}


$(document).ready(function () {
    	       $('.input-file-btn').change(function () {
                var imgObjPreview = $(this).parent().siblings(".input-file-pre").get(0);
                if (this.files && this.files[0]) {


                    imgObjPreview.src=window.URL.createObjectURL(this.files[0]);

                    console.log(imgObjPreview.src);
                }
               
                return true;
            })
        });

