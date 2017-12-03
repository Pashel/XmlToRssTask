$(document).ready(function () {
    var dropZone = $('#dropZone');

    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };

    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        dropZone.removeClass("error");
        dropZone.removeClass('hover');
        upload(event.dataTransfer.files[0]);
    };

    function upload(file) {
        var fd = new FormData();
        fd.append("file", file);

        $.ajax({
            url: '/home/UploadXml',
            data: fd,
            type: 'POST',
            processData: false,
            contentType: false, 
            success: function (data) {
                dropZone.text(data);
            },
            error: function (error) {
                dropZone.html(error.statusText);
                dropZone.addClass("error");
            }
        });
    };
});