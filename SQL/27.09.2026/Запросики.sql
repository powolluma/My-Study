-- Триггер 1
CREATE TRIGGER dbo.trg_PreventChiefDelete
ON dbo.Barbers
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM deleted d
        WHERE d.Position = 'chief'
          AND (SELECT COUNT(*) FROM dbo.Barbers WHERE Position = 'chief') = 1
    )
    BEGIN
        RAISERROR(N'Нельзя удалить единственного чиф-барбера. Сначала добавьте второго чиф-барбера.', 16, 1);
        RETURN;
    END

    DELETE FROM dbo.Barbers WHERE BarberID IN (SELECT BarberID FROM deleted);
END

-- Тест
DELETE FROM dbo.Barbers WHERE FullName = N'Иван Иванов';

-- Триггер 2


CREATE TRIGGER dbo.trg_PreventYoungBarber
ON dbo.Barbers
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE DATEDIFF(YEAR, BirthDate, GETDATE())
              - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, BirthDate, GETDATE()), BirthDate) > GETDATE()
                     THEN 1 ELSE 0 END < 21
    )
    BEGIN
        RAISERROR(N'Нельзя добавлять барберов младше 21 года.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END

-- Тест
INSERT INTO dbo.Barbers (FullName, Gender, Phone, Email, BirthDate, HireDate, Position)
VALUES (N'Тест Молодой', 'M', '+375290000000', 'young@test.by', DATEADD(YEAR,-18,GETDATE()), GETDATE(), 'junior');


-- Триггер 3

CREATE TRIGGER dbo.trg_PreventDoubleBooking
ON dbo.Schedule
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted  d ON d.ScheduleID = i.ScheduleID
        WHERE i.IsBooked = 1 AND d.IsBooked = 1
    )
    BEGIN
        RAISERROR(N'Это время уже занято. Запись на занятый слот запрещена.', 16, 1);
        RETURN;
    END

    UPDATE s
    SET IsBooked = i.IsBooked,
        ClientID = i.ClientID
    FROM dbo.Schedule s
    JOIN inserted i ON i.ScheduleID = s.ScheduleID;
END

-- Тест
DECLARE @BusyScheduleID INT;
SELECT TOP (1) @BusyScheduleID = ScheduleID FROM dbo.Schedule WHERE BarberID = 2 AND IsBooked = 1;

UPDATE dbo.Schedule
SET IsBooked = 1, ClientID = 9
WHERE ScheduleID = @BusyScheduleID;

-- Триггер 4
CREATE TRIGGER dbo.trg_LimitJuniorBarbers
ON dbo.Barbers
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted WHERE Position = 'junior')
       AND (SELECT COUNT(*) FROM dbo.Barbers WHERE Position = 'junior') > 5
    BEGIN
        RAISERROR(N'Нельзя добавить нового джуниор-барбера: в салоне уже работают 5 джуниор-барберов.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END

-- Тест
INSERT INTO dbo.Barbers (FullName, Gender, Phone, Email, BirthDate, HireDate, Position)
VALUES (N'Никита Громов', 'M', '+375291110011', 'gromov@barbershop.by', '1999-08-15', GETDATE(), 'junior');


-- Процедура 1
CREATE PROCEDURE dbo.sp_GetAllBarberNames
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FullName FROM dbo.Barbers;
END

EXEC dbo.sp_GetAllBarberNames;

-- Процедура 2
CREATE PROCEDURE dbo.sp_GetSeniorBarbers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Barbers WHERE Position = 'senior';
END

EXEC dbo.sp_GetSeniorBarbers;

-- Процедура 3
CREATE PROCEDURE dbo.sp_GetBeardShaveBarbers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT b.*
    FROM dbo.Barbers b
    JOIN dbo.BarberServices bs ON bs.BarberID = b.BarberID
    JOIN dbo.Services s        ON s.ServiceID = bs.ServiceID
    WHERE s.ServiceName = N'Традиционное бритье бороды';
END

EXEC dbo.sp_GetBeardShaveBarbers;

-- Процедура  4
CREATE PROCEDURE dbo.sp_GetBarbersByService
    @ServiceName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT b.*
    FROM dbo.Barbers b
    JOIN dbo.BarberServices bs ON bs.BarberID = b.BarberID
    JOIN dbo.Services s        ON s.ServiceID = bs.ServiceID
    WHERE s.ServiceName = @ServiceName;
END

EXEC dbo.sp_GetBarbersByService @ServiceName = N'Детская стрижка';

-- Процедура 5
CREATE PROCEDURE dbo.sp_GetExperiencedBarbers
    @Years INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *, DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsWorked
    FROM dbo.Barbers
    WHERE DATEDIFF(YEAR, HireDate, GETDATE()) > @Years;
END

EXEC dbo.sp_GetExperiencedBarbers @Years = 5;

-- Процедура 6
CREATE PROCEDURE dbo.sp_GetSeniorJuniorCount
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        SUM(CASE WHEN Position = 'senior' THEN 1 ELSE 0 END) AS SeniorCount,
        SUM(CASE WHEN Position = 'junior' THEN 1 ELSE 0 END) AS JuniorCount
    FROM dbo.Barbers;
END

EXEC dbo.sp_GetSeniorJuniorCount;

-- Процедура 7
CREATE PROCEDURE dbo.sp_GetRegularClients
    @VisitCount INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.*, COUNT(v.VisitID) AS VisitsCount
    FROM dbo.Clients c
    JOIN dbo.Visits v ON v.ClientID = c.ClientID
    GROUP BY c.ClientID, c.FullName, c.Phone, c.Email
    HAVING COUNT(v.VisitID) >= @VisitCount;
END

EXEC dbo.sp_GetRegularClients @VisitCount = 3;

-- Процедура 8
CREATE PROCEDURE dbo.sp_GetLongestWorkingBarber
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) *, DATEDIFF(DAY, HireDate, GETDATE()) AS DaysWorked
    FROM dbo.Barbers
    ORDER BY HireDate ASC;
