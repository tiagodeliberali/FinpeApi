﻿docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P24d!dBX!qRf" `
   -p 1433:1433 --name sql1 `
   -d microsoft/mssql-server-linux:2017-latest