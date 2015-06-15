var Gab =
{pending_subscriber:null,
    connection: null,
    on_roster: function (iq) {
        $(iq).find('item').each(function () {
            var jid = $(this).attr('jid');
            var name = $(this).attr('name') || jid;
            var jid_id = Gab.jid_to_id(jid);
            var contact = $("<li id='" + jid_id + "'>" +
                            "<div class='roster-contact offline'>" +
                            "<div class='roster-name'>" +
                            name +
                            "</div><div class='roster-jid'>" +
                            jid +
                            "</div></div></li>");
            Gab.insert_contact(contact);
        }); //each
        Gab.connection.addHandler(Gab.on_presence,null,"presence");
        Gab.connection.send($pres());
    }, //on_roster
    on_presence:function(presence){
        var ptype=$(presence).attr("type");
        var from=$(presence).attr("from");
        var jid_id = Gab.jid_to_id(from);
         if (ptype === 'subscribe') {
            // populate pending_subscriber, the approve-jid span, and
            // open the dialog
            Gab.pending_subscriber = from;
            $('#approve-jid').text(Strophe.getBareJidFromJid(from));
            $('#approve_dialog').dialog('open');
            }
        else if(ptype!=='error')
        {
           var contact = $('#roster-area li#' + jid_id + ' .roster-contact')
             .removeClass("online")
           .removeClass("away")
           .removeClass("offline"); 
           if(ptype==="unavailable")
	         {contact.addClass("offline");}
           else{
            var show=$(presence).find('show').text();
            if(show===""||show==="chat"){
                contact.addClass("online");
                }
            else{
                contact.addClass("away");
                }
            
            }
            var li=contact.parent();
            li.remove();
            Gab.insert_contact(li);
        }//if(ptype!==)
        //var jid_id = Gab.jid_to_id(from);
        $('#chat-' + jid_id).data('jid', Strophe.getBareJidFromJid(from));

        return true;
    },//on_presence
    /******on_roster_chaged*******/
     on_roster_changed: function (iq) {
        $(iq).find('item').each(function () {
            var sub = $(this).attr('subscription');
            var jid = $(this).attr('jid');
            var name = $(this).attr('name') || jid;
            var jid_id = Gab.jid_to_id(jid);

            if (sub === 'remove') {
                // contact is being removed
                $('#' + jid_id).remove();
            } else {
                // contact is being added or modified
                var contact_html = "<li id='" + jid_id + "'>" +
                    "<div class='" + 
                    ($('#' + jid_id).attr('class') || "roster-contact offline") +
                    "'>" +
                    "<div class='roster-name'>" +
                    name +
                    "</div><div class='roster-jid'>" +
                    jid +
                    "</div></div></li>";

                if ($('#' + jid_id).length > 0) {
                    $('#' + jid_id).replaceWith(contact_html);
                } else {
                    Gab.insert_contact($(contact_html));
                }
            }
        });

        return true;
    },
    /******end_roster_changed*********/ 
    jid_to_id: function (jid) {
        return Strophe.getBareJidFromJid(jid)
        .replace("@", "-")
        .replace(".", "-");
    },
    presence_value: function (elem) {
        if (elem.hasClass('online')) { return 2; }
        else if (elem.hasClass('away')) { return 1; }
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
                if(cmp_pres>pres)
                {
                    $(this).before(elem);
                    inserted=true;
                    return false;
                }else{
                    if(jid<cmp_jid)
                    {
                        $(this).before(elem);
                        inserted=true;
                        return false;
                    }
                }
            });//each
            if(!inserted)
            {
                $("#roster-area ul").append(elem);
            }
        }//if(contacts.length>0)
        else{
          $("#roster-area ul").append(elem);
        }
    },//insert_contact
};//var Gab

$(document).ready(function(){
/********contact_dialog*************/
$("#contact_dialog").dialog({
    autoOpen: false,
    draggable: false,
    modal: true,
    title: "Add a Contact",
    buttons: {
        "Add": function () {
            $(document).trigger("contact_added", {
                jid: $("#contact-jid").val(),
                name: $("#contact-name").val()
            });
            $("#contact-jid").val("");
            $("#contact-name").val("");
            $(this).dialog("close");
        }
    }
});
$("#new-contact").click(function (ev) {
    $("#contact_dialog").dialog("open");
});
/*******subscribe*******/
$('#approve_dialog').dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        title: 'Subscription Request',
        buttons: {
            "Deny": function () {
                Gab.connection.send($pres({
                    to: Gab.pending_subscriber,
                    "type": "unsubscribed"}));
                Gab.pending_subscriber = null;

                $(this).dialog('close');
            },

            "Approve": function () {
                Gab.connection.send($pres({
                    to: Gab.pending_subscriber,
                    "type": "subscribed"}));

                Gab.connection.send($pres({
                    to: Gab.pending_subscriber,
                    "type": "subscribe"}));
                
                Gab.pending_subscriber = null;

                $(this).dialog('close');
            }
        }
    });
/******Login********* */
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
            //$("#password").val();
            $(this).dialog("close");
          }//function
        }//buttons
    });//dialog
   
    
/*******event bind*********/
$(document).bind("connect",function(ev,data){
    var conn=new Strophe.Connection('http://localhost:7070/http-bind/');
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
    var iq=$iq({type:"get"}).c("query",{xmlns:"jabber:iq:roster"});
    Gab.connection.sendIQ(iq,Gab.on_roster);
    Gab.connection.addHandler(Gab.on_roster_changed,"jabber:iq:roster","iq","set");
});//bind
$(document).bind("contact_added",function(ev,data){
    var iq=$iq({type:"set"})
            .c("query",{xmlns:"jabber:iq:roster"})
            .c("item",data);
     
    Gab.connection.sendIQ(iq);
    var subscribe=$pres({to:data.jid,"type":"subscribe"});
    Gab.connection.send(subscribe);
});

//$(document).trigger("connect",{
//                jid:'yuanfei@yuanfei-pc',
//                password:'1'
//            });

});//$(document).ready()