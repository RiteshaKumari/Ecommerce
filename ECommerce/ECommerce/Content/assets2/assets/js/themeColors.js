const handleThemeUpdate = (cssVars) => {
    const root = document.querySelector(':root');
    const keys = Object.keys(cssVars);
    keys.forEach(key => {
        root.style.setProperty(key, cssVars[key]);
    });
}
 
function dynamicPrimaryColor(primaryColor) {
    'use strict'
    
    primaryColor.forEach((item) => {
        item.addEventListener('input', (e) => {
            const cssPropName = `--primary-${e.target.getAttribute('data-id')}`;
            const cssPropName1 = `--primary-${e.target.getAttribute('data-id1')}`;
            const cssPropName2 = `--primary-${e.target.getAttribute('data-id2')}`;
            const cssPropName7 = `--primary-${e.target.getAttribute('data-id7')}`;
            handleThemeUpdate({
                [cssPropName]: e.target.value,
                // 95 is used as the opacity 0.95  
                [cssPropName1]: e.target.value + 95,
                [cssPropName2]: e.target.value,
                [cssPropName7]: e.target.value + 20,
            });
        });
    });
}

function dynamicPrimaryBackground(bgColor) {
  bgColor.forEach((item) => {
      item.addEventListener('input', (e) => {
          const cssPropName3 = `--dark-${e.target.getAttribute('data-id3')}`;
          const cssPropName4 = `--dark-${e.target.getAttribute('data-id4')}`;
          handleThemeUpdate({
              [cssPropName3]: e.target.value+ 'dd',
              [cssPropName4]: e.target.value,
          });
      });
  });
}


(function () {
    "use strict";
  
    // Light theme color picker
      // const LightThemeSwitchers = document.querySelectorAll('.light-mode .switch_section span');
      const dynamicPrimaryLight = document.querySelectorAll('input.color-primary-light');
      const dynamicBackground = document.querySelectorAll('input.background-primary-light');
  
      // themeSwitch(LightThemeSwitchers);
      dynamicPrimaryColor(dynamicPrimaryLight);
      dynamicPrimaryBackground(dynamicBackground);
  
      localStorageBackup();
  
  })();
  
