CREATE TABLE [dbo].[Livre] (
    [idLivre]      INT            IDENTITY (1, 1) NOT NULL,
    [titre]        VARCHAR (50)   NOT NULL,
    [isbn]         VARCHAR (20)   NOT NULL,
    [idEditeur]    INT            NOT NULL,
    [idAuteur]     INT            NOT NULL,
    [idCat]        INT            NOT NULL,
    [descripLivre] VARCHAR (1000) NULL,
    [anneeEdition] INT            NULL,
    PRIMARY KEY CLUSTERED ([idLivre] ASC),
    UNIQUE NONCLUSTERED ([titre] ASC),
    FOREIGN KEY ([idEditeur]) REFERENCES [dbo].[Editeur] ([idEditeur]) ON DELETE CASCADE,
    FOREIGN KEY ([idAuteur]) REFERENCES [dbo].[Auteur] ([idAuteur]) ON DELETE CASCADE,
    FOREIGN KEY ([idCat]) REFERENCES [dbo].[Categorie] ([idCat]) ON DELETE CASCADE
);