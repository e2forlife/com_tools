# com_tools
Command-Line Serial port access for when you need it!

# Description
This application is a (very) simple command line tool used to send and receive bytes over a UART interface.  Presently this only supports Windows (Boo), but it is pretty useful to provide quick access to the port without having to go the PS route, or use a terminal application.

## How to Use this tool
Using this is pretty simple: Open a command shell, and make sure the tool is in the path or present directoy.

~~~
com_tools {options} ...
~~~

### Options
+ --p, --port : This specifies the name of the port to be used. The default port name is COM3. 
+ --b, --baud : This specifies the baud rate to be used for the communicatoin.  This is specified as an integer, and must be a valid rate for the port selected.  The default baudrate is 115200 baud.
+ --l, --list : This returns a list of the available ports that can be used.
+ --h, --help : Displays the command line help

Following the options, any number of optional values (integer bytes in ASCII format) are transmitted to the port. The port is scanned and received bytes are returned space delimited with a newline terminating the data.  Calling this command with no transmit data will read data from the port.  When using the `--list` option, this command will only print the list of available ports and does not execute serial transfers.

## Limitations
+ Presently, this tool is written around the .Net Framework for Windows.  I am looking into converting the c# code to Mono to support a wider group of platforms, but I was trying to build something quick here and .Net Framework was teh shortest path from go to done.
+ The tools do not support parity, stop bits, data length.
+ There is no way to see how much data is in the buffer ahead of reading.
+ There exists a possibility to miss data received that occurs between calls to the tool.

# Where to find more
Check out some of my other projects at:
+ https://www.e2forlife.com
+ https://opentokenproject.wordpress.org

Thanks for checking out the tools, I hope they help.
