    $(function () {
        var useTickBox = $('.useTickBox');
        var iptCode = $('.ipt-ticket');
        var error = $('.useTickBox .error');
        $('.use-ticket').click(function(){
            var rad = $('.iradio', this).toggleClass('iradio-checked');
            useTickBox.toggle(rad.hasClass('iradio-checked'));
        });
        // 验证优惠券
        $('.useTicket').click(function(e){
            e.preventDefault();
            var val = $.trim(iptCode.val());
            var self = $(this);
            if(val.length == 0){
                error.html('请输入优惠码').show();
                return;
            }
            error.hide();
            if(self.data('sending')){return;}
            self.data('sending', true);

            $.ajax({
                url:'aj/useticket.json',
                data:{
                    code:val
                },
                success:function(res){
                    if(res.code == 10000){
                        $('.cost-price').html('<em>￥</em>'+res.data.price)
                        return
                    }
                    else{
                        error.html(res.msg).show();
                    }
                    self.data('sending', false);
                }
            })
        });
        $('.submit').click(function(e){
            e.preventDefault();
            var self = $(this);
            var data = {};
            if(useTickBox.css('display') != 'none'){
                if(error.css('display') != 'none'){
                    return;
                }
                var val = $.trim(iptCode.val());
                if(val.length == 0){
                    error.html('请输入优惠码').show();
                    return;
                }
                error.hide();
                data['code'] = val;
                data['usecode'] = 1;
            }
            if(self.data('sending')){return;}
            self.data('sending', true);
            $.ajax({
                url:'aj/submit.json',
                data:data,
                success:function(res){
                    if(res.code == 10000){
                        // TODO
                        // 跳转到对应页面
                        alert('success')
                    }
                    self.data('sending', false);
                }
            })
        });
    });