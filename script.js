var d = document;

var name;
var surname;
var birthday;
var mail;
var password;
var password2;

function check() {
    surname = d.getElementById('surname').value;
    name = d.getElementById('name').value;
    birthday = d.getElementById('birthday').value;
    mail = d.getElementById('mail').value;
    password =  d.getElementById('password').value;
    password2 = d.getElementById('password2').value;

    var re = new RegExp("[A-Za-zА-Яа-яЁё]{3,}");

    if (!re.test(String(name))) {
        alert('Некорректное имя');      
        return;  
    }

    re = new RegExp("[A-Za-zА-Яа-яЁё]{3,}");
    if (!re.test(String(surname))) {
        alert('Некорректная фамилия');       
        return;  
    }

    re = /^\d{2}[./-]\d{2}[./-]\d{4}$/;
    if (!re.test(String(birthday))) {
        alert('Некорректная дата рождения');
        return;        
    }

    if (password2 != password)
    {
        alert('Пароли не совпадают!');
        return;   
    }

    re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test(String(mail).toLowerCase())) {
        alert('Некорректная почта');     
        return;    
    }
    
}



window.addEventListener("scroll", function () {

    var block = document.getElementById('infinite-scroll');
    var counter = 1;

    var contentHeight = block.offsetHeight;      /* 1) высота блока контента вместе с границами*/
    var yOffset = window.pageYOffset;            /* 2) текущее положение скролбара*/
    var window_height = window.innerHeight;      /* 3) высота внутренней области окна документа*/
    var y = yOffset + window_height;

    // если пользователь достиг конца
    if (y >= contentHeight) {
        //загружаем новое содержимое в элемент
        block.innerHTML = block.innerHTML + "<div>На реальном сайте получаем новые данные, используя AJAX.";

    }
});

