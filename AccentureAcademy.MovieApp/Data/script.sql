CREATE DATABASE AccentureAcademyMovieDb
GO

USE AccentureAcademyMovieDb
GO

CREATE TABLE Movie
(
	Id int primary key identity(1,1),
	Title varchar(200) not null,
	ReleaseDate DATE not null,
	RunningTime int not null,
	CONSTRAINT UQ_Title UNIQUE(Title,ReleaseDate),
	CONSTRAINT CHK_Duration CHECK (RunningTime BETWEEN 1 AND 500)
);
GO

INSERT INTO Movie
(Title, ReleaseDate, RunningTime)
VALUES
('Terminator: Destino Oculto',
'2019-10-31',
128)

