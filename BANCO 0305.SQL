
create database isports
go

use isports
go

create table usuarios
(
	id        int          not null	 primary key identity,
	estatus   int,
	email     varchar(250) not null  unique,
	nome      varchar(50)  not null,
	sobrenome varchar(50)  not null,
	dataNasc  date         not null,
	cel       varchar(20)  null,
	senha 	  varchar(50)  not null,
	foto_perfil  varchar(max),
	check (estatus in (1,2,3,4))
)
go

create table organizadores
(
	id_usuario   int not null primary key references usuarios,
	permissao    int,
	qualificacao decimal(10,2)
)
go

create table ufs
(	
	sigla  varchar(5)	not null primary key,
	nome   varchar(100) not null
)
go

create table cidades
(
	cod    int			not null primary key identity,
	nome   varchar(100) not null,
	estado varchar(5)	not null references ufs
)
go

create table locais
(
	id        	int          not null primary key identity,
	endereco  	varchar(500) not null unique,
	descricao	varchar(max),
	nome      	varchar(200) not null unique,	
	cod_cidade  int          not null references cidades
)
go


create table esportes
(
	id        int not null primary key identity,
	descricao varchar(50)
)
go

create table eventos
(
	id             int not null primary key identity,
	estatus        int,
	maxJogadores   int not null,
	id_esp         int not null references esportes,
	id_organizador int not null references organizadores,
	id_local       int not null references locais,
	imagens_evento varchar(max)   null,
	horario_evento varchar(20) not null,	
	data_evento    varchar(20) not null,
	nome 		   varchar(50) not null,
	descricao      varchar(300) not null
)
go

create table noticias
(
	id           int  not null primary key identity,
	id_evento    int  not null references eventos,
	descricao    varchar(max),
	data_noticia date not null
)
go

create table evento_usuario
(
	id_evento  	int not null,
	id_usuario 	int not null,
	status     	int not null,
	nota_evento	int null,
	primary key(id_evento, id_usuario)
)
go

Create Procedure CadUsuario
(
	@estatus     int,
	@email       varchar(250),
	@nome        varchar(50),
	@sobreNome   varchar(50),
	@dataNasc    date,
	@cel         varchar(20),
	@senha 	     varchar(50),
	@foto_perfil varchar(max)
)
as
begin
	insert into usuarios values (@estatus, @email, @nome, @sobreNome, @dataNasc, @cel, @senha, @foto_perfil)
	insert into organizadores values(@@IDENTITY, 1, 0)
end
Go

create procedure Logar
(
	@email varchar(250),
	@senha varchar(50)
)
as
begin
	select * from usuarios where email = @email and senha = @senha
end
go

Create Procedure CadEvento
(
	@nome				varchar(50),
	@descricao			varchar(300),
	@maxJogadores		int,
	@idSport			int,
	@idOrganizador		int,
	@endereco			varchar(500),
	@descricao_local	varchar(max),
	@local_nome			varchar(200),
	@dataEvento			varchar(20),
	@Horario		    varchar(20),
	@imagem				varchar(max),
	@idCidade			int
)
as
begin
	insert into locais values (@endereco, @descricao_local, @local_nome, @idCidade)
	insert into eventos values (1, @maxJogadores, @idSport, @idOrganizador, @@IDENTITY, @imagem, @Horario, @dataEvento, @nome, @descricao)
end
Go

create view v_esporte
as
select e.id			 codigoEsporte,
	   e.descricao   ESPORTE

from esportes e
go

create view v_noticia
as
select n.id				codigoNoticia,
	   n.id_evento		codigoEvento,
	   n.descricao		noticia,
	   n.data_noticia	dataPostagem

from noticias n
go

create view v_cidade
as
select c.cod    codigoCidade,
	   c.nome   nomeCidade,
	   e.sigla  UF,
	   e.nome   Estado

from cidades c, ufs e
where c.estado = e.sigla
go

create view v_organizador
as
select u.id				IdOrganizador,
	   u.nome			NomeOrganizador,
	   u.sobrenome		SobrenomeOrganizador,
	   u.email          EmailOrganizador,
	   u.cel            CelularOrganizador,
	   u.estatus        StatusUsuario,
	   u.foto_perfil	FotoOrganizador,
	   o.permissao      StatusOrganizador,
	   o.qualificacao   QualificacaoOrg

from usuarios u, organizadores o

where u.id = o.id_usuario and
	  u.estatus = 1
go

create view v_evento 
as
select e.id						Codigo,
	   e.estatus				Status,
	   e.nome					NomeEvento,
	   e.descricao				DescricaoEvento,
	   e.data_evento			DataEvento,
	   e.horario_evento			HoraEvento,
	   e.maxJogadores			MaxJogador,
	   e.imagens_evento			FotoEvento,
	   p.id						IdEsport,
	   p.descricao				NomeEsporte,
	   o.IdOrganizador			IdOrganizador,
	   o.NomeOrganizador		NomeOrganizador,
	   o.SobrenomeOrganizador	SobrenoeOrganizador,
	   o.FotoOrganizador		FotoOrganizador,
	   l.id						IdLocal,
	   l.nome					NomeLocal,
	   l.descricao				DescricaoLocal,
	   l.endereco				EnderecoLocal,
	   c.codigoCidade			CodigoCidade,
	   c.nomeCidade             Cidade,
	   c.UF						SiglaEstado,
	   c.Estado                 Estado
	   

from eventos e, esportes p, locais l, v_organizador o, v_cidade c
where p.id = e.id_esp and
	  l.id = e.id_local and
	  c.codigoCidade = l.cod_cidade  and
	  o.IdOrganizador= e.id_organizador and
	  o.StatusOrganizador = 1
go

create view v_usuario_evento
as
select u.id					IdUsuario,
	   u.nome				NomeUsuario,
	   u.sobrenome          SobreNomeUsuario,
	   u.dataNasc			DataNascUsuario,
	   u.cel                TelefoneUsuario,
	   u.email              EmailUsuario,
	   u.foto_perfil        FotoUsuario,
	   e.Codigo				IdEvento,
	   e.NomeEvento			NomeEvento,
	   e.DescricaoEvento	DescricaoEvento,
	   e.FotoEvento         FotoEvento,
	   uv.status			Status,
	   uv.nota_evento		Nota


from evento_usuario uv, v_evento e, usuarios u

where   uv.id_usuario = u.id and
		uv.id_evento = e.Codigo
go

Create Procedure UpdateEvento
(
	@idEvento           int,
	@nome				varchar(50),
	@descricao			varchar(300),
	@maxJogadores		int,
	@idSport			int,
	@idLocal			int,
	@endereco			varchar(500),
	@descricao_local	varchar(max),
	@local_nome			varchar(200),
	@dataEvento			varchar(20),
	@Horario		    varchar(20),
	@idCidade			int
)
as
begin
	UPDATE locais SET endereco = @endereco, descricao = @descricao_local, nome = @local_nome, cod_cidade = @idCidade 
					   WHERE id = @idLocal
	
	UPDATE eventos SET estatus = 1, maxJogadores = @maxJogadores, id_esp = @idSport, 
					   horario_evento = @Horario, data_evento =  @dataEvento, nome = @nome, descricao = @descricao
					   WHERE id = @idEvento
end
go

Create Procedure CadNoticiaEvento
(
	@idEvento     int,
	@Msg		  varchar(max)
)
as
begin
	insert into noticias values(@idEvento, @Msg, GETDATE ())
end
go

	