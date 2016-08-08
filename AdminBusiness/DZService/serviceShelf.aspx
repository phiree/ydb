<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ServiceShelf.aspx.cs" Inherits="DZService_ServiceShelf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>服务货架</h3>
        </div>
        <div class="content-main">
            <div class="container-fluid">
                <input id="merchantID" type="hidden" value="<%= merchantID %>">
                <div class="row">
                    <div class="col-md-12">
                        <div id="goodShelf"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

<!--app template-->
<script type="text/template" id="app_template">
    <div class="app_view">
        <div class="day-tabs"></div>
        <div class="day-container">
            <!-- 注入服务单日模版 -->
        </div>
    </div>
</script>

<!-- one day template 单日模版 -->
<script type="text/template" id="day_template">
    <div class="day-title">
        <div class="day-info"><span class="m-l10">当日已售&nbsp;&nbsp;{%= reOrder %}件&nbsp;当日剩余&nbsp;&nbsp;{%= maxOrder - reOrder %}件&nbsp;当日总量&nbsp;{%= maxOrder %}件&nbsp;</span></div>
        <div id="day-warn" class="day-warn hi"></div>
        <!--<div class="day-control">-->
            <!--<div class="day-control-item">-->
                <!--<span>服务编辑开关:</span>-->
                <!--<div class="round-checkbox">-->
                    <!--<input type="checkbox" class="day_edit" {%= "id=" + this.cid + "_dayEdit" %} />-->
                    <!--<label {%= "for=" + this.cid + "_dayEdit" %} ></label>-->
                    <!--<em></em>-->
                <!--</div>-->
            <!--</div>-->
            <!--<div class="day-control-item">-->
                <!--<span >当日服务开关:</span>-->
                <!--<div class="round-checkbox">-->
                    <!--<input type="checkbox" class="day_enable" checked value="dayEdit" {%= "id=" + this.cid + "_dayEnable" %} />-->
                    <!--<label {%= "for=" + this.cid + "_dayEnable" %} ></label>-->
                    <!--<em></em>-->
                <!--</div>-->
            <!--</div>-->
        <!--</div>-->
    </div>
    <div class="time-buckets-wrap">
        <!--<div class='time-buckets t-b-close'>-->
        <div class='time-buckets'>
            <!--注入时间段模版-->
        </div>
    </div>
</script>

<!-- timeBucket template 时间段模版 -->
<script type="text/template" id="timeBucket_template">
        <div class="t-b-main {%= open%}">
            <div class="t-b-time">
                <span class="_timeTip _timeStart">{%= startTime %}</span>-<span class="_timeTip _timeEnd">{%= endTime %}</span>
            </div>
            <div class="t-b-window">
                <!--<div class="good-prev"></div>-->
                <div class="good-list-wrap">
                    <div class="good-reorder">
                        <ul class="good-list">
                            {% for ( var i = 0 ; i < 1 ; i++ ){ %}
                            <li class="good good-off">
                                <!--<div class="good-icon-w">-->
                                    <!--<i class="icon"></i>-->
                                <!--</div>-->
                                <p class="_text">服务</p>
                            </li>
                            {% }; %}
                        </ul>
                        <span class="good-title"><strong class="_t">已售服务</strong><span class="_i">X</span><span class="_m">{%= orders.length %}</span>
