/// <reference path="../lib/jquery/jquery-1.11.0.js" />
var $loginForm = $('#login_form').addClass('loaded').submit(function () {
    var $button = $('button').text('登陆中...');
    $('#error_info').fadeOut();
    var data = $loginForm.serializeArray();//.serialize(); //序列化表单元素
    for (var i = 0; i < data.length; i++) {
        if (!data[i].value.trim() && data[i].name != 'redirect') {
            //$('#error_info').html('<strong>错误</strong>：请完整填写表单').fadeIn();
            showError('请完整填写表单');
            $button.text('登入');
            return false;
        }
    }
    $.post($loginForm.attr('action'), data, function (res) {
        if (res.status) {
            window.location.href = res.redirect;
        } else {
            //$('#error_info').html('<strong>错误</strong>：' + res.message).fadeIn();
            showError(res.message);
            $button.text('登入');
        }
    });
    return false;
});

function showError(message) {
    $('#error_info').html('<strong>错误</strong>：' + message).fadeIn();

    //boxLeft = ($(window).width() - $loginForm.width()) / 2;
    $loginForm.css({ 'left': 0, 'position': 'relative' });
    var i = 20;
    var interval = setInterval(function () {
        if (i % 2)
            $loginForm.css('left', i * 30);
        else
            $loginForm.css('left', -i * 30);
        if (i-- < 1)
            clearInterval(interval);
    }, 80);
    return false;
}