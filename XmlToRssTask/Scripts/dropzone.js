$(document).ready(function () {
    var dropZone = $('#dropZone');
    var initContainer = $('#init-container');

    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        initContainer.addClass('hidden');
        return false;
    };

    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        initContainer.removeClass('hidden');
        return false;
    };

    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        upload(event.dataTransfer.files[0]);
    };

    function upload(file) {
        var fd = new FormData();
        fd.append("file", file);

        $.ajax({
            url: '/home/index',
            data: fd,
            type: 'POST',
            processData: false,
            success: function (data) {
                dropZone.html(data);
            },
            error: function (data) {
                dropZone.html(data);
            }
        });
    };
});