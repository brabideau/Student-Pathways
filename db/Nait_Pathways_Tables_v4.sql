create database Nait_Pathways

drop table ProgramPreference
drop table PreferenceQuestion
drop table EntranceRequirement
drop table SubjectRequirement
drop table HighSchoolCourses
drop table ProgramCourses
drop table NaitCourses
drop table ProgramCategory
drop table Program
drop table Category
go


create table Category
(
	CategoryID int not null identity(1,1)
				constraint PK_Category primary key clustered,
	CategoryDescription varchar(50) not null
)
go

create table CredentialType
(
	CredentialTypeID int not null identity(1,1) constraint PK_CredentialType primary key clustered,
	CredentialTypeName varchar(20) not null
)

create table Program
(
	ProgramID int not null identity(1,1)
			  constraint PK_Program primary key clustered,
	CredentialTypeID int not null constraint FK_ProgramToCredentialType references CredentialType(CredentialTypeID),
	ProgramName varchar(50) not null,
	ProgramType varchar(10) not null,
	TotalCredits float not null,
	ProgramLength int not null,
	CompetitiveAdvantage int null,
	Active bit not null,
	WorkOutdoors bit not null,
	ShiftWork bit not null,
	WorkTravel bit not null,
	ProgramLink varchar(200) null,
	
)
go

create table ProgramCategory
(
	CategoryID int not null constraint FK_ProgramCategoryToCategory references Category(CategoryID),
	ProgramID int not null constraint FK_ProgramCategoryToProgram references Program(ProgramID)
	constraint PK_ProgramCategory primary key clustered (CategoryID, ProgramID)
)
go

create table NaitCourses
(
	CourseID varchar(10) not null constraint PK_NaitCourses primary key clustered,
	CourseName varchar(100) not null,
	CourseCredits float not null,
	Active bit not null
)
go

create table ProgramCourses
(
	CourseID varchar(10) not null constraint FK_ProgramCoursesToNaitCourses references NaitCourses(CourseID),
	ProgramID int not null constraint FK_ProgramCoursesToProgram references Program(ProgramID)
	constraint PK_ProgramCourses primary key clustered (CourseID, ProgramID)
)
go


create table HighSchoolCourses
(
	HighSchoolCourseID int not null identity(1,1) constraint PK_HighSchoolCourses primary key clustered,
	HighSchoolCourseName varchar(30) not null
)
go


create table SubjectRequirement
(
	SubjectRequirementID int not null identity(1,1) constraint PK_SubjectRequirement primary key clustered,
	SubjectDescription varchar(30) not null 
)
go

create table EntranceRequirement
(
	EntranceRequirementID int not null identity(1,1) constraint PK_EntranceRequirement primary key clustered,
	SubjectRequirementID int not null constraint FK_EntranceRequirementToSubjectRequirement references SubjectRequirement (SubjectRequirementID),
	HighSchoolCourseID int not null constraint FK_EntranceRequirementToCourses references HighSchoolCourses (HighSchoolCourseID),
	ProgramID int not null constraint FK_EnteranceRequirementToProgram references Program (ProgramID),
	RequiredMark int null
				constraint CK_RequiredMark check (RequiredMark between 0 and 100)
	

)
go

create table PreferenceQuestion
(
	QuestionID int not null identity(1,1) constraint PK_PreferenceQuestion primary key clustered,
	Description varchar(30) not null
)
go

create table ProgramPreference
(
	QuestionID int not null constraint FK_ProgramPreferenceToQuestion references PreferenceQuestion (QuestionID),
	ProgramID int not null constraint FK_ProgramPreferenceToProgram references Program (ProgramID),
	Quantity int not null default 0
	constraint PK_ProgramPreference primary key clustered (QuestionID, ProgramID)
)
go

Create nonclustered index ix_ProgramCourses_CourseID
on ProgramCourses(CourseID)

Create nonclustered index ix_ProgramCourses_ProgramID
on ProgramCourses(ProgramID)

Create nonclustered index ix_ProgramCategory_CategoryID
on ProgramCategory(CategoryID)

Create nonclustered index ix_ProgramCategory_ProgramID
on ProgramCategory(ProgramID)

Create nonclustered index ix_EntranceRequirement_HighSchoolCourse
on EntranceRequirement (HighSchoolCourseID)

Create nonclustered index ix_EntranceRequirement_SubjectRequirement
on EntranceRequirement (SubjectRequirementID)

Create nonclustered index ix_EntranceRequirement_Program
on Program(ProgramID)

Create nonclustered index ix_ProgramPreferences_QuestionID
on ProgramPreference (QuestionID)

Create nonclustered index ix_ProgramPreferences_ProgramID
on ProgramPreference (ProgramID)

