$nugetVersion = $Env:NUGETVERSION
echo "Nuget Version = "$nugetVersion

((Get-Content src/Calendar.Plugin.Sample/SampleApp/SampleApp.csproj) -replace '<ProjectReference Include="..\\..\\Calendar.Plugin\\CalendarPlugin.csproj"', "<PackageReference Include=""Xamarin.Plugin.Calendar"" Version=""$($nugetVersion)""") | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp/SampleApp.csproj

# (Get-Content src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj) | Where-Object {$_ -notmatch '81e938f4-a11c-4726-a13f-0d7ecc84ca66'} | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj
# (Get-Content src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj) | Where-Object {$_ -notmatch '<Name>CalendarPlugin'} | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj
(Get-Content src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj -Raw) -replace "(?sm)<ProjectReference Include=""..\\..\\Calendar.Plugin\\CalendarPlugin.csproj"">.*?</ProjectReference>", "<PackageReference Include=""Xamarin.Plugin.Calendar"" Version=""$($nugetVersion)"" />" | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.Android/SampleApp.Android.csproj

# (Get-Content src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj) | Where-Object {$_ -notmatch '81e938f4-a11c-4726-a13f-0d7ecc84ca66'} | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
# (Get-Content src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj) | Where-Object {$_ -notmatch '<Name>CalendarPlugin'} | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
(Get-Content src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj -Raw) -replace "(?sm)<ProjectReference Include=""..\\..\\Calendar.Plugin\\CalendarPlugin.csproj"">.*?</ProjectReference>", "<PackageReference Include=""Xamarin.Plugin.Calendar"" Version=""$($nugetVersion)"" />" | Out-File -encoding ASCII src/Calendar.Plugin.Sample/SampleApp.iOS/SampleApp.iOS.csproj
