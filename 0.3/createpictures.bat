"C:\Program Files (x86)\IrfanView\i_view32.exe" %1 /resize=(750,500) /silent /aspectratio /resample /convert=%2$Nmedium$O
"C:\Program Files (x86)\IrfanView\i_view32.exe" %1 /resize=(1050,700) /silent /aspectratio /resample  /convert=%2$Nlarge$O
ping 127.0.0.1 -n 2 -w 1000 > nul
ping 127.0.0.1 -n 10 -w 1000 > nul
del %1 /Q /s