#!/bin/sh

echo "Nuget Version = "$NUGETVERSION

sed -E -i .bak -e 's/<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj"/<PackageReference Include="Xamarin.Plugin.Calendar" Version="'$NUGETVERSION'"/g' src/Calendar.Plugin.Sample/SampleApp/SampleApp.csproj

sed -i .bak -e '/81e938f4-a11c-4726-a13f-0d7ecc84ca66/d' src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj 
sed -i .bak -e '/<Name>CalendarPlugin/d' src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj 
sed -E -i .bak -e '/<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj">/{N;s/\n.*//;}' src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj
sed -E -i .bak -e 's/<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj">/<PackageReference Include="Xamarin.Plugin.Calendar" Version="'$NUGETVERSION'" \/>/g' src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj

sed -i .bak -e '/81e938f4-a11c-4726-a13f-0d7ecc84ca66/d' src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
sed -i .bak -e '/<Name>CalendarPlugin/d' src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
sed -E -i .bak -e '/<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj">/{N;s/\n.*//;}' src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
sed -E -i .bak -e 's/<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj">/<PackageReference Include="Xamarin.Plugin.Calendar" Version="'$NUGETVERSION'" \/>/g' src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj

## Debug
#cat src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj
#cat src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj 