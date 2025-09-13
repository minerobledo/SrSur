# Imagen oficial de SQL Server 2022
FROM mcr.microsoft.com/mssql/server:2022-latest

# Variables necesarias para aceptar la licencia y setear contraseña
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=ARGDETECEsLaMejorEmprezaDELMunDoMundial123!

# Puerto de SQL Server
EXPOSE 1433

# Comando por defecto
CMD ["/opt/mssql/bin/sqlservr"]