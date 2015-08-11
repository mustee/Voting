CREATE TABLE [dbo].[Vote] (
    [Id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserId]      BIGINT   NOT NULL,
    [CandidateId] BIGINT   NOT NULL,
    [TimeStamp]   DATETIME NOT NULL,
    CONSTRAINT [PK_Vote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vote_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([Id]),
    CONSTRAINT [FK_Vote_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);







