set PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
%CSC% /target:module ..\..\..\Oscope_Control\Oscope_Control\Oscope_Control\fixed_lib.cs
%CSC% /target:module ..\..\..\Oscope_Control\Oscope_Control\Oscope_Control\serial_comm.cs
%CSC% /target:module ..\..\..\Oscope_Control\Oscope_Control\Oscope_Control\ini_file.cs
%CSC% /target:module ..\..\..\Oscope_Control\Oscope_Control\Oscope_Control\stopWatch.cs
%CSC% /target:module ..\..\..\Oscope_Control\Oscope_Control\Oscope_Control\ring_buffer.cs
%CSC% /out:console.exe program.cs -addmodule:fixed_lib.netmodule -addmodule:serial_comm.netmodule -addmodule:ini_file.netmodule -addmodule:stopWatch.netmodule -addmodule:ring_buffer.netmodule
