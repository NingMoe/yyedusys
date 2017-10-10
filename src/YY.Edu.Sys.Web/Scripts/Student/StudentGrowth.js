
jQuery.support.cors = true;
function LoadMyCurriculumV() {


    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetStudentGrowth/",
        dataType: "json",
        async: false,
        data: { VenueID: 1, StudentID: $("#hdStudentID").val(), FCType: 2 },
        //beforeSend: function () {
        //},
        success: function (data) {

            var str = "";
            var dataCu = data.data;

            $.each(dataCu, function (i, c) {

           
                    str += "<li> ";
                str += "<div class='am-gallery-item'>";
                str += "<a href='#' class=''> ";
                str += " <video controls='controls' autoplay='autoplay'> ";
                str += "<source src='http://localhost:37396/" + c.FCUrl + "' type='video/ogg' /> <source src='http://localhost:37396/" + c.FCUrl + "' type='video/flv' /></video> ";


                str += "  <h3 class='am-gallery-title'>" + c.Title + "</h3>";
                str += "  <div class='am-gallery-desc'>" + c.AddTime + "</div>";
                str += "    </a></div></li> ";

            });

            alert(str);
            $("#imgList2").html(str);

        }
    });
}

function LoadMyCurriculumImg() {


    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetStudentGrowth/",
        dataType: "json",
        async: false,
        data: { VenueID: 1, StudentID: $("#hdStudentID").val(), FCType: 1 },
        //beforeSend: function () {
        //},
        success: function (data) {
          
            var str = "";
            var dataCu = data.data;

            $.each(dataCu, function (i, c) {
                
                var Ftype = 1;
                if (Ftype == 1) {

                    str += '<li>';
                    str += '<img src="' + c.FCUrl + '" alt="' + c.Title + '" class="am-thumbnail" />';
                    str += ' <p class="textAlign">' + c.Title + '</p>';
                    str += ' <p class="textAlign">' + c.AddTime + '</p></li>';
                }
                else {
                    str += "<li> ";
                    str += "<div class='am-gallery-item'>";
                    str += "<a href='http://s.amazeui.org/media/i/demos/bing-1.jpg' class=''> ";
                    str += " <video controls='controls' autoplay='autoplay'> ";
                    str += "<source src='" + c.FCUrl + "' type='video/ogg' /> </video> ";

                    str += "  <h3 class='am-gallery-title'>" + c.Title + "</h3>";
                    str += "  <div class='am-gallery-desc'>" + c.AddTime + "</div>";
                    str += "    </a></div></li> ";
                }

            });

            $("#imgList").html(str);

        }
    });
}



$(document).ready(function () {

    LoadMyCurriculumImg();
  //  LoadMyCurriculumV();

});



$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);

    var datatype = $(".current").attr("data-type");
    if (datatype == "1") {
        LoadMyCurriculumImg();
    }
    else {
        LoadMyCurriculumV();
    }
    //hdPageSize
});

$('.order li a').bind('click', function () {

    $('.order li a').removeClass("current");
    $(this).addClass("current");
    $("#hdPageIndex").val(1);
    $("#imgList").html('');
    $("#imgList2").html('');
    var datatype = $(".current").attr("data-type");
    if (datatype == "1") {
        LoadMyCurriculumImg();
    }
    else {
        LoadMyCurriculumV();
    }
});



//读取图片实例,并上传到服务器
var fileBox = document.getElementById('file');

function uploadClick() {
    var fileList = fileBox.files;
    for (var i = 0; i < fileList.length; i++) {
        var file = fileList[i];
        //图片类型验证第二种方式
        uploadFile(file);
        //if (/image\/\w+/.test(file.type))
        //{ }
        //else
        //{ console.log(file.name + ':不是图片'); }
    }
}

//关键代码上传到服务器
function uploadFile(file) {

    var reader = new FileReader();
    reader.readAsArrayBuffer(file);
    reader.onload = function () {
        var blob = new Blob([reader.result]);
        //提交到服务器
        var fd = new FormData();
        fd.append('file', blob);
        fd.append('utype', 1);
        var ext = file.name.substring(file.name.lastIndexOf('.'));
        fd.append('extention', ext);
        fd.append('maxsize', 1024 * 1024 * 4);//Asp.net 默认一次上传最大4MB
        fd.append('isClip', -1);
        var xhr = new XMLHttpRequest();
        xhr.open('post', 'http://localhost:37396/data/UpLoad.ashx', true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var data = eval('(' + xhr.responseText + ')');
                if (data.success == 1) {
                    var parm = { Title: $("#txtTitle").val(), FCType: $("input[name='docInlineRadio']:checked").val(), FCUrl: data.msg, StudentID: $("#hdStudentID").val(), FCState: 1, VenueID: $("#selVenue").val() };
                    $.ajax({
                        type: "POST",
                        url: ApiUrl + "/Student/AddStudentGrowth",
                        contentType: "application/json",
                        data: JSON.stringify(parm),
                        success: function (data, status) {
                            if (status == "success") {
                                if (data.Code == 1001) {
                                    alert('添加成功');
                                    $("#doc-modal-1").modal('close');
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

$('#btnSave').bind('click', function () {

    if ($("#txtTitle").val() == "") {
        alert('标题不能为空');
        $("#txtTitle").focus();
        return false;
    }

    if ($("#file").val() == "") {
        alert('请选择要上传的文件');
        $("#file").focus();
        return false;
    }

    uploadClick();
});