</span>
                    </div>
                    <div class="good-total">
                        <ul class="good-list">
                            {% for ( var i = 0 ; i < (maxOrder - orders.length < 5 ? maxOrder - orders.length : 5) ; i++ ){ %}
                            <li class="good good-on">
                                <!--<div class="good-icon-w">-->
                                    <!--<i class="icon"></i>-->
                                <!--</div>-->
                                <p class="_text">服务</p>
                            </li>
                            {% }; %}
                        </ul>
                        <span class="good-title"><strong class="_t">可售服务</strong><span class="_i">X</span><span class="_m">{%= maxOrder - orders.length %}</span></span>
                    </div>
                </div>
                <!--<div class="good-next"></div>-->
            </div>
        </div>
        <div class="t-b-ctrl">
            <div class="t-b-add">
                <input class="_num multiNum" type="number" value="1" title="填写修改量"/>
                <input class="_btn multiAdd" type="button" value="补货"/>
            </div>
            <div class="t-b-num">
                <span class="_s">服务总量</span><span class="_i">X</span><span class="_n">{%= maxOrder %}</span>
            </div>
            <div class="t-b-switch">
                <div class="round-checkbox v-m">
                    <input type="checkbox" data-role="open" {% if ( open === "Y" ){ %} checked {% } %} id="{%= this.cid %}"  />
                    <label {%= "for=" + this.cid %} ></label>
                    <em></em>
                </div>
                <div class="l-h20">该时段服务开关</div>
            </div>
        </div>
        <!--<div class="t-b-edit">-->
            <!--<div class="edit-t">-->
                <!--<input class="multiDelete" type="button" value="下架货品"/>-->
                <!--<input class="multiNum" type="number" value="1" title="填写修改量"/>-->
                <!--<input class="multiAdd" type="button" value="上架货品"/>-->
            <!--</div>-->
            <!--<div class="edit-b">-->
                <!--<span class="l-h20">（填写要添加/删除的服务货品数量）</span>-->
            <!--</div>-->
        <!--</div>-->
</script>

<!-- 历史时间段模板 -->
<script type="text/template" id="hisWorkTimeTmp">
        <div class="t-b-top">
            <div class="t-b-time">
                <p class="t-b-timeStart">{%= startTime %}</p>
                <p class="t-b-timeEnd">{%= endTime %}</p>
            </div>
            <div class="t-b-switch">
                <div class="round-checkbox v-m">
                    <input type="checkbox" disabled {% if ( open === "Y" ){ %} checked {% } %} id="{%= this.cid %}" />
                    <label {%= "for=" + this.cid %} ></label>
                    <em></em>
                </div>
                <div class="l-h20">该时段服务开关</div>

            </div>
            <div class="t-b-num">
                <span class="t-b-num-n">{%= orders.length %}/{%= maxOrder %}</span><span>可售</span>
            </div>
            <div class="t-b-window">
                <div class="good-prev"><<</div>
                <div class="good-list-wrap wt-wrap">
                    <ul class="good-list">

                        {% _.each(orders, function(order){ %}
                        <li class="good good-off">
                            <div class="good-icon-w">
                                <i class="icon"></i>
                            </div>
                        </li>
                        {% });%}

                        {% for ( var i = 0 ; i < (maxOrder - orders.length) ; i++ ){ %}
                        <li class="good good-on">
                            <div class="good-icon-w">
                                <i class="icon"></i>
                            </div>
                        </li>
                        {% }; %}
                    </ul>
                </div>
                <div class="good-next"> >> </div>
            </div>
        </div>
        <div class="t-b-edit">
            <div class="edit-t">
                <input class="multiDelete" type="button" value="下架货品"/>
                <input class="multiNum" type="number" value="1" title="填写修改量"/>
                <input class="multiAdd" type="button" value="上架货品"/>
            </div>
            <div class="edit-b">
                <span class="l-h20">（填写要添加/删除的服务货品数量）</span>
            </div>
        </div>
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server" >
    <script src="/js/libs/json2.js"></script>
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/libs/backbone.js"></script>
    <script src="/js/core/backbone.customApi.js?v=1.0.0"></script>
    <script src="/js/core/interfaceAdapter.js?v=1.0.0"></script>
    <!--测试时的API接口-->
    <!--<script src="/js/mock/backbone.customAPI.test.js"></script>-->
    <!--<script src="/js/mock/mock.js"></script>-->
    <!--<script src="/js/mock/mock.shelf.js"></script>-->
    <!--<script src="/js/mock/mock.workTimeSet.js"></script>-->
    <script src="/js/core/event.js?v=1.0.0"></script>
    <script src="/js/apps/shelf.js?v=1.0.0"></script>
</asp:Content>