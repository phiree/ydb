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
        <div class="day-info"><span>{%= date %}</span><span class="m-l10">今日剩余&nbsp;{%= reOrder %}/{%= maxOrder %}</span></div>
        <div class="day-control">
            <div class="day-control-item">
                <span>服务编辑开关:</span>
                <div class="round-checkbox">
                    <input type="checkbox" class="day_edit" {%= "id=" + this.cid + "_dayEdit" %} />
                    <label {%= "for=" + this.cid + "_dayEdit" %} ></label>
                    <em></em>
                </div>
            </div>
            <div class="day-control-item">
                <span >当日服务开关:</span>
                <div class="round-checkbox">
                    <input type="checkbox" class="day_enable" checked value="dayEdit" {%= "id=" + this.cid + "_dayEnable" %} />
                    <label {%= "for=" + this.cid + "_dayEnable" %} ></label>
                    <em></em>
                </div>
            </div>
        </div>
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
        <div class="t-b-top">
            <div class="t-b-time">
                <p class="t-b-timeStart">{%= startTime %}</p>
                <p class="t-b-timeEnd">{%= endTime %}</p>
            </div>
            <div class="t-b-switch">
                <div class="round-checkbox v-m">
                    <input type="checkbox" data-role="open" {% if ( open === "Y" ){ %} checked {% } %} id="{%= this.cid %}"  />
                    <label {%= "for=" + this.cid %} ></label>
                    <em></em>
                </div>
                <div class="l-h20">该时段服务开关</div>
            </div>
            <div class="t-b-num">
                <span class="t-b-num-n">{%= orders.length %}/{%= maxOrder %}</span><span>剩余</span>
            </div>
            <div class="t-b-window">
                <div class="good-prev"><</div>
                <div class="good-list-wrap">
                    <ul class="good-list">
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
                    </ul>
                </div>
                <div class="good-next"> > </div>
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
                <span class="t-b-num-n">{%= orders.length %}/{%= maxOrder %}</span><span>剩余</span>
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
    <script src="/js/interfaceAdapter.js"></script>
    <script src="/js/libs/json2.js"></script>
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/libs/backbone.js"></script>
    <script src="/js/libs/backbone.customAPI.js"></script>
    <script src="/js/test/mock.js"></script>
    <script src="/js/test/mock.shelf.js"></script>
    <script src="/js/test/mock.workTimeSet.js"></script>
    <script src="/js/event.js"></script>
    <script src="/js/shelf.js"></script>
    <!--<script src="/js/shelf/MockData.js"></script>-->
    <!--<script src="/js/shelf/goods.js"></script>-->
</asp:Content>