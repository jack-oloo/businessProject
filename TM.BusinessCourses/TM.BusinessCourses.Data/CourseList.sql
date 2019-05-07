CREATE TABLE [dbo].[CourseList]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [course_no] INT NOT NULL, 
    [course_name] NCHAR(200) NOT NULL, 
    [course_section] INT NOT NULL, 
    [instructor_id] INT NOT NULL, 
    [location_id] INT NOT NULL, 
    [days] NVARCHAR(14) NOT NULL, 
    [start_date] DATE NOT NULL, 
    [end_date] DATE NOT NULL, 
    [start_time] TIME NOT NULL, 
    [end_time] TIME NOT NULL, 
    [coordinator_id] INT NULL, 
    [online] BIT NOT NULL
)
