Capgemini UK work experience miniproject
A basic C# console project, to teach Thomas how to use C#, GitHub and Microsoft Azure.
The console project uses Azure table storage to recieve weather data from a virtual Raspberry Pi, sending fake weather data, through the 
IoT hub on Azure, to the storage. The program then reads and parses the storage. (You need to open the table first before doing anything 
else)

Notes:

Azure account is free, so will run out soon rendering project useless.

App.config requires the connection key to the storage.

RasbPi sim (https://azure-samples.github.io/raspberry-pi-web-simulator/#GetStarted) needs the IoT hub connection string.
