CREATE TABLE [dbo].[Estudiantes] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]          VARCHAR (60) NOT NULL,
    [FechaNacimiento] DATETIME     NOT NULL,
    CONSTRAINT [PK_Estudiantes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

