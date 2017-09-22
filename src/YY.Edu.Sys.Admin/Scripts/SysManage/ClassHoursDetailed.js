
function bind_data() {

    columns_data = [
        { "data": "DetailedID" },
        { "data": "FullName" },
        { "data": "StudentFullName" },
        { "data": "DNumber" },
        { "data": "CMoney" },
        {
            "data": "PKType",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var edithtml = '<span class="btn btn-xs btn-info">一对一</span>';
                } else {
                    var edithtml = '<span class="btn btn-xs btn-info">一对多</span>';
                }
                return edithtml;
            }
        },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
            }
        },
    ];

    bindTable('classhoursdetailedtable', 'classhoursdetailedfrom', 'api/ClassHoursDetailed/Page4VenueIncome', columns_data, false);
}

