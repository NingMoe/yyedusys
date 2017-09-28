
jQuery.support.cors = true;

function Save(url,furl,VenueID)
{
    var PostUrl = "http://localhost:53262/api/Student/Create/";
  

    parm = { UserName: $("#txtMobile").val(), Pwd: $("#txtMobile").val(), NickName: $("#txtNickName").val(), FullName: $("#txtFullName").val(), NickName: $("#txtTitle").val(), Mobile: $("#txtMobile").val(), Address: $("#txtAddress").val(), ParentFullName: $("#txtParentFullName").val(), ParentMobile: $("#txtParentMobile").val(), HeadUrl: $("#hdHeadUrl").val(), VenueID: VenueID, BirthDate: $("#txtBirthDate").val(), OpenID: $("#hdOpenID").val() };
   

    $.ajax({
        type: "POST",
        url: PostUrl,
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('添加成功');                 
                }
                else { alert('添加失败，再来一次吧'); }
            }
        },
        error: function (e) {
            alert('添加出错了，再来一次吧');
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

    if ($("#txtMobile").val() == "") {
        alert('手机号不能为空');
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

   
        if ($("#txtParentFullName").val() == "") {
            alert('家长姓名不能为空');
            $("#txtParentFullName").focus();
            return false;
        }
        if ($("#txtParentMobile").val() == "") {
            alert('家长手机号不能为空');
            $("#txtParentMobile").focus();
            return false;
        }
   
    if ($("#txtCode").val() == "") {
        alert('场馆（学校）代码不能为空');
        $("#txtCode").focus();
        return false;
    }
    return IsExisitVCode();
});


function IsExisitVCode()
{
    var PostUrl = "http://localhost:53262/api/Student/IsExisitVenueByVCode/";
   
    $.ajax({
        type: "get",
        url: PostUrl,
        contentType: "application/json",
        data: { VenueCode: $("#txtCode").val() },
        success: function (data) {
         
            var c = data[0];
            if (c != null && c.VenueName != "") {
                Save('', '', c.VenueID);
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