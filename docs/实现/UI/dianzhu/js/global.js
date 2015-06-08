/**
 * Created by LiChang on 2015/5/15.
 */

$(document).ready(function(){

/* 优惠劵管理页面 total-tree样式控制 */
    (function (){
        try {
            var treeItem = $('.tree-item');
            var treeMain = $('.total-tree-main');

            $('.tree-item:even').css({
                'left' : '0'
            });
            $('.tree-item:odd').css({
                'right' : '0'
            });

            for (var i = 0 ; i < treeItem.length ; i++ ) {
                var circleIcon = $(document.createElement('i'));
                circleIcon.addClass('icon');
                circleIcon.addClass('tree-icon-circle');

                if ( (i+1)%2 ? 1 : 0  ) {
                    treeItem.eq(i).css({
                        'top' : (i/2)*( 20 + treeItem.eq(0).height()) + 20 + 'px'
                    })
                    treeItem.eq(i).find($('.tree-icon-tip')).addClass('tree-tipl');
                } else {
                    treeItem.eq(i).css({
                        'top' : ((i - 1)/2)*( 20 + treeItem.eq(0).height() ) + 40 + 'px'
                    })
                    treeItem.eq(i).find($('.tree-icon-tip')).addClass('tree-tipr');
                }
                treeMain.append(circleIcon);
                treeMain.find('.tree-icon-circle').eq(i).css({'top' : treeItem.eq(i).position().top + 10 + 'px'});
            }

            treeMain.height(
                    treeItem.eq(treeItem.length - 1).position().top + treeItem.eq(treeItem.length - 1).height()
            )

            var circleIcon = treeMain.find('.tree-icon-circle');

            circleIcon.hide();
            circleIcon.fadeIn(1000,'swing');

            treeItem.hide();
            treeItem.fadeIn(1000);
        } catch (err) {
            return;
        }
    })();

    (function (){
        var leftCont = document.getElementById('leftCont');
        var rightCont = document.getElementById('rightCont');

        if (rightCont.offsetHeight != 0 ) {
            leftCont.style.height = rightCont.offsetHeight + 'px';
        }
    })();
})




