del /q release\*.exe
qmake project.pro 
make -f Makefile.Release 2>error.txt
strip Release\iwscope.exe
copy Release\iwscope.exe ..\deliverables  /y
copy *.dll ..\deliverables  /y
copy ..\fixed_lib\bin\*.* bin  /y
copy *.dll Release
