function SetPageTitle(title) {
    document.title = title;
}

function UpdateInputFile(filename) {
    var $label = $('.input-file').next('.btn-file');
    $label.addClass('has-file').find('.js-filename').html(filename);
}

function ResetInputFile(message) {
    var $label = $('.input-file').next('.btn-file');
    $label.removeClass('has-file').find('.js-filename').html(message);
}