#!/usr/bin/env python3
import daemon
import socket
import selectors
import time
import sys
import logging
import libserver
import traceback
import os
import argparse
from daemon import pidfile

HOST = '127.0.0.1'
PORT = 65432
logger = logging.getLogger('pythonDaemonExampleLogger')

def accept_wrapper(sock, sel):
    logger.info("accepting connection")
    conn, addr = sock.accept()  # Should be ready to read
    logger.info("accepted connection from %s", addr)
    conn.setblocking(False)
    message = libserver.Message(sel, conn, addr)
    sel.register(conn, selectors.EVENT_READ, data=message)


def start_daemon(pidf, logf):
    with daemon.DaemonContext(
        working_directory='.',
        umask=0o002,
        pidfile=pidfile.TimeoutPIDLockFile(pidf),
    ) as context:
        run(logf)


def run(logf):
    logger.setLevel(logging.INFO)

    fh = logging.FileHandler(logf)
    fh.setLevel(logging.INFO)

    format = '[%(asctime)s] %(message)s'
    formatter = logging.Formatter(format)

    fh.setFormatter(formatter)

    logger.addHandler(fh)
    print("running in spawned process as daemon, so this shouldn't be printed")
    logger.info("running")
    sel = selectors.DefaultSelector()
    lsock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # Avoid bind() exception: OSError: [Errno 48] Address already in use
    lsock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    lsock.bind((HOST, PORT))
    lsock.listen()
    logger.info("listening on %s %s", HOST, PORT)
    lsock.setblocking(False)
    sel.register(lsock, selectors.EVENT_READ, data=None)
    logger.info("starting listen loop")
    try:
        while True:
            events = sel.select(timeout=None)
            for key, mask in events:
                if key.data is None:
                    accept_wrapper(key.fileobj, sel)
                else:
                    message = key.data
                    try:
                        message.process_events(mask)
                    except Exception:
                        logger.error(
                            "main: error: exception for" +
                            f"{message.addr}:\n{traceback.format_exc()}"
                        )
                        message.close()
    except Exception:
        logger.error(f"caught exception {traceback.format_exc()}")
    finally:
        sel.close()
    logger.info("exiting [run]")


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Example daemon in Python")
    parser.add_argument('-p', '--pid-file', default='/var/run/eg_daemon.pid')
    parser.add_argument('-l', '--log-file', default='/var/log/eg_daemon.log')

    args = parser.parse_args()

    start_daemon(pidf=args.pid_file, logf=args.log_file)
    #run(args.log_file)
