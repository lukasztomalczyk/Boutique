CREATE TABLE [dbo].[Customers]
(
	[Id] VARCHAR(32) NOT NULL, 
	[InvoiceId] NVARCHAR(MAX) NULL,
    [Name] NVARCHAR(15) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
	[Pesel] VARCHAR(11) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [CreateAt] DATETIME NULL DEFAULT GETDATE(), 
    [RowVersion] ROWVERSION NULL, 
    CONSTRAINT [PK_Customer_Id] PRIMARY KEY CLUSTERED  ([Id] DESC)

) ON [FG_PrimaryKey] TEXTIMAGE_ON [FG_Text]

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Id] ON [dbo].[Customers] ([Id] DESC, [Pesel] DESC) ON [FG_IDX]

GO

CREATE  NONCLUSTERED INDEX [IX_Customers_Name] ON [dbo].[Customers] ([Name] DESC) ON [FG_IDX]

GO

CREATE  NONCLUSTERED INDEX [IX_Customers_LastName] ON [dbo].[Customers] ([LastName] DESC) ON [FG_IDX]

GO

