clear

echo "Launched Fluint's Build Script"
(
	cd Fluint.Avalonia || exit

	while true; do
		read -r -p "Do you wish to run fluint <Y>es/<N>o : " yn
		case $yn in
			[Yy]* ) echo "Started Fluint Compilation"; dotnet run; break;;
			[Nn]* ) dotnet build; break;;
			* ) echo "Please answer yes or no.";;
		esac
	done
	cd ..
)

