$("button.DiscountSubmit").click(function () {
    let disc = $(this).parent().siblings("input.DiscountVal").val();
    if (disc > 0 && disc < 16) {
        catId = $(this).parent().siblings("input.CategId").val();
        $.post('/Categories/CreateDiscount', { 'discount': disc, 'categId': catId });
        alert("Скидка назначена");
    } else {
        alert("Неправильно введенное значение скидка должна быть от 1% до 15%");
    }
});
$("button.DiscountReset").click(function () {
    
    catId = $(this).parent().siblings("input.CategId").val();
    alert("Скидка сброшена");
    $.post('/Categories/ResetDiscount', { 'categId': catId });
});