﻿create database Permisos
go
use Permisos
go
create table Empleado (
	Id int identity(1,1) not null,
	NombreEmpleado varchar(100) not null,
	ApellidosEmpleado varchar(100) not null,
	FechaNacimiento date not null,
	FechaIngreso date not null,
	constraint PK_Empleado primary key clustered(
		Id
	)
)
go

create table Permiso (
	Id int identity(1,1) not null,
	EmpleadoId int not null,
	TipoPermisoId int not null,	
	FechaHoraInicioPermiso datetime not null,
	FechaHoraFinPermiso datetime not null,
	constraint PK_Permiso primary key clustered(
		Id
	)
)
go

create table TipoPermiso (
	Id int identity(1,1) not null,
	Descripcion varchar(150) not null,	
	constraint PK_TipoPermiso primary key clustered(
		Id
	)
)
go

alter table Permiso
	add 
	constraint FK_PermisoEmpleado foreign key (EmpleadoId) references Empleado(Id),
	constraint FK_PermisoTipoPermiso foreign key (TipoPermisoId) references TipoPermiso(Id);
	
go

use master 
go
create login userPersonal with password=N'password', default_database=Permisos
create user userPersonal for login userPersonal
go

use Permisos
go
create user userPersonal for login userPersonal
go
alter role db_owner add MEMBER userPersonal
go

use Permisos
go
create view viewPermiso
as
	select 
		p.Id, p.EmpleadoId, p.TipoPermisoId, p.FechaHoraInicioPermiso, p.FechaHoraFinPermiso
		, e.NombreEmpleado + ' ' +  e.ApellidosEmpleado as Empleado
		, e.NombreEmpleado 
		, e.ApellidosEmpleado
		, tp.Descripcion as TipoPermiso
	from Permiso p 
	inner join Empleado e on e.Id = p.EmpleadoId
	inner join TipoPermiso tp on tp.Id = p.TipoPermisoId
go

use Permisos
go

create procedure listEmpladoTodosPermisos
  @fechaHoraInicioPermiso  datetime
	, @fechaHoraFinPermiso datetime
as
	declare @countTiposPermiso int
	select  @countTiposPermiso = count(Id) from TipoPermiso

	select e.* from Empleado e where e.Id = (
		select fill.EmpleadoId 
			from (select EmpleadoId from Permiso 
					where ((FechaHoraInicioPermiso <=  @fechaHoraInicioPermiso and FechaHoraFinPermiso > @fechaHoraInicioPermiso)
						or (FechaHoraInicioPermiso <= @fechaHoraFinPermiso and FechaHoraFinPermiso >= @fechaHoraFinPermiso)
						or (@fechaHoraInicioPermiso < FechaHoraInicioPermiso and @fechaHoraFinPermiso >= FechaHoraInicioPermiso))
					group by EmpleadoId, TipoPermisoId) as fill	
			group by fill.EmpleadoId having count(fill.EmpleadoId) = @countTiposPermiso
			)
go

create procedure listPermisoEmpleadoCantidad 
	  @fechaHoraInicioPermiso  datetime
	, @fechaHoraFinPermiso datetime
as
	select NombreEmpleado, ApellidosEmpleado, EmpleadoId,  count(id) as Cantidad 
		from viewPermiso 
		where ((FechaHoraInicioPermiso <=  @fechaHoraInicioPermiso and FechaHoraFinPermiso > @fechaHoraInicioPermiso)
				or (FechaHoraInicioPermiso <= @fechaHoraFinPermiso and FechaHoraFinPermiso >= @fechaHoraFinPermiso)
				or (@fechaHoraInicioPermiso < FechaHoraInicioPermiso and @fechaHoraFinPermiso >= FechaHoraInicioPermiso))
		group by EmpleadoId, NombreEmpleado, ApellidosEmpleado
		order by count(id) desc,ApellidosEmpleado desc,NombreEmpleado desc
go

create procedure deletePermiso @id int
as
	declare @idPermiso int = 0
		delete from Permiso where Id=@id
		set @idPermiso = @id
	select @idPermiso

go

create procedure updatePermiso @id int
	, @empleadoId int
	, @tipoPermisoId  int
	, @fechaHoraInicioPermiso  datetime
	, @fechaHoraFinPermiso datetime
as
	declare @idPermiso int = 0	
	declare @count int = 0	
	select @count = count(id) from Permiso 
		where ((FechaHoraInicioPermiso <=  @fechaHoraInicioPermiso and FechaHoraFinPermiso > @fechaHoraInicioPermiso)
				or (FechaHoraInicioPermiso <= @fechaHoraFinPermiso and FechaHoraFinPermiso >= @fechaHoraFinPermiso)
				or (@fechaHoraInicioPermiso < FechaHoraInicioPermiso and @fechaHoraFinPermiso >= FechaHoraInicioPermiso))
				and EmpleadoId <> @empleadoId
	if (@count = 0)
	begin
		update Permiso set 
			EmpleadoId = @empleadoId
			, TipoPermisoId = @tipoPermisoId
			, FechaHoraInicioPermiso = @fechaHoraInicioPermiso
			, FechaHoraFinPermiso = @fechaHoraFinPermiso
		where Id = @id
		set @idPermiso = @id	
	end
	select @idPermiso
