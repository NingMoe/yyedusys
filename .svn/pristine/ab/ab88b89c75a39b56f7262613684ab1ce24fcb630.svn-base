
function LoadInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdID").val() },
        beforeSend: function () {
        },
        success: function (data) {
            var c = data[0];

            str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
            str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
            str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>学员数:" + c.Sucount + "  <font color='red'>已学完</font></p> </a>";
            $(".find-left").html(str);
        }
    });


}

$(document).ready(function () {

    LoadInfo();
    GetStudent();

});

function GetStudent()
{
    
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCurriculumStudentByPKID/",
        dataType: "json",
        async: false,
        data: { pkid: $("#hdID").val() },
        beforeSend: function () {
        },
        success: function (data) {
            $.each(data, function (i, c) {

                var CState = c.CState;
                //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课
                var strState = "上课完成";
                if (CState == 0)
                {
                    strState = "未上课";
                }
                else if (CState == 2) {
                    strState = "请假";
                }
                else if (CState == 4) {
                    strState = "场馆停课";
                }
                str += "<li class='jl'> <p class='address'></p>";
                str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>学员:" + c.FullName + "  <font color='red'>上课完成</font></p> </a>";
              
                if (CState == 0)
                {
                    str += " <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='StudentLeave(" + c.PKID + "," + c.CoashID + "," + c.StudentID + ")' class='order-btn' style='right: 6rem'>代请假</button> ";
                    str += " <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='SignLeave(" + c.PKID + "," + c.CoashID + "," + c.StudentID + ")' class='order-btn'>签到</button> ";
                }
                else if (CState == 1) {
                    str += " <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='Comment(" + c.PKID + "," + c.CoashID + "," + c.StudentID + ")' class='order-btn'>点评</button> ";
                }
                str += "</li>";
            });

           
            $(".order-list").html(str);
        }
    });
}

function Comment(pkid, coachid, cid) {
    location.href = "http://localhost:37396/Coash/MyComment/?pkid=" + pkid + "&coachid=" + coachid + "&cid=" + cid;
}

function StudentLeave(pkid, coachid, studentid)
{
    alert(1);
    //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/UpdateCurriculumState/",
        dataType: "json",
        async: false,
        data: { State:2, StudentIDs:studentid, pkid:pkid },
        beforeSend: function () {
        },
        success: function (data, status) {
            if (status == "success") {
                alert("代学生请假成功");
                console.log('ok');
            }
        }
    });
}

function SignLeave(pkid, coachid, studentid) {
    alert(2);
    //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/UpdateCurriculumState/",
        dataType: "json",
        async: false,
        data: { State: 1, StudentIDs: studentid, pkid: pkid },
        beforeSend: function () {
        },
        success: function (data, status) {
            if (status == "success") {
                alert("签到提交成功");
                console.log('ok');
            }
        }
    });
}

function dateformat(date) {
    var myDate = new Date(date);
    return myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
}