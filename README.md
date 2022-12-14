# ПМСОФТ Тестовое задание

### Для запуска при помощи Docker

- Клонировать репозиторий
- перейти в папку, в которую клонирован репозиторий и открыть в ней терминал
- создать HTTPS сертификат
         
         dotnet dev-certs https --clean
         
         dotnet dev-certs https -ep ./backend/conf.d/https/pmsoft.pfx -p Password123!
         
         dotnet dev-certs https --trust
         
- запустить docker-compose

      docker-compose up --build
- подождать пока запустится и перейти на страницу: "http://localhost:3000/"


### Рекомендации по использованию системы:
1. Для просмотра всех записей (книг) нужно перейти во вкладку Books в навигационной панеле.
2. Для регистрации и авторизации необходимо перейти во вкладку Login (При помощи переключателя можно менять форму входа на форму регистрации и наоборот).
При выборе Admin Mode, зарегистрированный пользователь будет иметь расширенный функционал по сравнению с неустановившим Admin Mode, который позволяет создавать, изменять и удалять записи. 
3. Для создания новой записи необходимо авторизоваться за админа и перейти во вкладку 'Books', в которой под заголовком 'Book Table' необходимо нажать на кнопку 'Create Book', после чего появится окно с формой, которое необходимо заполнить и нажать 'Submit' для создания.
4. Для обновления записи необходимо авторизоваться за админа и перейти во вкладку 'Books', найти в таблице необходимую запись и нажать на кнопку 'Edit' в столбце 'Operations', после чего появится окно с формой, которое необходимо заполнить и нажать 'Submit' для обновления.
5. Для удаления записи необходимо авторизоваться за админа и перейти во вкладку 'Books', найти в таблице необходимую запись и нажать на кнопку 'Delete' в столбце 'Operations', после чего запись удалится.
6. Для удаления нескольких записей необходимо авторизоваться за админа и перейти во вкладку 'Books', выбрать в первом столбце таблицы необходимые записи и нажать на появившуюся кнопку 'Delete Choosed' (пропадает если нет ни одной выбранной записи) над первом столбцом таблицы, после чего выбранные записи удалятся.
             
        По умолчанию создаётся один админ со следующими данными username: 'admin', password: 'admin'

### Технологии:
#### React (TypeScript) + .NET 5 + PostgreSQL (версия .NET и база данных выбраны исходя из используемых в компании) + Docker
#### .NET
* Аутентицикация и авторизация при помощи JWT
* Repository Pattern
* Service Layer
* Global Error Handling
* Async Code
* ASP.NET Core Identity
* Unit Testing (xUnit + FakeItEasy + FluentAssertions)

#### React (TypeScript)
* React-Router
* Formik
* React-Bootstrap
* Function Components
* Axios (для отправки запросов)

#### PostgreSQL (база данных создана при помощи Entity Framework Core)
* Таблица с книгами
* Таблицы, созданные при помощи ASP.NET Core Identity




