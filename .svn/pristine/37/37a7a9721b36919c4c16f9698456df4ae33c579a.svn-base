
jQuery.support.cors = true;
function LoadCoach() {


    var CurrentDate = "";
    
      
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachListByHourClass/",
        dataType: "json",
        async: false,
        data: {StudentID:1,VenueID: $("#hdVenueID").val()},
        beforeSend: function () {
        },
        success: function (data) {

            var str = "";
            var dataCu = data;          
            //取得教练
            $.each(dataCu, function (i, c) {           
              
                str += " <li data-v='ab'> ";
                str += "  <a href='order_detail.html'>";
               
                str += "  <p class='time'>" + c.FullName + "    </p>";
                str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.Introduce +"</p>";
                str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>剩余课时:  <font color='red'>" + c.hNumber + "</font></p> </a>";
                str += "  <p class='cx1'><i class='iconfont'>&#xe612;</i>购买课时:<input type='text' value='7' id='txtNumber' style='border:1;color:#000000' class='Number'></p>";
             
                    str += "  <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='Buy(" + c.CoachID + ",1,this)' class='order-btn'>购买</button> ";
            
                str += " </li>";

            });
           
            if (str == "") {
                $('#btnMore a').text("场馆正在入住教练");
            }
            $("#list").append(str);

        }
    });
}


$(document).ready(function () {
   
    LoadCoach();
   
    
});


function Buy(coachid, sid,obj)
    {

        var vid = $("#hdVenueID").val();
        var sid = $("#hdStudentID").val();
        
     
        var oj = $(obj).parent().find("input");
        var inumber = 0;
        if (oj.val() != "")
        {
            inumber = oj.val();
        }
       // BuyCurriculum(int StudentID,int CoachID,int number)
        $.ajax({
            type: "get",
            url: "http://localhost:53262/api/Student/BuyCurriculum/",//BuyCurriculum(int StudentID,int CoachID,int number)
            dataType: "json",
            async: false,
            data: { StudentID: sid, CoachID: coachid, number: inumber, VenueID:vid },
            beforeSend: function () {
            },
            success: function (data, status) {
                if (status == "success") {
                    alert("购买课时成功，快去约课吧");
                    console.log('ok');
                }
                else {
                    alert("购买课时失败，再操作一次吧");
                }


            }
        });
      
    }