Requirements Document
------------------------



Product Perspective
===================

System Interfaces
-----------------


Interfaces
----------

The system will have two main interfaces, a website for customers and a telesales interface for staff. The website will allow customers to browse the available options in terms of travelling and accomodation, create orders and make payments. Using the website, customers will be able to login or register for an account, which can be used to browse through past bookings and edit the details of current orders. The telesales interface, will allow staff to create new orders while on the phone with customers, examine and moderate existing orders and alter website settings. It will also allow staff to view or alter details on customers and notify customers of any outstanding payments. Finally, after a customer's request the operators will be able to cancel any current order.




------------------------
Hardware Interfaces
------------------------
The system has no hardware interface requirements other than the server machine or cloud virtual machine that is capable of running IIS 7.* and a client machine that is capable of running a modern web browser.


------------------------
Software Interfaces
------------------------
1.ASP.NET 4.0 Framework. The system will use ASP.NET framework for the underlying mechanisms for a dynamic web site.
2.ASP.NET MVC 3. The system will use the ASP.NET extension MVC which stands for Model View Controller. The extension will also define the software architecture of the project as MVC 
3.Microsoft SQL Server 2008 Express. The system will use Microsoft SQL Server 2008 Express as its database component.
4.ADO.NET. The system will utilize the auto generated classes from ADO.NET for communicating with the database.
5.ECB SDMX. The European Central Bank exchange rate web service that uses "Statistical Data and Metadata eXchange" standard will be used for receiving the exchange rates at the times of purchases.
6.JQuery 1.6.*. This javascript library will be used for multiple purposes such as DOM manipulation, event handling, animations, Ajax calls.
7.JQuery UI 1.8.*. The User interfaces plugin of Jquery will be used to easily make user interface elements such as dialog boxes and datepickers.




------------------------
Entities
------------------------
Resorts: These are the destinations for travel which have different types of hotels.
Hotels: Hotels can either be of type budget, standard or luxury.
Travel: A part of the holiday package which can be either 'train' or 'flight'.
Holiday package: Consists of the hotel and the travel arrangements.
Booking: This is formed when a customer purchases holiday package.
Extras: These are the additional activities that the customers may want to book such as ski hire. A resort may have any number of extras.
Customer: Customers are the people who are using the system to purchase holiday packages. Customers can also have login details consisting of an email address and a password which they can use the see their booking records and details.
Staff: Is super-type for admins and telesales personnel.
Admin: Admins are capable of editing or creating the content such as the travel package details.
Telesales staff: These users record modify or cancel bookings for customers over the phone. 






