# Shift.Demo.Server
A simple console app that contains a working Shift server. Demonstrate Shift server configuration, easy start up, dynamic assembly loading, and queue polling.

Open this solution in Visual Studio 2015, run and start the server, replace the connection string and the StorageMode settings according to your storage choice.

Refer to [Shift Quick Setup](https://github.com/hhalim/Shift/wiki/Quick-Start#infrastructure-setup) for link to Redis windows MSI installation package and how to setup the SQL database tables. 

```
  <connectionStrings>
    <!-- LOCAL SQL 
    <add name="ShiftDBConnection" connectionString="Data Source=localhost\SQL2014;Initial Catalog=ShiftJobsDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
    <add name="ShiftDBConnection" connectionString="mongodb://localhost" providerName="MongoDB" />
    <add name="ShiftDBConnection" connectionString="https://localhost:8081/" providerName="DocumentDB" />
    -->
    <add name="ShiftDBConnection" connectionString="localhost:6379" providerName="Redis" />
  </connectionStrings>

  <appSettings>
    <add key="MaxRunnableJobs" value="1" />
    <add key="ShiftWorkers" value="2" />

    <add key="AssemblyFolder" value="client-assemblies\" />
    <!-- <add key="AssemblyListPath" value="client-assemblies\assemblylist.txt" /> -->

    <!-- 
    <add key="StorageMode" value="mssql" />
    <add key="StorageMode" value="mongo" />
    <add key="StorageMode" value="documentdb" />
    <add key="DocumentDBAuthKey" value="C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==" />
    -->
    <add key="StorageMode" value="redis" />

    <!-- Set to 0 or low 1 sec for StorageMode = redis-->
    <add key="ProgressDBInterval" value="00:00:00" />

    <add key="ForceStopServer" value="true" />
    <add key="StopServerDelay" value="3000" />
    
    <!--
    <add key="AutoDeletePeriod" value="120" />
    <add key="PollingOnce" value="true" />
    -->
  </appSettings>

```

## Menu
```
Shift Server Demo
1. Start Shift Server.
2. Stop Shift Server.
Press escape (ESC) key to exit.
```

Press the #1 key to start the server and #2 to stop server. Once Shift server starts, it will regularly poll the job queue and run available jobs. Run and use the [Shift.Demo.Client](https://github.com/hhalim/Shift.Demo.Client) for adding jobs and view jobs' progress. 
