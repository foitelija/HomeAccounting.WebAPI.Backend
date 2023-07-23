# HomeAccounting.WebAPI.Backend

**Необходимо разработать серверное приложение, формирующее домашнюю бухгалтерию. Предусмотреть учет расходов с различными категориями затрат и просмотр статистики. Предусмотреть аутентификацию в серверном приложении**
##

| Использовал | 
| ----------- | 
| С# .NET 6    | 
| DI   | 
| HealthChecks   | 
| Swagger   | 
| Асинхронность   | 
| JWT   | 
| Onion + Clean Architecture   | 
| Pagination в отчётах   | 

# Поставленные задачи
1.[✅] Пользователи.
 - Методы аутентификации, обновления токена и создания нового пользователя. Метод аутентификации должен возвращать структуру с JWT токеном для использования в остальных контроллерах.     

2.[✅] Категории затрат.
 - Методы создания, изменения и удаления категории. Если категория уже используется, ее нельзя удалять.

3.[✅] Затраты.
 - Методы создания, изменения, удаления затрат. Ограничить операции с затратой от пользователя, создавшего её.
   
4.[✅] Отчеты.
 - Метод получения всех затрат за выбранный месяц. Метод получения статистики за выбранный период.  В методе статистики за период показать процент и сумму трат по каждой категории и общую сумму для каждого члена семьи.  Оба метода с nullable фильтрами по пользователю и категориям.

5.[✅] Валюты.
   - Методы для получения текущего курса и конвертации. Получить курс валюты из любого открытого источника в сети. 

##

# Небольшое пояснение.

### Главным проектом является HomeAccounting.API, Healthcheck настроен.

**Представим, что наш главный проект, это какой-то микросервис, у нас это конечно не совсем микросервис, а мегасервис 😁, но ничего.** 

**Проект HomeAccounting.Monitoring настроен на мониторинг нашего главного проекта, через Healthcheck.**

**Если хотите запустить оба проекта, в настройках нужно поставить запуск 2-х проектов сразу, или через 2 среды, разные проекты 👍**
