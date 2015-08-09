CREATE TABLE [dbo].[Country] (
    [Id]                  BIGINT       NOT NULL,
    [Name]                VARCHAR (50) NOT NULL,
    [FIPS104]             VARCHAR (50) NULL,
    [ISO2]                VARCHAR (50) NULL,
    [ISO3]                VARCHAR (50) NULL,
    [ISON]                VARCHAR (50) NULL,
    [Internet]            VARCHAR (50) NULL,
    [Capital]             VARCHAR (50) NULL,
    [MapReference]        VARCHAR (50) NULL,
    [NationalitySingular] VARCHAR (50) NULL,
    [NationalityPlural]   VARCHAR (50) NULL,
    [Currency]            VARCHAR (50) NULL,
    [CurrencyCode]        VARCHAR (50) NULL,
    [Population]          BIGINT       NOT NULL,
    [Title]               VARCHAR (50) NULL,
    [Comment]             TEXT         NULL,
    [PhoneCode]           VARCHAR (50) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Id] ASC)
);