END

EXEC dbo.sp_GetLongestWorkingBarber;

-- Процедура 9
CREATE PROCEDURE dbo.sp_GetTopBarberByPeriod
    @StartDate DATE,
    @EndDate   DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) b.*, COUNT(DISTINCT v.ClientID) AS ClientsServed
    FROM dbo.Barbers b
    JOIN dbo.Visits v ON v.BarberID = b.BarberID
    WHERE v.VisitDate BETWEEN @StartDate AND @EndDate
    GROUP BY b.BarberID, b.FullName, b.Gender, b.Phone, b.Email, b.BirthDate, b.HireDate, b.Position
    ORDER BY COUNT(DISTINCT v.ClientID) DESC, b.BarberID ASC;
END

EXEC dbo.sp_GetTopBarberByPeriod @StartDate = '1900-01-01', @EndDate = '2100-01-01';

-- Процедура 10
CREATE PROCEDURE dbo.sp_GetMostFrequentClient
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) c.*, COUNT(v.VisitID) AS VisitsCount
    FROM dbo.Clients c
    JOIN dbo.Visits v ON v.ClientID = c.ClientID
    GROUP BY c.ClientID, c.FullName, c.Phone, c.Email
    ORDER BY COUNT(v.VisitID) DESC, c.ClientID ASC;
END

EXEC dbo.sp_GetMostFrequentClient;

-- Процедура 11
CREATE PROCEDURE dbo.sp_GetTopSpendingClient
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) c.*, SUM(v.TotalCost) AS TotalSpent
    FROM dbo.Clients c
    JOIN dbo.Visits v ON v.ClientID = c.ClientID
    GROUP BY c.ClientID, c.FullName, c.Phone, c.Email
    ORDER BY SUM(v.TotalCost) DESC, c.ClientID ASC;
END

EXEC dbo.sp_GetTopSpendingClient;

-- Процедура 12
CREATE PROCEDURE dbo.sp_GetMostPopularBarber
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (1) b.*, COUNT(DISTINCT v.ClientID) AS UniqueClients
    FROM dbo.Barbers b
    JOIN dbo.Visits v ON v.BarberID = b.BarberID
    GROUP BY b.BarberID, b.FullName, b.Gender, b.Phone, b.Email, b.BirthDate, b.HireDate, b.Position
    ORDER BY COUNT(DISTINCT v.ClientID) DESC, b.BarberID ASC;
END

EXEC dbo.sp_GetMostPopularBarber;


-- Процедура 13
CREATE PROCEDURE dbo.sp_GetTop3BarbersByMonth
    @Year  INT,
    @Month INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (3) b.*, SUM(v.TotalCost) AS MonthRevenue
    FROM dbo.Barbers b
    JOIN dbo.Visits v ON v.BarberID = b.BarberID
    WHERE YEAR(v.VisitDate) = @Year AND MONTH(v.VisitDate) = @Month
    GROUP BY b.BarberID, b.FullName, b.Gender, b.Phone, b.Email, b.BirthDate, b.HireDate, b.Position
    ORDER BY SUM(v.TotalCost) DESC, b.BarberID ASC;
END

EXEC dbo.sp_GetTop3BarbersByMonth @Year = YEAR(GETDATE()), @Month = MONTH(GETDATE());

