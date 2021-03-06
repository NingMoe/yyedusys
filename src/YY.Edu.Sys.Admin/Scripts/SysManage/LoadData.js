﻿
//下拉框加载老师
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
                            programme_sel.push('<option selected value="-1">选择老师</option>');
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

//下拉框加载学生
function showStudent(venueId) {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/Student/GetStudents4Venue?VenueID=" + venueId,
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
                            programme_sel.push('<option selected value="-1">选择学生</option>')
                            programme_sel.push('<option value="' + programme.StudentID + '">' + programme.FullName + '</option>')
                        } else {
                            programme_sel.push('<option value="' + programme.StudentID + '">' + programme.FullName + '</option>')
                        }
                    }
                    $("#StudentID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}

//下拉加载校区
function showCampus(venueId) {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/campus/page?VenueID=" + venueId,
        contentType: 'application/json',
        success: function (data, status) {
            //console.log(data);
            if (status == "success") {

                if (data.data == null) {
                    alert('error');
                } else {
                    var programme_sel = [];

                    for (var i = 0; i < data.data.length; i++) {
                        var programme = data.data[i];
                        if (i == 0) {
                            programme_sel.push('<option selected value="-1">选择校区</option>')
                            programme_sel.push('<option value="' + programme.CampusID + '">' + programme.CampusName + '</option>')
                        } else {
                            programme_sel.push('<option value="' + programme.CampusID + '">' + programme.CampusName + '</option>')
                        }
                    }
                    $("#CampusID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}
