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



# System Description

## Operations

<!-- See SRS template

Specify the normal and special operations required by the user such as:
(1) The various modes of operations in the user organization
(2) Periods of interactive operations and periods of unattended operations
(3) Data processing support functions
(4) Backup and recovery operations 

(Note:  This is sometimes specified as part of the User Interfaces section.)  If you separate this from the UI stuff earlier, then cover business process type stuff that would impact the design.  For instance, if the company brings all their systems down at midnight for data backup that might impact the design.  These are all the work tasks that impact the design of an application, but which might not be located in software. -->



## User Characteristics

Users are seperated into to distict groups, customers and staff members.
Customers are expected to be able to operate an internet browser if they want to use the online service. As the process of booking involves using a credit card, the customer must be an adult and own a credit card. Customers will also need to have some form of identification (i.e. passport) which will be required by the company to book the train/airplane tickets and the hotel. A passport will also be required for every participant. 
Staff members will also need to be able to operate an internet browser as the telesales interface will be in the form of a website. Since they will be mostly on the phone with customers while using the website, it is important they have a good understanding of how the system works and be able to navigate through it quickly so as not to have the customer wait on the phone for too long. Also, they must have some basic troubleshooting experience in order for them to be able to react accordingly on errors while they interact with a customer.


## Constraints

The system has certain constraints that limit the developer's options. The after a user registers, the password cannot be revealed to anyone, not even the owner of the account as they are stored in the database in an encrypted form. The only allowed actions on the password are reset and update. In order to ensure fast loading time and the necessary uptime the hardware needs to be of good quality, properly configured and provided with an uninterrupted power supply. Any compensations in this part will have it's effect on the overall performance of the system.



## Assumptions and Dependencies

The server must run the latest version of the Windows Server operating system running the Internet Information Services (IIS) version 7.0 in order to support the frameworks used by the system. Also, the software will also require to constantly update the exchange rate between British Pounds and Euros. For this it will facilitate the exchange rate information taken from the European Central Bank's (ECB) website, which means that any changes to the way ECB provides the exchange rates will have to be reflected within the software. Finally, for the checkout process the website will facilitate a Credit Card Checker API that will have to be available at the time of each purchase. The software will assume that the certificates and validations provided by the API are credible and any failed or fradulent purchases will rely on errors from the API.