function localStorageBackup() {
    'use strict'

    // if there is a value stored, update color picker and background color
    // Used to retrive the data from local storage
    if (localStorage.yohaprimaryColor) {
        // document.getElementById('colorID').value = localStorage.yohaprimaryColor;
        document.querySelector('html').style.setProperty('--primary-bg-color', localStorage.yohaprimaryColor);
        document.querySelector('html').style.setProperty('--primary-bg-hover', localStorage.yohaprimaryHoverColor);
        document.querySelector('html').style.setProperty('--primary-bg-border', localStorage.yohaprimaryBorderColor);
    }
    if (localStorage.yohabgColor) {
        // document.getElementById('bgID').value = localStorage.yohabgColor;
        document.querySelector('html').style.setProperty('--dark-body', localStorage.yohabgColor);
        document.querySelector('html').style.setProperty('--dark-theme', localStorage.yohathemeColor);
        document.body.classList.add('dark-mode');
        document.body.classList.remove('light-mode');
        $('#myonoffswitch2').prop('checked', true);
        $('#myonoffswitch5').prop('checked', true);
        $('#myonoffswitch8').prop('checked', true);
    }

    if(localStorage.yohalightMode){
        document.querySelector('body')?.classList.add('light-mode');
		document.querySelector('body')?.classList.remove('dark-mode');
        $('#myonoffswitch1').prop('checked', true);
        $('#myonoffswitch3').prop('checked', true);
        $('#myonoffswitch6').prop('checked', true);
    }
    if(localStorage.yohadarkMode){
        document.querySelector('body')?.classList.remove('light-mode');
		document.querySelector('body')?.classList.add('dark-mode');
        $('#myonoffswitch2').prop('checked', true);
        $('#myonoffswitch5').prop('checked', true);
        $('#myonoffswitch8').prop('checked', true);
    }

    if(localStorage.yohahorizontal){
        document.querySelector('body').classList.add('horizontal')
    }
    
    if(localStorage.yohahorizontalHover){
        document.querySelector('body').classList.add('horizontal-hover')
    }

    if(localStorage.yohartl){
        document.querySelector('body').classList.add('rtl')
    }
    
    if(localStorage.yohaclosedmenu){
        document.querySelector('body').classList.add('closed-leftmenu')
        $("#myonoffswitch16").prop("checked", true);
    }

    if(localStorage.yohaicontextmenu){
        document.querySelector('body').classList.add('icontext-menu')
        $("#myonoffswitch14").prop("checked", true);
    }

    if(localStorage.yohaiconoverlay){
        document.querySelector('body').classList.add('icon-overlay')
        $("#myonoffswitch15").prop("checked", true);
    }

    if(localStorage.yohahoversubmenu){
        document.querySelector('body').classList.add('hover-submenu')
        $("#myonoffswitch17").prop("checked", true);
    }

    if(localStorage.yohahoversubmenu1){
        document.querySelector('body').classList.add('hover-submenu1')
        $("#myonoffswitch18").prop("checked", true);
    }

    if(localStorage.yohaboxedwidth){
        document.querySelector('body').classList.add('layout-boxed')
        $("#myonoffswitch10").prop("checked", true);
    }

    if(localStorage.yohascrollable){
        document.querySelector('body').classList.add('scrollable-layout')
        $("#myonoffswitch12").prop("checked", true);
    }

    // MENUS

    // LIGHT MENU
    if (localStorage.yohaLightmenu) {
        document.querySelector("body")?.classList.add("light-menu");
        document.querySelector("body")?.classList.remove("color-menu");
        document.querySelector("body")?.classList.remove("dark-menu");
        document.querySelector("body")?.classList.remove("gradient-menu");
        $("#myonoffswitch3").prop("checked", true);
    }

    // DARK MENU
    if (localStorage.yohaDarkmenu) {
        document.querySelector("body")?.classList.add("dark-menu");
        document.querySelector("body")?.classList.remove("light-menu");
        document.querySelector("body")?.classList.remove("color-menu");
        document.querySelector("body")?.classList.remove("gradient-menu");
        $("#myonoffswitch5").prop("checked", true);
    }

    // COLOR MENU
    if (localStorage.yohaColormenu) {
        document.querySelector("body")?.classList.add("color-menu");
        document.querySelector("body")?.classList.remove("light-menu");
        document.querySelector("body")?.classList.remove("dark-menu");
        document.querySelector("body")?.classList.remove("gradient-menu");
        $("#myonoffswitch4").prop("checked", true);
    }

    // GRADIENT MENU
    if (localStorage.yohaGradientmenu) {
        document.querySelector("body")?.classList.add("gradient-menu");
        document.querySelector("body")?.classList.remove("light-menu");
        document.querySelector("body")?.classList.remove("dark-menu");
        document.querySelector("body")?.classList.remove("color-menu");
        $("#myonoffswitch19").prop("checked", true);
    }

    // HEADERS

    // LIGHT HEADER
    if (localStorage.yohaLightheader) {
        document.querySelector("body")?.classList.add("header-light");
        document.querySelector("body")?.classList.remove("color-header");
        document.querySelector("body")?.classList.remove("dark-header");
        document.querySelector("body")?.classList.remove("gradient-header");
        $("#myonoffswitch6").prop("checked", true);
    }

    // DARK HEADER
    if (localStorage.yohaDarkheader) {
        document.querySelector("body")?.classList.add("dark-header");
        document.querySelector("body")?.classList.remove("header-light");
        document.querySelector("body")?.classList.remove("color-header");
        document.querySelector("body")?.classList.remove("gradient-header");
        $("#myonoffswitch8").prop("checked", true);
    }

    // COLOR HEADER
    if (localStorage.yohaColorheader) {
        document.querySelector("body")?.classList.add("color-header");
        document.querySelector("body")?.classList.remove("header-light");
        document.querySelector("body")?.classList.remove("dark-header");
        document.querySelector("body")?.classList.remove("gradient-header");
        $("#myonoffswitch7").prop("checked", true);
    }

    // GRADIENT HEADER
    if (localStorage.yohaGradientheader) {
        document.querySelector("body")?.classList.add("gradient-header");
        document.querySelector("body")?.classList.remove("header-light");
        document.querySelector("body")?.classList.remove("dark-header");
        document.querySelector("body")?.classList.remove("color-header");
        $("#myonoffswitch20").prop("checked", true);
    }
}

// triggers on changing the color picker
function changePrimaryColor() {
    'use strict'

    $('#myonoffswitch3').prop('checked', true);
    $('#myonoffswitch6').prop('checked', true);
    checkOptions();

    var userColor = document.getElementById('colorID').value;
    localStorage.setItem('yohaprimaryColor', userColor);
    // to store value as opacity 0.95 we use 95
    localStorage.setItem('yohaprimaryHoverColor', userColor + 95);
    localStorage.setItem('yohaprimaryBorderColor', userColor);
    localStorage.setItem('yohaprimaryTransparent', userColor + 20);
    names()
}

