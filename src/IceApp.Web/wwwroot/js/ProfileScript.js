$(document).ready(function () {

    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
                var files = input.files;
                var formData = new FormData();

                    formData.append("files", files[0]);
  
                $.ajax(
                    {
                        url: "Profile/UpdateImage",
                        data: formData,
                        processData: false,
                        contentType: false,
                        type: "POST",
                        success: function (data) {
                            alert("Files Uploaded!");
                        }
                    }
                );

            }
            reader.readAsDataURL(input.files[0]);
          

        }
    }

    $(".file-upload").on('change', function () {
        readURL(this);
    });

    $(".upload-button").on('click', function () {
        $(".file-upload").click();
    });
});