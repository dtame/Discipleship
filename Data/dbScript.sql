create table [Albums] (
	[Id] int identity primary key,
	[Title] varchar(50) not null,
	[Code] bigint not null
)

create table [VideoModels] (
	[Id] int identity primary key,
	[Code] bigint not null,
	[AlbumId] int not null foreign key references Albums(Id),
	[Name] varchar(50) not null,
	[Description] varchar(80) not null,
)

create table [Thumnails] (
	[Id] int identity primary key,
	[VideoId] int not null foreign key references VideoModels(Id),
	[Link] varchar(100) not null,
	[Width] int not null,
	[Heigth] int not null,
)