﻿
jQuery.support.cors = true;







//读取图片实例,并上传到服务器
var fileBox = document.getElementById('file');
var fileBox2 = document.getElementById('file2');


function uploadClick() {


  
    var fileList = $('#file')[0].files[0];
    var fileList2= $('#file2')[0].files[0];
   

       
    uploadFile(fileList, fileList2);
        //if (/image\/\w+/.test(file.type))
        //{ }
        //else
        //{ console.log(file.name + ':不是图片'); }
    
}


//关键代码上传到服务器
function uploadFile(file,file2) {
    
    var reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = function () {
        var blob = new Blob([reader.result]);
        //提交到服务器
        var fd = new FormData();
        fd.append('file', blob);
        var ext = file.name.substring(file.name.lastIndexOf('.'));
        fd.append('extention', ext);
        fd.append('maxsize', 1024 * 1024 * 4);//Asp.net 默认一次上传最大4MB
        fd.append('isClip', -1);
        var xhr = new XMLHttpRequest();
        xhr.open('post', 'http://localhost:37396/data/UpLoad.ashx?t=2', true);
        xhr.onreadystatechange = function () {          
            if (xhr.readyState == 4 && xhr.status == 200) {
                var data = eval('(' + xhr.responseText + ')');
                if (data.success == 1) {
                    uploadFileF(file2,data.msg);
                    //上传成功

                } else {
                    alert('上传失败：' + data.msg + ",重新操作下试试吧");
                }


            }
        }
        //开始发送
        xhr.send(fd);
    }
}

function uploadFileF(file,url) {

    var reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = function () {
        var blob = new Blob([reader.result]);
        //提交到服务器
        var fd = new FormData();
        fd.append('file', blob);
        var ext = file.name.substring(file.name.lastIndexOf('.'));
        fd.append('extention', ext);
        fd.append('maxsize', 1024 * 1024 * 4);//Asp.net 默认一次上传最大4MB
        fd.append('isClip', -1);
        var xhr = new XMLHttpRequest();
        xhr.open('post', 'http://localhost:37396/data/UpLoad.ashx?t=2', true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var data = eval('(' + xhr.responseText + ')');
                if (data.success == 1) {
                    Save(url,data.msg);
                    //上传成功

                } else {
                    alert('上传返面失败：' + data.msg + ",重新操作下试试吧");
                }


            }
        }
        //开始发送
        xhr.send(fd);
    }
}
function Save(url,furl)
{
    var PostUrl = "http://localhost:53262/api/Coach/Create/";
   
    var parm = { FullName:$("#txtFullName").val(),CardPositiveUrl:url,CardReverseUrl:furl,UserName:$("#txtMobile").val(),Pwd:$("#txtMobile").val(),Introduce:"",NickName:$("#txtTitle").val(),HeadUrl:$("#hdHeadUrl").val(),Address:$("#txtAddress").val(),Mobile:$("#txtMobile").val(),Sex:1,VenueID:$("#hdVenueID").val(),OpenID:$("#hdOpenID").val() };
   

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
    if ($("#file").val() == "") {
        alert('请上传身份证正面图片');
        $("#file").focus();
        return false;
    }
    if ($("#file2").val() == "") {
        alert('请上传身份证反面图片');
        $("#file2").focus();
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
    var PostUrl = "http://localhost:53262/api/Student/Create/";

    $.ajax({
        type: "POST",
        url: PostUrl,
        contentType: "application/json",
        data: { VenueCode: $("#txtCode").val() },
        success: function (data) {
            var c = data[0];
            if (c != null && c.VenueName != "") {
                uploadClick();
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