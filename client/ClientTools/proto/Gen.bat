@echo off  
rem set  path=..\..\..\proto
set inPath=.\proto
set outPath=..\..\Assets\Scripts\CSharp\Generate\
rem 查找文件  
for /f "delims=" %%i in ('dir /b "%inPath%\*.proto"') do echo %inPath%\%%i  
rem 转cpp  for /f "delims=" %%i in ('dir /b/a "*.proto"') do protoc -I=. --cpp_out=. %%i  
for /f "delims=" %%i in ('dir /b/a "%inPath%\*.proto"') do protogen.exe %inPath%\%%i --csharp_out=%outPath%

rem protogen.exe --proto_path=..\..\..\proto --csharp_out=./csharp/


echo Generate Success!!!!!
pause