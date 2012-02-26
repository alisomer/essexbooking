function AddHotelToCart(hotel_id, callback) {
    $.ajax({
        data: { hotel_id: hotel_id },
        url: '/Ajax/AddHotelToCart',
        cache: false,
        success: callback
    });
}
/*
function RemoveHotelFromCart(hotel_id, callback) {
    $.ajax({
        data: { hotel_id: hotel_id },
        url: '/Ajax/RemoveHotelFromCart',
        cache: false,
        success: callback
    });
}
*/

function RemoveBookingFromCart(temp_id, callback) {
    $.ajax({
        data: { temp_id: temp_id },
        url: '/Ajax/RemoveBookingFromCart',
        cache: false,
        success: callback
    });
}

function UpdateBooking(data, callback) {
    $.ajax({
        data: data,
        url: '/Ajax/UpdateBooking',
        type: "POST",
        success: callback,
        dataType:"json"
    });

}
