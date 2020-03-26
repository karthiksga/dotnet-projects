Note that the data source controls make use of a connection string named MyConnectionString . You need to add the following fragment to the web.config file of your
application:

<configuration>
    <connectionStrings>
        <add connectionString=”server=YOUR_SERVER_NAME;initial catalog=ProductsDB;integrated security=SSPI” name=”MyConnectionString”/>
    </connectionStrings>
</configuration>