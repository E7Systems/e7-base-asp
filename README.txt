A (very) brief overview of the folder structure.

3rdParty contains 3rd party dlls used everywhere (this is currently log4net and nUnit).
BusinessLayer contains the business layer code.
BusinessLayer_Tests contains unit tests for the Business Layer.
database contains all database scripts (currently just a create script)
DataLayer contains data access code.  
DataLayer_Tests contains unit tests for the Data Layer.
rsff_server_websetup will contain the setup project for creating an msi based installer (the folder is empty now).
web contains the web site.
web_deploy will contain a connector used with the setup project (it is empty now).


A (very) brief overview of getting the application going in Visual Studio 2010 (or later)
NOTE:  the db script is not working yet, so you will not be able to log in

Open the rsff_server.sln.
Manually add references to 3rdParty/log4net.dll to projects BusinessLayer, web, and DataLayer (one time only, this is due to a VS limitaion)
Manually add references to 3rdParty/nunit.framework.dll to projects BusinessLayer_Tests and DataLayer_Tests (one time only, this is due to a VS limitaion)
Compile the solution, it should compile without error.
Open the SQL Management Studio with Windows Authentication and execute database script database/rsffcreate.sql.  
Set the startup project to /web and the start page to Login.aspx.
Hit F5 and the app will come up.
Try and login with credentials dneary_admin/dneary_admin!!.  If you cannot login take a look at c:\logs\rsff.log.
