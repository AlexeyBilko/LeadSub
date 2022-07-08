let avatar=document.getElementById('uploadAvatar')
let mainImage = document.getElementById('uploadMainImage')
avatar.onchange = readURL;
mainImage.onchange = readURL;

function readURL(input) {
    if (input.files && input.files[0]) {

        console.log(input)
        var reader = new FileReader();
        reader.onload = function (e) {
            $(`#${input.id}-image-upload-wrap`).hide();

            $(`#${input.id}-file-upload-image`).attr('src', e.target.result);
            $(`#${input.id}-file-upload-content`).show();

            $(`#${input.id}-image-title`).html(input.files[0].name);
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        removeUpload();
    }
}

function removeUploadAvatar() {
    $('#avatar-file-upload-input').replaceWith($('.file-upload-input').clone());
    $('#avatar-file-upload-content').hide();
    $('#avatar-image-upload-wrap').show();
}
function removeUploadMainImage() {
    $('#mainImage-file-upload-input').replaceWith($('.file-upload-input').clone());
    $('#mainImage-file-upload-content').hide();
    $('#mainImage-image-upload-wrap').show();
}



$('.image-upload-wrap').bind('dragover', function () {
    $('.image-upload-wrap').addClass('image-dropping');
});
$('.image-upload-wrap').bind('dragleave', function () {
    $('.image-upload-wrap').removeClass('image-dropping');
});