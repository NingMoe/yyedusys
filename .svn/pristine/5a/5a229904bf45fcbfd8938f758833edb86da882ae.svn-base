$(function() {
    var phoneMins= 60, phoneTimer;
    var btnSubmit = $('.btn-blue');
    btnSubmit.click(function(e) {
        e.preventDefault();
        if(btnSubmit.data('sending')){
            return;
        }
        var isPassed = true,
            data = {};
        $('input').each(function() {
            var val = $.trim(this.value);
            if(!checkItem(this)){
                isPassed = false;
            }else{
                hideError(this);
            }
            data[this.name] = val;
        });
        if (isPassed) {
            btnSubmit.data('sending', true);
            $.ajax({
                url: 'aj/sendCode.json',
                data: data,
                success: function(data) {
                    //成功
                    if (data.code == 10000) {
                        //TODO 跳转页面
                    } else {
                        alert(data.msg);
                    }
                    btnSubmit.data('sending', false);
                },
                failure: function() {
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
    function checkItem(item) {
        var val = $.trim(item.value);

        if(val.length == 0){
            showError($(item).data('name')+'不能为空', item)
            return false;
        }        
        switch (item.name) {
            //身份证号码
            case 'phone':
                if (!/^1\d{10}$/.test(val)) {
                    showError('手机号码格式不对', item)
                    return false;
                }
                break;
            case 'code':
                if (!/^[a-z,A-Z,0-9]{4}$/.test(val)) {
                    showError('验证码格式不对', item)
                    return false;
                }
                break;
        }
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
    var btnPhone = $('.sendcode');
    var iptPhone = $('.form-phone input');
    btnPhone.on('click', function(e) {
        e.preventDefault();
        if (btnPhone.attr('disabled')) {
            return;
        }
        if (checkItem(iptPhone[0])) {
            $.ajax({
                type: 'get',
                url: 'aj/sendCode.json',
                data: {
                    phone: iptPhone.val()
                },
                dataType: 'json',
                success: function(data) {
                    if (data.code == 10000) {
                        phoneMins = 60;
                        phoneTimFn();
                        $('.submitForm, .inputCode').show();
                        sendedPhone = iptPhone.val();
                        return;
                    }
                    //1分钟内已发送过，刷新了页面，又重新点了发送
                    if (data.data.num) {
                        phoneMins = data.data.num;
                        phoneTimFn();
                        $('.submitForm, .inputCode').show();
                        sendedPhone = iptPhone.val();
                    }
                }
            });
        }
    });
    function phoneTimFn() {
        if (--phoneMins == 0) {
            clearTimeout(phoneTimer);
            btnPhone.removeAttr('disabled');
            btnPhone.html('重新发送验证码');
            return;
        }
        btnPhone.attr('disabled', 'disabled');
        btnPhone.html('发送成功，' + phoneMins + '秒后重新发送');
        phoneTimer = setTimeout(function() {
            phoneTimFn();
        }, 1000);
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
})