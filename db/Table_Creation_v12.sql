create table CourseEquivalency
(
	CourseEquivalencyID int not null identity(1,1)
			  constraint PK_CourseEquivalency primary key clustered,
	ProgramID int not null constraint FK_CourseEquivalency_to_Program references Program(ProgramID),
	DestinationCourseID int not null constraint FK_CourseEquivalency_to_DestCourse references NaitCourses(CourseID),
	CourseID int not null constraint FK_CourseEquivalency_to_Course references NaitCourses(CourseID)

)
go

create table DegreeEntranceRequirement
(
	DegreeEntranceReqID int not null identity(1,1)
			  constraint PK_DegreeEntranceRequirement primary key clustered,
	ProgramID int not null constraint FK_DegreeEntranceRequirement_to_Program references Program(ProgramID),
	CredentialTypeID int not null constraint FK_DegreeEntranceRequirement_to_Credential references CredentialType(CredentialTypeID),
	CategoryID int not null constraint FK_DegreeEntranceRequirement_to_Category references Category(CategoryID),
	GPA decimal not null
)
go