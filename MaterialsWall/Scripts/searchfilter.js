function filter(containerClass, searchBox) {
    var searchText = $(searchBox).val();

    $(containerClass).each(function () {
        var materialName = $(this).find(".materialname").text();
        var indexInName = materialName.search(new RegExp(searchText, "i"));

        if (indexInName > -1) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}
