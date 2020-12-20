## Семестровая работа по информатике
### Тема: интернет
### Команда:
* [Сагадеев Артем](https://github.com/adderall333) (dev1)
* [Галиева Раилина](https://github.com/RaiRG) (dev2)
* [Туктаров Дамир](https://github.com/demxk) (po)

---

### Детали:
* нельзя использовать ASPNET Core MVC
* нельзя использовать Entity Framework
* требуется реализовать свою авторизацию
* требуется реализовать свой доступ к бд (DAO или Repository)

### Использованные технологии:
* ASP NET Core
* Razor Pages
* PostgreSQL
* Npgsql
* Bootstrap
* JQuery
* FetchAPI

---

#### Интеграция базы данных:
* Нужно создать новую пустую базу данных (postgres) и применить в ней SQL из файла "database" (находится в корне репозитория).
* Когда база данных создана, вставьте своё имя пользователя, пароль (в postgres) и название созданной бд в 14-ой строке файла ArmchairExpertsCom/Models/Utilities/ORM.cs
