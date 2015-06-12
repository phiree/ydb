var Gab =
{
    connection: null,
    on_roster: function (iq) {
        $(iq).find('item').each(function () {
            var jid = $(this).attr('jid');
            var name = $(this).attr('name') || jid;
            var jid_id = Gab.jid_to_id(jid);
            var contact = $("li id='" + jid_id + "'>" +
                            "<div> class='roster-contact offline'>" +
                            "div class='roster-name'" +
                            name +
                            "</div><div class='roster-jid'>" +
                            jid +
                            "</div></div></li>");
            Gab.insert_contact(contact);
        }); //each
    }, //on_roster
    insert_contact: function () { }, //insert_contact
    jid_to_id: function () {
        return Strophe.getBareJidFromJid(jid)
        .replace("@", "-")
        .replace(".", "-");
    },
    presence_value: function (elem) {
        if (element.hasClass('online')) { return 2; }
        else if (element.hasClass('away')) { return 1; }
        else { return 0; }
    }, //presense_value
    insert_contact: function (elem) { 
        var jid=elem.find('.roster-jid').text();
        var pres=Gab.presence_value(elem.find('.roster-contact'));
        var contacts=$("#roster-area li");
        if(contacts.length>0)
        {
            var inserted=false;
            contacts.each(function(){
                var cmp_pres=Gab.presence_value($(this).find('.roster-contact'));
                var cmp_jid=$(this).find('.roster-jid').text();
                if(cmp_pre>pres)
                {
                    $(this).before(elme);
                    inserted=true;
                    return false;
                }else{
                    if(jid<cmp_jid)
                    {
                        $(this).before(elme);
                        inserted=true;
                        return false;
                    }
                }
            });//each
        }
    },//insert_contact
};

$(document).ready(function(){
/******Login**********/
    $("#login_dialog").dialog(
    {
        autoOpen:true,
        modal:true,
        buttons:{
        "登录":function(){
            $(document).trigger("connect",{
                jid:$("#jid").val(),
                password:$("#password").val()
            });
            $("#password").val();
            $(this).dialog("close");
          }//function
        }//buttons
    });//dialog
/*******event bind*********/
$(document).bind("connect",function(ev,data){
    var conn=new Strophe.Connection('http://localhost-pc:7070/http-bind/');
    conn.connect(data.jid,data.password,function(status){
    switch(status)
    {
        case Strophe.Status.CONNECTING:
        $(document).trigger("connecting");
        break;
        case Strophe.Status.AUTHENTICATING:
         $(document).trigger("authenticating");
        break;
        case Strophe.Status.CONNECTED:
        
         $(document).trigger("connected");
        break;
        case Strophe.Status.DISCONNECTING:
         $(document).trigger("disconnecting");
        break;
        case Strophe.Status.DISCONNECTED:
         $(document).trigger("disconnected");
        break;
        case Strophe.Status.CONNFAIL:
         $(document).trigger("connfail");
        break;
        case Strophe.Status.AUTHFAIL:
         $(document).trigger("authfail");
        break;
        default:break;
    }
    Gab.connection=conn;//为什么不在 CONNECTED状态里面?
    });//conn.connect
});//bind

$(document).bind("connecting",function(ev,data){});//bind
$(document).bind("connected",function(ev,data){
    var iq=$iq({type:"get"}).c("query",{xmlns:"jabber.iq.roster"});
    Gab.connection.sendIQ(iq,Gab.on_roster);
});//bind

});//$(document).ready()