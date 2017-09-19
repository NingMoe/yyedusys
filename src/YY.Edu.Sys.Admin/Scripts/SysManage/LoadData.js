

function showCoach(venueId) {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/coach/GetCoach4Teaching?VenueID=" + venueId,
        contentType: 'application/json',
        success: function (data, status) {
            if (status == "success") {

                if (data.data == null) {
                    alert('error');
                } else {
                    var programme_sel = [];

                    for (var i = 0; i < data.data.length; i++) {
                        var programme = data.data[i];
                        if (i == 0) {
                            programme_sel.push('<option selected value="-1">全部教练</option>')
                            programme_sel.push('<option value="' + programme.CoachID + '">' + programme.FullName + '</option>')
                        } else {
                            programme_sel.push('<option value="' + programme.CoachID + '">' + programme.FullName + '</option>')
                        }
                    }
                    $("#CoachID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}
