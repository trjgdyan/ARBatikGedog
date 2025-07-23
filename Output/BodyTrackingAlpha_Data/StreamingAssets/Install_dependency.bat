@echo off
echo Membuat virtual environment...
python -m venv venv

echo Mengaktifkan virtual environment...
call venv\Scripts\activate.bat

echo Menginstal mediapipe...
pip install mediapipe

echo Instalasi selesai.
pause
