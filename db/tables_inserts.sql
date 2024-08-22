CREATE DATABASE ACTIVIFY

USE ACTIVIFY
GO

--Tables without FK
CREATE TABLE Profesor(
	ProfesorId INT IDENTITY PRIMARY KEY,
	Nombre VARCHAR(50),
	Email VARCHAR(50),
	Telefono VARCHAR(50),
	DPI VARCHAR(50)
);

CREATE TABLE TipoActividad(
	TipoActividadId INT IDENTITY PRIMARY KEY,
	Descripcion VARCHAR(250)
);

CREATE TABLE Seccion(
	SeccionId INT IDENTITY PRIMARY KEY,
	Grado VARCHAR(75),
	Aula VARCHAR(10),
	ProfesorId INT FOREIGN KEY REFERENCES Profesor(ProfesorId)
);

CREATE TABLE Actividad(
	ActividadId INT IDENTITY PRIMARY KEY,
	Nombre VARCHAR(250),
	Fecha DATETIME,
	Monto DECIMAL(10, 2),
	Observaciones VARCHAR(250),
	LastModifiedDate DATETIME,
	TipoActividadId INT FOREIGN KEY REFERENCES TipoActividad(TipoActividadId),
	SeccionId INT FOREIGN KEY REFERENCES Seccion(SeccionId),
	AlumnoId INT
);

CREATE TABLE Gasto(
	GastoId INT IDENTITY PRIMARY KEY,
	Descripcion VARCHAR(250),
	Monto DECIMAL(10, 2),
	LastModifiedDate DATETIME,
	ActividadId INT FOREIGN KEY REFERENCES Actividad(ActividadId)
);

CREATE TABLE Comprobante(
	ComprobanteId INT IDENTITY PRIMARY KEY,
	Nombre VARCHAR(250),
	Imagen IMAGE,
	CreateDate DATETIME,
	GastoId INT FOREIGN KEY REFERENCES Gasto(GastoId)
);

--Tables with FK
CREATE TABLE Alumno(
	AlumnoId INT IDENTITY PRIMARY KEY,
	Carnet VARCHAR(50),
	Nombre VARCHAR(50),
	Telefono INT,
	SeccionId INT FOREIGN KEY REFERENCES Seccion(SeccionId)
);


CREATE TABLE Bitacora(
	BitacoraId INT IDENTITY PRIMARY KEY,
	Action VARCHAR(250),
	SpName VARCHAR(250),
	Parameters VARCHAR(MAX),
	UserId BIGINT,
	CreateDate DATETIME
);

-- INSERCION DE DATOS 
USE ACTIVIFY

INSERT INTO Profesor (Nombre,Email,Telefono,DPI) VALUES ('Carlos Alvarez','calvarezr@miumg.edu.gt',41597014,'2945519480101');

INSERT INTO TipoActividad(descripcion) VALUES ('Academica');
INSERT INTO TipoActividad(descripcion) VALUES ('Recreativa');
INSERT INTO TipoActividad(descripcion) VALUES ('Cultural');
INSERT INTO TipoActividad(descripcion) VALUES ('Civica');
INSERT INTO TipoActividad(descripcion) VALUES ('Deportiva');
INSERT INTO TipoActividad(descripcion) VALUES ('Extracurricular');
INSERT INTO TipoActividad(descripcion) VALUES ('Ambiental');

INSERT INTO Seccion (Grado, Aula, ProfesorId) VALUES ('1ro Basico','"A"', 1);
INSERT INTO Seccion (Grado, Aula, ProfesorId) VALUES ('2do Basico','"B"', 1);

INSERT INTO Actividad VALUES ('Cumpleaños Pedrito', '2024-09-01',500.00,'Ninguna', GETDATE(), 2, 1,1);
INSERT INTO Actividad VALUES ('Torneo de Futbol', '2024-08-15',250.00,'Arbitraje y refaccion', GETDATE(), 5, 2,2);

INSERT INTO Gasto VALUES ('Compra de Pastel', 150.00, GETDATE(), 1);
INSERT INTO Gasto VALUES ('Pizza', 200.00, GETDATE(), 1);
INSERT INTO Gasto VALUES ('Piñata y dulces', 150.00, GETDATE(), 1);
INSERT INTO Gasto VALUES ('Arbitraje', 50.00, GETDATE(),2);
INSERT INTO Gasto VALUES ('Pizza', 150.00, GETDATE(),2);
INSERT INTO Gasto VALUES ('Bebidas', 150.00, GETDATE(),2);

INSERT INTO Comprobante VALUES ('Pastel', NULL,GETDATE(),1);
INSERT INTO Comprobante VALUES ('Pizza', NULL,GETDATE(),1);
INSERT INTO Comprobante VALUES ('Piza', NULL,GETDATE(),2);

INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202401010015','Alejandro Castellanos',45363423, 1);
INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202401010016','Pedro Lopez',56873910, 1);
INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202401010017','Laura Garcia',34872359, 1);
INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202402020012','Mateo Fernandez',45781234, 2);
INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202402020013','Sofía López',53981276, 2);
INSERT INTO Alumno (Carnet, Nombre, Telefono, SeccionId) VALUES ('202402020014','Lucas Martínez',54981571, 2);
