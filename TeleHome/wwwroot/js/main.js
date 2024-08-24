
(function ($) {
    "use strict";

    var windowOn = $(window);
    ////////////////////////////////////////////////////
    // 01. PreLoader Js
    windowOn.on('load', function () {
        $("#loading").fadeOut(500);
    });


    ////////////////////////////////////////////////////
    // 02. Mobile Menu Js
    //$('#mobile-menu').meanmenu({
    //    meanMenuContainer: '.mobile-menu',
    //    meanScreenWidth: "991",
    //    meanExpand: ['<i class="fa-solid fa-chevron-right"></i>'],
    //});

    //$('#mobile-menu-lg').meanmenu({
    //    meanMenuContainer: '.mobile-menu-lg',
    //    meanScreenWidth: "1199",
    //    meanExpand: ['<i class="fa-solid fa-chevron-right"></i>'],
    //});

    ////////////////////////////////////////////////////
    // 03. Common Js

    $("[data-background").each(function () {
        $(this).css("background-image", "url( " + $(this).attr("data-background") + "  )");
    });

    $("[data-width]").each(function () {
        $(this).css("width", $(this).attr("data-width"));
    });

    $("[data-bg-color]").each(function () {
        $(this).css("background-color", $(this).attr("data-bg-color"));
    });

    $("[data-text-color]").each(function () {
        $(this).css("color", $(this).attr("data-text-color"));
    });

    $(".has-img").each(function () {
        var imgSrc = $(this).attr("data-menu-img");
        var img = `<img class="mega-menu-img" src="${imgSrc}" alt="img">`;
        $(this).append(img);

    });


    $('.wp-menu nav > ul > li').slice(-4).addClass('menu-last');

    $('.tp-header-side-menu > nav > ul > li a, .offcanvas__category > nav > ul > li a').each(function (i, v) {
        $(v).contents().eq(2).wrap('<span class="menu-text"/>');
    });


    if ($('.main-menu.menu-style-3 > nav > ul').length > 0) {
        $('.main-menu.menu-style-3 > nav > ul').append(`<li id="marker" class="tp-menu-line"></li>`);
    }

    if ($("#tp-offcanvas-lang-toggle").length > 0) {
        window.addEventListener('click', function (e) {

            if (document.getElementById('tp-offcanvas-lang-toggle').contains(e.target)) {
                $(".tp-lang-list").toggleClass("tp-lang-list-open");
            }
            else {
                $(".tp-lang-list").removeClass("tp-lang-list-open");
            }
        });
    }

    if ($("#tp-offcanvas-currency-toggle").length > 0) {
        window.addEventListener('click', function (e) {

            if (document.getElementById('tp-offcanvas-currency-toggle').contains(e.target)) {
                $(".tp-currency-list").toggleClass("tp-currency-list-open");
            }
            else {
                $(".tp-currency-list").removeClass("tp-currency-list-open");
            }
        });
    }

    // for header language
    if ($("#tp-header-lang-toggle").length > 0) {
        window.addEventListener('click', function (e) {

            if (document.getElementById('tp-header-lang-toggle').contains(e.target)) {
                $(".tp-header-lang ul").toggleClass("tp-lang-list-open");
            }
            else {
                $(".tp-header-lang ul").removeClass("tp-lang-list-open");
            }
        });
    }

    // for header currency
    if ($("#tp-header-currency-toggle").length > 0) {
        window.addEventListener('click', function (e) {

            if (document.getElementById('tp-header-currency-toggle').contains(e.target)) {
                $(".tp-header-currency ul").toggleClass("tp-currency-list-open");
            }
            else {
                $(".tp-header-currency ul").removeClass("tp-currency-list-open");
            }
        });
    }

    // for header setting
    if ($("#tp-header-setting-toggle").length > 0) {
        window.addEventListener('click', function (e) {

            if (document.getElementById('tp-header-setting-toggle').contains(e.target)) {
                $(".tp-header-setting ul").toggleClass("tp-setting-list-open");
            }
            else {
                $(".tp-header-setting ul").removeClass("tp-setting-list-open");
            }
        });
    }

    $('.tp-hamburger-toggle').on('click', function () {
        $('.tp-header-side-menu').slideToggle('tp-header-side-menu');
    });


    ////////////////////////////////////////////////////
    // 04. Menu Controls JS
    //$("body").click(function (e) {
    //    console.log($(e.target).parents('.tp-category-mobile-menu').length);
    //    if ($(e.target).parents('.tp-category-mobile-menu').length == 0) {
    //        $('.tp-offcanvas-category-toggle').siblings().find('nav').slideToggle();
    //    }
    //})
    document.addEventListener("click", e => {
       
    }, true)
    if ($('.tp-category-menu-content').length && $('.tp-category-mobile-menu').length) {
        let navContent = document.querySelector(".tp-category-menu-content").outerHTML;
        let mobileNavContainer = document.querySelector(".tp-category-mobile-menu");
        mobileNavContainer.innerHTML = navContent;

        $('.tp-offcanvas-category-toggle').on('click', function () {
            $(this).siblings().find('nav').slideToggle();
        });


        let arrow = $(".tp-category-mobile-menu .has-dropdown > a");

        arrow.each(function () {
            let self = $(this);
            let arrowBtn = document.createElement("BUTTON");
            arrowBtn.classList.add("dropdown-toggle-btn");
            arrowBtn.innerHTML = "<i class='fa-solid fa-chevron-right'></i>";

            self.append(function () {
                return arrowBtn;
            });

            self.find("button").on("click", function (e) {
                e.preventDefault();
                let self = $(this);
                self.toggleClass("dropdown-opened");
                self.parent().toggleClass("expanded");
                self.parent().parent().addClass("dropdown-opened").siblings().removeClass("dropdown-opened");
                self.parent().parent().children(".tp-submenu").slideToggle();
            });

        });
    }
    
    if ($('.tp-main-menu-content').length && $('.tp-main-menu-mobile').length) {
        let navContent = document.querySelector(".tp-main-menu-content").outerHTML;
        let mobileNavContainer = document.querySelector(".tp-main-menu-mobile");
        mobileNavContainer.innerHTML = navContent;


        let arrow = $(".tp-main-menu-mobile .has-dropdown > a");

        arrow.each(function () {
            let self = $(this);
            let arrowBtn = document.createElement("BUTTON");
            arrowBtn.classList.add("dropdown-toggle-btn");
            arrowBtn.innerHTML = "<i class='fa-solid fa-chevron-right'></i>";

            self.append(function () {
                return arrowBtn;
            });

            self.find("button").on("click", function (e) {
                e.preventDefault();
                let self = $(this);
                self.toggleClass("dropdown-opened");
                self.parent().toggleClass("expanded");
                self.parent().parent().addClass("dropdown-opened").siblings().removeClass("dropdown-opened");
                self.parent().parent().children(".tp-submenu").slideToggle();


            });

        });
    }

    $(".tp-category-menu-toggle").on("click", function (e) {
        e.stopPropagation();
        $(".tp-category-menu > nav > ul").slideToggle();
    });
    $(document).on("click", function (e) {
        if (!$(".tp-category-menu-toggle").is(e.target) && !$(".tp-category-menu-toggle").has(e.target).length) {
            $(".tp-category-menu > nav > ul").slideUp();
        }
    });



    ////////////////////////////////////////////////////
    // 05. Offcanvas Js
    $(".tp-offcanvas-open-btn").on("click", function () {
        $(".offcanvas__area").addClass("offcanvas-opened");
        $(".body-overlay").addClass("opened");
    });
    $(".offcanvas-close-btn").on("click", function () {
        $(".offcanvas__area").removeClass("offcanvas-opened");
        $(".body-overlay").removeClass("opened");
    });

    ////////////////////////////////////////////////////
    // 06. Search Js
    $(".tp-search-open-btn").on("click", function () {
        $(".tp-search-area").addClass("opened");
        $(".body-overlay").addClass("opened");
    });
    $(".tp-search-close-btn").on("click", function () {
        $(".tp-search-area").removeClass("opened");
        $(".body-overlay").removeClass("opened");
    });

    ////////////////////////////////////////////////////
    // 07. cartmini Js
    $(".cartmini-open-btn").on("click", function (e) {
        e.stopPropagation(); 
        $(".cartmini__area").addClass("cartmini-opened");
        $(".body-overlay").addClass("opened");
        // Make an AJAX GET request to the ASP.NET Core endpoint
        $.ajax({
            url: '/main/GetFavorites', // Replace with the actual URL of your endpoint
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                // Handle the successful response here

                var favoritesList = $('#favitmes');
                favoritesList.empty(); // Clear any previous data

                $(data).each(function () {
                    favoritesList.append(
                `<div class="cartmini__widget-item">
                    <div class="cartmini__thumb">
                        <a href="../main/productdetails/${this.FavoriteProductId}">
                            <img src="/sekiller/${this.FavoriteProduct.ProductFirstPhotoUrl}" alt="${this.FavoriteProduct.ProductName}">
                        </a>
                    </div>
                    <div class="cartmini__content">
                        <h5 class="cartmini__title"><a href="../main/productdetails/${this.FavoriteProductId}">${this.FavoriteProduct.ProductName}</a></h5>
                        <div class="cartmini__price-wrapper">
                            <span class="cartmini__price">${this.FavoriteProduct.ProductPrice} ₼</span>
                        </div>
                    </div>
                    <button style="cursor:pointer;" class="cartmini__del" data-prid="${this.FavoriteProductId}" data-favoriteid="${this.FavoriteId}"><i class="fa-solid fa-xmark"></i></button>
                </div>`)
                });

            }, 
            error: function () {
                // Handle any errors that occurred during the request
                console.log('Xəta baş verdi')
            }
        });

    });
    $(document).on("click", function (e) {
        if (!$(".cartmini__area").is(e.target) && $(".cartmini__area").has(e.target).length === 0) {
            // Click occurred outside of .cartmini__area, so close it
            $(".cartmini__area").removeClass("cartmini-opened");
            $(".body-overlay").removeClass("opened");
        }
    });

   ////////////////////////
    var fvrt = document.querySelector('.fvrt');
    // Assuming you have a parent container with the ID "favitmes"
    $(document).on('click', '.cartmini__del', function () {
        //data-productid="1035"
        var row = $(this).parent('div')
        var favoriteId = this.getAttribute("data-favoriteid");
        var prId = this.getAttribute("data-prid");
        //var productLi = $(`li[data-productid='${favoriteId}']`);
        var element = $(`.row li[data-productid='${prId}'] i`); // $('li[data-productid="1035"] i');
        element.removeClass('text-danger');

        // Add the new class (e.g., text-black)
        element.addClass('text-black');
        var xhr = new XMLHttpRequest();
                 xhr.open("POST", "/main/deletefavorite/" + favoriteId, true);
                 xhr.setRequestHeader("Content-Type", "application/json");
                 xhr.onreadystatechange = function () {
                     if (xhr.readyState === 4 && xhr.status === 200) {
                            $(row).remove();
                         fvrt.textContent = parseInt(fvrt.textContent) - 1;
                         console.log("success");
                     }
                 };
                 xhr.send();
    });

   ////////////////
    $(".cartmini-close-btn").on("click", function () {
        $(".cartmini__area").removeClass("cartmini-opened");
        $(".body-overlay").removeClass("opened");
       
    });
   

    ////////////////////////////////////////////////////
    // 08. filter
    $(".filter-open-btn").on("click", function () {
        $(".tp-filter-offcanvas-area").addClass("offcanvas-opened");
        $(".body-overlay").addClass("opened");
    });


    $(".filter-close-btn").on("click", function () {
        $(".tp-filter-offcanvas-area").removeClass("offcanvas-opened");
        $(".body-overlay").removeClass("opened");
    });

    $(".filter-open-dropdown-btn").on("click", function () {
        $(".tp-filter-dropdown-area").toggleClass('filter-dropdown-opened');
    });


    ////////////////////////////////////////////////////
    // 09. Body overlay Js
    $(".body-overlay").on("click", function () {
        $(".offcanvas__area").removeClass("offcanvas-opened");
        $(".tp-search-area").removeClass("opened");
        $(".cartmini__area").removeClass("cartmini-opened");
        $(".tp-filter-offcanvas-area").removeClass("offcanvas-opened");
        $(".body-overlay").removeClass("opened");
    });


    ////////////////////////////////////////////////////
    // 10. Sticky Header Js
    windowOn.on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $("#header-sticky").removeClass("header-sticky");
            $("#header-sticky-2").removeClass("header-sticky-2");
        } else {
            $("#header-sticky").addClass("header-sticky");
            $("#header-sticky-2").addClass("header-sticky-2");
        }
    });

    windowOn.on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $(".tp-side-menu-5").removeClass("sticky-active");
        } else {
            $(".tp-side-menu-5").addClass("sticky-active");
        }
    });




    ////////////////////////////////////////////////////
    // 11. Theme Settings Js

    // settings append in body
    function tp_settings_append($x) {
        var settings = $('body');
        let dark;
        $x == true ? dark = 'd-block' : dark = 'd-none';
        var settings_html = `<div class="tp-theme-settings-area transition-3">
		<div class="tp-theme-wrapper">
		   <div class="tp-theme-header text-center">
			  <h4 class="tp-theme-header-title">Harry Theme Settings</h4>
		   </div>

		   <!-- THEME TOGGLER -->
		   <div class="tp-theme-toggle mb-20 ${dark}">
			  <label class="tp-theme-toggle-main" for="tp-theme-toggler">
				 <span class="tp-theme-toggle-dark"><i class="fa-light fa-moon"></i> Dark</span>
					<input type="checkbox" id="tp-theme-toggler">
					<i class="tp-theme-toggle-slide"></i>
				 <span class="tp-theme-toggle-light active"><i class="fa-light fa-sun-bright"></i> Light</span>
			  </label>
		   </div>

		   <!--  RTL SETTINGS -->
		   <div class="tp-theme-dir mb-20">
			  <label class="tp-theme-dir-main" for="tp-dir-toggler">
				 <span class="tp-theme-dir-rtl"> RTL</span>
					<input type="checkbox" id="tp-dir-toggler">
					<i class="tp-theme-dir-slide"></i>
				 <span class="tp-theme-dir-ltr active"> LTR</span>
			  </label>
		   </div>

		   <!-- COLOR SETTINGS -->
		   <div class="tp-theme-settings">
			  <div class="tp-theme-settings-wrapper">
				 <div class="tp-theme-settings-open">
					<button class="tp-theme-settings-open-btn">
					   <span class="tp-theme-settings-gear">
						  <i class="fa-light fa-gear"></i>
					   </span>
					   <span class="tp-theme-settings-close">
						  <i class="fa-regular fa-xmark"></i>
					   </span>
					</button>
				 </div>
				 <div class="row row-cols-4 gy-2 gx-2">
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn d-none" data-color-default="#F50963" type="button" data-color="#F50963"></button>
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#F50963"></button>
					   </div>
					</div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#008080"></button>
					   </div>
					</div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#AB6C56"></button>
					   </div>
					</div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#3661FC"></button>
					   </div>
					</div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#2CAE76"></button>
					   </div>
					</div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#FF5A1B"></button>
					   </div>
					</div>
					<div class="col">
                        <div class="tp-theme-color-item tp-color-active">
                           <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#03041C"></button>
                        </div>
                     </div>
					<div class="col">
					   <div class="tp-theme-color-item tp-color-active">
						  <button class="tp-theme-color-btn tp-color-settings-btn" type="button" data-color="#ED212C"></button>
					   </div>
					</div>
				 </div>
			  </div>
			  <div class="tp-theme-color-input">
				 <h6>Choose Custom Color</h6>
				 <input type="color" id="tp-color-setings-input" value="#F50963">
				 <label id="tp-theme-color-label" for="tp-color-setings-input"></label>
			  </div>
		   </div>
		</div>
	 </div>`;
        settings.append(settings_html);
    }
    // tp_settings_append(true); // if want to enable dark light mode then send "true";

    // settings open btn
    $(".tp-theme-settings-open-btn").on("click", function () {
        $(".tp-theme-settings-area").toggleClass("settings-opened");
    });

    // rtl settings
    function tp_rtl_settings() {

        $('#tp-dir-toggler').on("change", function () {
            toggle_rtl();

        });


        // set toggle theme scheme
        function tp_set_scheme(tp_dir) {
            localStorage.setItem('tp_dir', tp_dir);
            document.documentElement.setAttribute("dir", tp_dir);

            if (tp_dir === 'rtl') {
                var list = $("[href='assets/css/bootstrap.css']");
                $(list).attr("href", "assets/css/bootstrap-rtl.html");
            } else {
                var list = $("[href='assets/css/bootstrap.css']");
                $(list).attr("href", "assets/css/bootstrap.css");
            }
        }

        // toogle theme scheme
        function toggle_rtl() {
            if (localStorage.getItem('tp_dir') === 'rtl') {
                tp_set_scheme('ltr');
                var list = $("[href='assets/css/bootstrap-rtl.css']");
                $(list).attr("href", "assets/css/bootstrap.css");
            } else {
                tp_set_scheme('rtl');
                var list = $("[href='assets/css/bootstrap.css']");
                $(list).attr("href", "assets/css/bootstrap-rtl.html");
            }
        }

        // set the first theme scheme
        function tp_init_dir() {
            if (localStorage.getItem('tp_dir') === 'rtl') {
                tp_set_scheme('rtl');
                var list = $("[href='assets/css/bootstrap.css']");
                $(list).attr("href", "assets/css/bootstrap-rtl.html");
                document.getElementById('tp-dir-toggler').checked = true;
            } else {
                tp_set_scheme('ltr');
                document.getElementById('tp-dir-toggler').checked = false;
                var list = $("[href='assets/css/bootstrap.css']");
                $(list).attr("href", "assets/css/bootstrap.css");
            }
        }
        tp_init_dir();
    }
    if ($("#tp-dir-toggler").length > 0) {
        tp_rtl_settings();
    }

    // dark light mode toggler
    function tp_theme_toggler() {

        $('#tp-theme-toggler').on("change", function () {
            toggleTheme();

        });


        // set toggle theme scheme
        function tp_set_scheme(tp_theme) {
            localStorage.setItem('tp_theme_scheme', tp_theme);
            document.documentElement.setAttribute("tp-theme", tp_theme);
        }

        // toogle theme scheme
        function toggleTheme() {
            if (localStorage.getItem('tp_theme_scheme') === 'tp-theme-dark') {
                tp_set_scheme('tp-theme-light');
            } else {
                tp_set_scheme('tp-theme-dark');
            }
        }

        // set the first theme scheme
        function tp_init_theme() {
            if (localStorage.getItem('tp_theme_scheme') === 'tp-theme-dark') {
                tp_set_scheme('tp-theme-dark');
                document.getElementById('tp-theme-toggler').checked = true;
            } else {
                tp_set_scheme('tp-theme-light');
                document.getElementById('tp-theme-toggler').checked = false;
            }
        }
        tp_init_theme();
    }
    if ($("#tp-theme-toggler").length > 0) {
        tp_theme_toggler();
    }


    // color settings
    function tp_color_settings() {

        // set color scheme
        function tp_set_color(tp_color_scheme) {
            localStorage.setItem('tp_color_scheme', tp_color_scheme);
            document.querySelector(':root').style.setProperty('--tp-theme-1', tp_color_scheme);
            document.getElementById("tp-color-setings-input").value = tp_color_scheme;
            document.getElementById("tp-theme-color-label").style.backgroundColor = tp_color_scheme;
        }

        // set color
        function tp_set_input() {
            var color = localStorage.getItem('tp_color_scheme');
            document.getElementById("tp-color-setings-input").value = color;
            document.getElementById("tp-theme-color-label").style.backgroundColor = color;


        }
        tp_set_input();

        function tp_init_color() {
            var defaultColor = $(".tp-color-settings-btn").attr('data-color-default');
            var setColor = localStorage.getItem('tp_color_scheme');

            if (setColor != null) {

            } else {
                setColor = defaultColor;
            }

            if (defaultColor !== setColor) {
                document.querySelector(':root').style.setProperty('--tp-theme-1', setColor);
                document.getElementById("tp-color-setings-input").value = setColor;
                document.getElementById("tp-theme-color-label").style.backgroundColor = setColor;
                tp_set_color(setColor);
            } else {
                document.querySelector(':root').style.setProperty('--tp-theme-1', defaultColor);
                document.getElementById("tp-color-setings-input").value = defaultColor;
                document.getElementById("tp-theme-color-label").style.backgroundColor = defaultColor;
                tp_set_color(defaultColor);
            }
        }
        tp_init_color();


        let themeButtons = document.querySelectorAll('.tp-color-settings-btn');

        themeButtons.forEach(color => {
            color.addEventListener('click', () => {
                let datacolor = color.getAttribute('data-color');
                document.querySelector(':root').style.setProperty('--tp-theme-1', datacolor);
                document.getElementById("tp-theme-color-label").style.backgroundColor = datacolor;
                tp_set_color(datacolor);
            });
        });



        const colorInput = document.querySelector('#tp-color-setings-input');
        const colorVariable = '--tp-theme-1';


        colorInput.addEventListener('change', function (e) {
            var clr = e.target.value;
            document.documentElement.style.setProperty(colorVariable, clr);
            tp_set_color(clr);
            tp_set_check(clr);
        });


        function tp_set_check(clr) {
            const arr = Array.from(document.querySelectorAll('[data-color]'));

            var a = localStorage.getItem('tp_color_scheme');

            let test = arr.map(color => {
                let datacolor = color.getAttribute('data-color');

                return datacolor;
            }).filter(color => color == a);

            var arrLength = test.length;

            if (arrLength == 0) {
                $('.tp-color-active').removeClass('active');
            } else {
                $('.tp-color-active').addClass('active');
            }
        }

        function tp_check_color() {
            var a = localStorage.getItem('tp_color_scheme');

            var list = $(`[data-color="${a}"]`);

            list.parent().addClass('active').parent().siblings().find('.tp-color-active').removeClass('active')
        }
        tp_check_color();

        $('.tp-color-active').on('click', function () {
            $(this).addClass('active').parent().siblings().find('.tp-color-active').removeClass('active');
        });

    }
    if (($(".tp-color-settings-btn").length > 0) && ($("#tp-color-setings-input").length > 0) && ($("#tp-theme-color-label").length > 0)) {
        tp_color_settings();
    }



    ////////////////////////////////////////////////////
    // 12. Nice Select Js
    $('.tp-header-search-category select, .tp-shop-area select, .tp-checkout-area select, .profile__area select').niceSelect();

    ////////////////////////////////////////////////////
    // 13. Smooth Scroll Js
    function smoothSctoll() {
        $('.smooth a').on('click', function (event) {
            var target = $(this.getAttribute('href'));
            if (target.length) {
                event.preventDefault();
                $('html, body').stop().animate({
                    scrollTop: target.offset().top - 120
                }, 1500);
            }
        });
    }
    smoothSctoll();

    function back_to_top() {
        var btn = $('#back_to_top');
        var btn_wrapper = $('.back-to-top-wrapper');

        windowOn.scroll(function () {
            if (windowOn.scrollTop() > 300) {
                btn_wrapper.addClass('back-to-top-btn-show');
            } else {
                btn_wrapper.removeClass('back-to-top-btn-show');
            }
        });

        btn.on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: 0 }, '300');
        });
    }
    back_to_top();

    var tp_rtl = localStorage.getItem('tp_dir');
    let rtl_setting = tp_rtl == 'rtl' ? true : false;


    ////////////////////////////////////////////////////
    // 14. Slider Activation Area Start
    $('.tp-slider-active-4').slick({
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        fade: true,
        centerMode: true,
        prevArrow: `<button type="button" class="tp-slider-3-button-prev"><svg width="16" height="14" viewBox="0 0 16 14" fill="none" xmlns="http://www.w3.org/2000/svg">
		   <path d="M1.00073 6.99989L15 6.99989" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
		   <path d="M6.64648 1.5L1.00011 6.99954L6.64648 12.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg></button>`,
        nextArrow: `<button type="button" class="tp-slider-3-button-next"><svg width="16" height="14" viewBox="0 0 16 14" fill="none" xmlns="http://www.w3.org/2000/svg">
		<path d="M14.9993 6.99989L1 6.99989" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
		<path d="M9.35352 1.5L14.9999 6.99954L9.35352 12.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
		</svg></button>`,
        asNavFor: '.tp-slider-nav-active',
        appendArrows: $('.tp-slider-arrow-4'),

    });

    $('.tp-slider-nav-active').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 1,
        vertical: true,
        asNavFor: '.tp-slider-active-4',
        dots: false,
        arrows: false,
        prevArrow: '<button type="button" class="tp-slick-prev"><i class="fa-solid fa-arrow-up"></i></button>',
        nextArrow: '<button type="button" class="tp-slick-next"><i class="fa-solid fa-arrow-down"></i></button>',
        centerMode: false,
        focusOnSelect: true,
    });


    // home electronics
    var mainSlider = new Swiper('.tp-slider-active', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-button-next",
            prevEl: ".tp-slider-button-prev",
        },
        pagination: {
            el: ".tp-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    mainSlider.on('slideChangeTransitionStart', function (realIndex) {
        if ($('.swiper-slide.swiper-slide-active, .tp-slider-item .is-light').hasClass('is-light')) {
            $('.tp-slider-variation').addClass('is-light');
        } else {
            $('.tp-slider-variation').removeClass('is-light');
        }
    });

    // home electronics
    var slider = new Swiper('.shop-mega-menu-slider-active', {
        slidesPerView: 3,
        spaceBetween: 20,
        loop: true,
        rtl: rtl_setting,
        // Navigation arrows
        navigation: {
            nextEl: ".tp-shop-mega-menu-button-next",
            prevEl: ".tp-shop-mega-menu-button-prev",
        },
        pagination: {
            el: ".tp-shop-mega-menu-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    // home electronics
    var slider = new Swiper('.tp-blog-main-slider-active', {
        slidesPerView: 3,
        spaceBetween: 20,
        loop: true,
        autoplay: {
            delay: 4000,
        },
        rtl: rtl_setting,
        // Navigation arrows
        navigation: {
            nextEl: ".tp-blog-main-slider-button-next",
            prevEl: ".tp-blog-main-slider-button-prev",
        },
        pagination: {
            el: ".tp-blog-main-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        breakpoints: {
            '1200': {
                slidesPerView: 3,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 1,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    // home 2 fashion
    var slider = new Swiper('.tp-slider-active-2', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-2-button-next",
            prevEl: ".tp-slider-2-button-prev",
        },
        pagination: {
            el: ".tp-slider-2-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    // home 3 beauti
    var slider = new Swiper('.tp-slider-active-3', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-3-button-next",
            prevEl: ".tp-slider-3-button-prev",
        },
        pagination: {
            el: ".tp-slider-3-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    // home 3 beauti
    var slider = new Swiper('.tp-slider-active-5', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-5-button-next",
            prevEl: ".tp-slider-5-button-prev",
        },
        pagination: {
            el: ".tp-slider-5-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    var mainSliderThumb4 = new Swiper('.tp-slider-nav-actives', {
        slidesPerView: 3,
        spaceBetween: 20,
        loop: true,
        direction: 'vertical',
    });

    // home 3 beauti
    var mainSlider4 = new Swiper('.tp-slider-active-4s', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-3-button-next",
            prevEl: ".tp-slider-3-button-prev",
        },
        pagination: {
            el: ".tp-slider-3-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    // home 3 beauti
    var slider = new Swiper('.tp-slider-nav-actives', {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        // Navigation arrows
        navigation: {
            nextEl: ".tp-slider-3-button-next",
            prevEl: ".tp-slider-3-button-prev",
        },
        pagination: {
            el: ".tp-slider-3-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
    });

    var slider = new Swiper('.tp-product-offer-slider-active', {
        slidesPerView: 4,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        pagination: {
            el: ".tp-deals-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        breakpoints: {
            '1200': {
                slidesPerView: 3,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 1,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-product-arrival-active', {
        slidesPerView: 4,
        spaceBetween: 30,
        loop: true,
        rtl: rtl_setting,
        pagination: {
            el: ".tp-arrival-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-arrival-slider-button-next",
            prevEl: ".tp-arrival-slider-button-prev",
        },
        breakpoints: {
            '1200': {
                slidesPerView: 4,
            },
            '992': {
                slidesPerView: 3,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-product-banner-slider-active', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        effect: 'fade',
        pagination: {
            el: ".tp-product-banner-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },

    });

    var slider = new Swiper('.tp-product-gadget-banner-slider-active', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        effect: 'fade',
        pagination: {
            el: ".tp-product-gadget-banner-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },

    });

    var slider = new Swiper('.tp-category-slider-active-2', {
        slidesPerView: 5,
        spaceBetween: 20,
        loop: false,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-category-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-category-slider-button-next",
            prevEl: ".tp-category-slider-button-prev",
        },
        scrollbar: {
            el: '.swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },
        breakpoints: {
            '1200': {
                slidesPerView: 5,
            },
            '992': {
                slidesPerView: 4,
            },
            '768': {
                slidesPerView: 3,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-featured-slider-active', {
        slidesPerView: 3,
        spaceBetween: 10,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-featured-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-featured-slider-button-next",
            prevEl: ".tp-featured-slider-button-prev",
        },

        breakpoints: {
            '1200': {
                slidesPerView: 3,
            },
            '992': {
                slidesPerView: 3,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 1,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-product-related-slider-active', {
        slidesPerView: 4,
        spaceBetween: 24,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-related-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-related-slider-button-next",
            prevEl: ".tp-related-slider-button-prev",
        },

        scrollbar: {
            el: '.tp-related-swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },

        breakpoints: {
            '1200': {
                slidesPerView: 4,
            },
            '992': {
                slidesPerView: 3,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-testimoinal-slider-active-3', {
        slidesPerView: 2,
        spaceBetween: 24,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-testimoinal-slider-dot-3",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-testimoinal-slider-button-next-3",
            prevEl: ".tp-testimoinal-slider-button-prev-3",
        },

        breakpoints: {
            '1200': {
                slidesPerView: 2,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 1,
            },
            '576': {
                slidesPerView: 1,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-category-slider-active-4', {
        slidesPerView: 5,
        spaceBetween: 25,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-category-slider-dot-4",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-category-slider-button-next-4",
            prevEl: ".tp-category-slider-button-prev-4",
        },

        scrollbar: {
            el: '.tp-category-swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },

        breakpoints: {
            '1400': {
                slidesPerView: 5,
            },
            '1200': {
                slidesPerView: 4,
            },
            '992': {
                slidesPerView: 3,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-category-slider-active-5', {
        slidesPerView: 6,
        spaceBetween: 12,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-category-slider-dot-4",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-category-slider-button-next-5",
            prevEl: ".tp-category-slider-button-prev-5",
        },

        scrollbar: {
            el: '.tp-category-5-swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },

        breakpoints: {
            '1400': {
                slidesPerView: 6,
            },
            '1200': {
                slidesPerView: 5,
            },
            '992': {
                slidesPerView: 4,
            },
            '768': {
                slidesPerView: 3,
            },
            '576': {
                slidesPerView: 2,
            },
            '400': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-brand-slider-active', {
        slidesPerView: 5,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-brand-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-brand-slider-button-next",
            prevEl: ".tp-brand-slider-button-prev",
        },

        breakpoints: {
            '1200': {
                slidesPerView: 5,
            },
            '992': {
                slidesPerView: 5,
            },
            '768': {
                slidesPerView: 4,
            },
            '576': {
                slidesPerView: 3,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-best-slider-active', {
        slidesPerView: 4,
        spaceBetween: 24,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-best-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-best-slider-button-next",
            prevEl: ".tp-best-slider-button-prev",
        },

        scrollbar: {
            el: '.tp-best-swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },

        breakpoints: {
            '1200': {
                slidesPerView: 4,
            },
            '992': {
                slidesPerView: 4,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-best-slider-active-5', {
        slidesPerView: 3,
        spaceBetween: 24,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-best-slider-dot-5",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-best-slider-5-button-next",
            prevEl: ".tp-best-slider-5-button-prev",
        },

        scrollbar: {
            el: '.tp-best-5-swiper-scrollbar',
            draggable: true,
            dragClass: 'tp-swiper-scrollbar-drag',
            snapOnRelease: true,
        },

        breakpoints: {
            '1200': {
                slidesPerView: 3,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-product-details-thumb-slider-active', {
        slidesPerView: 2,
        spaceBetween: 13,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-product-details-thumb-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-product-details-thumb-slider-5-button-next",
            prevEl: ".tp-product-details-thumb-slider-5-button-prev",
        },


        breakpoints: {
            '1200': {
                slidesPerView: 2,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var slider = new Swiper('.tp-trending-slider-active', {
        slidesPerView: 2,
        spaceBetween: 24,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-trending-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-trending-slider-button-next",
            prevEl: ".tp-trending-slider-button-prev",
        },

        breakpoints: {
            '1200': {
                slidesPerView: 2,
            },
            '992': {
                slidesPerView: 2,
            },
            '768': {
                slidesPerView: 2,
            },
            '576': {
                slidesPerView: 2,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var specialSlider = new Swiper('.tp-special-slider-active', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        effect: 'fade',
        enteredSlides: false,
        pagination: {
            el: ".tp-special-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-special-slider-button-next",
            prevEl: ".tp-special-slider-button-prev",
        },
    });

    var slider = new Swiper('.tp-testimonial-slider-active', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        pagination: {
            el: ".tp-testimonial-slider-dot",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-testimonial-slider-button-next",
            prevEl: ".tp-testimonial-slider-button-prev",
        },
    });

    var slider = new Swiper('.tp-testimonial-slider-active-5', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        pagination: {
            el: ".tp-testimonial-slider-dot-5",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-testimonial-slider-5-button-next",
            prevEl: ".tp-testimonial-slider-5-button-prev",
        },

    });

    var slider = new Swiper('.tp-best-banner-slider-active-5', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        enteredSlides: false,
        effect: 'fade',
        pagination: {
            el: ".tp-best-banner-slider-dot-5",
            clickable: true,
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + '<button>' + (index + 1) + '</button>' + "</span>";
            },
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-best-banner-slider-5-button-next",
            prevEl: ".tp-best-banner-slider-5-button-prev",
        },
    });

    var postboxSlider = new Swiper('.tp-postbox-slider', {
        slidesPerView: 1,
        spaceBetween: 0,
        loop: true,
        rtl: rtl_setting,
        autoplay: {
            delay: 3000,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".tp-postbox-slider-button-next",
            prevEl: ".tp-postbox-slider-button-prev",
        },
        breakpoints: {
            '1200': {
                slidesPerView: 1,
            },
            '992': {
                slidesPerView: 1,
            },
            '768': {
                slidesPerView: 1,
            },
            '576': {
                slidesPerView: 1,
            },
            '0': {
                slidesPerView: 1,
            },
        },
    });

    var historyNav = new Swiper(".tp-history-nav-active", {
        spaceBetween: 220,
        slidesPerView: 4,
        watchSlidesProgress: true,
    });
    var historyMain = new Swiper(".tp-history-slider-active", {
        spaceBetween: 0,
        effect: 'fade',
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        thumbs: {
            swiper: historyNav,
        },
    });


    ////////////////////////////////////////////////////
    // 15. Masonary Js
    $('.grid').imagesLoaded(function () {
        // init Isotope
        var $grid = $('.grid').isotope({
            itemSelector: '.grid-item',
            percentPosition: true,
            masonry: {
                // use outer width of grid-sizer for columnWidth
                columnWidth: '.grid-item',
            }
        });


        // filter items on button click
        $('.masonary-menu').on('click', 'button', function () {
            var filterValue = $(this).attr('data-filter');
            $grid.isotope({ filter: filterValue });
        });

        //for menu active class
        $('.masonary-menu button').on('click', function (event) {
            $(this).siblings('.active').removeClass('active');
            $(this).addClass('active');
            event.preventDefault();
        });

    });

    /* magnificPopup img view */
    $('.popup-image').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        }
    });

    /* magnificPopup video view */
    $(".popup-video").magnificPopup({
        type: "iframe",
    });


    if ($('.scene').length > 0) {
        $('.scene').parallax({
            scalarX: 5.0,
            scalarY: 5.0,
        });
    };

    ////////////////////////////////////////////////////
    // 16. Wow Js
    new WOW().init();

    function tp_ecommerce() {
        $('.tp-cart-minus').on('click', function () {
            var $input = $(this).parent().find('.tp-cart-input');
            var count = parseInt($input.val()) - 1;
            count = count < 1 ? 1 : count;
            $input.val(count);
            $input.change();
            return false;
        });

        $('.tp-cart-plus').on('click', function () {
            var $input = $(this).parent().find('.tp-cart-input');
            $input.val(parseInt($input.val()) + 1);
            $input.change();
            return false;
        });

        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 500,
            values: [75, 300],
            slide: function (event, ui) {
                $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
            }
        });
        $("#amount").val("$" + $("#slider-range").slider("values", 0) +
            " - $" + $("#slider-range").slider("values", 1));

        $("#slider-range-offcanvas").slider({
            range: true,
            min: 0,
            max: 500,
            values: [75, 300],
            slide: function (event, ui) {
                $("#amount-offcanvas").val("$" + ui.values[0] + " - $" + ui.values[1]);
            }
        });
        $("#amount-offcanvas").val("$" + $("#slider-range-offcanvas").slider("values", 0) +
            " - $" + $("#slider-range-offcanvas").slider("values", 1));



        $('.tp-checkout-payment-item label').on('click', function () {
            $(this).siblings('.tp-checkout-payment-desc').slideToggle(400);

        });


        $('.tp-color-variation-btn').on('click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });


        $('.tp-size-variation-btn').on('click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });

        ////////////////////////////////////////////////////
        // 17. Show Login Toggle Js
        $('.tp-checkout-login-form-reveal-btn').on('click', function () {
            $('#tpReturnCustomerLoginForm').slideToggle(400);
        });

        ////////////////////////////////////////////////////
        // 18. Show Coupon Toggle Js
        $('.tp-checkout-coupon-form-reveal-btn').on('click', function () {
            $('#tpCheckoutCouponForm').slideToggle(400);
        });

        ////////////////////////////////////////////////////
        // 19. Create An Account Toggle Js
        $('#cbox').on('click', function () {
            $('#cbox_info').slideToggle(900);
        });

        ////////////////////////////////////////////////////
        // 20. Shipping Box Toggle Js
        $('#ship-box').on('click', function () {
            $('#ship-box-info').slideToggle(1000);
        });

        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 500,
            values: [75, 300],
            slide: function (event, ui) {
                $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
            },
        });
    }
    tp_ecommerce();



    ////////////////////////////////////////////////////
    // 17. Counter Js
    new PureCounter();
    new PureCounter({
        filesizing: true,
        selector: ".filesizecount",
        pulse: 2,
    });

    ////////////////////////////////////////////////////
    // 18. InHover Active Js
    $('.hover__active').on('mouseenter', function () {
        $(this).addClass('active').parent().siblings().find('.hover__active').removeClass('active');
    });

    $('.activebsba').on("click", function () {
        $('#services-item-thumb').removeClass().addClass($(this).attr('rel'));
        $(this).addClass('active').siblings().removeClass('active');
    });


    ////////////////////////////////////////////////////
    // 19. Line Animation Js
    if ($('#marker').length > 0) {
        function tp_tab_line() {
            var marker = document.querySelector('#marker');
            var item = document.querySelectorAll('.menu-style-3  > nav > ul > li');
            var itemActive = document.querySelector('.menu-style-3  > nav > ul > li.active');

            function indicator(e) {
                marker.style.left = e.offsetLeft + "px";
                marker.style.width = e.offsetWidth + "px";
            }


            item.forEach(link => {
                link.addEventListener('mouseenter', (e) => {
                    indicator(e.target);
                });

            });


            var activeNav = $('.menu-style-3 > nav > ul > li.active');
            var activewidth = $(activeNav).width();
            var activePadLeft = parseFloat($(activeNav).css('padding-left'));
            var activePadRight = parseFloat($(activeNav).css('padding-right'));
            var totalWidth = activewidth + activePadLeft + activePadRight;

            var precedingAnchorWidth = anchorWidthCounter();


            $(marker).css('display', 'block');

            $(marker).css('width', totalWidth);

            function anchorWidthCounter() {
                var anchorWidths = 0;
                var a;
                var aWidth;
                var aPadLeft;
                var aPadRight;
                var aTotalWidth;
                $('.menu-style-3 > nav > ul > li').each(function (index, elem) {
                    var activeTest = $(elem).hasClass('active');
                    marker.style.left = elem.offsetLeft + "px";
                    if (activeTest) {
                        // Break out of the each function.
                        return false;
                    }

                    a = $(elem).find('li');
                    aWidth = a.width();
                    aPadLeft = parseFloat(a.css('padding-left'));
                    aPadRight = parseFloat(a.css('padding-right'));
                    aTotalWidth = aWidth + aPadLeft + aPadRight;

                    anchorWidths = anchorWidths + aTotalWidth;

                });

                return anchorWidths;
            }
        }
        tp_tab_line();
    }


    if ($('#productTabMarker').length > 0) {
        function tp_tab_line_2() {
            var marker = document.querySelector('#productTabMarker');
            var item = document.querySelectorAll('.tp-product-tab button');
            var itemActive = document.querySelector('.tp-product-tab .nav-link.active');

            // rtl settings
            var tp_rtl = localStorage.getItem('tp_dir');
            let rtl_setting = tp_rtl == 'rtl' ? 'right' : 'left';

            function indicator(e) {
                marker.style.left = e.offsetLeft + "px";
                marker.style.width = e.offsetWidth + "px";
            }


            item.forEach(link => {
                link.addEventListener('click', (e) => {
                    indicator(e.target);
                });
            });

            var activeNav = $('.nav-link.active');
            var activewidth = $(activeNav).width();
            var activePadLeft = parseFloat($(activeNav).css('padding-left'));
            var activePadRight = parseFloat($(activeNav).css('padding-right'));
            var totalWidth = activewidth + activePadLeft + activePadRight;

            var precedingAnchorWidth = anchorWidthCounter();


            $(marker).css('display', 'block');

            $(marker).css('width', totalWidth);

            function anchorWidthCounter() {
                var anchorWidths = 0;
                var a;
                var aWidth;
                var aPadLeft;
                var aPadRight;
                var aTotalWidth;
                $('.tp-product-tab button').each(function (index, elem) {
                    var activeTest = $(elem).hasClass('active');
                    marker.style.left = elem.offsetLeft + "px";
                    if (activeTest) {
                        // Break out of the each function.
                        return false;
                    }

                    a = $(elem).find('button');
                    aWidth = a.width();
                    aPadLeft = parseFloat(a.css('padding-left'));
                    aPadRight = parseFloat(a.css('padding-right'));
                    aTotalWidth = aWidth + aPadLeft + aPadRight;

                    anchorWidths = anchorWidths + aTotalWidth;

                });

                return anchorWidths;
            }
        }
        tp_tab_line_2();
    }

    ////////////////////////////////////////////////////
    // 20. Video Play Js
    var play = false;
    $('.tp-video-toggle-btn').on('click', function () {

        if (play === false) {
            $('.tp-slider-video').addClass('full-width');
            $(this).addClass('hide');
            play = true;

            $('.tp-slider-video').find('video').each(function () {
                $(this).get(0).play();
            });
        } else {
            $('.tp-slider-video').removeClass('full-width');
            $(this).removeClass('hide');
            play = false;
            $('.tp-slider-video').find('video').each(function () {
                $(this).get(0).pause();
            });
        }

    });

    ////////////////////////////////////////////////////
    // 21. Password Toggle Js
    if ($('#password-show-toggle').length > 0) {
        var btn = document.getElementById('password-show-toggle');

        btn.addEventListener('click', function (e) {

            var inputType = document.getElementById('tp_password');
            var openEye = document.getElementById('open-eye');
            var closeEye = document.getElementById('close-eye');

            if (inputType.type === "password") {
                inputType.type = "text";
                openEye.style.display = 'block';
                closeEye.style.display = 'none';
            } else {
                inputType.type = "password";
                openEye.style.display = 'none';
                closeEye.style.display = 'block';
            }
        });
    }


    if ($('.tp-header-height').length > 0) {
        var headerHeight = document.querySelector(".tp-header-height");
        var setHeaderHeight = headerHeight.offsetHeight;

        $(".tp-header-height").each(function () {
            $(this).css({
                'height': setHeaderHeight + 'px'
            });
        });
    }

    if ($('.infinite-container').length > 0) {
        let ias = new InfiniteAjaxScroll('.infinite-container', {
            item: '.infinite-item',
            next: '.infinite-next-link',
            pagination: '.infinite-pagination'
        });

    }

    if ($('.load-more-content').length > 0) {
        $('.load-more-content').btnLoadmore({
            showItem: 9,
            whenClickBtn: 3,
            textBtn: 'Load More Products',
            classBtn: 'tp-btn tp-btn-border tp-btn-border-primary'
        });
    }

})(jQuery);





/*calculator*/

//const qiymetInput = document.getElementById("qiymetidaxilet");

//const tr3ay = document.getElementById("3ay");
//const tr6ay = document.getElementById("6ay");
//const tr9ay = document.getElementById("9ay");
//const tr12ay = document.getElementById("12ay");
//const tr15ay = document.getElementById("15ay");
//const tr18ay = document.getElementById("18ay");




//const td3ayOdeme = tr3ay.getElementsByTagName("td")[1];
//const td3ayToplam = tr3ay.getElementsByTagName("td")[2];

//const td6ayOdeme = tr6ay.getElementsByTagName("td")[1];
//const td6ayToplam = tr6ay.getElementsByTagName("td")[2];

//const td9ayOdeme = tr9ay.getElementsByTagName("td")[1];
//const td9ayToplam = tr9ay.getElementsByTagName("td")[2];

//const td12ayOdeme = tr12ay.getElementsByTagName("td")[1];
//const td12ayToplam = tr12ay.getElementsByTagName("td")[2];

//const td15ayOdeme = tr15ay.getElementsByTagName("td")[1];
//const td15ayToplam = tr15ay.getElementsByTagName("td")[2];

//const td18ayOdeme = tr18ay.getElementsByTagName("td")[1];
//const td18ayToplam = tr18ay.getElementsByTagName("td")[2];



//qiymetInput.addEventListener("input", hesapla);

//function hesapla() {
//    const qiymet = parseFloat(qiymetInput.value);

//    // 3 ay hesablamasi
//    const faizOrani3ay = 0.13;
//    const toplamOdeme3ay = qiymet * (1 + faizOrani3ay);
//    const aylikOdeme3ay = toplamOdeme3ay / 3;

//    td3ayOdeme.textContent = aylikOdeme3ay.toFixed(2);
//    td3ayToplam.textContent = toplamOdeme3ay.toFixed(2);

//    // 6 ay hesablamasi
//    const faizOrani6ay = 0.20;
//    const toplamOdeme6ay = qiymet * (1 + faizOrani6ay);
//    const aylikOdeme6ay = toplamOdeme6ay / 6;

//    td6ayOdeme.textContent = aylikOdeme6ay.toFixed(2);
//    td6ayToplam.textContent = toplamOdeme6ay.toFixed(2);

//    //   9 ay hesablamasi
//    const faizOrani9ay = 0.26;
//    const toplamOdeme9ay = qiymet * (1 + faizOrani9ay);
//    const aylikOdeme9ay = toplamOdeme9ay / 9;

//    td9ayOdeme.textContent = aylikOdeme9ay.toFixed(2);
//    td9ayToplam.textContent = toplamOdeme9ay.toFixed(2);

//    //   12 ay hesablamasi
//    const faizOrani12ay = 0.35;
//    const toplamOdeme12ay = qiymet * (1 + faizOrani12ay);
//    const aylikOdeme12ay = toplamOdeme12ay / 12;

//    td12ayOdeme.textContent = aylikOdeme12ay.toFixed(2);
//    td12ayToplam.textContent = toplamOdeme12ay.toFixed(2);

//    //   15 ay hesablamasi
//    const faizOrani15ay = 0.39;
//    const toplamOdeme15ay = qiymet * (1 + faizOrani15ay);
//    const aylikOdeme15ay = toplamOdeme15ay / 15;

//    td15ayOdeme.textContent = aylikOdeme15ay.toFixed(2);
//    td15ayToplam.textContent = toplamOdeme15ay.toFixed(2);

//    //   18 ay hesablamasi
//    const faizOrani18ay = 0.47;
//    const toplamOdeme18ay = qiymet * (1 + faizOrani18ay);
//    const aylikOdeme18ay = toplamOdeme18ay / 18;

//    td18ayOdeme.textContent = aylikOdeme18ay.toFixed(2);
//    td18ayToplam.textContent = toplamOdeme18ay.toFixed(2);

//}







const qiymetInput = document.getElementById("qiymetidaxilet");

const tr3ay = document.getElementById("3ay");
const tr6ay = document.getElementById("6ay");
const tr9ay = document.getElementById("9ay");
const tr12ay = document.getElementById("12ay");
const tr15ay = document.getElementById("15ay");
const tr18ay = document.getElementById("18ay");

const td3ayOdeme = tr3ay.getElementsByTagName("td")[1];
const td3ayToplam = tr3ay.getElementsByTagName("td")[2];

const td6ayOdeme = tr6ay.getElementsByTagName("td")[1];
const td6ayToplam = tr6ay.getElementsByTagName("td")[2];

const td9ayOdeme = tr9ay.getElementsByTagName("td")[1];
const td9ayToplam = tr9ay.getElementsByTagName("td")[2];

const td12ayOdeme = tr12ay.getElementsByTagName("td")[1];
const td12ayToplam = tr12ay.getElementsByTagName("td")[2];

const td15ayOdeme = tr15ay.getElementsByTagName("td")[1];
const td15ayToplam = tr15ay.getElementsByTagName("td")[2];

const td18ayOdeme = tr18ay.getElementsByTagName("td")[1];
const td18ayToplam = tr18ay.getElementsByTagName("td")[2];


hesapla()
function hesapla() {
    const qiymet = parseFloat(qiymetInput.value);

    if (isNaN(qiymet) || !isFinite(qiymet)) {
        td3ayOdeme.textContent = '';
        td3ayToplam.textContent = '';

        td6ayOdeme.textContent = '';
        td6ayToplam.textContent = '';

        td9ayOdeme.textContent = '';
        td9ayToplam.textContent = '';

        td12ayOdeme.textContent = '';
        td12ayToplam.textContent = '';

        td15ayOdeme.textContent = '';
        td15ayToplam.textContent = '';

        td18ayOdeme.textContent = '';
        td18ayToplam.textContent = '';

        return; // Hesaplama işlemini durdur
    }

    // 3 ay hesablaması
    const faizOrani3ay = 0.13;
    const toplamOdeme3ay = qiymet * (1 + faizOrani3ay);
    const aylikOdeme3ay = toplamOdeme3ay / 3;

    td3ayOdeme.textContent = aylikOdeme3ay.toFixed(2) + " ₼";
    td3ayToplam.textContent = toplamOdeme3ay.toFixed(2) + " ₼";

    // 6 ay hesablaması
    const faizOrani6ay = 0.20;
    const toplamOdeme6ay = qiymet * (1 + faizOrani6ay);
    const aylikOdeme6ay = toplamOdeme6ay / 6;

    td6ayOdeme.textContent = aylikOdeme6ay.toFixed(2) + " ₼";
    td6ayToplam.textContent = toplamOdeme6ay.toFixed(2) + " ₼";

    //   9 ay hesablamasi
    const faizOrani9ay = 0.26;
    const toplamOdeme9ay = qiymet * (1 + faizOrani9ay);
    const aylikOdeme9ay = toplamOdeme9ay / 9;

    td9ayOdeme.textContent = aylikOdeme9ay.toFixed(2) + " ₼";
    td9ayToplam.textContent = toplamOdeme9ay.toFixed(2) + " ₼";

        //   12 ay hesablamasi
    const faizOrani12ay = 0.35;
    const toplamOdeme12ay = qiymet * (1 + faizOrani12ay);
    const aylikOdeme12ay = toplamOdeme12ay / 12;

    td12ayOdeme.textContent = aylikOdeme12ay.toFixed(2) + " ₼";
    td12ayToplam.textContent = toplamOdeme12ay.toFixed(2) + " ₼";

    //   15 ay hesablamasi
    const faizOrani15ay = 0.39;
    const toplamOdeme15ay = qiymet * (1 + faizOrani15ay);
    const aylikOdeme15ay = toplamOdeme15ay / 15;

    td15ayOdeme.textContent = aylikOdeme15ay.toFixed(2) + " ₼";
    td15ayToplam.textContent = toplamOdeme15ay.toFixed(2) + " ₼";

    //   18 ay hesablamasi
    const faizOrani18ay = 0.47;
    const toplamOdeme18ay = qiymet * (1 + faizOrani18ay);
    const aylikOdeme18ay = toplamOdeme18ay / 18;

    td18ayOdeme.textContent = aylikOdeme18ay.toFixed(2) + " ₼";
    td18ayToplam.textContent = toplamOdeme18ay.toFixed(2) + " ₼";
}





// label up-down
const input = document.getElementById("qiymetidaxilet");
const label = document.querySelector(".calcinput label");

input.addEventListener("focus", moveLabelUp);
input.addEventListener("blur", moveLabelToInitial);

function moveLabelUp() {
    label.style.top = "-15px";
}

function moveLabelToInitial() {
    if (input.value === "") {
        label.style.top = "-15px";
    } else {
        label.style.top = "-18px";
    }
}






