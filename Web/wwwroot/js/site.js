function SetPageTitle(title) {
    document.title = title;
}

function UpdateInputFile(id, filename) {
    $('#'.concat(id)).addClass('has-file').find('.js-filename').html(filename);
}

function ResetInputFile(id, message) {
    $('#'.concat(id)).removeClass('has-file').find('.js-filename').html(message);
}