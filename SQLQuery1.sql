CREATE TABLE Radnici (
	radnikID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	imePrezime NVARCHAR(50) NOT NULL,
	radnoMesto NVARCHAR(50) NOT NULL,
	plata INT NOT NULL,
)

INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES ('Ismar Sehovic','Menadzer',999999)
INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES ('Petar Petrovic','Menadzer',60000)
INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES ('Marko Markovic','Administracija',50000)
INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES ('Zika Zikic','Proizvodnja',45000)
INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES ('Mika Mikic','Proizvodnja',45000)


CREATE TABLE RadnoMesto (
	radnoMestoID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	nazivMesta NVARCHAR(50) NOT NULL,
)

INSERT INTO RadnoMesto (nazivMesta) VALUES ('Menadzer')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('Ketering')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('Marketing')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('Administracija')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('Proizvodnja')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('PR')
INSERT INTO RadnoMesto (nazivMesta) VALUES ('Magacioner')

CREATE TABLE ListaPoslova (
	posaoID INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	opisPosla NVARCHAR(50) NOT NULL,
	rok NVARCHAR(20) NOT NULL,
)