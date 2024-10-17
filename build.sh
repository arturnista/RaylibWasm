echo Building...
dotnet publish -c Release
# Clear build files
echo Clearing old files...

mkdir Build/
rm -rf Build/Web
rm -rf Build/Windows
rm -rf ./Source/bin/Release/net7.0/publish/resources/

# Clear copy resources
echo Copyin resources...
cp -r ./Source/resources/ ./Source/bin/Release/net7.0/publish/resources/
# Create zips
echo Creating zips...

cp -r DotnetRaylibWasm/bin/Release/net7.0/browser-wasm/AppBundle/ ./Build/Web
cp -r Source/bin/Release/net7.0/publish/ ./Build/Windows