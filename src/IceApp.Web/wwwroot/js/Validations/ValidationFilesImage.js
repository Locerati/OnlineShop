$("#ImagePth").change(function () {
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alert("Only formats are allowed : " + fileExtension.join(', '));

        $("#ImagePth")[0].value = "";
    }
});