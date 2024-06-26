slide: function (event, ui) {
    $('#minAmount').val(ui.values[0]);
    $('#maxAmount').val(ui.values[1]);
    $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
}