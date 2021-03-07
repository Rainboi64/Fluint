@echo off

echo Started Fluint build script

:start
SET choice=
SET /p choice=Run Fluint.Avalonia? Y or [N]: 
IF NOT '%choice%'=='' SET choice=%choice:~0,1%
IF '%choice%'=='Y' GOTO yes
IF '%choice%'=='y' GOTO yes
IF '%choice%'=='N' GOTO no
IF '%choice%'=='n' GOTO no
IF '%choice%'=='' GOTO no
ECHO "%choice%" is not valid
ECHO.
GOTO start

:no

echo.
echo building Fluint.Avalonia
echo.

cd Fluint.Avalonia
dotnet build
cd ..

PAUSE
EXIT

:yes

echo.
echo building and running Fluint.Avalonia
echo.

cd Fluint.Avalonia
dotnet run
cd ..

PAUSE
EXIT
