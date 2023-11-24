use StoredProcedureMVC





Create Table Departments(
DepId INT PRIMARY KEY NOT NULL IDENTITY (1,1),
DepName VARCHAR(100),
)
Create Table Countries(
CountryId INT PRIMARY KEY NOT NULL IDENTITY (1,1),
CountryName VARCHAR(100),
)

CREATE TABLE Statements (
    StateId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    StateName VARCHAR(255) NOT NULL,
    CountryId INT NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Countries(CountryId)
);
drop table Statements
CREATE TABLE Cities (
    CityId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    CityName VARCHAR(255) NOT NULL,
    StateId INT NOT NULL,
    FOREIGN KEY (StateId) REFERENCES Statements(StateId)
);


CREATE TABLE Employees (
    EmpId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    EmpName VARCHAR(255) NOT NULL,
    Age INT NOT NULL,

    MobileNo VARCHAR(20) NOT NULL,
    DepId INT NOT NULL,
    CountryId INT NOT NULL,
    StateId INT NOT NULL,
    CityId INT NOT NULL,
    FOREIGN KEY (DepId) REFERENCES Departments(DepId),
    FOREIGN KEY (CountryId) REFERENCES Countries(CountryId),
    FOREIGN KEY (StateId) REFERENCES Statements(StateId),
    FOREIGN KEY (CityId) REFERENCES Cities(CityId)
);
DROP TABLE Employees
DROP TABLE Departments
DROP TABLE Countries
DROP TABLE Statements
DROP TABLE Cities

CREATE OR ALTER PROCEDURE CreateOrUpdateEmployee
    @EmpId INT,
    @EmpName VARCHAR(255),
    @Age INT,
 
    @MobileNo VARCHAR(20),
    @DepId INT,
    @CountryId INT,
    @StateId INT,
    @CityId INT
AS
BEGIN
    -- Check if the employee with the provided EmpId exists
    IF EXISTS (SELECT 1 FROM Employees WHERE EmpId = @EmpId)
    BEGIN
        -- Update the existing employee record
        UPDATE Employees
        SET 
            EmpName = @EmpName,
            Age = @Age,
        
            MobileNo = @MobileNo,
            DepId = @DepId,
            CountryId = @CountryId,
            StateId = @StateId,
            CityId = @CityId
        WHERE EmpId = @EmpId;
    END
    ELSE
    BEGIN
        -- Insert a new employee record
        INSERT INTO Employees ( EmpName, Age, MobileNo, DepId, CountryId, StateId, CityId)
        VALUES ( @EmpName, @Age,  @MobileNo, @DepId, @CountryId, @StateId, @CityId);
    END
END


CREATE OR ALTER PROCEDURE DeleteEmployee
    @EmpId INT
AS
BEGIN
    -- Check if the employee with the provided EmpId exists
    IF EXISTS (SELECT 1 FROM Employees WHERE EmpId = @EmpId)
    BEGIN
        -- Delete the employee record
        DELETE FROM Employees WHERE EmpId = @EmpId;
    END
END


CREATE OR ALTER PROCEDURE CreateOrUpdateDepartment
    @DepId INT OUTPUT,
    @DepName VARCHAR(100)
AS
BEGIN
    -- Check if the department with the provided DepId exists
    IF EXISTS (SELECT 1 FROM Departments WHERE DepId = @DepId)
    BEGIN
        -- Update the existing department record
        UPDATE Departments
        SET DepName = @DepName
        WHERE DepId = @DepId;
    END
    ELSE
    BEGIN
        -- Insert a new department record
        INSERT INTO Departments (DepName)
        VALUES (@DepName);

        -- Get the newly generated DepId
        SET @DepId = SCOPE_IDENTITY();
    END
END

CREATE OR ALTER PROCEDURE DeleteDepartment
    @DepId INT
AS
BEGIN
    -- Check if the department with the provided DepId exists
    IF EXISTS (SELECT 1 FROM Departments WHERE DepId = @DepId)
    BEGIN
        -- Delete the department record
        DELETE FROM Departments WHERE DepId = @DepId;
    END
END



