## Semester work
### Theme: internet
### Task: internet service about books, films and serials with evaluatings, reviews, selections etc.
### Team:
* [Artem](https://github.com/adderall333) (backend)
* [Railina](https://github.com/RaiRG) (frontend)
* [Damir](https://github.com/demxk) (product owner)

---

### Details:
* we can't use ASPNET Core MVC
* we can't use Entity Framework
* we need to implement our own auth system
* we need to implement DAO or ORM

### Technologies:
* ASP NET Core
* Razor Pages
* PostgreSQL
* Npgsql
* Bootstrap
* JQuery
* FetchAPI

---

#### How to integrate database with our test data:
* You need to create a new database (postgres) and run there SQL code from file named "database" (located in the root of repo).
* Then, you need to add your postgres username, password and name of database you created at the previous step into 14-th row of "ArmchairExpertsCom/Models/Utilities/ORM.cs"
