(function($) {
	"use strict";


    // LIGHT THEME START
    $(document).on("click", '#myonoffswitch1', function () {
        if (this.checked) {
            $('body').addClass('light-mode');
            $('#myonoffswitch3').prop('checked', true);
            $('#myonoffswitch6').prop('checked', true);
            $('body').removeClass('dark-mode');
            
            $('body')?.removeClass('color-menu');
            $('body')?.removeClass('gradient-menu');
            $('body')?.removeClass('dark-menu');
            $('body')?.removeClass('color-header');
            $('body')?.removeClass('gradient-header');
            $('body')?.removeClass('dark-header');
            localStorage.setItem("yohalightMode", "true");
            localStorage.removeItem("yohadarkMode");


            $('#myonoffswitch1').prop('checked', true);
            $('#myonoffswitch2').prop('checked', false);

            checkOptions();
            const root = document.querySelector(':root');
            root.style = "";
            names()
        } 
        localStorageBackup();
    });
    // LIGHT THEME END

    // DARK THEME START
    $(document).on("click", '#myonoffswitch2', function () {
        if (this.checked) {
            $('body').addClass('dark-mode');
            $('#myonoffswitch5').prop('checked', true);
            $('#myonoffswitch8').prop('checked', true);
            $('body').removeClass('light-mode');
            
            $('body')?.removeClass('color-menu');
            $('body')?.removeClass('gradient-menu');
            $('body')?.removeClass('light-menu');
            $('body')?.removeClass('color-header');
            $('body')?.removeClass('gradient-header');
            $('body')?.removeClass('header-light');
            localStorage.setItem("yohadarkMode", "true");
            localStorage.removeItem("yohalightMode");

            $('#myonoffswitch1').prop('checked', false);
            $('#myonoffswitch2').prop('checked', true);
            //
            checkOptions();
            const root = document.querySelector(':root');
            root.style = "";
            names()
        }
        localStorageBackup()
    });
    // DARK THEME END

    // LIGHT LEFTMENU START
    $(document).on("click", '#myonoffswitch3', function () {
        if (this.checked) {
            $('body').addClass('light-menu');
            $('body').removeClass('color-menu');
            $('body').removeClass('dark-menu');
            $('body').removeClass('gradient-menu');

            localStorage.setItem("yohaLightmenu", true);
            localStorage.removeItem("yohaDarkmenu");
            localStorage.removeItem("yohaGradientmenu");
            localStorage.removeItem("yohaColormenu");
            localStorage.removeItem("yohathemestyle");
        } else {
            $('body').removeClass('light-menu');
        }
    });
    // LIGHT LEFTMENU END

    // COLOR LEFTMENU START
    $(document).on("click", '#myonoffswitch4', function () {
        if (this.checked) {
            $('body').addClass('color-menu');
            $('body').removeClass('light-menu');
            $('body').removeClass('dark-menu');
            $('body').removeClass('gradient-menu');

            localStorage.setItem("yohaColormenu", true);
            localStorage.removeItem("yohaDarkmenu");
            localStorage.removeItem("yohaGradientmenu");
            localStorage.removeItem("yohaLightmenu");
            localStorage.removeItem("yohathemestyle");

        } else {
            $('body').removeClass('color-menu');
        }
    });
    // COLOR LEFTMENU END

    // DARK LEFTMENU START
    $(document).on("click", '#myonoffswitch5', function () {
        if (this.checked) {
            $('body').addClass('dark-menu');
            $('body').removeClass('color-menu');
            $('body').removeClass('light-menu');
            $('body').removeClass('gradient-menu');

            localStorage.setItem("yohaDarkmenu", true);
            localStorage.removeItem("yohaColormenu");
            localStorage.removeItem("yohaGradientmenu");
            localStorage.removeItem("yohaLightmenu");
            localStorage.removeItem("yohathemestyle");
        } else {
            $('body').removeClass('dark-menu');
        }
    });
    // DARK LEFTMENU END

    // GRADIENT LEFTMENU START
    $(document).on("click", '#myonoffswitch19', function () {
        if (this.checked) {
            $('body').addClass('gradient-menu');
            $('body').removeClass('color-menu');
            $('body').removeClass('light-menu');
            $('body').removeClass('dark-menu');

            localStorage.setItem("yohaGradientmenu", true);
            localStorage.removeItem("yohaColormenu");
            localStorage.removeItem("yohaDarkmenu");
            localStorage.removeItem("yohaLightmenu");
            localStorage.removeItem("yohathemestyle");

        } else {
            $('body').removeClass('gradient-menu');
        }
    });
    // GRADIENT LEFTMENU END

    // LIGHT HEADER START
    $(document).on("click", '#myonoffswitch6', function () {
        if (this.checked) {
            $('body').addClass('header-light');
            $('body').removeClass('color-header');
            $('body').removeClass('dark-header');
            $('body').removeClass('gradient-header');

            localStorage.setItem("yohaLightheader", true);
            localStorage.removeItem("yohaDarkheader");
            localStorage.removeItem("yohaGradientheader");
            localStorage.removeItem("yohaColorheader");

        } else {
            $('body').removeClass('header-light');
        }
    });
    // LIGHT HEADER END

    // COLOR HEADER START
    $(document).on("click", '#myonoffswitch7', function () {
        if (this.checked) {
            $('body').addClass('color-header');
            $('body').removeClass('header-light');
            $('body').removeClass('dark-header');
            $('body').removeClass('gradient-header');

            localStorage.setItem("yohaColorheader", true);
            localStorage.removeItem("yohaDarkheader");
            localStorage.removeItem("yohaGradientheader");
            localStorage.removeItem("yohaLightheader");
            localStorage.removeItem("yohathemestyle");

        } else {
            $('body').removeClass('color-header');
        }
    });
    // COLOR HEADER END

    // DARK HEADER START
    $(document).on("click", '#myonoffswitch8', function () {
        if (this.checked) {
            $('body').addClass('dark-header');
            $('body').removeClass('color-header');
            $('body').removeClass('header-light');
            $('body').removeClass('gradient-header');

            localStorage.setItem("yohaDarkheader", true);
            localStorage.removeItem("yohaColorheader");
            localStorage.removeItem("yohaGradientheader");
            localStorage.removeItem("yohaLightheader");
            localStorage.removeItem("yohathemestyle");

        } else {
            $('body').removeClass('dark-header');
        }
    });
    // DARK HEADER END

    // GRADIENT HEADER START
    $(document).on("click", '#myonoffswitch20', function () {
        if (this.checked) {
            $('body').addClass('gradient-header');
            $('body').removeClass('color-header');
            $('body').removeClass('header-light');
            $('body').removeClass('dark-header');

            localStorage.setItem("yohaGradientheader", true);
            localStorage.removeItem("yohaColorheader");
            localStorage.removeItem("yohaDarkheader");
            localStorage.removeItem("yohaLightheader");
            localStorage.removeItem("yohathemestyle");
        } else {
            $('body').removeClass('gradient-header');
        }
    });
    // GRADIENT HEADER END

    // FULL WIDTH LAYOUT START
    $(document).on("click", '#myonoffswitch9', function () {
        if (this.checked) {
            $('body').addClass('layout-fullwidth');
            $('body').removeClass('layout-boxed');
            checkHoriMenu();

            localStorage.setItem("yohafullwidth", true);
            localStorage.removeItem("yohaboxedwidth");

        }
    });
    // FULL WIDTH LAYOUT END

    // BOXED LAYOUT START
    $(document).on("click", '#myonoffswitch10', function () {
        if (this.checked) {
            $('body').addClass('layout-boxed');
            $('body').removeClass('layout-fullwidth');
            checkHoriMenu();

            localStorage.setItem("yohaboxedwidth", true);
            localStorage.removeItem("yohafullwidth");
        }
    });
    // BOXED LAYOUT END

    // HEADER POSITION STYLES START
    $(document).on("click", '#myonoffswitch11', function () {
        if (this.checked) {
            $('body').addClass('fixed-layout');
            $('body').removeClass('scrollable-layout');

            localStorage.setItem("yohafixed", true);
            localStorage.removeItem("yohascrollable");
        }
    });
    $(document).on("click", '#myonoffswitch12', function () {
        if (this.checked) {
            $('body').addClass('scrollable-layout');
            $('body').removeClass('fixed-layout');

            localStorage.setItem("yohascrollable", true);
            localStorage.removeItem("yohafixed");
        }
    });
    // HEADER POSITION STYLES END

    // DEFAULT SIDEMENU START
    $(document).on("click", '#myonoffswitch13', function () {
        if (this.checked) {
            $('body').addClass('default-menu');
            $('body').removeClass('sidenav-toggled');
            hovermenu();
            $('body').removeClass('icontext-menu');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('hover-submenu');
            $('body').removeClass('hover-submenu1');

            localStorage.setItem("yohadefaultmenu", true);
            localStorage.removeItem("yohaclosedmenu");
            localStorage.removeItem("yohaicontextmenu");
            localStorage.removeItem("yohaiconoverlay");
            localStorage.removeItem("yohahoversubmenu");
            localStorage.removeItem("yohahoversubmenu1");
        }
    });
    // DEFAULT SIDEMENU END

    // ICON OVERLAY SIDEMENU START
    $(document).on("click", '#myonoffswitch15', function () {
        if (this.checked) {
            $('body').addClass('icon-overlay');
            hovermenu();
            iconoverlay();
            $('body').addClass('sidenav-toggled');
            $('body').removeClass('hover-submenu1');
            $('body').removeClass('default-menu');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('hover-submenu');
            $('body').removeClass('icontext-menu');

            localStorage.setItem("yohaiconoverlay", true);
            localStorage.removeItem("yohadefaultmenu");
            localStorage.removeItem("yohaclosedmenu");
            localStorage.removeItem("yohaicontextmenu");
            localStorage.removeItem("yohahoversubmenu");
            localStorage.removeItem("yohahoversubmenu1");
        }
    });
    // ICON OVERLAY SIDEMENU END

    // ICONTEXT SIDEMENU START
    $(document).on("click", '#myonoffswitch14', function () {
        if (this.checked) {
            $('body').addClass('icontext-menu');
            icontext();
            $('body').addClass('sidenav-toggled');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('hover-submenu1');
            $('body').removeClass('default-menu');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('hover-submenu');

            localStorage.setItem("yohaicontextmenu", true);
            localStorage.removeItem("yohadefaultmenu");
            localStorage.removeItem("yohaclosedmenu");
            localStorage.removeItem("yohaiconoverlay");
            localStorage.removeItem("yohahoversubmenu");
            localStorage.removeItem("yohahoversubmenu1");
        }
    });
    // ICONTEXT SIDEMENU END

    // CLOSED SIDEMENU START
    $(document).on("click", '#myonoffswitch16', function () {
        if (this.checked) {
            $('body').addClass('closed-leftmenu');
            $('body').addClass('sidenav-toggled');
            $('body').removeClass('default-menu');
            $('body').removeClass('hover-submenu1');
            $('body').removeClass('hover-submenu');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('icontext-menu');
        
            localStorage.setItem("yohaclosedmenu", true);
            localStorage.removeItem("yohadefaultmenu");
            localStorage.removeItem("yohaicontextmenu");
            localStorage.removeItem("yohaiconoverlay");
            localStorage.removeItem("yohahoversubmenu");
            localStorage.removeItem("yohahoversubmenu1");
        }
    });
    // CLOSED SIDEMENU END

    // HOVER SUBMENU START
    $(document).on("click", '#myonoffswitch17', function () {
        if (this.checked) {
            $('body').addClass('hover-submenu');
            hovermenu();
            $('body').addClass('sidenav-toggled');
            $('body').removeClass('hover-submenu1');
            $('body').removeClass('default-menu');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('icontext-menu');
            $('.app-sidebar').removeClass('sidemenu-scroll');

            localStorage.setItem("yohahoversubmenu", true);
            localStorage.removeItem("yohadefaultmenu");
            localStorage.removeItem("yohaclosedmenu");
            localStorage.removeItem("yohaicontextmenu");
            localStorage.removeItem("yohaiconoverlay");
            localStorage.removeItem("yohahoversubmenu1");
        }
    });
    // HOVER SUBMENU END

    // HOVER SUBMENU STYLE-1 START
    $(document).on("click", '#myonoffswitch18', function () {
        if (this.checked) {
            $('body').addClass('hover-submenu1');
            hovermenu();
            $('body').addClass('sidenav-toggled');
            $('body').removeClass('hover-submenu');
            $('body').removeClass('default-menu');
            $('body').removeClass('closed-leftmenu');
            $('body').removeClass('icon-overlay');
            $('body').removeClass('icontext-menu');
            $('.app-sidebar').removeClass('sidemenu-scroll');
        
            localStorage.setItem("yohahoversubmenu1", true);
            localStorage.removeItem("yohadefaultmenu");
            localStorage.removeItem("yohaclosedmenu");
            localStorage.removeItem("yohaicontextmenu");
            localStorage.removeItem("yohaiconoverlay");
            localStorage.removeItem("yohahoversubmenu");
        }
    });
    // HOVER SUBMENU STYLE-1 END


    let bodyhorizontal = $('body').hasClass('horizontal');
    if (bodyhorizontal) {
        if( !document.querySelector('.login-img') ){
            ActiveSubmenu();
            checkHoriMenu();
            responsive();
        }
        if(window.innerWidth>=992){
            let li = document.querySelectorAll('.side-menu li')
            li.forEach((e, i) => {
                e.classList.remove('is-expanded')
            })
            var animationSpeed = 300;
            // first level
            var parent = $("[data-bs-toggle='sub-slide']").parents('ul');
            var ul = parent.find('ul:visible').slideUp(animationSpeed);
            ul.removeClass('open');
            var parent1 = $("[data-bs-toggle='sub-slide2']").parents('ul');
            var ul1 = parent1.find('ul:visible').slideUp(animationSpeed);
            ul1.removeClass('open');
        }
        $('body').addClass('horizontal');
        $(".main-content").addClass("hor-content");
        $(".main-content").removeClass("app-content");
        $(".main-container").addClass("container");
        $(".main-container").removeClass("container-fluid");
        $(".app-header").addClass("hor-header");
        $(".hor-header").removeClass("app-header");
        $(".app-sidebar").addClass("horizontal-main")
        $(".main-sidemenu").addClass("container")
        $('body').removeClass('sidebar-mini');
        $('body').removeClass('sidenav-toggled');
        $('body').removeClass('horizontal-hover');
        $('body').removeClass('default-menu');
        $('body').removeClass('icontext-menu');
        $('body').removeClass('icon-overlay');
        $('body').removeClass('closed-leftmenu');
        $('body').removeClass('hover-submenu');
        $('body').removeClass('hover-submenu1');
        // // To enable no-wrap horizontal style
        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        localStorage.setItem("yohahorizontal", "true");
        localStorage.removeItem("yohasidebarMini");
        localStorage.removeItem("yohahorizontalHover");
        document.querySelector('.horizontal .side-menu')?.classList.add('flex-nowrap')
        // To enable wrap horizontal style
        // $('#slide-left').addClass('d-none');
        // $('#slide-right').addClass('d-none');
        // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
        
    } 

    function light() {
        "use strict";
        if (document.querySelector('body').classList.contains('light-mode')) {
            $('#myonoffswitch3').prop('checked', true);
            $('#myonoffswitch6').prop('checked', true);
        }
    }
    light();
    let bodyhorizontalHover = $('body').hasClass('horizontal-hover');
    if (bodyhorizontalHover) {
        if( !document.querySelector('.login-img') ){
            checkHoriMenu();
            responsive();
        }
        if(window.innerWidth>=992){
            let li = document.querySelectorAll('.side-menu li')
            li.forEach((e, i) => {
                e.classList.remove('is-expanded')
            })
            var animationSpeed = 300;
            // first level
            var parent = $("[data-bs-toggle='sub-slide']").parents('ul');
            var ul = parent.find('ul:visible').slideUp(animationSpeed);
            ul.removeClass('open');
            var parent1 = $("[data-bs-toggle='sub-slide2']").parents('ul');
            var ul1 = parent1.find('ul:visible').slideUp(animationSpeed);
            ul1.removeClass('open');
        }
        $('body').addClass('horizontal-hover');
        $('body').addClass('horizontal');
        // $('#slide-left').addClass('d-none');
        // $('#slide-right').addClass('d-none');
        // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
        $('#slide-left').addClass('d-none');
        $('#slide-right').addClass('d-none');
        document.querySelector('.horizontal .side-menu')?.classList.add('flex-nowrap')
        $(".main-content").addClass("hor-content");
        $(".main-content").removeClass("app-content");
        $(".main-container").addClass("container");
        $(".main-container").removeClass("container-fluid");
        $(".app-header").addClass("hor-header");
        $(".app-header").removeClass("app-header");
        $(".app-sidebar").addClass("horizontal-main")
        $(".main-sidemenu").addClass("container")
        $('body').removeClass('sidebar-mini');
        $('body').removeClass('sidenav-toggled');
        $('body').removeClass('default-menu');
        $('body').removeClass('icontext-menu');
        $('body').removeClass('icon-overlay');
        $('body').removeClass('closed-leftmenu');
        $('body').removeClass('hover-submenu');
        $('body').removeClass('hover-submenu1');
        localStorage.setItem("yohahorizontalHover", "true");
        localStorage.removeItem("yohasidebarMini");
        localStorage.removeItem("yohahorizontal");
    }

    let bodyRtl = $('body').hasClass('rtl');
    if (bodyRtl) {
        $('body').addClass('rtl');
        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        $("html[lang=en]").attr("dir", "rtl");
        $(".select2-container").attr("dir", "rtl");
        $("#smartwizard").attr("dir", "rtl");
        $('body').removeClass('ltr');
        $("head link#style").attr("href", $(this));
        (document.getElementById("style").setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.rtl.min.css"));
        var carousel = $('.owl-carousel');
        $.each(carousel, function (index, element) {
            // element == this
            var carouselData = $(element).data('owl.carousel');
            carouselData.settings.rtl = true; //don't know if both are necessary
            carouselData.options.rtl = true;
            $(element).trigger('refresh.owl.carousel');
        });
        localStorage.setItem('yohartl', true)
        localStorage.removeItem('yohaltr')
    } 

})(jQuery);

$(document).on("click", '#myonoffswitch34', function () {
    if (this.checked) {
        ActiveSubmenu();
        $('body').removeClass('horizontal');
        $('body').removeClass('horizontal-hover');
        $(".main-content").removeClass("hor-content");
        $(".main-content").addClass("app-content");
        $(".main-container").removeClass("container");
        $(".main-container").addClass("container-fluid");
        $(".app-header").removeClass("hor-header");
        $(".hor-header").addClass("app-header");
        $(".app-sidebar").removeClass("horizontal-main")
        $(".main-sidemenu").removeClass("container")
        $(".slide-menu").removeClass("ps")
        $(".slide-menu").removeClass("ps--active-y")
        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        $('body').addClass('sidebar-mini');
        localStorage.removeItem("yohahorizontal");
        localStorage.removeItem("yohahorizontalHover");
        responsive();
        
        if(!(document.querySelector('.icontext-menu')!== null)){
            hovermenu();
        }
        
    } else {
        $('body').removeClass('sidebar-mini');
        localStorage.setItem("yohasidebar-mini", "False");
    }
});

// HORIZONTAL
$(document).on("click", '#myonoffswitch35', function () {
    if (this.checked) {
        if( !document.querySelector('.login-img') ){
            ActiveSubmenu();
            checkHoriMenu();
            responsive();
        }
        if(window.innerWidth>=992){
            let li = document.querySelectorAll('.side-menu li')
            li.forEach((e, i) => {
                e.classList.remove('is-expanded')
            })
            var animationSpeed = 300;
            // first level
            var parent = $("[data-bs-toggle='sub-slide']").parents('ul');
            var ul = parent.find('ul:visible').slideUp(animationSpeed);
            ul.removeClass('open');
            var parent1 = $("[data-bs-toggle='sub-slide2']").parents('ul');
            var ul1 = parent1.find('ul:visible').slideUp(animationSpeed);
            ul1.removeClass('open');
        }
        $('body').addClass('horizontal');
        $(".main-content").addClass("hor-content");
        $(".main-content").removeClass("app-content");
        $(".main-container").addClass("container");
        $(".main-container").removeClass("container-fluid");
        $(".app-header").addClass("hor-header");
        $(".hor-header").removeClass("app-header");
        $(".app-sidebar").addClass("horizontal-main")
        $(".main-sidemenu").addClass("container")
        $('body').removeClass('sidebar-mini');
        $('body').removeClass('sidenav-toggled');
        $('body').removeClass('horizontal-hover');
        $('body').removeClass('default-menu');
        $('body').removeClass('icontext-menu');
        $('body').removeClass('icon-overlay');
        $('body').removeClass('closed-leftmenu');
        $('body').removeClass('hover-submenu');
        $('body').removeClass('hover-submenu1');
        // // To enable no-wrap horizontal style
        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        localStorage.setItem("yohahorizontal", "true");
        localStorage.removeItem("yohasidebarMini");
        localStorage.removeItem("yohahorizontalHover");
        document.querySelector('.horizontal .side-menu')?.classList.add('flex-nowrap')
        // To enable wrap horizontal style
        // $('#slide-left').addClass('d-none');
        // $('#slide-right').addClass('d-none');
        // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
    }
});

// HORIZONTAL END

// HORIZONTAL HOVER
$(document).on("click", '#myonoffswitch111', function () {
    if (this.checked) {
        if( !document.querySelector('.login-img') ){
            checkHoriMenu();
            responsive();
        }
        if(window.innerWidth>=992){
            let li = document.querySelectorAll('.side-menu li')
            li.forEach((e, i) => {
                e.classList.remove('is-expanded')
            })
            var animationSpeed = 300;
            // first level
            var parent = $("[data-bs-toggle='sub-slide']").parents('ul');
            var ul = parent.find('ul:visible').slideUp(animationSpeed);
            ul.removeClass('open');
            var parent1 = $("[data-bs-toggle='sub-slide2']").parents('ul');
            var ul1 = parent1.find('ul:visible').slideUp(animationSpeed);
            ul1.removeClass('open');
        }
        $('body').addClass('horizontal-hover');
        $('body').addClass('horizontal');
        // $('#slide-left').addClass('d-none');
        // $('#slide-right').addClass('d-none');
        // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
        $('#slide-left').addClass('d-none');
        $('#slide-right').addClass('d-none');
        document.querySelector('.horizontal .side-menu')?.classList.add('flex-nowrap')
        $(".main-content").addClass("hor-content");
        $(".main-content").removeClass("app-content");
        $(".main-container").addClass("container");
        $(".main-container").removeClass("container-fluid");
        $(".app-header").addClass("hor-header");
        $(".app-header").removeClass("app-header");
        $(".app-sidebar").addClass("horizontal-main")
        $(".main-sidemenu").addClass("container")
        $('body').removeClass('sidebar-mini');
        $('body').removeClass('sidenav-toggled');
        $('body').removeClass('default-menu');
        $('body').removeClass('icontext-menu');
        $('body').removeClass('icon-overlay');
        $('body').removeClass('closed-leftmenu');
        $('body').removeClass('hover-submenu');
        $('body').removeClass('hover-submenu1');
        localStorage.setItem("yohahorizontalHover", "true");
        localStorage.removeItem("yohasidebarMini");
        localStorage.removeItem("yohahorizontal");
    } 
});

// HORIZONTAL HOVER END

/***************** CLOSEDMENU HAs Class *********************/
let bodyclosed = $('body').hasClass('closed-leftmenu');
if (bodyclosed) {
    $('body').addClass('closed-leftmenu');
    $('body').addClass('sidenav-toggled');
if(document.querySelector('.closed-leftmenu').classList.contains('login-page') !== true){
    hovermenu();
}
}
/***************** CLOSEDMENU HAs Class *********************/

/***************** ICONTEXT MENU HAs Class *********************/
let bodyicontext = $('body').hasClass('icontext-menu');
if (bodyicontext) {
    $('body').addClass('icontext-menu');
    $('body').addClass('sidenav-toggled');
if(document.querySelector('.icontext-menu').classList.contains('login-page') !== true){
    icontext();
}
}
/***************** ICONTEXT MENU HAs Class *********************/

/***************** ICONOVERLAY MENU HAs Class *********************/
let bodyiconoverlay = $('body').hasClass('icon-overlay');
if (bodyiconoverlay) {
    $('body').addClass('icon-overlay');
    $('body').addClass('sidenav-toggled');
if(document.querySelector('.icon-overlay').classList.contains('login-page') !== true){
    hovermenu();
}
}
/***************** ICONOVERLAY MENU HAs Class *********************/

/***************** HOVER-SUBMENU HAs Class *********************/
let bodyhover = $('body').hasClass('hover-submenu');
if (bodyhover) {
    $('body').addClass('hover-submenu');
    $('body').addClass('sidenav-toggled');
if(document.querySelector('.hover-submenu').classList.contains('login-page') !== true){
    hovermenu();
}
}
/***************** HOVER-SUBMENU HAs Class *********************/

/***************** HOVER-SUBMENU HAs Class *********************/
let bodyhover1 = $('body').hasClass('hover-submenu1');
if (bodyhover1) {
    $('body').addClass('hover-submenu1');
    $('body').addClass('sidenav-toggled');
if(document.querySelector('.hover-submenu1').classList.contains('login-page') !== true){
    hovermenu();
}
}
/***************** HOVER-SUBMENU HAs Class *********************/

// CHECK OPTIONS
function checkOptions() {
    "use strict";
    // rtl
	if (document.querySelector('body').classList.contains('rtl')) {
		$('#myonoffswitch24').prop('checked', true);
	}
	// horizontal
	if (document.querySelector('body').classList.contains('horizontal')) {
		$('#myonoffswitch35').prop('checked', true);
	}
	// horizontal-hover
	if (document.querySelector('body').classList.contains('horizontal-hover')) {
		$('#myonoffswitch111').prop('checked', true);
	}

    // light header 
    if (document.querySelector('body').classList.contains('header-light')) {
        $('#myonoffswitch6').prop('checked', true);
    }
    // color header 
    if (document.querySelector('body').classList.contains('color-header')) {
        $('#myonoffswitch7').prop('checked', true);
    }
    // gradient header 
    if (document.querySelector('body').classList.contains('gradient-header')) {
        $('#myonoffswitch20').prop('checked', true);
    }
    // dark header 
    if (document.querySelector('body').classList.contains('dark-header')) {
        $('#myonoffswitch8').prop('checked', true);
    }

    // light menu
    if (document.querySelector('body').classList.contains('light-menu')) {
        $('#myonoffswitch3').prop('checked', true);
    }
    // color menu
    if (document.querySelector('body').classList.contains('color-menu')) {
        $('#myonoffswitch4').prop('checked', true);
    }
    // gradient menu
    if (document.querySelector('body').classList.contains('gradient-menu')) {
        $('#myonoffswitch19').prop('checked', true);
    }
    // dark menu
    if (document.querySelector('body').classList.contains('dark-menu')) {
        $('#myonoffswitch5').prop('checked', true);
    }
}
checkOptions();

// RESET SWITCHER TO DEFAULT
function resetData() {
    "use strict";

    $('#myonoffswitch3').prop('checked', true);
    $('#myonoffswitch6').prop('checked', true);
    $('#myonoffswitch1').prop('checked', true);
    $('#myonoffswitch9').prop('checked', true);
    $('#myonoffswitch11').prop('checked', true);
    $('#myonoffswitch13').prop('checked', true);
    $('#myonoffswitch34').prop('checked', true);
    $('#myonoffswitch23').prop('checked', true);
    $('body')?.removeClass('dark-mode');
    $('body')?.removeClass('dark-menu');
    $('body')?.removeClass('color-menu');
    $('body')?.removeClass('light-menu');
    $('body')?.removeClass('gradient-menu');
    $('body')?.removeClass('dark-header');
    $('body')?.removeClass('color-header');
    $('body')?.removeClass('header-light');
    $('body')?.removeClass('gradient-header');
    $('body')?.removeClass('layout-boxed');
    $('body')?.removeClass('icontext-menu');
    $('body')?.removeClass('icon-overlay');
    $('body')?.removeClass('closed-leftmenu');
    $('body')?.removeClass('hover-submenu');
    $('body')?.removeClass('hover-submenu1');
    $('body')?.removeClass('sidenav-toggled');
    $('body')?.removeClass('scrollable-layout');
	$('body')?.removeClass('rtl');
	$('body')?.addClass('ltr');
    names();

    document.querySelector('html').setAttribute("dir","ltr");
    $(".select2-container").attr("dir", "ltr");
    $("#smartwizard").attr("dir", "ltr");

	// resetting horizontal to vertical
    if( !document.querySelector('.login-img') ){
        ActiveSubmenu();
        responsive();
    }
    $('body').removeClass('horizontal');
    $('body').removeClass('horizontal-hover');
    $(".main-content").removeClass("hor-content");
    $(".main-content").addClass("app-content");
    $(".main-container").removeClass("container");
    $(".main-container").addClass("container-fluid");
    $(".app-header").removeClass("hor-header");
    $(".hor-header").addClass("app-header");
    $(".app-sidebar").removeClass("horizontal-main")
    $(".main-sidemenu").removeClass("container")
    $(".slide-menu").removeClass("ps")
    $(".slide-menu").removeClass("ps--active-y")
    $('#slide-left').removeClass('d-none');
    $('#slide-right').removeClass('d-none');
    $('body').addClass('sidebar-mini');
    
    $("head link#style").attr("href", $(this));
    (document.getElementById("style").setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.min.css"));
}
// RTL STYLE START
$(document).on("click", '#myonoffswitch24', function () {
    if (this.checked) {
        $('body').addClass('rtl');

        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        $("html[lang=en]").attr("dir", "rtl");
        $(".select2-container").attr("dir", "rtl");
        $("#smartwizard").attr("dir", "rtl");
        $('body').removeClass('ltr');
        localStorage.setItem("yohartl", "true");
        localStorage.removeItem("yohaltr");
        $("head link#style").attr("href", $(this));
        (document.getElementById("style").setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.rtl.min.css"));
        var carousel = $('.owl-carousel');
        $.each(carousel, function (index, element) {
            // element == this
            var carouselData = $(element).data('owl.carousel');
            carouselData.settings.rtl = true; //don't know if both are necessary
            carouselData.options.rtl = true;
            $(element).trigger('refresh.owl.carousel');
        });
    }
});
// RTL STYLE END

// LTR STYLE START
$(document).on("click", '#myonoffswitch23', function () {
    if (this.checked) {
        $('body').addClass('ltr');

        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        $("html[lang=en]").attr("dir", "ltr");
        $(".select2-container").attr("dir", "ltr");
        $('body').removeClass('rtl');
        localStorage.setItem("yohaltr", "True");
        localStorage.removeItem("yohartl");
        $("head link#style").attr("href", $(this));
        (document.getElementById("style").setAttribute("href", "../assets/plugins/bootstrap/css/bootstrap.min.css"));
        var carousel = $('.owl-carousel');
        $.each(carousel, function (index, element) {
            // element == this
            var carouselData = $(element).data('owl.carousel');
            carouselData.settings.rtl = false; //don't know if both are necessary
            carouselData.options.rtl = false;
            $(element).trigger('refresh.owl.carousel');
        });
    }
});
// LTR STYLE END

