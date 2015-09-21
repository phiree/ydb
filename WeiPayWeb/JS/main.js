$(document).ready(function(){

    var animate1 = function(){ //圆点图标弹出动画
        $('.conAnm1').find('.layer1').stop(true,false).animate(
            {
                bottom: 118 + 'px',
                opacity: 1 ,                     //透明度改变
                filter : 'Alpha(Opacity=100)'   //IE透明度改变
            },
            800,'swing',false); //动画时间为800，动画缓冲为‘swing’效果，false为callback函数
        $('.conAnm1').find('.layer2').stop(true,false).animate(
            {
                bottom: 0+ 'px',
                opacity: 1 ,
                filter : 'Alpha(Opacity=100)'
            },
            800,'swing',false);
        for (var i = 0 ; i < 5 ; i++) {
            switch (i)
            {
                case (0):
                    $('.conAnm1').find('.r').eq(i).stop(true,false).animate(
                        {
                            left: 120 + 'px',
                            top: 106 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        700,'swing',false);
                    break;
                case (1):
//                    alert(3);
                    $('.conAnm1').find('.r').eq(i).stop(true,false).animate(
                        {
                            left: 165 + 'px',
                            top: 0 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        900,'swing',false);
                    break;
                case (2):
                    $('.conAnm1').find('.r').eq(i).stop(true,false).animate(
                        {
                            left: 275 + 'px',
                            top: 60 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        800,'swing',false);
                    break;
                case (3):
                    $('.conAnm1').find('.r').eq(i).stop(true,false).animate(
                        {
                            left: 410 + 'px',
                            top: 35 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        900,'swing',false);
                    break;
                case (4):
                    $('.conAnm1').find('.r').eq(i).stop(true,false).animate(
                        {
                            left: 430 + 'px',
                            top: 155 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        700,'swing',false);
                    break;
                default :
                    false;
            }
        }
    };

    var animate2 = function(){ //
        for (var i = 0 ; i < 5 ; i++) {
            switch (i)
            {
                case (0):
                    $('.conAnm2').find('.lay').eq(i).stop(true,false).animate(
                        {
                            left: -540 + 'px'
                        },
                        800,'swing',false);
                    break;
                case (1):
                    $('.conAnm2').find('.lay').eq(i).stop(true,false).animate(
                        {
                            left: 220 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        800,'swing',false);
                    break;
                case (2):
                    $('.conAnm2').find('.lay').eq(i).stop(true,false).animate(
                        {
                            left: 380 + 'px',
                            opacity: 1 ,
                            filter : 'Alpha(Opacity=100)'
                        },
                        800,'swing',false);
                    break;
                default :
                    false;
            }
        }
    };

    var scrollAct = function(obj,func1,func2){ //obj为动画框元素或其他元素，去底部边界判断为执行动画判断开关，
                                               //func1为达到边界时动作，func2为未到达边界时动作

        var actFlag = false; //动画是否执行过标志

        var judgeScl  = function(a){
            $(window).scroll(function(){
                var scrollBottom = $(document).height() - $(window).height() - $(window).scrollTop(); //滚动条距文档底部的距离
                var offSetBottom = $(document).height() - a.height() - a.offset().top;  //元素距离文档底部的距离

//                if ($(window).scrollTop() > a.offset().top) { //可以改为其顶部为判断边界
                if (scrollBottom < offSetBottom) { //元素完全出现在浏览器窗口内时，开始动画；
                    if (!actFlag) { //通过判断，若动画已执行过就不在执行，反正动画卡顿
                        func1();
                        actFlag = true;
                    }else{return false};
                }else{
                    func2();
                    return false;
                }
            });
        }
        judgeScl(obj);
    }
    scrollAct($('.conTwo'),animate1,function(){});
    scrollAct($('.conThree'),animate2,function(){});
})