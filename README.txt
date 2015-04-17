Installation Procedure
1.  Open SQL Server Management Studio (2005 or later)
2.  From git, get the sql script rsffcreate.sql.  It is in /database.  Execute the script in SQL Server Management Studio (2005 or later).  
    This will create all the database objects needed.  It will not, however create any data so you will not be able to to log in (as we discussed).
    However you can navigate directly to /parcel or /project without logging in for now.
3.  Open the rsff_server.sln Solution using Visual Studio 2013.
4.  In the web_mvc project, open the web.config file and search for   <connectionStrings>.  There will be 1 element inside <connectionStrings>
    Modify the connection string Rsff.Db.ConnectionString (specifically the Data Source attribute) with the IP address of your SQL Server.
5.  If you want to run the unit tests, repeat step 4 for the App.Config files in projects BusinessLayer_Tests and DataLayer_Tests.  
    Also run the rsff_unitestingcreate.sql script to create the unit testing database (this has some test only stored procs in it).
6.  Verify SQL Server Connectivity.  A good way to do this is to run the tests.
7.  Run the application by hitting F5.
8.  You will be taken to the login page.  Since the db does not have data in it, you will not be able to log in.  However
    you can navigate directly to /parcel or /project without logging in for now.
9.  Take a look at c:\logs\rsff_web_mvc.log, it should help a lot with diagnostics.
