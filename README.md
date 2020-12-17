# Socket Listeren

Two mini applications that communicate with each other via the port.


#Socket Listeren
A class that serves specific ports and data about the data that is sent.

**public static void SetUp(int port = 11000, string nameHost = "localhost", string folder = "/PortParameters", string file = "/smsPort.txt")**
Default values are set that you can change. SetUp is called to assign to the class a port, a hostname, a path to store parameters accessed by Sender.
-port port that opens for communication
-nameHost The host being used
-folder the absolute path on which the folder is located. In this example it is on c:\
-file the absolute path on which file is located. For the file path it is only necessary to write / and the file name. In this example it si c:\PortParameters\smsPort.txt


public static void StartServer()
It starts the socket on a special thread which accepts the string being sent. The string being sent is in json format and keeps it in Listeren.receivedData


#Socket Sender
Is a class that allows sending data in json format.
public static void SetUp(string folder = "/PortParameters", string file = "/smsPort.txt")
The absolute path of the folder and file you loaded into Listeren.SetUp (...) in which all the necessary data are stored.

public static void StartServer()
Runs a task that sends data from a list, if it sends it then removes it.

**Example**
program a
Listeren.SetUp();
Listeren.StartServer();

program b
Sender.SetUp();
Sender.StartClient();
Sender.Sender.messages.Add(new Sms() { Number = "111111111", Text = "Example" });

