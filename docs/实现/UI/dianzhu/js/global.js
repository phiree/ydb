/**
 * Created by LiChang on 2015/5/15.
 */

(function (){
    var leftCont = document.getElementById('leftCont');
    var rightCont = document.getElementById('rightCont');

    console.log(leftCont.offsetHeight);
    console.log(rightCont.offsetHeight);
    if (rightCont.offsetHeight != 0 ) {
        leftCont.style.height = rightCont.offsetHeight + 'px';
    }
})()


