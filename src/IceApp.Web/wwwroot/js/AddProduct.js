$(".addProduct").one('click', function (e) {
    e.preventDefault();
    $.post('/Basket/AddProduct', { 'ProductId': $(this).siblings(".addInfo").attr('data-ProductId'), 'Quantity': $(".quantity_inner input").val() });
    this.textContent = "Перейти в корзину";
    $(this).attr("href", "/Basket/Index");
    $(this).removeClass("btn-danger text-white");
    $(this).addClass("btn-outline-danger");

});
$(document).ajaxError(function (event, jqxhr, settings) {
    if (jqxhr.status == 401) {
        window.location.replace("/Account/Login");
    }
});
