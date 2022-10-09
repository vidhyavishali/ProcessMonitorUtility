# ProcessMonitorUtility

This is a simple C# Utility to Monitor the given process. 
The application takes 3 command line arguments. 
1. Process name
2. Maximum allowed time for the process
3. Time interval for monitoring

Ex. Here, it considers Notepad, which lets the program run for 2 minutes(120 seconds) and the monitor checks every 10 seconds using System.Timers.Timer object.
![image](https://user-images.githubusercontent.com/38219645/194775916-fe0fb4a7-f082-43c5-bb2b-1c5f219a0a34.png)


If the monitor does not find any process running, it asks the user to continue monitoring, if yes, monitoring continues. If no, monitoring stops.
