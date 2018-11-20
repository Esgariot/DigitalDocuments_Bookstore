created with help from tutorial at https://realpython.com/python-sockets/.

using pipenv: pip + virtualenv


### example usage
1. to start run `$ ./pythonDaemonExample.py -p pythonDaemon.pid -l pythonDaemon.log `
2. to check that it's running you can check `pythonDaemon.log` or `$ lsof -i tcp:65432`
3. and to stop `$ kill` PID returned by `lsof`