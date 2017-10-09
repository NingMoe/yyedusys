

var container = ''; var pullContainer = ''; var moreContainer = ''; var scrollerContainer = ''; var callBackFunction;
function initIscroll(containerParam, pullContainerParam, moreContainerParam, scrollerContainerParam, callBackFunctionParam) {
   

    var myscroll = new iScroll(container, {
        onScrollMove: function () {
            if (this.y < (this.maxScrollY)) {
                $('#' + pullContainer).addClass('flip');
                $('#' + pullContainer).removeClass('loading');
                $('#' + moreContainer + 'span').text('释放加载...');
            } else {
                $('#' + pullContainer).removeClass('flip loading');
                $('#' + moreContainer + 'span').text('上拉加载...')
            }
        },
        onScrollEnd: function () {
            if ($('#' + pullContainer).hasClass('flip')) {
                $('#' + pullContainer).addClass('loading');
                $('#' + moreContainer + 'span').text('加载中...');
                pullUpAction();
            }
        },
        onRefresh: function () {
            $('#' + moreContainer).removeClass('flip');
            $('#' + moreContainer + 'span').text('上拉加载...');
        }
    });

    function pullUpAction() {
        setTimeout(function () {

            //if ($("#hdPageIndex").val() != "1") {
            //    if (typeof arguments[4] == "function") {
            //        arguments[4].call(this);
            //    }
            //}
            myscroll.refresh();
        }, 200)
    }

    if ($('#' + scrollerContainer).height() < $('#' + container).height()) {
        $('#' + moreContainer).hide();
        myscroll.destroy();
    }


}

