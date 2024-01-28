echo Building...
dotnet publish -c Release
# Clear build files
echo Clearing old files...
rm -rf Web.zip
rm -rf Windows.zip
rm -rf ./Source/bin/Release/net7.0/publish/resources/
# Clear copy resources
echo Copyin resources...
cp -r ./Source/resources/ ./Source/bin/Release/net7.0/publish/resources/
# Create zips
echo Creating zips...
Tar -a -cf Web.zip DotnetRaylibWasm/bin/Release/net7.0/browser-wasm/AppBundle/
cp -r Source/bin/Release/net7.0/publish/ ./WindowsGame
Tar -a -cf Windows.zip WindowsGame/
rm -rf WindowsGame