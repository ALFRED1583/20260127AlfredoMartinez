
USE master;
GO
--CREATE ROLE db_users;
go
USE master;
CREATE LOGIN PruebaBSCI WITH PASSWORD = 'D4MhNqndI4Vb~V';
USE master;
CREATE USER PruebaBSCI FOR LOGIN PruebaBSCI;

ALTER ROLE db_users ADD MEMBER PruebaBSCI;

DENY VIEW ANY DATABASE TO PruebaBSCI;

GRANT SELECT ON sys.databases TO db_users;

USE master;
-- Permisos para crear bases de datos
GRANT CREATE ANY DATABASE TO PruebaBSCI;

-- Permisos para ver y modificar bases de datos creadas por ellos mismos
GRANT VIEW DEFINITION TO PruebaBSCI;
GRANT ALTER ON SCHEMA::dbo TO PruebaBSCI;
-- Denegar acceso a otras bases de datos existentes
DENY VIEW ANY DATABASE TO PruebaBSCI;
