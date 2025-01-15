import socket
import json
import matplotlib.pyplot as plt

def start_server(host="127.0.0.1", port=5000):
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(1)
    print(f"Server listening on {host}:{port}")

    conn, addr = server_socket.accept()
    print(f"Connection from {addr}")

    trajectory_data = []  # List to store received data for later use

    try:
        while True:
            data = conn.recv(1024).decode("utf-8")
            if not data:
                break

            try:
                # Parse incoming JSON
                message = json.loads(data)

                # Validate the expected fields
                if "time" in message and "position" in message and "velocity" in message:
                    print(f"Received valid data: {message}")
                    trajectory_data.append(message)  # Store data for later

                    # Optionally visualize in real-time
                    position = message["position"]
                    plt.scatter(position["x"], position["y"], c="blue", label="Real-Time Position")
                    plt.pause(0.01)  # Update plot
                else:
                    print(f"Incomplete data received: {data}")

            except json.JSONDecodeError:
                print(f"Invalid data format: {data}")
    finally:
        conn.close()
        server_socket.close()
        print("Server closed")

        # Save data to a file
        with open("trajectory_data.json", "w") as f:
            json.dump(trajectory_data, f, indent=4)
        print("Data saved to trajectory_data.json")

if __name__ == "__main__":
    # Start the server
    start_server()

