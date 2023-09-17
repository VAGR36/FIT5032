CREATE TABLE CTMachine(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    )

    INSERT INTO CTMachine VALUES('Machine_1','This is the first CT scan machine', 'Available')
    INSERT INTO CTMachine VALUES('Machine_2','This is the second CT scan machine', 'Unavailable')