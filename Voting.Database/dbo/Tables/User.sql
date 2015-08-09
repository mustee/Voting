CREATE TABLE [dbo].[User] (
    [Id]                    BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]             VARCHAR (50)  NOT NULL,
    [LastName]              VARCHAR (50)  NOT NULL,
    [MiddleName]            VARCHAR (50)  NULL,
    [Email]                 VARCHAR (50)  NOT NULL,
    [DateJoined]            DATETIME      NOT NULL,
    [Country]               BIGINT        NULL,
    [MobileNumber]          VARCHAR (50)  NULL,
    [MobileNumberCode]      BIGINT        NULL,
    [MobileNumberConfirmed] BIT           CONSTRAINT [DF_User_MobileNumberConfirmed] DEFAULT ((0)) NOT NULL,
    [ConfirmationToken]     VARCHAR (128) NULL,
    [Confirmed]             BIT           NOT NULL,
    [DateConfirmed]         DATETIME      NULL,
    [LastLogin]             DATETIME      NULL,
    [AuthType]              INT           NULL,
    [AuthID]                VARCHAR (128) NULL,
    [Deleted]               BIT           NOT NULL,
    [Password]              VARCHAR (128) NULL,
    [Salt]                  VARCHAR (128) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

