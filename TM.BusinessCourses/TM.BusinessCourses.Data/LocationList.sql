CREATE TABLE [dbo].[LocationList]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [name] NVARCHAR(100) NOT NULL, 
    [description] NVARCHAR(MAX) NOT NULL
)
