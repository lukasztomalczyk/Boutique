-- $DatabaseName$
print '$DatabaseName$'

ALTER DATABASE $DatabaseName$
	ADD FILEGROUP [FG_PrimaryKey]

ALTER DATABASE $DatabaseName$
	ADD FILEGROUP [FG_IDX]

ALTER DATABASE $DatabaseName$
	ADD FILEGROUP [FG_Text]