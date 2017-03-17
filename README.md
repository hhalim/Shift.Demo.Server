# Shift.Demo.Server
A simple console app that contains a working Shift server. Demonstrate Shift server configuration, easy start up, dynamic assembly loading, and queue polling.

Refer to [Shift Quick Setup](https://github.com/hhalim/Shift/wiki/Quick-Start#infrastructure-setup) for link to Redis windows MSI installation package and how to setup the SQL database tables. Before running this app, if configure the Redis or SQL server connection string and cache usage in App.config file to match your installation settings. 

```
<connectionStrings>
   <!-- LOCAL SQL 
    <add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
    -->
    <add name="ShiftDBConnection" connectionString="localhost:6379" providerName="System.Data.Redis" />
</connectionStrings>

<appSettings>
    <add key="MaxRunableJobs" value="10" />
    <add key="ShiftPID" value="123"/>

    <add key="AssemblyFolder" value="client-assemblies\" />
    <!-- <add key="AssemblyListPath" value="client-assemblies\assemblylist.txt" /> -->

    <!-- 
    <add key="StorageMode" value="mssql" />
    -->
    <add key="StorageMode" value="redis" />

    <!-- Set to 0 or low 1 sec for StorageMode = redis-->
    <add key="ProgressDBInterval" value="00:00:00" />
    
    <!-- Shift AutoDelete -->
    <add key="AutoDeletePeriod" value="120"/>

    <!--
    <add key="UseCache" value="true" />
    <add key="CacheConfigurationString" value="localhost:6379"/>
    -->
</appSettings>
```

The storage connection string is required, if you're using SQL Server and Redis cache, set `UseCache` to true and the `CacheConfigurationString`. If you're using pure Redis storage, no cache is used, setting `UseCache` to true does not do anything.

## Menu
```
Shift Server Demo
1. Start Shift Server.
2. Stop Shift Server.
Press escape (ESC) key to exit.
```

Press the 1. key to start the server and 2. to stop server. Once Shift server starts, it will regularly poll the job queue and run available jobs. Run and use the [Shift.Demo.Client](https://github.com/hhalim/Shift.Demo.Client) for adding jobs. 
