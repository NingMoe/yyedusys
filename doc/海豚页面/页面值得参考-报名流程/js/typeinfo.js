$(function() {
    var btnSubmit = $('.btn-blue');
    btnSubmit.click(function(e) {
        e.preventDefault();
        if(btnSubmit.data('sending')){
            return;
        }
        var isPassed = true,
            data = {};
        $('input').each(function() {
            // /TODO 验证不能为空 
            var val = $.trim(this.value);
            if(!checkItem(this)){
                isPassed = false;
            }else{
                hideError(this);
            }
            data[this.name] = val;
        });
        if (isPassed) {
            //性别
            data['sex'] = $('.sex-item.selected').attr('data-value');
            btnSubmit.data('sending', true);
            $.ajax({
                url: 'aj/typeinfo.json',
                data: data,
                success: function(data) {
                    //成功
                    if (data.code == 1) {
                        //TODO 跳转页面
                    } else {
                        alert(data.msg);
                    }
                    btnSubmit.data('sending', false);
                },
                failure: function() {
                    // alert(data.msg);
                    btnSubmit.data('sending', false);
                }
            })
        }
    });
    $('input:not([data-type=select])').blur(function () {
        if(checkItem(this)){
            hideError(this);
        }
    });
    $('.sex-item').click(function(e){
        e.preventDefault();
        var self = $(this);
        if(self.hasClass('selected')){
            return;
        }
        $('.sex-item.selected').removeClass('selected');
        self.addClass('selected');
    });
    function checkItem(item) {
        var val = $.trim(item.value);

        if(val.length == 0){
            showError($(item).data('name')+'不能为空', item)
            return false;
        }          
        switch (item.name) {
            //身份证号码
            case 'idnumber':
                if (!/^(\d{15}|\d{17}[\dx])$/.test(val)) {
                    showError('身份证格式不对', item)
                    return false;
                }
                break;
            case 'height':
                if (!/^\d{3}(\.\d{1,2})?$/.test(val)) {
                    showError('身高格式不对', item)
                    return false;
                }
                break;
        }
        hideError(item);
        return true;
    }

    function showError(msg, ipt) {
        hideError(ipt);
        $(ipt).parent().append('<div class="error">'+msg+'</div>');
    }
    function hideError(ipt) {
        var parent = $(ipt).parent();
        parent.find('.error').remove();
    }
    //选择户籍
    var sel_select_list = $('.select-list');
    $('.hj').click(function(e){
        $('.hj input').blur();
        e.preventDefault();
        sel_select_list.show();
        $('.mask').show();        
        $('body').addClass('select-list-body');
    });
    $('.select-list li').click(function(e){
        e.preventDefault();
        var ele = $('.hj input');
        ele.val($(this).html())
        checkItem(ele[0]);
        $('body').removeClass('select-list-body');
        sel_select_list.hide();
        $('.mask').hide();        
    });
    $('.birthday input').date({show:true},function(date){
        var ele = $('.birthday input');
        ele.val(date)[0]
        checkItem(ele[0]);
    });
})