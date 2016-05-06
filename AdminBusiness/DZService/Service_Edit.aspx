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
                <div class="wt-load"></div>
            </div>
            <div class="wd-b">
                <div class="wd-add animated fadeInUpSmall">
                    <input type="hidden" value="{%= week %}" data-role="cWeek">
                    <div class="wd-bar">
                        <span>接单时间段：</span>
                        <div class="m-b10">
                            <input class="time-pick" type="text" data-role="cStartTime" />-<input class="time-pick" type="text" data-role="cEndTime" />
                        </div>
                    </div>
                    <div class="wd-bar">
                        <span>最大接单量：</span>
                        <input class="wd-num" type="number" value="0" data-role="cMaxOrder">
                    </div>
                    <div class="wd-errMsg">
                        时间段设置错误或重复
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
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/libs/backbone.js"></script>
    <script src="/js/libs/backbone.customApi.js"></script>
    <script src="/js/interfaceAdapter.js"></script>
    <script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js'></script>
    <script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js' ></script>
    <script src="/js/jquery.form.min.js"></script>
    <script src="/js/jquery.lightbox_me.js"></script>
    <script src="/js/ServiceType.js"></script>
    <script src="/js/stepByStep.js"></script>
    <script src="/js/CascadeCheck.js"></script>
    <script src="/js/timePick.js"></script>
    <script>
        var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$ctl00$';
    </script>
    <script src="/js/validation_service_edit.js"></script>
    <script src="/js/validation_invalidHandler.js"></script>
    <script src="/js/test/mock.js"></script>
    <script src="/js/test/mock.workTimeset.js"></script>
    <script src="/js/workTimeSet.js"></script>
    <script src="/js/CityList.js"></script>
    <script src="/js/baiduMapLib.js"></script>
    <script src="/js/service.js"></script>
    <script src="/js/iptag.js"></script>
    <script src="/js/select.js"></script>
    <script>
        $(function () {
            $($("form")[0]).validate(
                {
                    ignore:[],
                    errorElement: "p",
                    errorPlacement: function(error, element) {
                        error.appendTo( element.parent() );
                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler,
                    showErrors: showErrorsHandler
                }
            );

            $(".steps-wrap").stepByStep({
                defaultStep : function(){
                    if ( Adapter.getParameterByName("step")){
                        return parseInt(Adapter.getParameterByName("step")) - 1
                    } else {
                        return 0;
                    }
                },
                stepValid : function(){
                    return $('.steps-wrap').find('.cur-step').find('input,textarea,select').valid();
                },
                stepNextFunc : function(step){
                    if (step == "1"){
                        // TODO: 简单粗暴的修复服务初始化时服务点不居中的问题，待重构解决。
                        initializeService();
                    }
                }
            });

            $('[data-toggle="tooltip"]').tooltip(
                {
                    delay: {show : 500, hide : 100},
                    trigger: 'hover'
                }
            );

            $(".select").customSelect();

            (function (){
                if ( $("#hiTypeId").attr("value") ){ $("#lblSelectedType").removeClass("hide"); }

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
            })();
        });
        function loadBaiduMapScript() {
            var script = document.createElement("script");
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=n7GnSlMbBkmS3BrmO0lOKKceafpO5TZc&callback=initializeService";
            document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        });
    </script>
</asp:Content>

