# main.py - VERSI PERBAIKAN

from body import BodyThread
import time
import global_vars
from sys import exit

print("Starting Body Thread...")
thread = BodyThread()
thread.start()

# Loop ini menggantikan "i = input()"
# Loop akan berjalan selama thread di latar belakang masih hidup.
try:
    while thread.is_alive():
        time.sleep(0.5)  # Cek status thread setiap 0.5 detik
except KeyboardInterrupt:
    # Ini untuk jika Anda menjalankan sebagai skrip dan menekan Ctrl+C
    print("Keyboard interrupt received. Exiting...")

# Kode di bawah ini akan berjalan setelah thread berhenti
# (misalnya, setelah Anda menutup jendela OpenCV yang dibuat oleh BodyThread)
print("Thread finished. Exiting...")
global_vars.KILL_THREADS = True
time.sleep(0.5) # Beri waktu sejenak untuk cleanup
exit()