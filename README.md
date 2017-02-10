# Shift.Demo.Server
A simple console app that contains a working Shift server. Demonstrate Shift server configuration, easy start up, dynamic assembly loading, and queue polling.

Before running this app, please configure the storage connection string, cache usage, and assembly folder location in App.config file.

## Menu
Shift Server Demo
1. Start Shift Server.
2. Stop Shift Server.
Press escape (ESC) key to exit.

Press the 1. key to start the server and 2. to stop server. Once Shift server starts, it will regularly poll the job queue and run available jobs. Run and use the [Shift.Demo.Client](https://github.com/hhalim/Shift.Demo.Client) for adding jobs. 
