
$(function () {
    var venueid = $("#VenueID").val();

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/venueinfo/get/" + venueid,
        data: {},
        success: function (data, status) {
            if (status == "success") {
                console.log(data);
                if (data.Info == null) {
                    $("#IsEdit").val(data.Info ? 1 : 0);
                } else {
                    $("#Introduce").val(data.Info.Introduce);
                    $("#BusinessTime").val(data.Info.BusinessTime);
                    $("#VInfoID").val(data.Info.VInfoID);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });

});

function save() {

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/venueinfo/save",
        contentType: "application/json",
        data: JSON.stringify($('#venueinfofrom').serializeJSON()),
        success: function (data, status) {
            if (status == "success") {
                alert("ok");
                console.log('ok');
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });

}