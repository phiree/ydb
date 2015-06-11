<%@ Page Title="" Language="C#" MasterPageFile="~/a.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .hidden
        {
            display: none;
        }
        .incoming
        {
            background-color: #ddd;
        }
        .xml_punc
        {
            color: #888;
        }
        .xml_tag
        {
            color: #e77;
        }
        .xml_aname
        {
            color: #55d;
        }
        .xml_avalue
        {
            color: #77f;
        }
        .xml_text
        {
            color: #aaa;
        }
        .xml_level0
        {
            padding-left: 0;
        }
        .xml_level1
        {
            padding-left: 1em;
        }
        .xml_level2
        {
            padding-left: 2em;
        }
        .xml_level3
        {
            padding-left: 3em;
        }
        .xml_level4
        {
            padding-left: 4em;
        }
        .xml_level5
        {
            padding-left: 5em;
        }
        .xml_level6
        {
            padding-left: 6em;
        }
        .xml_level7
        {
            padding-left: 7em;
        }
        .xml_level8
        {
            padding-left: 8em;
        }
        .xml_level9
        {
            padding-left: 9em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Hello</h1>
    <div id="log">
    </div>
    <input type="button" id="ping_button" value="PingServer" />
    <h1>
        Peek</h1>
    <div id="console" style="overflow: auto; height: 300px;">
    </div>
    <textarea  rows="5" cols="10" id="input" class="disabled" disabled="disabled"></textarea>
    <div id="buttonbar">
     
        <input type="button" id="send_button" value="send Data" disabled="disabled" class="button" />
        <input type="button" id="disconnect_button" value="Disconnect" disabled="disabled"
            class="button" />
    </div>
    <div id="login_dialog" class="hidden">
        <label>
            JID:</label><input type="text" id="jid" />
        <label>
            Password:</label><input type="password" id="password" />
    </div>
    <script type="text/javascript">
        var Hello = {
            connection: null,
            log: function (msg) {
                $("#log").append("<p>" + msg + "</p>");
            },
            send_ping: function (to) {
              var ping = $iq(
                                { to: to,
                                    type: "get",
                                    id: "ping1"
                                }
                               )
                               .c("ping", { xmlns: "urn:xmpp:ping" });
                Hello.log("sending ping to "+to+".");
                Hello.start_time=(new Date()).getTime();
                Hello.connection.send(ping);
            },
            handle_pong: function (iq) {
                var elapsed=(new Date()).getTime()-Hello.start_time;
                Hello.log("Received pong from server in "+elapsed+"ms");
                //Hello.connection.disconnect();
                Hello.ping_loop_index+=1;
                if(Hello.ping_loop_index<Hello.ping_loop_max)
                    {
                        var domain = Strophe.getDomainFromJid(Hello.connection.jid);
                        Hello.send_ping(domain);
                        return true;
                    }
                    else{
                        return false;
                    }
               
             },
            ping_loop_index:0,
            ping_loop_max:2,
            star_time:null,
        };
        var Peek={
        connection:null,
        show_traffic:function(body,type){
            if(body.childNodes.length>0)
            {
                var console=$("#console").get(0);
                var at_bottom=console.scrollTop>=console.scrollHeight-console.clientHeight;
                $.each(body.childNodes,function(){
                    $("#console").append(
                            "<div class='"+type+"'>" 
                           // Peek.xml2html(Strophe.serialize(this))
                            +Peek.pretty_xml(this)
                            +"</div><hr/>"
                            );
                    });
                    if(at_bottom)
                        {
                            console.scrollTop=console.scrollHeight;
                        }
            }//if
         },//show_trafic
         xml2html:function(x)
         {
             return x.replace(/&/g,"&amp;")
             .replace(/</g,"&lt;")
             .replace(/>/g,"&gt;");
         },
         pretty_xml:function(xml,level)
         {

    var i, j;
    var result = [];
    
    if(!level){
        level = 0;
    }
    result.push("<div class='xml_level" + level + "'>");
    result.push("<span class='xml_punc'>&lt;</span>");
    result.push("<span class='xml_tag'>");
    result.push(xml.tagName);
    result.push("</span>");
    // attributes
    var attrs = xml.attributes;
     var attr_lead = []
    for (i = 0; i < xml.tagName.length + 1; i++) {
        attr_lead.push("&nbsp;");
    }
    attr_lead = attr_lead.join("");
    for (i = 0; i < attrs.length; i++) {
        result.push(" <span class='xml_aname'>");
        result.push(attrs[i].nodeName);
        result.push("</span><span class='xml_punc'>='</span>");
        result.push("<span class='xml_avalue'>");
        result.push(attrs[i].nodeValue);
        result.push("</span><span class='xml_punc'>'</span>");
        if (i !== attrs.length - 1) {
            result.push("</div><div class='xml_level" + level + "'>");
            result.push(attr_lead);
        }
    }
    if (xml.childNodes.length === 0) {
        result.push("<span class='xml_punc'>/&gt;</span></div>");
    } else {
        result.push("<span class='xml_punc'>&gt;</span></div>");
        // children
        $.each(xml.childNodes, function () {
            if (this.nodeType === 1) {
                result.push(Peek.pretty_xml(this, level + 1));
            } else if (this.nodeType === 3) {
                result.push("<div class='xml_text xml_level" +
                            (level + 1) + "'>");
                result.push(this.nodeValue);result.push("</div>");
            }
        });
        result.push("<div class='xml xml_level" + level + "'>");
        result.push("<span class='xml_punc'>&lt;/</span>");
        result.push("<span class='xml_tag'>");
        result.push(xml.tagName);
        result.push("</span>");
        result.push("<span class='xml_punc'>&gt;</span></div>");
    }
    return result.join("");

      
         },
         text_to_xml:function(text)
         {
         var doc = null;
    if (window['DOMParser']) {
        var parser = new DOMParser();
        doc = parser.parseFromString(text, 'text/xml');
    } else if (window['ActiveXObject']) {
        var doc = new ActiveXObject("MSXML2.DOMDocument");
        doc.async = false;
        doc.loadXML(text);
    } else {
        throw {
            type: 'PeekError',
            message: 'No DOMParser object found.'
        };
    }
    var elem = doc.documentElement;
    if ($(elem).filter('parsererror').length > 0) {
 
        return null;
    }
    return elem;
         /**/
         }
        };
    $(document).ready(function () {
        $("#login_dialog").dialog(
        {
            autoOpen: false,
            modal: true,
            title: "Connect to xmpp server",
            buttons: {
                "Connect": function () {
                    $(document).trigger('connect', {
                        jid: $("#jid").val(),
                        password: $("#password").val()
                    });
                    $("#password").val("");
                    $(this).dialog("close");

                }
            }
        }
        );

        $(document).bind("connect", function (ev, data) {
            var conn = new Strophe.Connection("http://localhost:7070/http-bind/");
            conn.xmlInput=function(body){Peek.show_traffic(body,"incoming");}
            conn.xmlOutput=function(body){Peek.show_traffic(body,"outgoing");}
            conn.connect(data.jid, data.password, function (status) {   
                if (status === Strophe.Status.CONNECTED) {
                    $(document).trigger("connected");
                   
                }
                else if (status === Strophe.Status.CONNECTING) {
                    $(document).trigger("connecting");
                }
                else if (status === Strophe.Status.DISCONNECTED) {
                    $(document).trigger("disconnected");
                }
                else if (status === Strophe.Status.AUTHENTICATING) {
                    $(document).trigger("authenticating");
                }
                else if (status === Strophe.Status.DISCONNECTING) {
                    $(document).trigger("disconnecting");
                }
                else if (status === Strophe.Status.CONNFAIL) {
                    $(document).trigger("connfail");
                }
                else if (status === Strophe.Status.AUTHFAIL) {
                    $(document).trigger("authfail");
                }
            });
            Hello.connection = conn;
            Peek.connection=conn;
        });


        $(document).bind("connected", function () {
            Hello.log("Connected");
            Hello.connection.addHandler(Hello.handle_pong,null,"iq",null,"ping1");
             var domain = Strophe.getDomainFromJid(Hello.connection.jid);
             Hello.send_ping(domain);

             $("#disconnect_button").removeAttr("disabled");

             $('.button').removeAttr('disabled');
             $('#input').removeClass('disabled').removeAttr('disabled');
             

        });
        $(document).bind("connecting", function () {
            Hello.log("connecting");
        });
        $(document).bind("connfail", function () {
            Hello.log("connfail");
            Hello.log(Hello.connection);
        });
        $(document).bind("disconnected", function () {
            Hello.log("disconnected");
             $("#disconnect_button").attr("disabled","disabled");

              $('.button').attr('disabled','disabled');
    $('#input').addClass('disabled').attr('disabled', 'disabled');

        });
        $("#ping_button").click(function(){
         $(document).trigger("connect", {
                        jid: "yuanfei@yuanfei-pc",
                        password: "1"
                    });
        });
       
        $("#disconnect_button").click(function(){
             Peek.connection.disconnect();
        });
             $('#send_button').click(function () {
        var input = $('#input').val();
        var error = false;
        if (input.length > 0) {
            if (input[0] === '<') {
                var xml = Peek.text_to_xml(input);
                if (xml) {
                    Peek.connection.send(Strophe.copyElement(xml));
                    $('#input').val('');
                } else {
                    error = true;
                }
            } else if (input[0] === '$') {
                try {
                    var builder = eval(input);
                    Peek.connection.send(builder);
                    $('#input').val('');
                } catch (e) {
                    console.log(e);
                    error = true;
                }
            } else {
                error = true;
            }
        }

        if (error) {
            $('#input').animate({backgroundColor: "#faa"});
        }
    });

 
   $('#input').keypress(function () {
        $(this).css({'backgroundColor': '#fff'});
    });
        
    });
    </script>
</asp:Content>
