

$(function () {
    $('#doc-confirm-toggle').
      on('click', function () {
          $('#my-confirm').modal({
              relatedTarget: this,
              onConfirm: function (options) {
                  var $link = $(this.relatedTarget).prev('a');
                  $.ajax({
                      type: "get",
                      url: "http://localhost:53262/api/Student/ApplyLeave/",
                      dataType: "json",
                      async: false,
                      data: {State:6, cID: $("#hdCID").val() },
                      //beforeSend: function () {
                      //},
                      success: function (data, status) {
                          if (status == "success") {
                              alert("请假申请提交成功");
                              console.log('ok');
                          }
                          else { alert("请假申请提交失败，请重新操作"); }
                      }
                  });

              },
              // closeOnConfirm: false,
              onCancel: function () {
                  
              }
          });
      });


    $("#btndp").on('click', function () {
        location.href = "MyEvaluate/?pkid="+$("#hdID").val()+"&sid="+$("#hdSID").val()+"&cid="+$("#hdCID").val();
    });
});


$(document).ready(function () {
    $("#btndp").hide();
    $("#doc-confirm-toggle").hide();
    GetInfo();
});


function GetInfo()
{
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Student/GetStudentCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {

            var c = data[0];
            $("#spJL").text(c.FullName);
            $("#spTitle").text(c.Title);
            var State = c.KSstate;
          
            if (State == 1)
            {
                $("#btndp").show();
            }

            if (State ==0) {
                $("#doc-confirm-toggle").show();
            }
        }
    });
}

