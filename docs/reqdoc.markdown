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
Requirements
------------------------

Functional Requirements
This section outlines the Functional requirements of the system.

General Requirements
The system must:
•	Be a web based system
•	Deal with resort dependant prices for transfers to and from resort, Ski Hire, Ski-lift passes, Snowboard hire (Prices in Euros).
•	Keep a record of all:
o	Bookings
o	Payments
Interfaces
The system must:
•	Include an interface for telesales staff – to enable staff to record bookings taken over the phone.
•	Include a Staff Administrator interface to:
o	Change prices of hotels, travel and extras.
o	Add new resorts.
Bookings
The system must:
•	Include facilities to book:
o	Travel
o	Accommodation
o	Activities
o	Transfers to and from resort
o	Ski Hire
o	Ski-lift passes
o	Snowboard hire
•	Not force users to be logged in before making a booking.
•	Allow customers to book either the budget, standard or luxury hotels at each resort location.
•	Take into account if the booking is made during peak/off-peak season.
•	Only allow customers to travel by air from Stansted or train from St. Pancras.
•	Only allow flights to be booked on Wednesdays and Saturdays.
•	Only allow bookings for either 7 nights or 14 nights.
•	Offer a 10% discount for 14 night stays.
•	Charge 20% extra for single rooms.
•	Ensure both travel and accommodation are booked together.
•	Charge travel and accommodation in pounds sterling.
•	Allow holiday extras (transfers to and from resort, Ski Hire, Ski-lift passes, Snowboard hire) to be booked at the time of booking or at a later date.
•	Give loyalty discounts to customers who have previously booked with the company before.
•	Make group discounts available to clubs and schools. (Minimum of 10 people)
•	Allow customers booking 6 weeks before to either:
o	Pay a 25% deposit at the time of booking, with the remaining balance due 6 weeks before departure.
o	Pay the balance in full.
•	Indicate that 25% of the booking charge is taken as a non-refundable deposit.
Cancelations
The system must:
•	Allow cancelations from the telesales interface.
•	Charge a 25% cancelation charge; this is done through the non-refundable deposit.
Exchange Rates
The system must:
•	Convert any Euros prices to sterling according to exchange rate on the day of booking.
Login/Registration
The System must:
•	Enable customers should be able to register an account on the website to enable them to receive loyalty discounts.
Website Pages
The following pages must be included:
•	Login/Registration Page
•	Information Pages
o	Resort and County Features (Things to do)
o	Hotels (Resort 1, Resort 2, Resort 3)
•	Booking Pages
o	Travel (Air, Train)
o	Hotel/Bookings
o	Facilities/Extras (Ski Hire…)
•	Current Currency Conversion Rate Page
•	Contact Page
•	Accounts Page
•	Telesales Page
•	Admin Page

Non-functional Requirements
The bullet points below outline the necessary non-functional requirements of the system. 
•	Privacy
o	 Any information entered by customers into the site must be kept private.
•	Security
o	Any passwords should be encrypted.
o	Any inputs into the database should be tied down to ensure no erroneous inputs can be entered, for example drop database.
•	Standards
o	All code developed for the system should follow W3C web standards and a pre-defined coding naming standard for any code written for the site.
•	Reliability
o	The system should be robust, and should not experience any un-necessary down time.
•	Performance
o	The pages should load within a reasonable amount of time
•	Usability
o	The website should be available on the following browsers and devices
o	Browsers
	Internet Explorer
	Mozilla Firefox
	Google Chrome
	Apple Safari
o	Devices
	Iphone, Ipad, Ipod Touch
	Android Phones
