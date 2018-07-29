CREATE DATABASE test_linq_2_tablas;
GO
USE test_linq_2_tablas;
GO

CREATE TABLE ciudad(
	id INT IDENTITY(1,1),
	nombre VARCHAR(100),
	PRIMARY KEY(id)
);

INSERT INTO ciudad(nombre) VALUES('Rancagua');
INSERT INTO ciudad(nombre) VALUES('Rengo');

CREATE TABLE persona(
	id INT IDENTITY(1,1),
	nombre VARCHAR(100),
	ciudad INT,
	PRIMARY KEY(id),
	FOREIGN KEY(ciudad) REFERENCES ciudad(id)
);

INSERT INTO persona(nombre, ciudad) VALUES('Juan Pérez','1');
INSERT INTO persona(nombre, ciudad) VALUES('María Pérez','2');

SELECT * FROM ciudad;
SELECT * FROM persona;

CREATE VIEW listadoPersonas AS
SELECT 
	p.id AS 'ID',
	p.nombre AS 'Nombre',
	c.id AS 'ID ciudad',
	c.nombre AS 'Ciudad'
FROM 
	persona p 
	INNER JOIN ciudad c ON p.ciudad = c.id;

-- Eliminar base de datos
USE master;
DROP DATABASE test_linq_2_tablas;
-- Eliminar base de datos

DROP VIEW listadoPersonas;