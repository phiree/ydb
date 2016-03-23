<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Service_Edit.aspx.cs" Inherits="DZService_Service_Edit" %>
<%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <UC:ServiceEdit runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
    <!--工作时间模板-->
    <script id="workTimeTmp" type="text/template">
        <div class="wt-item" data-role="workTimeItem" data-wid="{%= workTimeID %}">
            <div class="wt-bar">
                <div class="wt-show">
                    <div>
                        <span class="time-select-label">{%= startTime %}</span>-<span class="time-select-label">{%= endTime %}</span>
                    </div>
                    <div>最大接单量:<span>{%= maxOrder %}</span></div>
                </div>
                <div class="wt-editing">
                    <input  class="time-pick" type="text" data-role="startTime" value="{%= startTime %}"/>-<input class="time-pick" type="text" data-role="endTime" value="{%= endTime %}" />
                    <div>最大接单量:<input class="wt-number" type="number" data-role="maxOrder" value="{%= maxOrder %}"/></div>
                </div>
            </div>
            <div class="wt-ctrl">
                <div class="round-checkbox v-m">
                    <input type="checkbox" {% if ( open==="Y"  ){ %} checked {% } %} id="{%= workTimeID %}" data-role="open" />
                    <label {%= "for=" + workTimeID %} ></label>
                    <em></em>
                </div>
                <input class="wt-delete" type="button" data-role="delete" title="删除"/>
                <input class="wt-edit" type="button" data-role="edit" title="编辑"/>
                <input class="wt-confirm" type="button" data-role="confirm" title="确定"/>
            </div>
        </div>
    </script>
    <!--工作日模板-->
    <script id="workDayTmp" type="text/template">
        <div class="wd-item">
            <div class="wd-week">星期{%= week  %}</div>
            <div class="wd-order-edit">每日最大接单量：<span class="wd-maxOrder">{%= maxOrder %}</span><input class="wd-maxOrder-edit" type="number" data-role="dayMaxOrder" value="{%= maxOrder %}"/>
                <input class="wd-maxOrder-edit-btn" data-role="changeMax" type="button" value="修改">
                <input class="wd-maxOrder-conf-btn" data-role="changeMaxConf" type="button" value="确定">
            </div>
            <div class="wt-container">
                <!--注入工作时间-->
            </div>
            <div class="wd-b">
                <div class="wd-add animated fadeInUpSmall">
                    <input type="hidden" value="1" data-role="cWeek">
                    <div class="wd-bar">
                        <span>接单时间段：</span>
                        <div>
                            <input  class="time-pick" type="text" data-role="cStartTime" />-<input class="time-pick" type="text" data-role="cEndTime" />
                        </div>
                    </div>
                    <div class="wd-bar">
                        <span>最大接单量：</span>
                        <input class="wd-num" type="number" value="0" data-role="cMaxOrder">
                    </div>
                    <div class="wd-add-btn">
                        <input class="wd-confirm" type="button" value="确认" data-role="cConfirm">
                        <input class="wd-cancel" type="button" value="取消" data-role="cCancel">
                    </div>
                </div>
                <input class="wd-trigger" data-role="cTrigger" type="button" value="+ 添加新时段">
            </div>
        </div>
    </script>
    <!-- 工作时间设置模板 -->
    <script id="workTimeSetTmp" type="text/template" >
        <div class="wt-set">
        </div>
    </script>
    <script src="/js/libs/json2.js"></script>
    <script src="/js/libs/mock.js"></script>
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/libs/backbone.js"></script>
    <script src="/js/libs/backbone.customApi.js"></script>
    <script src="/js/interfaceAdapter.js"></script>
    <script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js'></script>
    <script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js' ></script>
    <script src="/js/jquery.form.min.js"></script>
    <script src="/js/jquery.lightbox_me.js"></script>
    <script src="/js/ServiceType.js"></script>
    <script src="/js/StepByStep.js"></script>
    <script src="/js/CascadeCheck.js"></script>
    <script src="/js/timePick.js"></script>
    <script>
        var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$ctl00$';
    </script>
    <script src="/js/validation_service_edit.js"></script>
    <script src="/js/validation_invalidHandler.js"></script>
    <script>
        Mock.mockjax(jQuery);
        Mock.mock(/test.001001.json/, function(){
            return {
                "protocol_CODE": "WTM001006",
                "state_CODE": "009000",
                "RespData": {
                    "arrayData": [
                        "6F9619FF-8B86-D011-B42D-00C04FC964FF"
                    ]
                },
                "stamp_TIMES": "1490192929335",
                "serial_NUMBER": "00147001015869149751"
            }
        });
        Mock.mock(/test.001002.json/, function(){
            return {
                "protocol_CODE": "WTM001006",
                "state_CODE": "009000",
                "stamp_TIMES": "1490192929335",
                "serial_NUMBER": "00147001015869149751"
            }
        });
        Mock.mock(/test.001006.json/, function(){
            return {
                "protocol_CODE": "WTM001006",
                "state_CODE": "009000",
                "RespData": {
                    "arrayData": [
                        {
                            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                            "tag": "默认工作时间",
                            "startTime": "07:00",
                            "endTime": "19:00",
                            "week": "1",
                            "open": "Y",
                            "maxOrder": "60"
                        },
                        {
                            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FE",
                            "tag": "默认工作时间",
                            "startTime": "07:00",
                            "endTime": "19:00",
                            "week": "2",
                            "open": "N",
                            "maxOrder": "60"
                        }
                    ]
                },
                "stamp_TIMES": "1490192929335",
                "serial_NUMBER": "00147001015869149751"
            }
        });

        Mock.mock(/test.001003.json/, function(){
            return {
                "protocol_CODE": "WTM001006",
                "state_CODE": "009000",
                "RespData": {
                    "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "tag": "Y",
                    "startTime": "07:00",
                    "endTime": "19:00",
                    "open": "Y"
                },
                "stamp_TIMES": "1490192929335",
                "serial_NUMBER": "00147001015869149751"
            }
        });
        Mock.mock(/test.s001005.json/, function(){
            return {
                "protocol_CODE": "SVC001005",
                "state_CODE": "009000",
                "RespData": {
                    "svcObj": {
                        "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "name": "特色洗衣",
                        "type": "保洁,洗衣",
                        "introduce": "超越常规洗衣，突破极限",
                        "area": "海南省海口市",
                        "startAt": "60",
                        "unitPrice": "70",
                        "deposit": "70",
                        "appointmentTime": "60",
                        "serviceTimes": "6F9619FF-8B86-D011-B42D-00C04FC964FF, 6F9619FF-8B86-D011-B42D-00C04FC964FF, 6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "maxOrder": "90",
                        "doorService": "Y",
                        "serviceObject": "all",
                        "payWay": "WeiPay",
                        "tag": "洗衣,常规",
                        "open": "Y",
                        "maxOrderString" : "10,10,10,10,10,20,90"
                    }
                },
                "stamp_TIMES": "1490192929335",
                "serial_NUMBER": "00147001015869149751"
            }
        })
    </script>
    <script src="/js/workTimeSet.js"></script>
    <script>
        $(function () {
            (function (){
                var hiTypeValue = $("#hiTypeId").attr("value");
                if ( typeof hiTypeValue === "undefined" ) {
                    return;
                } else {
                    $("#lblSelectedType").removeClass("hide");
                }
            })();

            $(".service-time-table tbody tr:even").addClass("list-item-odd");

            $('#serChoiceContainer').cascadeCheck({
                localData : true,
                data : typeList,

                outputTarget : null,
                confirmTrigger : '#serChoiceConf',

                checkedCallBack : showCheck,

                submitTarget : '#hiTypeId',
                submitCallback : showSubmit,

                /* 取lightBox_me.js里面的位置重置函数，使每次新建列表时重置位置 */
                printListCk : setSelfPosition

            });

            function setSelfPosition() {
                var $self = $('#serLightContainer');
                $self.css({left: '50%', marginLeft: ($self.outerWidth() / 2) * -1, zIndex: (999 + 3)});
                if (($self.height() + 80 >= $(window).height()) && ($self.css('position') != 'absolute')) {
                    var topOffset = $(document).scrollTop() + 40;
                    $self.css({position: 'absolute', top: topOffset + 'px', marginTop: 0})
                } else if ($self.height() + 80 < $(window).height()) {
                    $self.css({position: 'fixed', top: '50%', marginTop: ($self.outerHeight() / 2) * -1})
                }
            }

            function showCheck(checkId, checkText){
                var checkTextEle = document.createElement('span');
                checkTextEle.innerText = checkText;
                $('#serChoiceResult').removeClass('dis-n').html(checkTextEle);
            }

            function removeCheck(){
                $('#serChoiceResult').addClass('dis-n').html();
            }

            function showSubmit (checked, checkId, checkText){
                var $selected = $('#lblSelectedType');
                if ( $selected.hasClass("hide") ) $selected.removeClass("hide");
                $selected.html(checkText)
            }

            $("#setSerType").click(function (e) {
                removeCheck();
                $('#serChoiceContainer').cascadeCheck('build');

                $('#serLightContainer').lightbox_me({
                    centered: true,
                    preventScroll: true
                });
                e.preventDefault();
            });

            $(function () {
                $('[data-toggle="tooltip"]').tooltip(
                    {
                        delay: {show : 500, hide : 100},
                        trigger: 'hover'
                    }
                );
            });

            function setTime(date,timeString){
                var arr=timeString.split(":");
                var hour=parseInt(arr[0]);
                var minites = arr[1]?parseInt(arr[1]):0;
                var seconds=arr[2]?parseInt(arr[2]):0;
                return date.setHours(hour,minites,seconds);
            }

            $.validator.addMethod("endtime_should_greater_starttime", function (value, element) {
                var x_date = new Date();
                var start = $("#tbxServiceTimeBegin").val();
                var end = $("#tbxServiceTimeEnd").val();

                var date_start = setTime(x_date,start);
                var date_end = setTime(x_date,end);
                return date_end > date_start;

            }, "结束时间应该大于开始时间");

            $.validator.addMethod("totalday_should_greater_totalhour", function (value, element) {

                var day = parseInt( $("#tbxMaxOrdersPerDay").val());
                var hour = parseInt( $("#tbxMaxOrdersPerHour").val());

                return day >= hour;

            }, "每日接单量应该大于每小时最大接单量");


            $($("form")[0]).validate(
                {
                    ignore:[],
                    errorElement: "p",
                    errorPlacement: function(error, element) {
                        error.appendTo( element.parent() );
                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
//                    invalidHandler: invalidHandler,
//                    showErrors: showErrorsHandler
                }
            );

            $(".steps-wrap").StepByStep({
                stepNextFunc : function(){
                    return $('.steps-wrap').find('.cur-step').find('input,textarea,select').valid();
                }
            });
        });

        function loadBaiduMapScript() {
            var script = document.createElement("script");
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initializeService";
            document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        })
    </script>
    <script src="/js/CityList.js"></script>
    <script src="/js/baiduMapLib.js"></script>
    <script src="/js/service.js"></script>
    <script src="/js/iptag.js"></script>
</asp:Content>

