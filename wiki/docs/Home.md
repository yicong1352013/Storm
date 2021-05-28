## For those who are looking for a similar tool that works on both WCF and Web services, try this out : [WCF Storm](http://www.wcfstorm.com/wcf/default.aspx)

**Project Description**
STORM is a free and open source tool for testing web services. 

It is written mostly in F#. (I love this language!)

STORM allows you to
  1. Test web services written using any technology (.NET , Java, etc.)
  2. Dynamically invoke web service methods even those that have input parameters of complex data types
  3. Save development time and money.  Creating throw-away test client apps just to test the web service is just too wasteful 
  4.  Test multiple web services from within one UI.
  5.  Edit/Manipulate the raw soap requests.
  6  Others (Try out the tool and find out yourself!)

The inspiration for this tool is the .NET Web Service Studio tool originally released at the now closed gotdotnet site.

Side note:
  * STORM was supposed to be an acronym. "ST" could stand Soap Testing, but I cant think of any meaning for "ORM".  If you have any ideas, please suggest.
  * Another reason, I wanted to call this tool "Storm" is that, oddly enough, I miss the storms (~ 20x/year) that frequently visit my little island hometown of Catanduanes -  which meteorologists have  nicknamed "Land of the Howling Winds" - in the Philippines 
 

----------------------------------------------------------------------------------
_Commercial Web Service Test Tools_ : **$300+**
_Storm_ : **FREE**
_Keeping an open source project alive_ : **PRICELESS.**   
## [Please Donate!](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=erik%2earaojo%40hotmail%2ecom&item_name=STORM%20Project%20Donation&no_shipping=1&return=http%3a%2f%2fwww%2ecodeplex%2ecom%2fstorm&cancel_return=http%3a%2f%2fwww%2ecodeplex%2ecom%2fstorm&tax=0&currency_code=USD&lc=US&bn=PP%2dDonationsBF&charset=UTF%2d8)
---------------------------------------------------------------------------------

**+Storm showing multiple open tabs and a test case that failed.+**
![](Home_StormCompare.png )

Requirements :
   .NET Framework 2.0
   F# 1.9.3.14 (OPTIONAL) -  Releases labeled with ""standalone" doesn't need F# to be installed on the machine 


**QUICK GUIDE**

1. Add a web service

Click Add -> type in the wsdl endpoint URL.

+Sample URLs+
- [http://www.deeptraining.com/webservices/weather.asmx](http://www.deeptraining.com/webservices/weather.asmx)
- [http://api.google.com/GoogleSearch.wsdl](http://api.google.com/GoogleSearch.wsdl)
-[http://webservices.imacination.com/distance/Distance.jws?wsdl](http://webservices.imacination.com/distance/Distance.jws?wsdl)

2. Select a web method (TreeView on the left side).  This will cause the tool to analyze the web method
   and display the parameters (if any) needed to invoke the method

3, Select a web method parameter and provide the necessary values
4 Click _Go_ (Green arrow button). This will invoke the webmethod.