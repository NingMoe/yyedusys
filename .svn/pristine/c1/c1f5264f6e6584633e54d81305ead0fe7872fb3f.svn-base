
$(document).ready(function () {

    LoadCoach();


});

function LoadCoach() {
    var CurrentDate = "";
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetWages/",
        dataType: "json",
        async: false,
        data: { CoachID: $("#hdCoachID").val()},
        beforeSend: function () {
        },
        success: function (data) {

          
            var dataCu = data;
            //取得教练
            $.each(dataCu, function (i, c) {
          
                str += '  <li>';

                str += '   <div class="am-g">';
                str += '   <p class="_PaymentName am-u-sm-9 am-u-md-9 am-u-lg-9">' + c.VenueName + '</p>';
                str += '    <span class="am-u-sm-3 am-u-md-3 am-u-lg-3 _payBiao">发放日期：' + c.PayTime + '</span>';
                str += '  </div>';
                str += '  <div class="am-g"><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">月课时数：<span>' + c.Number + '次</span></p><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">金额：' + c.Price + '<span></span></p></div>';
                str += '  <div class="am-g"><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">工资月份：<span>' + c.WorkDate.substring(1,7) + '</span></p><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">备注：'+c.Remark+'<span></span></p></div>';
                str += '   </li>';

            });

            //if (str == "") {
            //    $('#btnMore a').text("场馆正在入住教练");
            //}
            alert(str);
            $("._Payment").html(str);
        }
    });
}