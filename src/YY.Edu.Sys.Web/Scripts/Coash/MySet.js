function LoadInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/Get/",
        dataType: "json",
        async: false,
        data: { id: $("#hdCoachID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {
            var c = data.Info;
            if (c != null)
            {
                $("#imgH").attr("src",  c.HeadUrl);
                $("#spNC").text(c.NickName);
                $("#txtNC").val(c.NickName);
                $("#spName").text(c.FullName);
                $("#spMobile").text(c.Mobile);
                $("#tMobile").val(c.Mobile);
                $("#spAddress").text(c.Address);
                $("#txtAddress").val(c.Address);

                var Sex = c.Sex;
                if (Sex == 1) {
                    $("#spSex").text('男');
                    $("#rN").attr("checked", "checked");
                    $("#rNV").removeAttr("checked");
                }
                else {
                    $("#spSex").text('女');
                    $("#rNV").attr("checked", "checked");
                    $("#rN").removeAttr("checked");
                }
            }

        }
    });
}

$(function () {
    LoadInfo();


    $('#leftMenu').on('click', function () {
   
        $('.slide-wrapper').css('height', 0).removeClass('moved');     
    });
   

    $(function () {
        $('aside.slide-wrapper').on('touchstart', 'li', function (e) {
            $(this).addClass('current').siblings('li').removeClass('current');
        });


        $('#spEdit').on('click', function (e) {

            var wh = $('._otherSet').height();

            $('.slide-wrapper').css('height', wh).addClass('moved');
        });
      
    });

});
$('#SaveMobile').on('click', function () {

    if ($("#txtNC").val() == "")
    {
        $("#txtNC").focus();
        alert("昵称信息不能为空");
        return false;
    } 


    var phone = $("#tMobile").val();
    var flag = false;

    var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0-9]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (phone == '') {
        message = "手机号码不能为空！";

    } else if (phone.length != 11) {
        message = "请输入有效的手机号码！";
    } else if (!myreg.test(phone)) {
        message = "请输入有效的手机号码！";
    } else {
        flag = true;
    }
    if (!flag) {
        alert(message);
        $("#tMobile").focus();
        return false;
    }


    var PostUrl = ApiUrl + "/Coach/UpdateCoach/";
    var sex = $('.am-form-group input[name="x"]:checked').val();

    var parm = { CoachID: $("#hdCoachID").val(), NickName: $("#txtNC").val(), Address: $("#txtAddress").val(), Mobile: $("#tMobile").val(), Sex: sex };


    $.ajax({
        type: "POST",
        url: PostUrl,
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('保存成功');

                    $("#spNC").text($("#txtNC").val()); 
                    $("#spMobile").text($("#tMobile").val());
                   
                    $("#spAddress").text($("#txtAddress").val());                    
                   
                    if (sex == 1) {
                        $("#spSex").text('男');                       
                    }
                    else {
                        $("#spSex").text('女');                      
                    }

                    $('.slide-wrapper').css('height', 0).removeClass('moved');
                }
                else {
                    alert(data.Msg);
                }
            }
        },
        error: function (e) {
            alert('保存成功出错了，再来一次吧');
        },
        complete: function () {

        }
    });
    
});
