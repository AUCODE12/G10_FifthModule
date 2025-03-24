create table [User] (
	Id bigint identity(1, 1) primary key,
	Name nvarchar(100) not null,
	Email nvarchar(255) unique not null,
	Role nvarchar(50) not null
);

go
create procedure sp_AddUserAsync
	@Id bigint output,
	@Name nvarchar(100),
	@Email nvarchar(255),
	@Role nvarchar(50)
as
begin
	insert into [User] (Name, Email, Role)
	values (@Name, @Email, @Role);

	set @Id = SCOPE_IDENTITY(); 
end
go
create procedure sp_UpdateUserAsync
	@Id bigint,
	@Name nvarchar(100),
	@Email nvarchar(255),
	@Role nvarchar(50)
as
begin
	update [User]
	set Name = @Name, Email = @Email, Role = @Role
	where Id = @Id;
end
go
create procedure sp_DeleteUserAsync
	@Id bigint
as
begin
	delete from [User] where Id = @Id;
end
go
create procedure sp_GetAllUser
as
begin
	select * from [User];
end
go
create procedure sp_GetUserById
	@Id bigint
as
begin
	select * from [User] where Id = @Id;
end
go

create table Election (
	Id bigint identity(1, 1) primary key,
	Name nvarchar(100) not null,
	Description nvarchar(500),
	StartDate datetime not null,
	EndDate datetime not null
);

create table Candidate (
	Id bigint identity(1, 1) primary key,
	Name nvarchar(100) not null,
	Party nvarchar(100) not null,
	ElectionId bigint not null,
	constraint FK_Candidate_Election foreign key (ElectionId) references Election(Id) on delete cascade
);

create table Vote (
    Id bigint identity(1, 1) primary key,
    UserId bigint  not null,
    ElectionId bigint not null,
    CandidateId bigint not null,
    constraint FK_Vote_User foreign key (UserId) references [User](Id) on delete cascade,
    constraint FK_Vote_Election foreign key (ElectionId) references Election(Id) on delete cascade,
    constraint FK_Vote_Candidate foreign key (CandidateId) references Candidate(Id) 
);

create table Result (
	Id bigint identity(1, 1) primary key,
	TotalVotes int not null,
	ElectionId bigint unique not null,
    WinnerCandidateId bigint not null,
	constraint FK_Result_Election foreign key (ElectionId) references Election(Id) on delete cascade,
	constraint FK_Result_Candidate foreign key (WinnerCandidateId) references Candidate(Id) 
);

create table Comment (
	Id bigint identity(1, 1) primary key,
	Massage nvarchar(500) not null,
	UserId bigint not null,
	ElectionId bigint not null,
	constraint FK_Comment_User foreign key (UserId) references [User](Id) on delete cascade,
	constraint FK_Comment_Election foreign key (ElectionId) references Election(Id) on delete cascade
);

create table Notification (
	Id bigint identity(1, 1) primary key,
	Message nvarchar(500) not null,
	ReadStatus bit default 0,
	UserId bigint not null,
	constraint FK_Notification_User foreign key (UserId) references [User](Id) on delete cascade
);

create table Campaign (
	Id bigint identity(1, 1) primary key,
	Description nvarchar(500),
	CandidateId bigint not null,
	constraint FK_Campaign_Candidate foreign key (CandidateId) references Candidate(Id) on delete cascade
);

create table Debate (
	Id bigint identity(1, 1) primary key,
	Topic nvarchar(200) not null,
    Date datetime not null,
	ElectionId bigint not null,
	constraint FK_Debate_Election foreign key (ElectionId) references Election(Id) on delete cascade
);