-- Процедура 14
CREATE PROCEDURE dbo.sp_GetTop3BarbersByAvgRating
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (3) b.*, AVG(CAST(v.RatingID AS FLOAT)) AS AvgRating, COUNT(*) AS VisitsCount
    FROM dbo.Barbers b
    JOIN dbo.Visits v ON v.BarberID = b.BarberID
    GROUP BY b.BarberID, b.FullName, b.Gender, b.Phone, b.Email, b.BirthDate, b.HireDate, b.Position
    HAVING COUNT(*) >= 30
    ORDER BY AVG(CAST(v.RatingID AS FLOAT)) DESC, b.BarberID ASC;
END

EXEC dbo.sp_GetTop3BarbersByAvgRating;

-- Процедура 15
CREATE PROCEDURE dbo.sp_GetBarberScheduleForDay
    @BarberID INT,
    @SlotDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT sch.*, c.FullName AS ClientName
    FROM dbo.Schedule sch
    LEFT JOIN dbo.Clients c ON c.ClientID = sch.ClientID
    WHERE sch.BarberID = @BarberID AND sch.SlotDate = @SlotDate
    ORDER BY sch.SlotTime;
END

DECLARE @Today DATE = CAST(GETDATE() AS DATE);
EXEC dbo.sp_GetBarberScheduleForDay @BarberID = 2, @SlotDate = @Today;

-- Процедура 16
CREATE PROCEDURE dbo.sp_GetFreeSlotsForWeek
    @BarberID      INT,
    @WeekStartDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM dbo.Schedule
    WHERE BarberID = @BarberID
      AND SlotDate BETWEEN @WeekStartDate AND DATEADD(DAY, 6, @WeekStartDate)
      AND IsBooked = 0
    ORDER BY SlotDate, SlotTime;
END

DECLARE @Today2 DATE = CAST(GETDATE() AS DATE);
EXEC dbo.sp_GetFreeSlotsForWeek @BarberID = 2, @WeekStartDate = @Today2;

-- Процедура 17
CREATE PROCEDURE dbo.sp_ArchiveCompletedVisits
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    INSERT INTO dbo.VisitsArchive (OriginalVisitID, ClientID, BarberID, VisitDate, VisitTime, TotalCost, RatingID, Feedback)
    SELECT VisitID, ClientID, BarberID, VisitDate, VisitTime, TotalCost, RatingID, Feedback
    FROM dbo.Visits
    WHERE VisitDate < CAST(GETDATE() AS DATE);

    INSERT INTO dbo.VisitServicesArchive (ArchiveID, ServiceID)
    SELECT va.ArchiveID, vs.ServiceID
    FROM dbo.VisitServices vs
    JOIN dbo.VisitsArchive va ON va.OriginalVisitID = vs.VisitID
    WHERE vs.VisitID IN (SELECT VisitID FROM dbo.Visits WHERE VisitDate < CAST(GETDATE() AS DATE));

    DELETE FROM dbo.VisitServices
    WHERE VisitID IN (SELECT VisitID FROM dbo.Visits WHERE VisitDate < CAST(GETDATE() AS DATE));

    DELETE FROM dbo.Visits
    WHERE VisitDate < CAST(GETDATE() AS DATE);

    COMMIT TRANSACTION;
END

EXEC dbo.sp_ArchiveCompletedVisits;

-- Процедура 18
CREATE PROCEDURE dbo.sp_GetClientsWithoutFeedbackOrRating
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.*
    FROM dbo.Clients c
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.Visits v
        WHERE v.ClientID = c.ClientID
          AND (v.Feedback IS NOT NULL OR v.RatingID IS NOT NULL)
    )
    AND NOT EXISTS (
        SELECT 1 FROM dbo.VisitsArchive va
        WHERE va.ClientID = c.ClientID
          AND (va.Feedback IS NOT NULL OR va.RatingID IS NOT NULL)
    );
END

EXEC dbo.sp_GetClientsWithoutFeedbackOrRating;

-- Процедура 19
CREATE PROCEDURE dbo.sp_GetClientsInactiveOverYear
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.*, MAX(AllVisits.VisitDate) AS LastVisitDate
    FROM dbo.Clients c
    LEFT JOIN (
        SELECT ClientID, VisitDate FROM dbo.Visits
        UNION ALL
        SELECT ClientID, VisitDate FROM dbo.VisitsArchive
    ) AllVisits ON AllVisits.ClientID = c.ClientID
    GROUP BY c.ClientID, c.FullName, c.Phone, c.Email
    HAVING MAX(AllVisits.VisitDate) < DATEADD(YEAR, -1, GETDATE())
        OR MAX(AllVisits.VisitDate) IS NULL;
END

EXEC dbo.sp_GetClientsInactiveOverYear;
