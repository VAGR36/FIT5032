CREATE TABLE [dbo].[Appointment] (
[Id] int IDENTITY(1,1) NOT NULL,
[MachineID] nvarchar(max) NOT NULL,
[Date] datetime NOT NULL,
[PatientName] nvarchar(max) NOT NULL,
PRIMARY KEY (Id)
);
GO