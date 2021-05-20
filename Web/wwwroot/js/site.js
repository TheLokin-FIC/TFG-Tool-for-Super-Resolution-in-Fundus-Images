$('.input-file').each(function () {
    var $input = $(this);
    var $label = $input.next('.btn-file');
    var labelValue = $label.html();

    $input.on('change', function (element) {
        if (element.target.value) {
            var filename = element.target.value.split('\\').pop();
            $label.addClass('has-file').find('.js-filename').html(filename);
        } else {
            $label.removeClass('has-file').html(labelValue);
        }
    });
});

$('form').submit(function (evt) {
    if ($('#file').get(0).files.length) {
        $('.field-validation-error').each(function () {
            $(this).css('display', 'none');
        });
        $('.spinner').css('visibility', 'visible');
    }
});