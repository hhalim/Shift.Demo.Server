# Shift.Demo.Server
A simple console app that contains a working Shift server. Demonstrate Shift server configuration, easy start up, dynamic assembly loading, and queue polling.

Before running this app, please configure the storage connection string, cache usage, and assembly folder location in App.config file. Refer to [Shift Quick Setup](https://github.com/hhalim/Shift/wiki/Quick-Start#infrastructure-setup) for link to Redis windows MSI installation package and how to setup the SQL database tables.

```
<connectionStrings>
  <add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
</connectionStrings>

<appSettings>
  <add key="MaxRunableJobs" value="10" />
  <add key="AssemblyFolder" value="client-assemblies\" />
  <!-- <add key="AssemblyListPath" value="client-assemblies\assemblylist.txt" /> -->
  <add key="ShiftPID" value="-123"/>

  <add key="UseCache" value="true" />
  <add key="CacheConfigurationString" value="localhost:6379"/>

  <!-- Shift AutoDelete -->
  <add key="AutoDeletePeriod" value="120"/>
</appSettings>
```

The database connection string is required, if you're using Redis cache, set `UseCache` to true and the `CacheConfigurationString`.

## Menu
```
Shift Server Demo
1. Start Shift Server.
2. Stop Shift Server.
Press escape (ESC) key to exit.
```

Press the 1. key to start the server and 2. to stop server. Once Shift server starts, it will regularly poll the job queue and run available jobs. Run and use the [Shift.Demo.Client](https://github.com/hhalim/Shift.Demo.Client) for adding jobs. 
