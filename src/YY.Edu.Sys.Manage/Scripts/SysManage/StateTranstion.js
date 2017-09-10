﻿function transtionTeachingSchePKType(data) {

    if (data == 1) {
        return '<span class="btn btn-xs btn-info">一对一</span>';
    } else {
        return '<span class="btn btn-xs btn-info">一对多</span>';
    }

}

function transtionTeachingScheState(data) {
    console.log(data);
    if (data == 0) {
        return '<span class="btn btn-xs btn-info">排课完成</span>';
    } else if (data == 1) {
        return '<span class="btn btn-xs btn-warning">约课完成</span>';
    } else if (data == 2) {
        return '<span class="btn btn-xs btn-danger">学校停课</span>';
    } else if (data == 3) {
        return '<span class="btn btn-xs btn-danger">请假申请</span>';
    } else if (data == 4) {
        return '<span class="btn btn-xs btn-danger">请假批准</span>';
    } else if (data == 5) {
        return '<span class="btn btn-xs btn-success">上课中</span>';
    } else if (data == 6) {
        return '<span class="btn btn-xs btn-success">上课完成</span>';
    } else {
        return '<span class="btn btn-xs btn-danger">其他</span>';
    }
}


function transtionTeachingScheStateHandle(state, pkId, venueId) {

    var edithtml = '<a href="/TeachingSchedule/Details/' + pkId + '" class="btn btn-xs btn-success"><i class="fa fa-edit"> 查看 </i></a>&nbsp;&nbsp;';
    if (state == 0) {
        edithtml += '<button class="btn btn-xs btn-danger" onclick="return change_state(' + "'" + 'StopTeachingSche' + "'," + pkId + ',' + venueId + ');"><i class="fa fa-edit"> 停课 </i></button>&nbsp;&nbsp;';
        return edithtml;
    } else if (state == 1) {
        edithtml += '<button class="btn btn-xs btn-danger" onclick="return change_state(' + "'" + 'StopTeachingSche' + "'," + pkId + ',' + venueId + ');"><i class="fa fa-edit"> 停课 </i></button>&nbsp;&nbsp;';
        return edithtml;
    } else if (state == 2) {
        return edithtml;
    } else if (state == 3) {
        edithtml += '<button class="btn btn-xs btn-danger" onclick="return change_state(' + "'" + 'AgreeLeave' + "'," + pkId + ',' + venueId + ');"><i class="fa fa-edit"> 同意请假 </i></button>&nbsp;&nbsp;';
        return edithtml;
    } else if (state == 4) {
        return edithtml;
    } else if (state == 5) {
        edithtml += '<button class="btn btn-xs btn-danger" onclick="return change_state(' + "'" + 'Done' + "'," + pkId + ',' + venueId + ');"><i class="fa fa-edit"> 上课完成 </i></button>&nbsp;&nbsp;';
        return edithtml;
    } else if (state == 6) {
        return edithtml;
    } else {
        return edithtml;
    }

}


function transtionVenueNoticeRange(data) {
    if (data == 1) {
        return '<span class="btn btn-xs btn-info">场馆</span>';
    } else if (data == 2) {
        return '<span class="btn btn-xs btn-info">教练</span>';
    }else if (data == 3) {
        return '<span class="btn btn-xs btn-info">场馆教练</span>';
    }else if (data == 4) {
        return '<span class="btn btn-xs btn-info">学生</span>';
    }else if (data == 5) {
        return '<span class="btn btn-xs btn-info">场馆学生</span>';
    }else if (data == 6) {
        return '<span class="btn btn-xs btn-info">教练学生</span>';
    }else if (data == 7) {
        return '<span class="btn btn-xs btn-info">全部</span>';
    }else{
        return '<span class="btn btn-xs btn-info">全部</span>';
    }

}