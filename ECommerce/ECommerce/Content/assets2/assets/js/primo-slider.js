(function(){
	let ltr = document.querySelector('#myonoffswitch23');
	ltr.addEventListener('click', () => {
		let html = document.querySelector('html');
		let body = document.querySelector('body');
		let styleBoot = document.querySelector('#style');
			_bs('.carousel').create.trigger('ltr');
			html.setAttribute('dir', 'ltr');
			body.classList.add('ltr');
			body.classList.remove('rtl');
			styleBoot.setAttribute('href', '../assets/plugins/bootstrap/css/bootstrap.min.css');
	})
	let rtl = document.querySelector('#myonoffswitch24');
	rtl.addEventListener('click', () => {
		let html = document.querySelector('html');
		let body = document.querySelector('body');
		let styleBoot = document.querySelector('#style');
			_bs('.carousel').create.trigger('rtl');
			html.setAttribute('dir', 'rtl');
			body.classList.add('rtl');
			body.classList.remove('ltr');
			styleBoot.setAttribute('href', '../assets/plugins/bootstrap/css/bootstrap.rtl.min.css');
	})

	if(document.body.classList.contains('rtl')){
		let styleBoot = document.querySelector('#style');
		let html = document.querySelector('html');
		_bs('.carousel').create.trigger('rtl');
	 	html.setAttribute('dir', 'rtl');
		styleBoot.setAttribute('href', '../assets/plugins/bootstrap/css/bootstrap.rtl.min.css');
	}
})()