// triggers on changing the color picker
function changeBackgroundColor() {

  var userColor = document.getElementById('bgID').value;
  localStorage.setItem('yohabgColor', userColor + 'dd');
  localStorage.setItem('yohathemeColor', userColor);
  names()

  document.body.classList.add('dark-mode');
  document.body.classList.remove('light-mode');
  $('#myonoffswitch2').prop('checked', true);
  $('#myonoffswitch5').prop('checked', true);
  $('#myonoffswitch8').prop('checked', true);

  localStorage.setItem("yohadarkMode", true);
}

// to check the value is hexa or not
const isValidHex = (hexValue) => /^#([A-Fa-f0-9]{3,4}){1,2}$/.test(hexValue)

const getChunksFromString = (st, chunkSize) => st.match(new RegExp(`.{${chunkSize}}`, "g"))
    // convert hex value to 256
const convertHexUnitTo256 = (hexStr) => parseInt(hexStr.repeat(2 / hexStr.length), 16)
    // get alpha value is equla to 1 if there was no value is asigned to alpha in function
const getAlphafloat = (a, alpha) => {
        if (typeof a !== "undefined") { return a / 255 }
        if ((typeof alpha != "number") || alpha < 0 || alpha > 1) {
            return 1
        }
        return alpha
    }
    // convertion of hex code to rgba code 
function hexToRgba(hexValue, alpha) {
    'use strict'

    if (!isValidHex(hexValue)) { return null }
    const chunkSize = Math.floor((hexValue.length - 1) / 3)
    const hexArr = getChunksFromString(hexValue.slice(1), chunkSize)
    const [r, g, b, a] = hexArr.map(convertHexUnitTo256)
    return `rgba(${r}, ${g}, ${b}, ${getAlphafloat(a, alpha)})`
}


let myVarVal, myVarVal1, myVarVal2, myVarVal3

function names() {
    'use strict'

    let primaryColorVal = getComputedStyle(document.documentElement).getPropertyValue('--primary-bg-color').trim();

    //get variable
    myVarVal = localStorage.getItem("yohaprimaryColor") || primaryColorVal;
    myVarVal1 = localStorage.getItem("yohaprimaryColor") || "#05c3fb";
    myVarVal2 = localStorage.getItem("yohaprimaryColor") || null;
    myVarVal3 = localStorage.getItem("yohaprimaryColor")|| null;

    if(document.querySelector('#sales') !== null){
        sales();
    }

    if(document.querySelector('#canvasDoughnut1') !== null){
        canvasDoughnut1();
    }
    
    if(document.querySelector('#pieChart') !== null){
        pieChart();
    }

    if(document.querySelector('#widget1') !== null){
        appwidget1();
    }

    if(document.querySelector('#learners') !== null){
        learnerschart();
    }

    if(document.querySelector('#devices') !== null){
        devices();
    }

    if(document.querySelector('#total-Installed') !== null){
        totalInstalled();
    }

    if(document.querySelector('#widgetChart1') !== null){
        widgetChart1();
    }

    if(document.querySelector('#rate') !== null){
        rate();
    }

    if(document.querySelector('#sites') !== null){
        sites();
    }

    if(document.querySelector('#echart') !== null){
        AduienceOverview();
    }
    
    if(document.querySelector('#canvasDoughnut2') !== null){
        canvasDoughnut2();
    }

    if(document.querySelector('#morrisBarchart1') !== null){
        morrisBarchart1();
    }

    if(document.querySelector('#countries') !== null){
        countries();
    }

    if(document.querySelector('#storage-usage') !== null){
        storage();
    }
    
    let colorData = hexToRgba(myVarVal || "#525ce5", 0.1)
    document.querySelector('html').style.setProperty('--primary01', colorData);

    let colorData1 = hexToRgba(myVarVal || "#525ce5", 0.2)
    document.querySelector('html').style.setProperty('--primary02', colorData1);

    let colorData2 = hexToRgba(myVarVal || "#525ce5", 0.3)
    document.querySelector('html').style.setProperty('--primary03', colorData2);

    let colorData3 = hexToRgba(myVarVal || "#525ce5", 0.5)
    document.querySelector('html').style.setProperty('--primary05', colorData3);

    let colorData4 = hexToRgba(myVarVal || "#525ce5", 0.6)
    document.querySelector('html').style.setProperty('--primary06', colorData4);

    let colorData5 = hexToRgba(myVarVal || "#525ce5", 0.7)
    document.querySelector('html').style.setProperty('--primary07', colorData5);

    let colorData6 = hexToRgba(myVarVal || "#525ce5", 0.8)
    document.querySelector('html').style.setProperty('--primary08', colorData6);

    let colorData7 = hexToRgba(myVarVal || "#525ce5", 0.9)
    document.querySelector('html').style.setProperty('--primary09', colorData7);

    let colorData8 = hexToRgba(myVarVal || "#525ce5", 0.05)
    document.querySelector('html').style.setProperty('--primary005', colorData8);

}
names()

