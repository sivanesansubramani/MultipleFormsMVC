Create database EnployeeAppTask;

use EnployeeAppTask;

CREATE TABLE Bio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    MobileNumber NVARCHAR(15) NOT NULL
);

Select * from Bio;