function filter(containerClass, searchBox) {
    var searchText = $(searchBox).val();

    $(containerClass).each(function () {
        var materialName = $(this).find(".materialname").text();
        var indexOfSearchText = materialName.search(new RegExp(searchText, "i"));

        if (indexOfSearchText > -1) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}
