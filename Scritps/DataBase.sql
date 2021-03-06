USE [dbESX]
GO
/****** Object:  Table [dbo].[tblMarca]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMarca](
	[MarcaId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[DataInclusaoRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_tblMarca_MarcaId] PRIMARY KEY CLUSTERED 
(
	[MarcaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPatrimonio]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPatrimonio](
	[PatrimonioId] [int] IDENTITY(1,1) NOT NULL,
	[MarcaId] [int] NOT NULL,
	[Descricao] [varchar](100) NULL,
	[NumeroTombo] [uniqueidentifier] NOT NULL,
	[DataInclusaoRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_tblPatrimonio_PatrimonioId] PRIMARY KEY CLUSTERED 
(
	[PatrimonioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UNK_tblPatrimonio_NumeroTombo] UNIQUE NONCLUSTERED 
(
	[NumeroTombo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRole]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[DataInclusaoRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_tblRole_RoleId] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UNK_tblRole_Nome] UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsuario]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsuario](
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[Senha] [binary](64) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
	[DataInclusaoRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_tblUsuario_UsuarioId] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UNK_tblUsuario_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsuarioRole]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsuarioRole](
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
	[DataInclusaoRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_tblUsuarioRole_UsuarioId_RoleId] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblMarca] ADD  DEFAULT (getutcdate()) FOR [DataInclusaoRegistro]
GO
ALTER TABLE [dbo].[tblPatrimonio] ADD  DEFAULT (newid()) FOR [NumeroTombo]
GO
ALTER TABLE [dbo].[tblPatrimonio] ADD  DEFAULT (getutcdate()) FOR [DataInclusaoRegistro]
GO
ALTER TABLE [dbo].[tblRole] ADD  DEFAULT (getutcdate()) FOR [DataInclusaoRegistro]
GO
ALTER TABLE [dbo].[tblUsuario] ADD  DEFAULT (newid()) FOR [UsuarioId]
GO
ALTER TABLE [dbo].[tblUsuario] ADD  DEFAULT (getutcdate()) FOR [DataInclusaoRegistro]
GO
ALTER TABLE [dbo].[tblUsuarioRole] ADD  DEFAULT (getutcdate()) FOR [DataInclusaoRegistro]
GO
ALTER TABLE [dbo].[tblPatrimonio]  WITH CHECK ADD  CONSTRAINT [FK_tblPatrimonio_tblMarca_MarcaId] FOREIGN KEY([MarcaId])
REFERENCES [dbo].[tblMarca] ([MarcaId])
GO
ALTER TABLE [dbo].[tblPatrimonio] CHECK CONSTRAINT [FK_tblPatrimonio_tblMarca_MarcaId]
GO
ALTER TABLE [dbo].[tblUsuarioRole]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarioRole_tblRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[tblRole] ([RoleId])
GO
ALTER TABLE [dbo].[tblUsuarioRole] CHECK CONSTRAINT [FK_tblUsuarioRole_tblRole_RoleId]
GO
ALTER TABLE [dbo].[tblUsuarioRole]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarioRole_tblUsuario_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[tblUsuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[tblUsuarioRole] CHECK CONSTRAINT [FK_tblUsuarioRole_tblUsuario_UsuarioId]
GO
/****** Object:  StoredProcedure [dbo].[USP_Marca_DEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Marca_DEL
-- Data: 19/01/2019 16:18
-- Função: Remover Registro na tabela tblMarca
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Marca_DEL]
	@MarcaId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM tblMarca
	WHERE MarcaId = @MarcaId

END
GO
/****** Object:  StoredProcedure [dbo].[USP_Marca_INS]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Marca_INS
-- Data: 19/01/2019 16:15
-- Função: Inserir Registro na tabela tblMarca
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Marca_INS]
	@Nome VARCHAR (100)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO tblMarca(Nome)
	OUTPUT inserted.MarcaId
	VALUES(@Nome)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Marca_SEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Marca_SEL
-- Data: 19/01/2019 16:18
-- Função: Seleciona Registro na tabela tblMarca
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Marca_SEL]
	@MarcaId INT = NULL,
	@Nome VARCHAR(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		MarcaId, 
		Nome, 
		DataInclusaoRegistro 
	FROM tblMarca
	WHERE 
		(@MarcaId IS NULL OR MarcaId = @MarcaId)
	AND (@Nome IS NULL OR Nome = @Nome)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Marca_UPD]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Marca_UPD
-- Data: 19/01/2019 16:18
-- Função: Atualiza Registro na tabela tblMarca
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Marca_UPD]
	@MarcaId INT,
	@Nome VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE tblMarca
	SET 
		Nome = @Nome
	WHERE 
		MarcaId = @MarcaId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Patrimonio_DEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Patrimonio_DEL
-- Data: 19/01/2019 16:18
-- Função: Remover Registro na tabela tblPatrimonio
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Patrimonio_DEL]
	@PatrimonioId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM tblPatrimonio
	WHERE PatrimonioId = @PatrimonioId

END
GO
/****** Object:  StoredProcedure [dbo].[USP_Patrimonio_INS]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Patrimonio_INS
-- Data: 19/01/2019 16:24
-- Função: Inserir Registro na tabela tblPatrimonio
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Patrimonio_INS]
	@MarcaId INT,
	@Descricao VARCHAR(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO tblPatrimonio(MarcaId,Descricao)
	OUTPUT 
		inserted.PatrimonioId, 
		inserted.MarcaId, 
		inserted.Descricao,
		CAST(inserted.NumeroTombo AS VARCHAR(36)) AS NumeroTombo, 
		inserted.DataInclusaoRegistro	
	VALUES(@MarcaId,@Descricao)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Patrimonio_SEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Patrimonio_SEL
-- Data: 19/01/2019 16:26
-- Função: Seleciona Registro na tabela tblPatrimonio
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Patrimonio_SEL]
	@PatrimonioId INT = NULL,
	@MarcaId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		PatrimonioId,
		MarcaId,
		Descricao,
		CAST(NumeroTombo AS VARCHAR(36)) AS NumeroTombo,
		DataInclusaoRegistro
	FROM tblPatrimonio
	WHERE 
		(@PatrimonioId IS NULL OR PatrimonioId = @PatrimonioId)
	AND (@MarcaId IS NULL OR MarcaId = @MarcaId)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Patrimonio_UPD]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Patrimonio_UPD
-- Data: 19/01/2019 16:28
-- Função: Atualiza Registro na tabela tblPatrimonio
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Patrimonio_UPD]
	@PatrimonioId INT,
	@MarcaId INT,
	@Descricao VARCHAR(100) NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE tblPatrimonio
	SET 
		MarcaId = @MarcaId,
		Descricao = @Descricao
	WHERE 
		PatrimonioId = @PatrimonioId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Role_INS]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Role_INS
-- Data: 19/01/2019 16:48
-- Função: Inserir Registro na tabela tblRole
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Role_INS]
	@Nome VARCHAR(30)	
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO tblRole(Nome)
	OUTPUT inserted.RoleId
	VALUES(@Nome)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Role_SEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Role_SEL
-- Data: 19/01/2019 16:46
-- Função: Seleciona Registro na tabela tblRole
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Role_SEL]
	@RoleId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		RoleId,
		Nome,
		DataInclusaoRegistro
	FROM tblRole
	WHERE 
		@RoleId IS NULL OR RoleId = @RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Usuario_DEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Usuario_DEL
-- Data: 19/01/2019 16:33
-- Função: Remover Registro na tabela tblUsuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Usuario_DEL]
	@UsuarioId VARCHAR(36)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM tblUsuario
	WHERE UsuarioId = CAST(@UsuarioId AS UNIQUEIDENTIFIER)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_Usuario_INS]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Usuario_INS
-- Data: 19/01/2019 16:36
-- Função: Inserir Registro na tabela tblUsuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Usuario_INS]
	@Login VARCHAR(100),
	@Senha VARCHAR(50),
	@Nome VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO tblUsuario(Login,Senha,Nome)
	OUTPUT inserted.UsuarioId
	VALUES(@Login,HASHBYTES('SHA2_512', @Senha),@Nome)
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Usuario_SEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Usuario_SEL
-- Data: 19/01/2019 16:32
-- Função: Seleciona Registro na tabela tblUsuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Usuario_SEL]
	@UsuarioId VARCHAR(16)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		UsuarioId,
		Login,
		Nome,
		DataInclusaoRegistro
	FROM tblUsuario
	WHERE 
		UsuarioId = @UsuarioId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Usuario_UPD]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_Usuario_UPD
-- Data: 19/01/2019 16:31
-- Função: Atualiza Registro na tabela tblUsuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_Usuario_UPD]
	@UsuarioId VARCHAR(16),
	@Nome VARCHAR(200),
	@Senha VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE tblUsuario
	SET 
		Nome = @Nome,
		Senha = HASHBYTES('SHA2_512', @Senha)
	WHERE 
		UsuarioId = @UsuarioId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UsuarioLogin]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_UsuarioLogin
-- Data: 19/01/2019 20:11
-- Função: Login do usuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_UsuarioLogin]
	@Login VARCHAR(100),
	@Senha VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		CAST(UsuarioId AS VARCHAR(36)) AS UsuarioId,
		Login,
		Nome,
		DataInclusaoRegistro
	FROM tblUsuario
	WHERE 
		Login = @Login
	AND Senha = HASHBYTES('SHA2_512',@Senha)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UsuarioRole_DEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_UsuarioRole_DEL
-- Data: 19/01/2019 16:49
-- Função: Remover Registro na tabela tblUsuarioRole
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_UsuarioRole_DEL]
	@UsuarioId VARCHAR(36),
	@RoleId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM tblUsuarioRole
	WHERE 
		UsuarioId = CAST(@UsuarioId AS UNIQUEIDENTIFIER)
	AND RoleId = @RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UsuarioRole_INS]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_UsuarioRole_INS
-- Data: 19/01/2019 16:48
-- Função: Inserir Registro na tabela tblUsuarioRole
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_UsuarioRole_INS]
	@UsuarioId VARCHAR(36),
	@RoleId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO tblUsuarioRole(UsuarioId,RoleId)	
	VALUES(@UsuarioId,@RoleId)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UsuarioRole_SEL]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_UsuarioRole_SEL
-- Data: 19/01/2019 20:23
-- Função: Seleciona todas as Roles do Usuario
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_UsuarioRole_SEL]
	@UsuarioId VARCHAR(36)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		A.RoleId,
		A.Nome,
		A.DataInclusaoRegistro	
	FROM tblRole A
	INNER JOIN tblUsuarioRole B ON A.RoleId = B.RoleId
	WHERE 
		B.UsuarioId = CAST(@UsuarioId AS UNIQUEIDENTIFIER)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_VerificaUsuarioLogin]    Script Date: 21/01/2019 01:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Nome: USP_VerificaUsuarioLogin
-- Data: 19/01/2019 22:36
-- Função: Verifica se login do usaurio ja foi cadastrado
-- Autor: Humberto Pereira
--=============================================
CREATE PROCEDURE [dbo].[USP_VerificaUsuarioLogin]
	@Login VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT COUNT(UsuarioId) 
	FROM tblUsuario
	WHERE 
		Login = @Login	
END

GO