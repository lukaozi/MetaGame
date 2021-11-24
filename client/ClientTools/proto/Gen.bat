@echo off  
rem 查找文件  
for /f "delims=" %%i in ('dir /b ".\*.proto"') do echo %%i  
rem 转cpp  for /f "delims=" %%i in ('dir /b/a "*.proto"') do protoc -I=. --cpp_out=. %%i  
for /f "delims=" %%i in ('dir /b/a "*.proto"') do protogen.exe %%i --csharp_out=./csharp/
pause