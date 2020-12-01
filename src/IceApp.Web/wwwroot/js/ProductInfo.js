$('#minus').click(function () {
    if ($("#calc").val() > 0)
    $("#calc").val(parseInt($("#calc").val()) - 1);
});
$('#plus').click(function () {
    if ($("#calc").val() < 32767)
    $("#calc").val(parseInt($("#calc").val()) + 1);
});
/*$('#AddComment').click(function () {
    $.post('/Products/AddComment', { 'ProductId': $('#AddComment').data('productid'), 'TextComment': $('#comment_text').val() })
});*/

$(function () {
    $("form[name='comment']").validate({
        rules: {
            commenttext: {
                required: true,
                minlength: 3,
                maxlength:500
                }        
        },
        // Specify validation error messages
        messages: {
            commenttext: {
                required: "Поле не может быть пустым",
                minlength: "Кол-во символов должно быть не меньше 3",
                maxlength: "Максимальное кол-во символов 500"
            }           
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            $.post('/Comments/AddComment', { 'ProductId': $("#ProductId").val(), 'TextComment': $('#comment_text').val() });

            $("#Success_send").show('slow');
            setTimeout(function () { $("#Success_send").hide('slow'); }, 4500);
            form.reset();
        }
    });
});
let countComments;
let currentNumberComments;
$(function () {
    $.get('/Comments/GetCountComments', { 'ProductId': $("#ProductId").val() }, function (data) {
        countComments = data;

        $("h2").last().append(' (' + countComments + ')');
        if (data == 0) {
            $('div.comments_area').append('<h2>Нет комментариев</h2>').css({ 'text-align': 'center', 'color': 'gray' });
            $('div#upload_comments').hide();

        }
        else {
            GetComments(0);
        }
    });
});

async function GetComments(number) {
    
    $.getJSON('/Comments/GetComments', { 'ProductId': $("#ProductId").val(), 'startnumber': number }, function (data) {

        PrintComments(data, number);
    });
    
}
function PrintComments(data,number)
{
    
    for (let i = 0; i < data.length; i++) {

        const div = document.createElement("div");
        div.classList.add("comment_messages");
        const img = document.createElement("img");
        img.setAttribute("id", "PeopleAvatar");
        img.setAttribute("src", "data:image;base64,"+ data[i].image);
        div.append(img);
        const pfirst = document.createElement("p");
        pfirst.setAttribute("id", "PersonName");
        pfirst.append(data[i].personName);
        div.append(pfirst);
        const psecond = document.createElement("p");
        psecond.setAttribute("id", "PersonComment");
        psecond.append(data[i].textComment);
        div.append(psecond);
        $('div.comments_area').append(div);
    }
    currentNumberComments = number + 3;
    if (currentNumberComments >= countComments) {
        $('div#upload_comments').hide();
    }

}
$("div#upload_comments").click(function () {
    GetComments(currentNumberComments);
});




