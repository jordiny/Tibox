(function (tibox) {

    tibox.order = tibox.order || {};

    tibox.order.getCustomers = function () {
        $.ajax(
        {
            url: '../Customer/Customers',
            type: 'GET',
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            success: function (response) {
                response.forEach(function (item) {
                    $('#CustomerId')
                    .append("<option value='" + item.Id + "'>" + item.FirstName + " " + item.LastName + "</option>")
                }, this);
            },
            error: function (error) {
                alert(error);
            }
        }
            );



    };



    tibox.order.getProducts = function () {
        $.ajax(
        {
            url: '../Product/Products',
            type: 'GET',
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            success: function (response) {
                response.forEach(function (item) {
                    $('#product')
                    .append("<option value='" + item.Id + "'>" + item.ProductName + "</option>");

                    $('#product').change(function () {
                        $('#unitPrice').val(item.UnitPrice);
                    });


                }, this);
            },
            error: function (error) {
                alert(error);
            }
        }
            );



    };


    tibox.order.addOrderItem = function () {

        var $row = $("#contentRow").clone().removeAttr('id');
        $('#product', $row).val($('#product').val());


        $('#addItemButton', $row).addClass('remove')
            .val('Remove')
            .removeClass('btn-success')
            .addClass('btn-danger');


        $('#product,#unitPrice,#quantity', $row).removeAttr('id');

        $('#orderItemList').append($row);

        $('#product').val('0');

        $('#unitPrice,#quantity').val('');


    };


    //Order OrderItems

    tibox.order.save = function () {

        var jsonOrder = {
            orderdate: $("#OrderDate").val(),
            orderNumber: $("#OrderNumber").val(),
            customerId: $("#CustomerId").val(),
            totalAmount: $("#TotalAmount").val()
        };

        var jsonOrderItem = [];


        $('#orderItemList tbody tr').each(function (indice, ele) {
            var OrderItem = {

                ProductId: $("select.product", this).val(),
                UnitPrice: parseFloat($(".unitPrice", this).val()),
                Quantity: parseInt($(".quantity", this).val())

            };
            jsonOrderItem.push(OrderItem);
        });
        var model = {
            Order: {
                orderdate: $("#OrderDate").val(),
                orderNumber: $("#OrderNumber").val(),
                customerId: $("#CustomerId").val(),
                totalAmount: $("#TotalAmount").val()
            },
            OrderItems: jsonOrderItem
        };

        $.ajax({
              url: '../Order/Save',
              type: 'POST',
              data: '{param : model}',
               ContentType : 'application/json',
               
              success: function (response) {
                  window.location.replace("/Order/Index");
              },
              error: function (error) {
                  alert(error);
              }
          });

        console.log(jsonOrder);
        console.log(jsonOrderItem);
    };



    function init() {
        tibox.order.getCustomers();
        tibox.order.getProducts();
        $('#addItemButton').click(tibox.order.addOrderItem);
        $('#Save').click(tibox.order.save);

        $('#orderItemList').on('click', '.remove', function () {
            $(this).parents('tr').remove();
        });
    }

    init();


})(window.tibox = window.tibox || {});