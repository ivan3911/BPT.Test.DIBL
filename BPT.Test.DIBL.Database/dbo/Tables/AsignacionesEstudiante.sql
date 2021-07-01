CREATE TABLE [dbo].[AsignacionesEstudiante] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [IdAsignacion] INT NULL,
    [IdEstudiante] INT NULL,
    CONSTRAINT [PK_AsignacionesEstudiante] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AsignacionesEstudiante_Asignaciones] FOREIGN KEY ([IdAsignacion]) REFERENCES [dbo].[Asignaciones] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AsignacionesEstudiante_Estudiantes] FOREIGN KEY ([IdEstudiante]) REFERENCES [dbo].[Estudiantes] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

