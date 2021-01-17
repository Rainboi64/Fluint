clear

echo "Launched Fluint's Build Script"

echo ""
echo ""
echo ""


echo "Compiling the Layer"

echo ""
echo ""
echo ""

cd Fluint.Layer
dotnet build
cd ..

echo ""
echo ""
echo ""


echo "Compiling the Engine"

echo ""
echo ""
echo ""

cd Fluint.Engine
dotnet build
cd ..
echo "Compiling Avalonia"

echo ""
echo ""
echo ""

cd Fluint.Avalonia

while true; do
    read -p "Do you wish to run fluint <Y>es/<N>o : " yn
    case $yn in
        [Yy]* ) echo "Started Fluint Compilation"; dotnet run; break;;
        [Nn]* ) dotnet build; break;;
        * ) echo "Please answer yes or no.";;
    esac
done

cd ..

