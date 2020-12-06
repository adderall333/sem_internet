var d = document;

var name;
var surname;
var birthday;
var mail;
var password;
var password2;

function showHide(element_id) {
    //Если элемент с id-шником element_id существует
    if (document.getElementById(element_id)) { 
        //Записываем ссылку на элемент в переменную obj
        var obj = document.getElementById(element_id); 
        //Если css-свойство display не block, то: 
        if (obj.style.display != "block") { 
            obj.style.display = "block"; //Показываем элемент
        }
        else obj.style.display = "none"; //Скрываем элемент
    }
    //Если элемент с id-шником element_id не найден, то выводим сообщение
    else alert("Элемент с id: " + element_id + " не найден!"); 
}   

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

