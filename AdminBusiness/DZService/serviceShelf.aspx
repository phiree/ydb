<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ServiceShelf.aspx.cs" Inherits="DZService_ServiceShelf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>服务货架 - "ServiceName"</h3>
        </div>
        <div class="content-main">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div id="goodShelf"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
        <div class="day-info"><span>{%= date %}</span><span class="m-l10">今日剩余&nbsp;{%= dayDoneOrder %}/{%= dayMaxOrder %}</span></div>
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
                    <input type="checkbox" class="day_enable" value="dayEdit" {% if ( dayEnable ){ %} checked {% } %} {%= "id=" + this.cid + "_dayEnable" %} />
                    <label {%= "for=" + this.cid + "_dayEnable" %} ></label>
                    <em></em>
                </div>
            </div>
        </div>
    </div>
    <div class="time-buckets-wrap">
        <div class='time-buckets {% if ( !dayEnable ){ %}t-b-close{% } %}'>
            <!--注入时间段模版-->
        </div>
    </div>
</script>

<!-- timeBucket template 时间段模版 -->
<script type="text/template" id="timeBucket_template">
    <div class="t-b-top">
        <div class="t-b-tip"><span>{%= timeStart %}</span>-<span>{%= timeEnd %}</span></div>
        <div class="t-b-switch">
            <div class="round-checkbox v-m">
                <input type="checkbox" {% if ( timeEnable ){ %} checked {% } %} id="{%= this.cid %}" />
                <label {%= "for=" + this.cid %} ></label>
                <em></em>
            </div>
            <div class="l-h20">该时段服务开关</div>
        </div>
        <div class="t-b-num">
            <span class="t-b-num-n">{%= maxNum %}/{%= doneNum %}</span><span>剩余</span>
        </div>
        <div class="t-b-window">
            <div class="good-prev"><<</div>
            <div class="good-list-wrap">
                <ul class="good-list">
                    {% _.each(arrayGoods,function(good){ %}
                    {% if( good.ordered ) { %} <li class="good good-off"> {% } else { %} <li class="good good-on"> {% } %}
                        <div class="good-icon-w">
                            <i class="icon"></i>
                        </div>
                        <p>{% if(good.ordered) { %}已预约{% } else { %}可预约{% } %}</p>

                        {% if( !good.ordered ) { %} <input class="deleteGood" type="button" value="x" /> {% } %}

                    </li>
                    {% }); %}
                </ul>
            </div>
            <div class="good-next"> >> </div>
        </div>
    </div>
    <div class="t-b-edit">
        <div class="edit-t">
            <input class="multiDelete" type="button" value="-"/>
            <input class="multiNum" type="number" value="1" title="填写修改量"/>
            <input class="multiAdd" type="button" value="+"/>
        </div>
        <div class="edit-b">
            <span class="l-h20">添加或删除服务数量</span>
        </div>
    </div>
</script>

<!-- 服务单个商品模版 -->
<script type="text/template" id="goods_template">
    {% _.each(arrayGoods,function(good){ %}
    {% if( good.ordered ) { %} <li class="good good-off"> {% } else { %} <li class="good good-on"> {% } %}
        <div class="good-icon-w">
            <i class="icon"></i>
        </div>
        <p>{% if(good.ordered) { %}已预约{% } else { %}可预约{% } %}</p>

        {% if( !good.ordered ) { %} <input class="deleteGood" type="button" value="x" /> {% } %}

    </li>
    {% }); %}
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server" >
    <!--<script src="js/jquery-1.11.3.js"></script>-->
    <script src="/js/shelf/json2.js"></script>
    <script src="/js/shelf/mock.js"></script>
    <script src="/js/shelf/MockData.js"></script>
    <script src="/js/shelf/underscore.js"></script>
    <script src="/js/shelf/backbone.js"></script>
    <script src="/js/shelf/backbone.customAPI.js"></script>
    <!--<script src="js/backbone.localStorage.js"></script>-->
    <script src="/js/shelf/goods.js"></script>
</asp:Content>