﻿
jQuery.support.cors = true;


           
function Save(url,furl)
{
    var PostUrl = ApiUrl+"/Coach/Create/";
    var sex = $('#XB input[name="X"]:checked ').val();
    var parm = { FullName: $("#txtFullName").val(), CardPositiveUrl: url, CardReverseUrl: furl, UserName: $("#txtMobile").val(), Pwd: $("#txtMobile").val(), Introduce: "", NickName: $("#txtNickName").val(), HeadUrl: $("#imgHurl").attr("src"), Address: $("#txtAddress").val(), Mobile: $("#txtMobile").val(), Sex: sex, VenueID: $("#hdVenueID").val(), OpenID: $("#hdOpenID").val() };
   

    $.ajax({
        type: "POST",
        url: PostUrl,
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('绑定成功');
                    location.href = '../index';
                }
                else {
                    alert(data.Msg);
                }
            }
        },
        error: function (e) {
            alert('绑定出错了，再来一次吧');
        },
        complete: function () {

        }
    });
}
$('#btnSave').bind('click', function () {

    if ($("#txtFullName").val() == "") {
        alert('姓名不能为空');
        $("#txtFullName").focus();
        return false;
    }
    var phone = $("#txtMobile").val();
    var message = "";
    var flag = false;
    var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
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
        $("#txtMobile").focus();
        return false;
    }

    if ($("#txtAddress").val() == "") {
        alert('住址不能为空');
        $("#txtAddress").focus();
        return false;
    }

    if ($("#txtAddress").val() == "") {
        alert('住址不能为空');
        $("#txtAddress").focus();
        return false;
    }
  
    if ($("#txtCode").val() == "") {
        alert('场馆（学校）代码不能为空');
        $("#txtCode").focus();
        return false;
    }

   return IsExisitVCode();

});

function IsExisitVCode() {
   
    var PostUrl = ApiUrl+"/Coach/IsExisitVenueByVCode/";  
    $.ajax({
        type: "get",
        url: PostUrl,
        contentType: "application/json",
        data: { VenueCode: $("#txtCode").val() },
        success: function (data) {   
            var c = data[0];
            if (c != null && c.VenueName!=null&&c.VenueName != "") {
                $("#hdVenueID").val(c.VenueID);
                Save('', '');
            }
            else {
                alert('场馆代码不存在，快去核实下吧')
            }
        },
        error: function (e) {
            alert('场馆代码不存在，快去核实下吧')
        },
        complete: function () {

        }
    });
}
