DROP TABLE IF EXISTS Sd.Main.UserClaims;
DROP TABLE IF EXISTS Sd.Main.Users;
DROP TABLE IF EXISTS Sd.Main.Trainings;
DROP TABLE IF EXISTS Sd.Main.Disciples;
DROP TABLE IF EXISTS Sd.Main.Fathers;
DROP TABLE IF EXISTS Sd.Main.Mothers;
DROP TABLE IF EXISTS Sd.Main.Squads;
DROP TABLE IF EXISTS Sd.Main.DiscipleLevels;
DROP TABLE IF EXISTS Sd.Main.SdProjects;

CREATE TABLE Sd.Main.SdProjects (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(20) NOT NULL,
    CONSTRAINT PK_SdProjects PRIMARY KEY (Id)
);

CREATE TABLE Sd.Main.Users (
    Id uniqueidentifier NOT NULL,
    FirstName nvarchar(20) NULL,
    LastName nvarchar(20) NULL,
    Email varchar(50) NOT NULL,
    City nvarchar(20) NULL,
    HashPassword varchar(1000) NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Id)
);

CREATE TABLE Sd.Main.UserClaims (
    Id uniqueidentifier NOT NULL,
    Name varchar(20) NOT NULL,
    Value varchar(1000) NOT NULL,
    UserId uniqueidentifier NOT NULL,
    CONSTRAINT PK_UserClaims PRIMARY KEY (Id),
    CONSTRAINT FK_UserClaims_User FOREIGN KEY (UserId) REFERENCES Sd.Main.Users (id) ON DELETE CASCADE
);


CREATE TABLE Sd.Main.DiscipleLevels (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(20) NOT NULL,
);

CREATE TABLE Sd.Main.Squads (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(20) NULL,
    City nvarchar(20) NOT NULL,
    Location nvarchar(50) NULL,
    UserId uniqueidentifier NOT NULL,
    CONSTRAINT PK_Squads PRIMARY KEY (id),
    CONSTRAINT FK_Squad_User FOREIGN KEY (UserId) REFERENCES Sd.Main.Users (id)
);

CREATE TABLE Sd.Main.Mothers (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(50) NOT NULL,
    Phone nvarchar(20) NOT NULL,
    Comment nvarchar(1000) NULL,
    CONSTRAINT PK_Mothers PRIMARY KEY (Id)
);

CREATE TABLE Sd.Main.Fathers (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(50) NOT NULL,
    Phone nvarchar(20) NOT NULL,
    Comment nvarchar(1000) NULL,
    CONSTRAINT PK_Fathers PRIMARY KEY (Id)
);

CREATE TABLE Sd.Main.Disciples (
    Id uniqueidentifier NOT NULL,
    FirstName nvarchar(20) NULL,
    LastName nvarchar(20) NULL,
    DateOfBirth Date NULL,
    Sex nvarchar(10) NULL,
    ProjectId uniqueidentifier NOT NULL,
    LevelId uniqueidentifier NULL,
    FirstTrainingDate varchar(10) NULL,
    Status nvarchar(20) NULL,
    MotherId uniqueidentifier NULL,
    FatherId uniqueidentifier NULL,
    SquadId uniqueidentifier NOT NULL,
    CONSTRAINT PK_Disciples PRIMARY KEY (Id),
    CONSTRAINT FK_Disciples_SdProjects FOREIGN KEY (ProjectId) REFERENCES Sd.Main.SdProjects (id),
    CONSTRAINT FK_Disciples_Mothers FOREIGN KEY (MotherId) REFERENCES Sd.Main.Mothers (id),
    CONSTRAINT FK_Disciples_Fathers FOREIGN KEY (FatherId) REFERENCES Sd.Main.Fathers (id),
    CONSTRAINT FK_Disciples_Squads FOREIGN KEY (SquadId) REFERENCES Sd.Main.Squads (id) ON DELETE CASCADE
);

CREATE TABLE Sd.Main.Trainings (
    Id uniqueidentifier NOT NULL,
    Date Date NOT NULL,
    Month int NOT NULL,
    Number int NOT NULL,
    Presence varchar(20) NULL,
    Comment nvarchar(1000) NULL,
    DiscipleId uniqueidentifier NOT NULL,
    CONSTRAINT PK_Trainings PRIMARY KEY (Id),
    CONSTRAINT FK_Trainings_Disciples FOREIGN KEY (DiscipleId) REFERENCES Sd.Main.Disciples (id) ON DELETE CASCADE
);