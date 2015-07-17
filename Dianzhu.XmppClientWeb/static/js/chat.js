var Chat =
{ pending_subscriber: null,
    server: "@yuanfei-pc",
    connection: null,
    file_type: [{ 'name': 'image', 'ext': ["jpg", "jpeg", "bmp", "gif", "png"] },
                { 'name': 'audio', 'ext': ["mp3", "wav", "wma"] },
                ],
    decide_file_type: function (filename) {
        var ext = filename.split('.').pop();
        for (var i=0;i<this.file_type.length;i++) {
            if ($.inArray(ext, this.file_type[i].ext) > -1) {
                return  this.file_type[i].name;
            }
        }
        return null;
    }, //decide_file_type
    on_roster: function (iq) {
        $(iq).find('item').each(function () {
            var jid = $(this).attr('jid');
            var name = $(this).attr('name') || jid;
            var jid_id = Chat.jid_to_id(jid);
            var contact = $("<li id='" + jid_id + "'>" +
                            "<div class='roster-contact offline'>" +
                            "<div class='roster-name'>" +
                            name +
                            "</div><div class='roster-jid'>" +
                            jid +
                            "</div></div></li>");
            Chat.insert_contact(contact);
        }); //each
        Chat.connection.addHandler(Chat.on_presence, null, "presence");
        Chat.connection.send($pres());
    }, //on_roster
    on_presence: function (presence) {
        var ptype = $(presence).attr("type");
        var from = $(presence).attr("from");
        var jid_id = Chat.jid_to_id(from);
        if (ptype === 'subscribe') {
            // populate pending_subscriber, the approve-jid span, and
            // open the dialog
            Chat.pending_subscriber = from;
            $('#approve-jid').text(Strophe.getBareJidFromJid(from));
            $('#approve_dialog').dialog('open');
        }
        else if (ptype !== 'error') {
            var contact = $('#roster-area li#' + jid_id + ' .roster-contact')
             .removeClass("online")
           .removeClass("away")
           .removeClass("offline");
            if (ptype === "unavailable")
            { contact.addClass("offline"); }
            else {
                var show = $(presence).find('show').text();
                if (show === "" || show === "chat") {
                    contact.addClass("online");
                }
                else {
                    contact.addClass("away");
                }

            }
            var li = contact.parent();
            li.remove();
            Chat.insert_contact(li);
        } //if(ptype!==)
        //var jid_id = Chat.jid_to_id(from);
        $('#chat-' + jid_id).data('jid', Strophe.getBareJidFromJid(from));

        return true;
    }, //on_presence
    /******on_roster_chaged*******/
    on_roster_changed: function (iq) {
        $(iq).find('item').each(function () {
            var sub = $(this).attr('subscription');
            var jid = $(this).attr('jid');
            var name = $(this).attr('name') || jid;
            var jid_id = Chat.jid_to_id(jid);

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
                    Chat.insert_contact($(contact_html));
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
        var jid = elem.find('.roster-jid').text();
        var pres = Chat.presence_value(elem.find('.roster-contact'));
        var contacts = $("#roster-area li");
        if (contacts.length > 0) {
            var inserted = false;
            contacts.each(function () {
                var cmp_pres = Chat.presence_value($(this).find('.roster-contact'));
                var cmp_jid = $(this).find('.roster-jid').text();
                if (cmp_pres > pres) {
                    $(this).before(elem);
                    inserted = true;
                    return false;
                } else {
                    if (jid < cmp_jid) {
                        $(this).before(elem);
                        inserted = true;
                        return false;
                    }
                }
            }); //each
            if (!inserted) {
                $("#roster-area ul").append(elem);
            }
        } //if(contacts.length>0)
        else {
            $("#roster-area ul").append(elem);
        }
    }, //insert_contact
    scroll_chat: function (jid_id) {
        var div = $('#chat-' + jid_id + ' .chat-messages').get(0);
        div.scrollTop = div.scrollHeight;
    }, //scrool_chat
    //
    display_media: function (message_body,is_local) {
        var fileType = this.decide_file_type(message_body);
        if(is_local)
        {
            if (fileType == "image") {
                return "<img src='" + message_body + "' />";
            }
        }
        else
        {


        }
    }, //display_media
    on_message: function (message) {
        var full_jid = $(message).attr('from');
        var jid = Strophe.getBareJidFromJid(full_jid);
        var jid_id = Chat.jid_to_id(jid);
        // 
        var tabIndex = 0;
        if ($('#chat-' + jid_id).length === 0) { //create a new tab to hanlde it
            $('#chat-area ul').append("<li><a href='#chat-" + jid_id + "'>" + jid + "</a>");
            $("#chat-area").append("<div id='chat-" + jid_id + "'>"
                                      + "<div class='chat-messages'></div>"
                                      + "<input type='text' class='chat-input' />"
                                      +"<input type='file'  class='chat-file'/>"
                                  + "</div>");

            $('#chat-area').tabs();
        }
        $('#chat-' + jid_id).data('jid', full_jid);
        tabIndex = $("#chat-area>div").index($('#chat-' + jid_id));
        $('#chat-area').tabs("refresh");
        $('#chat-area').tabs("option", "active", tabIndex);
        //$('#chat-area').tabs('active', '#chat-' + jid_id);

        $('#chat-' + jid_id + ' input').focus();

        //显示对方 正在输入 的信息
        var composing = $(message).find('composing');
        if (composing.length > 0) {
            $('#chat-' + jid_id + ' .chat-messages').append(
                "<div class='chat-event'>" +
                Strophe.getNodeFromJid(jid) +
                " is typing...</div>");

            Chat.scroll_chat(jid_id);
        }
        //显示信息
        var body = $(message).find("html > body");

        if (body.length === 0) {
            body = $(message).find('body');
            if (body.length > 0) {
                body = body.text()
            } else {
                body = null;
            }
        } else {
            body = body.contents();
            //body = this.display_media(body);
            var span = $("<span></span>");
            body.each(function () {
                if (document.importNode) {
                    $(document.importNode(this, true)).appendTo(span);
                } else {
                    // IE workaround
                    span.append(this.xml);
                }
            });

            body = span;
        }

        if (body) {

           Chat.display_local_message(body,$('#chat-' + jid_id + ' .chat-messages').parent(),jid);
            // remove notifications since user is now active
           /* $('#chat-' + jid_id + ' .chat-event').remove();

            // add the new message
            $('#chat-' + jid_id + ' .chat-messages').append(
                "<div class='chat-message'>" +
                "&lt;<span class='chat-name'>" +
                Strophe.getNodeFromJid(jid) +
                "</span>&gt;<span class='chat-text'>" +
                "</span></div>");

            $('#chat-' + jid_id + ' .chat-message:last .chat-text')
                .append(body);

            Chat.scroll_chat(jid_id);*/
        }

        return true;
    }, //on_message,
    send_message: function (body,jid) {
        //发送消息
        var message = $msg({
            to: jid, //todo
            "type": "chat"
        }).c("body").t(body).up()
             .c('active', { xmlns: "http://jabber.org.protocal/chatstates" });
        Chat.connection.send(message);
    },//send_message
    //显示自己发送的消息,如果是文件,显示的是服务器路径.
    display_local_message:function(message,chat_body_container,jid){
        var fileType=this.decide_file_type(message);
        switch (fileType)
        {
            case 'image':message="<img src='"+message+"'/>"; break;
            case 'audio':message="<a href='"+message+"'>"+message+"</a>";break;
        }
        chat_body_container.find('.chat-messages').append(
            "<div class='chat-message'>&lt;" +
            "<span class='chat-name me'>" +
            Strophe.getNodeFromJid(Chat.connection.jid) +
            "</span>&gt;<span class='chat-text'>" +
            message+
            "<img class='img-preview'>"+
            "</span></div>");

        Chat.scroll_chat(this.jid_to_id(jid));

    }//display_local_message
};             //var Chat

$(document).ready(function () {
    /********contact_dialog*************/
    $("#contact_dialog").dialog({
        autoOpen: false,
        draggable: false,
        modal: true,
        title: "Add a Contact",
        buttons: {
            "Add": function () {
                $(document).trigger("contact_added", {
                    jid: $("#contact-jid").val() + Chat.server,
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
                Chat.connection.send($pres({
                    to: Chat.pending_subscriber,
                    "type": "unsubscribed"
                }));
                Chat.pending_subscriber = null;

                $(this).dialog('close');
            },

            "Approve": function () {
                Chat.connection.send($pres({
                    to: Chat.pending_subscriber,
                    "type": "subscribed"
                }));

                Chat.connection.send($pres({
                    to: Chat.pending_subscriber,
                    "type": "subscribe"
                }));

                Chat.pending_subscriber = null;

                $(this).dialog('close');
            }
        }
    });
    /******Login********* */
    $("#login_dialog").dialog(
    {
        autoOpen: true,
        modal: true,
        buttons: {
            "登录": function () {
                $(document).trigger("connect", {
                    jid: $("#jid").val() + Chat.server,
                    password: $("#password").val()
                });
                //$("#password").val();
                $(this).dialog("close");
            } //function
        }//buttons
    }); //dialog


    /*******event bind*********/
    $(document).bind("connect", function (ev, data) {
        var conn = new Strophe.Connection('http://192.168.1.140:7070/http-bind/');
        conn.connect(data.jid, data.password, function (status) {
            switch (status) {
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
                default: break;
            }
            Chat.connection = conn; //为什么不在 CONNECTED状态里面?
        }); //conn.connect
    }); //bind

    $(document).bind("authfail", function (ev, data) { alert("authfail,用户名密码错误"); }); //bind
    $(document).bind("connfail", function (ev, data) { alert("connfail 连接失败 "); }); //bind
    $(document).bind("disconnected", function (ev, data) { alert("disconnected 连接断开 "); }); //bind
    $(document).bind("connecting", function (ev, data) { }); //bind

    $(document).bind("connected", function (ev, data) {
        var iq = $iq({ type: "get" }).c("query", { xmlns: "jabber:iq:roster" });
        Chat.connection.sendIQ(iq, Chat.on_roster);
        Chat.connection.addHandler(Chat.on_roster_changed, "jabber:iq:roster", "iq", "set");
        Chat.connection.addHandler(Chat.on_message, null, "message", "chat");
    }); //bind
    $(document).bind("contact_added", function (ev, data) {
        var iq = $iq({ type: "set" })
            .c("query", { xmlns: "jabber:iq:roster" })
            .c("item", data);

        Chat.connection.sendIQ(iq);
        var subscribe = $pres({ to: data.jid, "type": "subscribe" });
        Chat.connection.send(subscribe);

    });
    //点击好友, 打开聊天窗口
    $("#roster-area").on("click", ".roster-contact", function (event) {
        var jid = $(this).find(".roster-jid").text();
        var name = $(this).find(".roster-name").text();
        var jid_id = Chat.jid_to_id(jid);
        var tabIndex = 0;
        if ($('#chat-' + jid_id).length === 0) {
            //  $('#chat-area').tabs('add', '#chat-' + jid_id, name);
            $('#chat-area ul').append(
              "<li><a href='#chat-" + jid_id + "'>" + name + "</a></li>"
            );
            var chat_area_each = $(
            "<div id='chat-" + jid_id + "'>"
            + "<div class='chat-messages'></div>"
            + "<input type='text' class='chat-input' />"
               + "<input type='file' class='chat-file' />"
            + "</div>"
            );
            $('#chat-area').append(chat_area_each);

            $('#chat-area').tabs();
            // $('#chat-area').tabs("option","active",2);

        }
        $('#chat-' + jid_id).data('jid', jid);
        tabIndex = $("#chat-area>div").index($('#chat-' + jid_id));
        $('#chat-area').tabs("refresh");
        $('#chat-area').tabs("option", "active", tabIndex);
        //$('#chat-area').tabs('active', '#chat-' + jid_id);

        $('#chat-' + jid_id + ' input').focus();
    });
    //文件上传
    $(document).on('change','.chat-file',function (ev) {
        var that=this;
        var formData = new FormData($('form')[0]);
        formData.append('file', $(that)[0].files[0]);
        $.ajax(
                {
                    url: '/FileUploader.ashx',
                    type: "post",
                    async: false,
                    processData: false,
                    contentType: false,
                    data:formData,
                    success: function (filepath) {

                        var jid = $(that).parent().data('jid');
                        var container=$(that).parent();
                        ev.preventDefault();
                        var remote_message=filepath;
                        Chat.display_local_message(remote_message,container,jid)
                        Chat.send_message(remote_message, jid);
                    }, //success
                    error: function (errmsg) {
                        alert('transfer error:' + errmsg);
                    } //error
                }); //ajax
    });
    $("#chat-area").on("keypress", ".chat-input", function (ev) {
        var jid = $(this).parent().data('jid');
        if (ev.which === 13) {
            ev.preventDefault();
            var body = $(this).val();
            Chat.display_local_message(body,$(this).parent(),jid);
            Chat.send_message(body, jid);
            $(this).val('');
            $(this).parent().data('composing', false);
        }
        else {   //显示 正在输入
            var composing = $(this).parent().data('composing');
            if (!composing) {
                var notify = $msg({ to: jid, "type": "chat" })
                    .c('composing', { xmlns: "http://jabber.org/protocol/chatstates" });
                Chat.connection.send(notify);

                $(this).parent().data('composing', true);
            }
        }
    });
    //$(document).trigger("connect",{
    //                jid:'yuanfei@yuanfei-pc',
    //                password:'1'
    //            });

});        //$(document).ready()