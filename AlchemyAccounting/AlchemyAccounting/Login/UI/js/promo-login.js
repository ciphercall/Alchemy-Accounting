$(document).ready(function() {
    $("#promo-slider #promo-slider-images").owlCarousel({
        jsonPath: 'http://alchemy-bd.com/portfolio-api/?category=website',
        jsonSuccess: customDataSuccess,
        navigation: true, // Show next and prev buttons
        itemsMobile: [479, 1],
        slideSpeed: 100,
        rewindSpeed: 1000,
        paginationSpeed: 400,
        singleItem: false,
        navigation: false,
        pagination: false,
        //Autoplay
        autoPlay: true,
        // stopOnHover: true,
        //Auto height
        autoHeight: false,
        // "singleItem:true" is a shortcut for:
        items: 1,
        // itemsDesktop : false,
        // itemsDesktopSmall : false,
        itemsTablet: false,
        itemsMobile: false
    });

    function customDataSuccess(data) {
            var content = "";
            for (var i in data) {
                var img = data[i].image;
                var name = data[i].name;
                content += "<img src=\"" + img + "\" alt=\"" + name + "\" title=" + name + " data-toggle=\"tooltip\" data-placement=\"top\">"
            }
            $("#promo-slider-images").html(content);
        }
});
