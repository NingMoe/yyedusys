(function() {
    $.dialog = function(opts) {
        if ($.dialog.ele) {
            return;
        }
        var html = '<div class="mask">' +
            '<div class="dialog-box ' + (opts.class || '') + '">' +
            '<div class="title">' +
            '<img src="img/ilogo.png" alt="">' +
            '</div>';
        if (opts.title) {
            html += '<div class="dia-title">' + opts.title + '</div>';
        }
        if (opts.content) {
            html += '<div class="dia-ctx">' + opts.content + '</div>'
        }
        if (opts.btns) {
            html += '<div class="btns ' + (opts.btns.yes && opts.btns.no ? ' btns2' : '') + '">';
            if (opts.btns.no) {
                html += '<a href="#" class="btn btn-gray btn-rc">' + opts.btns.no + '</a>';
            }
            if (opts.btns.yes) {
                html += '<a href="#" class="btn btn-blue btn-rc">' + opts.btns.yes + '</a>';
            }
            html += '</div>';
        }
        html += '</div></div>';
        var html = $(html);

        if (opts.on && opts.on.yes) {
            html.find('.btn-blue').on('click', opts.on.yes)
        }
        if (opts.on && opts.on.no) {
            html.find('.btn-gray').on('click', opts.on.no)
        }
        if (opts.maskClose) {
            html.on('click', function(e) {
                if ($(e.target).hasClass('mask')) {
                    html.remove();
                }
            })
        }
        $(document.body).append(html);
        $.dialog.ele = html;
        if (opts.delay) {
            setTimeout(function() {
                html.remove();
            }, opts.delay);
        }
    };
    $.dialog.close = function() {
        $.dialog.ele.remove();
        $.dialog.ele = null
    }
}());