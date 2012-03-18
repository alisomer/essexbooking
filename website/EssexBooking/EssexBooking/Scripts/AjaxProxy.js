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

function RemoveBookingFromCart(booking_id, callback) {
    $.ajax({
        data: { booking_id: booking_id },
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

function AddExtraToBooking(booking_id, extra_id, number, extra_date, callback) {
    $.ajax({
        data: { 'booking_id': booking_id, 'extra_id': extra_id, 'number': number, 'extra_date': extra_date},
            url: '/Ajax/AddExtraToBooking',
            type: "POST",
            success: callback
        });
    }

    function setGuests(booking_id, guests, callback) {
        $.ajax({
            data: { 'booking_id': booking_id, 'guests': guests },
            url: '/Ajax/SetGuests',
            type: "POST",
            success: callback
        });
    }

    function setRooms(booking_id, guests, callback) {
        $.ajax({
            data: { 'booking_id': booking_id, 'guests': guests },
            url: '/Ajax/SetRooms',
            type: "POST",
            success: callback
        });
    }

    function setDoubles(booking_id, rooms, guests, callback) {
        $.ajax({
            data: { 'booking_id': booking_id, 'rooms': rooms,  'guests': guests},
            url: '/Ajax/SetDoubles',
            type: "POST",
            success: callback
        });
    }
