Create Table Ambulances(
	Id int not null primary key identity(1,1),
	"Name" nvarchar(32),
	Station nvarchar(32),
	"Status" nvarchar(32)
);

Create Table Patients(
	Id int not null primary key identity(1,1),
	"Name" varchar(128),
	"Status" varchar(32),
	CPR char(10) unique
);

Create Table Hospitals(
	Id int not null primary key identity(1,1),
	"Name" varchar(64),
	"Address" varchar(128),
	Region varchar(32),
	City varchar(32)
);

Create Table Transports(
	Id int not null primary key identity(1,1),
	"From" int not null foreign key references Hospitals(Id),
	"To" int not null foreign key references Hospitals(Id),
	Patient int not null foreign key references Patients(Id),
	Arrival smalldatetime not null
);

Create Table AssignedAmbulances(
	Ambulance int not null foreign key references Ambulances(Id),
	Transport int not null unique foreign key references Transports(Id)
);

Insert Into Hospitals("Name", "Address", Region, City) VALUES ('OUH', 'J.B. Winslowsvej 4', 'Region Syddanmark', 'Odense C');
Insert Into Hospitals("Name", "Address", Region, City) VALUES ('Rigshospitalet', 'Blegdamsvej 9', 'Region Hovedstaden', 'København Ø');
Insert Into Hospitals("Name", "Address", Region, City) VALUES ('AUH', 'Palle Juul-Jensens Blvd. 99', 'Region Midtjylland', 'Aarhus N');
Insert Into Hospitals("Name", "Address", Region, City) VALUES ('Herlev Hospital', 'Borgmester Ib Juuls Vej 1', 'Region Hovedstaden', 'Herlev');