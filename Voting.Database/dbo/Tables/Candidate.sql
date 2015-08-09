CREATE TABLE [dbo].[Candidate] (
    [Id]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NOT NULL,
    [LastName]   VARCHAR (50) NOT NULL,
    [MiddleName] VARCHAR (50) NULL,
    [PositionId] BIGINT       NOT NULL,
    CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Candidate_Position] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Position] ([Id])
);

