import socket
import json

def start_server(host="127.0.0.1", port=5000):
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(1)
    print(f"Server listening on {host}:{port}")

    conn, addr = server_socket.accept()
    print(f"Connection from {addr}")

    try:
        while True:
            data = conn.recv(1024).decode("utf-8")
            if not data:
                break

            try:
                message = json.loads(data)
                print(f"Received: {message}")
            except json.JSONDecodeError:
                print(f"Invalid data: {data}")
    finally:
        conn.close()
        server_socket.close()
        print("Server closed")

if __name__ == "__main__":
    start_server()
