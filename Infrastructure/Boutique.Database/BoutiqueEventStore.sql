CREATE TABLE [dbo].[BoutiqueEventStore]
(
	[Id] INT IDENTITY(1,1),
	[Type] VARCHAR(50) NOT NULL,
	[AdditionalData] NVARCHAR(MAX) NOT NULL,
	[CreateAt] DATETIME2(2) NOT NULL,

	CONSTRAINT [PK_BoutiqueEventStore_Id] PRIMARY KEY CLUSTERED  ([Id] DESC)
)
ON [FG_PrimaryKey] TEXTIMAGE_ON [FG_Text]
