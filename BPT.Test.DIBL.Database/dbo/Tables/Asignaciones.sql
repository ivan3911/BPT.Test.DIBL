CREATE TABLE [dbo].[Asignaciones] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_Asignaciones] PRIMARY KEY CLUSTERED ([Id] ASC)
);

