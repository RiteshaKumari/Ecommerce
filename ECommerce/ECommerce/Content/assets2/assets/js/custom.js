(function($) {
	"use strict";

	// ______________Accordion Style
	$(document).on("click", '[data-bs-toggle="collapse"]', function () {
		$(this).toggleClass('active').siblings().removeClass('active');
	});
	
	// ______________Full screen
	$(document).on("click", ".fullscreen-button", function toggleFullScreen() {
		if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
			if (document.documentElement.requestFullScreen) {
				document.documentElement.requestFullScreen();
			} else if (document.documentElement.mozRequestFullScreen) {
				document.documentElement.mozRequestFullScreen();
			} else if (document.documentElement.webkitRequestFullScreen) {
				document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
			} else if (document.documentElement.msRequestFullscreen) {
				document.documentElement.msRequestFullscreen();
			}
		} else {
			if (document.cancelFullScreen) {
				document.cancelFullScreen();
			} else if (document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			} else if (document.webkitCancelFullScreen) {
				document.webkitCancelFullScreen();
			} else if (document.msExitFullscreen) {
				document.msExitFullscreen();
			}
		}
	})

	// ______________ PAGE LOADING
	$(window).on("load", function(e) {
		$("#global-loader").fadeOut("slow");
	})
	
	// ______________ BACK TO TOP BUTTON
	$(window).on("scroll", function(e) {
		if ($(this).scrollTop() > 0) {
			$('#back-to-top').fadeIn('slow');
		} else {
			$('#back-to-top').fadeOut('slow');
		}
	});
	$(document).on("click", "#back-to-top", function(e) {
		$("html, body").animate({
			scrollTop: 0
		}, 0);
		return false;
	});
	
	
	// ______________ COVER IMAGE
	$(".cover-image").each(function() {
		var attr = $(this).attr('data-image-src');
		if (typeof attr !== typeof undefined && attr !== false) {
			$(this).css('background', 'url(' + attr + ') center center');
		}
	});
	
	// ______________Quantity Cart Increase & Descrease
	$(function () {
		$('.add').on('click',function(){
			var $qty=$(this).closest('div').find('.qty');
			var currentVal = parseInt($qty.val());
			if (!isNaN(currentVal)) {
				$qty.val(currentVal + 1);
			}
		});
		$('.remove').on('click',function(){
			var $qty=$(this).closest('div').find('.qty');
			var currentVal = parseInt($qty.val());
			if (!isNaN(currentVal) && currentVal > 0) {
				$qty.val(currentVal - 1);
			}
		});
	});
	
	// ______________Chart-circle
	if ($('.chart-circle').length) {
		$('.chart-circle').each(function() {
			let $this = $(this);
			$this.circleProgress({
				fill: {
					color: $this.attr('data-color')
				},
				size: $this.height(),
				startAngle: -Math.PI / 4 * 2,
				emptyFill: '#e3e8f7',
				lineCap: 'round'
			});
		});
	}
	
	// ______________ CARD
	const DIV_CARD = 'div.card';
	
	// ______________ FUNCTION FOR REMOVE CARD
	$(document).on('click', '[data-bs-toggle="card-remove"]', function(e) {
		let $card = $(this).closest(DIV_CARD);
		$card.remove();
		e.preventDefault();
		return false;
	});

	
    // ______________ FUNCTIONS FOR COLLAPSED CARD
    $(document).on('click', '[data-bs-toggle="card-collapse"]', function(e) {
        let $card = $(this).closest(DIV_CARD);
        $card.toggleClass('card-collapsed');
        e.preventDefault();
        return false;
    });
	
	// ______________ CARD FULL SCREEN
	$(document).on('click', '[data-bs-toggle="card-fullscreen"]', function(e) {
		let $card = $(this).closest(DIV_CARD);
		$card.toggleClass('card-fullscreen').removeClass('card-collapsed');
		e.preventDefault();
		return false;
	});

    // ___________TOOLTIP
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })

    // __________POPOVER
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function(popoverTriggerEl) {
            return new bootstrap.Popover(popoverTriggerEl)
        })
		
    // ______________ Toast
    var toastElList = [].slice.call(document.querySelectorAll('.toast'))
    var toastList = toastElList.map(function(toastEl) {
        return new bootstrap.Toast(toastEl)
    })
    $("#liveToastBtn").click(function() {
        $('#liveToast').toast('show');
    })
	
	
	// ______________Banner Skip
	$("#skip").click(function() {
        $(".banner").fadeOut();
      })
	  
	/*off canvas Style*/
	$('.off-canvas').on('click', function() {
		$('body').addClass('overflow-y-scroll');
		$('body').addClass('pe-0');
	});
	/*------ LTR END -------*/

	
	
	 // ______________ SWITCHER-toggle ______________//

     $('.layout-setting').on("click", function(e) {
		if (!(document.querySelector('body').classList.contains('dark-mode'))) {
			$('body').addClass('dark-mode');
			$('body').removeClass('light-mode');

            $('body')?.removeClass('color-menu');
            $('body')?.removeClass('gradient-menu');
            $('body')?.removeClass('light-menu');
            $('body')?.removeClass('color-header');
            $('body')?.removeClass('gradient-header');
            $('body')?.removeClass('header-light');

            $('#myonoffswitch5').prop('checked', true);
            $('#myonoffswitch8').prop('checked', true);

			localStorage.setItem('yohadarkMode', true);
			localStorage.removeItem('yohalightMode');
            $('#myonoffswitch2').prop('checked', true);
            
		} else {
			$('body').removeClass('dark-mode');
			$('body').addClass('light-mode');
            $('#myonoffswitch3').prop('checked', true);
            $('#myonoffswitch6').prop('checked', true);

			localStorage.setItem('yohalightMode', true);
			localStorage.removeItem('yohadarkMode');
            $('#myonoffswitch1').prop('checked', true);
		}
	});

	/******* Theme Style ********/


	//---- Dark mode ----- //
	// $('body').addClass('dark-mode');
	// $('body').removeClass('light-mode');


	/******* Header Styles ********/

	// $('body').addClass('header-light');
	// $('body').addClass('color-header');
	// $('body').addClass('dark-header');
	// $('body').addClass('gradient-header');


	/******* Menu Styles ********/

	// $('body').addClass('light-menu');	
	// $('body').addClass('color-menu');
	// $('body').addClass('dark-menu');
	// $('body').addClass('gradient-menu');


	/******* Full Width Layout Start ********/

	// $('body').addClass('layout-boxed');
	

	/******** *Header-Position Styles Start* ********/

	// $('body').addClass('scrollable-layout');


	// Sidemenu layout Styles //

	// ***** Icon with Text *****//
	// $('body').addClass('icontext-menu');
	// $('body').addClass('sidenav-toggled');
	// if(document.querySelector('.icontext-menu').firstElementChild.classList.contains('login-img') !== true){
	// 	icontext();
	// }

	// ***** Icon Overlay ***** //
	// $('body').addClass('icon-overlay');
	// $('body').addClass('sidenav-toggled');

	// ***** closed-leftmenu ***** //
	// $('body').addClass('closed-leftmenu');
	// $('body').addClass('sidenav-toggled');
	// if(document.querySelector('.closed-leftmenu').classList.contains('login-page') !== true){
	// hovermenu();
	// }

	// ***** hover-submenu ***** //
	// $('body').addClass('hover-submenu');
	// $('body').addClass('sidenav-toggled')
	// if(document.querySelector('.hover-submenu').firstElementChild.classList.contains('login-img') !== true){
	// hovermenu();
	// }

	// ***** hover-submenu style 1 ***** //
	// $('body').addClass('hover-submenu1');
	// $('body').addClass('sidenav-toggled')
	// if(document.querySelector('.hover-submenu1').firstElementChild.classList.contains('login-img') !== true){
	// hovermenu();
	// }


	/******* RTL VERSION *******/

	// $('body').addClass('rtl');

    let bodyRtl = $('body').hasClass('rtl');
    if (bodyRtl) {
        $('body').addClass('rtl');

        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        $("html[lang=en]").attr("dir", "rtl");
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
    } 


	/******* Navigation Style *******/

	// ***** Horizontal Click Menu ***** //

	// $('body').addClass('horizontal');

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
        document.querySelector('.horizontal .side-menu')?.classList.add('flex-nowrap')
        $('#slide-left').removeClass('d-none');
        $('#slide-right').removeClass('d-none');
        // To enable wrap horizontal style
        // document.querySelector('.horizontal .side-menu').style.flexWrap = 'wrap'
        // $('#slide-left').addClass('d-none');
        // $('#slide-right').addClass('d-none');
        
    } 

	// ***** Horizontal Hover Menu ***** //

	// $('body').addClass('horizontal-hover');

    function light() {
        if (document.querySelector('body').classList.contains('light-mode')) {
            $('#myonoffswitch8').prop('checked', true);
            $('#myonoffswitch12').prop('checked', true);
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
        
    }

})(jQuery);