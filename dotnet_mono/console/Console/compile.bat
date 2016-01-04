set PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319
rem csc /target:my.exe /out:File2.dll /warn:0 /nologo /debug *.cs
rem csc /out:my.exe  /warn:0 /nologo /debug /reference:PresentationFramework.dll program.cs serial_comm.cs ini_file.cs stopWatch.cs ring_buffer.cs fixed_lib.cs >error.txt
csc /out:my.exe  /warn:0 /nologo /debug program.cs serial_comm.cs ini_file.cs stopWatch.cs ring_buffer.cs fixed_lib.cs >error.txt