go

create procedure savePermiso @empleadoId int
	, @tipoPermisoId  int
	, @fechaHoraInicioPermiso  datetime
	, @fechaHoraFinPermiso datetime
as
	declare @idPermiso int = 0	
	declare @count int = 0	
	select @count = count(id) from Permiso 
		where ((FechaHoraInicioPermiso <=  @fechaHoraInicioPermiso and FechaHoraFinPermiso > @fechaHoraInicioPermiso)
				or (FechaHoraInicioPermiso <= @fechaHoraFinPermiso and FechaHoraFinPermiso >= @fechaHoraFinPermiso)
				or (@fechaHoraInicioPermiso < FechaHoraInicioPermiso and @fechaHoraFinPermiso >= FechaHoraInicioPermiso))
				and EmpleadoId =  @empleadoId
	if (@count = 0)
	begin		
		insert into Permiso(EmpleadoId, TipoPermisoId, FechaHoraInicioPermiso, FechaHoraFinPermiso)
				values(@empleadoId,@tipoPermisoId,@fechaHoraInicioPermiso,@fechaHoraFinPermiso)
		set @idPermiso = SCOPE_IDENTITY();	
	end
	select @idPermiso
go

create procedure getPermiso @id int
as
	select Id, EmpleadoId, TipoPermisoId, Empleado, TipoPermiso, FechaHoraInicioPermiso, FechaHoraFinPermiso
	from viewPermiso 
	where Id = @id
go

create procedure listPermisoEmpleado @empleadoId int
as
	select Id, EmpleadoId, TipoPermisoId, Empleado, TipoPermiso, FechaHoraInicioPermiso, FechaHoraFinPermiso
	from viewPermiso 
	where EmpleadoId = @empleadoId
go

create procedure deleteEmpleado @id int
as
	declare @idEmplado int = 0
	declare @count int = 0
	select @count = count(Id) from Permiso where EmpleadoId = @id
	if(@count = 0)
	begin
		delete from Empleado where Id=@id
		set @idEmplado = @id
	end
	select @idEmplado

go

create procedure updateEmpleado @id int
	, @nombreEmpleado varchar(100)
	, @apellidosEmpleado  varchar(100)
	, @fechaNacimiento  varchar(10)
	, @fechaIngreso  varchar(10)
as
	declare @idEmpleado int = 0		
		update Empleado set 
			NombreEmpleado = @nombreEmpleado
			, ApellidosEmpleado = @apellidosEmpleado
			, FechaNacimiento = @fechaNacimiento
			, FechaIngreso = @fechaIngreso
		where Id = @id
		set @idEmpleado = @id
	
	select @idEmpleado

go

create procedure saveEmpleado @nombreEmpleado varchar(100)
	, @apellidosEmpleado  varchar(100)
	, @fechaNacimiento  varchar(10)
	, @fechaIngreso  varchar(10)
as
	declare @idEmpleado int = 0		
	insert into Empleado(NombreEmpleado, ApellidosEmpleado, FechaNacimiento, FechaIngreso)
			values(@nombreEmpleado,@apellidosEmpleado,@fechaNacimiento,@fechaIngreso)
	set @idEmpleado = SCOPE_IDENTITY();	
	select @idEmpleado
go

create procedure listEmpleado
as
	select Id, NombreEmpleado, ApellidosEmpleado, FechaNacimiento, FechaIngreso from Empleado
go

create procedure getEmpleado @id int
as
	select Id, NombreEmpleado, ApellidosEmpleado, FechaNacimiento, FechaIngreso from Empleado where Id = @id
go


create procedure deleteTipoPermiso @id int
as
	declare @idTipoPermiso int = 0
	declare @count int = 0
	select @count = count(id) from Permiso where TipoPermisoId = @id
	if(@count = 0)
	begin
		delete from TipoPermiso where Id=@id
		set @idTipoPermiso = @id
	end
	select @idTipoPermiso

go

create procedure updateTipoPermiso @id int, @descripcion varchar(150)
as
	declare @idTipoPermiso int = 0
	declare @count int = 0
	select @count = count(id) from TipoPermiso where ltrim(rtrim(Descripcion)) = ltrim(rtrim(@descripcion))
	if(@count = 0)
	begin
		update TipoPermiso set Descripcion = @descripcion where Id = @id
		set @idTipoPermiso = @id
	end
	select @idTipoPermiso

go

create procedure saveTipoPermiso @descripcion varchar(200)
as
	declare @idTipoPermiso int = 0
	declare @count int = 0
	select @count = count(id) from TipoPermiso where ltrim(rtrim(Descripcion)) = ltrim(rtrim(@descripcion))
	if(@count = 0)
	begin 
		insert into TipoPermiso(Descripcion) values(@descripcion)
	
		set @idTipoPermiso = SCOPE_IDENTITY();
	end
	select @idTipoPermiso
go

create procedure listTipoPermiso
as
	select Id, Descripcion from TipoPermiso
go