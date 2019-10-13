default: publish
build:
	dotnet publish -r linux-arm -c Release /p:PublishSingleFile=true
	cp pitempd.service bin/Release/netcoreapp3.0/linux-arm/publish/

publish: build
	scp -r bin/Release/netcoreapp3.0/linux-arm/publish/* pi@mini-display:/home/pi/pitempd

package: build
	tar -C ./bin/Release/netcoreapp3.0/linux-arm/publish -zcvf ./pitempd.tar.gz .
