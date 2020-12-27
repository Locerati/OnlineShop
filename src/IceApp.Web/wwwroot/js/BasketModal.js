
        $("#myModal").on("shown.bs.modal", function () {
            $("#myInput").trigger("focus");
      });

      $(document).ready(function () {
            $('input[type="radio"]').click(function () {
                if ($("input#DeliveryInput").is(":checked") && !$(".address-block").length) {
                    $("div.delivery-check").after('<div class="form-group address-block"> '
                        + '<label for="InputAddress">Адрес доставки</label> '
                        + '<input type="text" class="form-control" id="InputAddress" aria-describedby="addressHelp" name="Address" placeholder="Введите адресс" required />'
                        + '</div>');
                }
                else {
                    if (!$("input#DeliveryInput").is(":checked")) {
                        $(".address-block").remove();
                    }

                }
            });
      });

$(".item-in-basket__deleteLink").click(function () {
    $(".do-order-total-price").text('');
    $("img.UploadImage").css('display', 'inline');
    $(this).parents(".item-in-basket").remove();
    $.get('/Basket/DeleteItem', { 'basketId': $(this).siblings(".BasketId").val() }, function (data) {
        if (data.totalQuentity == 0) {
            $(".basketItems").remove();
            var $header1 = $("<h3 class='text-center text-secondary mt-5 pt-5'>В вашей корзине пока ничего нет</h3>")
            $(".header").after($header1);
        }
        else {

     
            $("img.UploadImage").css("display", "none");
            if (data.discountSum != data.totalSum) {
                $(".do-order-total-price").html("<span><strike>" + data.totalSum + "&nbsp;₽</strike> </span><span class='text-danger'>" + data.discountSum+"&nbsp;₽</span>");

            } else {
                $(".do-order-total-price").text(data.totalSum + " ₽");
            }
            
            $(".OrderTotalPrice strong").text("Сумма к оплате: " + data.discountSum + " ₽");
            $(".basket-right .countProducts span").text("Товары, " + data.totalQuentity + " шт");
        }
        
     });
});