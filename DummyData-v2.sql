/*Drop table SubjectRequirement;
Drop table Program;
Drop table ProgramCategory;
Drop table ProgramCourses;
Drop table CompletedProgram;
Drop table CompletedCourses;
Drop table CompletedHighSchoolCourses;
Drop table EntranceRequirement;
Drop table StaffProgram;
Drop table NaitCourses;
Drop table Student;
Drop table HighSchoolCourses;
Drop table ProgramType;
Drop table Category;
Drop table Staff;
*/

insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (1, 'Youbridge');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (2, 'Gabspot');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (3, 'Eadel');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (4, 'Photolist');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (5, 'Thoughtbridge');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (6, 'Brightdog');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (7, 'Npath');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (8, 'Jetpulse');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (9, 'Skidoo');
insert into ProgramType (ProgramTypeID, ProgramTypeDescription) values (10, 'Blogpad');

insert into Category (CategoryID, CategoryDescription) values (1, 'Gabspot');
insert into Category (CategoryID, CategoryDescription) values (2, 'Thoughtworks');
insert into Category (CategoryID, CategoryDescription) values (3, 'Trilia');
insert into Category (CategoryID, CategoryDescription) values (4, 'Tagopia');
insert into Category (CategoryID, CategoryDescription) values (5, 'Camimbo');
insert into Category (CategoryID, CategoryDescription) values (6, 'Geba');
insert into Category (CategoryID, CategoryDescription) values (7, 'Devcast');
insert into Category (CategoryID, CategoryDescription) values (8, 'Brightdog');
insert into Category (CategoryID, CategoryDescription) values (9, 'Flipstorm');
insert into Category (CategoryID, CategoryDescription) values (10, 'Teklist');


insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (1, 'kevans0@si.edu', 1, 'YJaGOg8QELfq');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (2, 'lgibson1@elpais.com', 2, 'iKCo7iCPP37E');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (3, 'jjacobs2@newyorker.com', 3, '0YfYOcRU4f6');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (4, 'jgarrett3@163.com', 4, 'AzXrWQP');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (5, 'sjames4@yellowbook.com', 5, 'SEY18n3diDQu');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (6, 'jramos5@about.com', 6, 'WpmFqnb');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (7, 'mtaylor6@comsenz.com', 7, 'ZVY1Y45EbTd');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (8, 'gwright7@amazon.co.uk', 8, 'kShiK7');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (9, 'acrawford8@ucoz.ru', 9, 'oh3khuJJVf');
insert into Staff (StaffID, StaffEmail, EmployeeID, Password) values (10, 'lcox9@wiley.com', 10, 'wdw7Izky8VFY');


insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (1, 'Realfire', 18, 9, 0, 0, 1, 0, 1, 1);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (2, 'Bubblebox', 3, 9, 1, 1, 0, 1, 0, 2);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (3, 'Voomm', 2, 2, 0, 0, 0, 1, 0, 3);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (4, 'Eamia', 17, 4, 0, 0, 0, 0, 1, 4);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (5, 'Podcat', 1, 9, 1, 1, 1, 0, 1, 5);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (6, 'Katz', 9, 9, 1, 0, 0, 0, 1, 6);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (7, 'Zoombox', 4, 4, 0, 0, 0, 0, 0, 7);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (8, 'Miboo', 4, 6, 1, 0, 1, 0, 1, 8);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (9, 'Edgeblab', 18, 8, 0, 0, 0, 0, 1, 9);
insert into Program (ProgramID, ProgramName, TotalCredits, ProgramLength, Active, WorkOutdoors, ShiftWork, WorkTravel, ProgramLink, ProgramTypeID) values (10, 'Browseblab', 16, 2, 0, 1, 0, 0, 1, 10);


insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (1, 'Jetwire');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (2, 'Photobug');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (3, 'Roomm');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (4, 'Ainyx');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (5, 'Edgeify');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (6, 'Twitternation');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (7, 'Shuffletag');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (8, 'Kaymbo');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (9, 'Gigabox');
insert into HighSchoolCourses (HighSchoolCourseID, HighSchoolCourseName) values (10, 'Twiyo');


insert into Student (StudentID, Email, Password) values (1, 'pday0@constantcontact.com', '9La1Sc');
insert into Student (StudentID, Email, Password) values (2, 'rclark1@state.gov', 'mYuEpZkHJOuX');
insert into Student (StudentID, Email, Password) values (3, 'ahernandez2@omniture.com', 'RODHQ1CG');
insert into Student (StudentID, Email, Password) values (4, 'mshaw3@go.com', 'hUqiSPvPzf');
insert into Student (StudentID, Email, Password) values (5, 'hsmith4@networkadvertising.org', '3Nfpgiz');
insert into Student (StudentID, Email, Password) values (6, 'baustin5@ask.com', 'rENdWz');
insert into Student (StudentID, Email, Password) values (7, 'thenry6@sciencedaily.com', 'Yj8gm8ktGz');
insert into Student (StudentID, Email, Password) values (8, 'lparker7@sohu.com', 'kZb71Wq4');
insert into Student (StudentID, Email, Password) values (9, 'jward8@rakuten.co.jp', 'vUswlIpaG');
insert into Student (StudentID, Email, Password) values (10, 'bgutierrez9@prweb.com', 'GLsPtep7WrUJ');


insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (1, 'Topicblab', 3.7, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (2, 'Devshare', 3.9, 0);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (3, 'Dablist', 3.3, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (4, 'Dazzlesphere', 3.6, 0);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (5, 'Buzzdog', 3.5, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (6, 'Oba', 3.0, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (7, 'Digitube', 3.1, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (8, 'Roodel', 3.9, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (9, 'Fliptune', 4.4, 1);
insert into NaitCourses (CourseID, CourseName, CourseCredits, Active) values (10, 'Eire', 3.5, 0);


insert into StaffProgram (ProgramID, StaffID) values (1, 1);
insert into StaffProgram (ProgramID, StaffID) values (2, 2);
insert into StaffProgram (ProgramID, StaffID) values (3, 3);
insert into StaffProgram (ProgramID, StaffID) values (4, 4);
insert into StaffProgram (ProgramID, StaffID) values (5, 5);
insert into StaffProgram (ProgramID, StaffID) values (6, 6);
insert into StaffProgram (ProgramID, StaffID) values (7, 7);
insert into StaffProgram (ProgramID, StaffID) values (8, 8);
insert into StaffProgram (ProgramID, StaffID) values (9, 9);
insert into StaffProgram (ProgramID, StaffID) values (10, 10);

insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (1, 1, 'Sample1');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (2, 1, 'Sample2');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (3, 2, 'Sample3');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (4, 4, 'This is a subject');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (5, 3, 'SomeRequirement');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (6, 5, 'AnotherRequirement');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (7, 6, 'Subjects');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (8, 7, 'blah blah blah');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (9, 8, 'zoom');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (10, 8, 'faster zoom');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (11, 9, 'beep boop');
insert into SubjectRequirement (SubjectRequirementID, ProgramID, SubjectDescription) values (12, 10, 'beep beep');



insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (1, 1, 52);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (1, 2, 55);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (2, 3, 57);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (3, 3, 81);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (3, 5, 66);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (4, 6, 95);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (4, 7, 51);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (5, 8, 76);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (6, 9, 78);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (7, 8, 66);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (7, 9, 77);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (7, 10, 96);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (8, 4, 88);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (8, 10, 99);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (9, 10, 70);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (10, 10, 60);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (11, 2, 50);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (11, 10, 40);
insert into EntranceRequirement (SubjectrequirementID, HighSchoolCourseID, RequiredMark) values (12, 10, 80);

insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (1, 1, 9);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (1, 2, 49);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (3, 3, 56);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (4, 6, 43);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (5, 5, 61);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (5, 6, 95);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (7, 7, 16);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (8, 1, 62);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (9, 9, 44);
insert into CompletedHighSchoolCourses (HighSchoolCourseID, StudentID, Mark) values (10, 10, 32);


insert into CompletedCourses (CourseID, StudentID) values (1, 1);
insert into CompletedCourses (CourseID, StudentID) values (2, 2);
insert into CompletedCourses (CourseID, StudentID) values (5, 2);
insert into CompletedCourses (CourseID, StudentID) values (4, 4);
insert into CompletedCourses (CourseID, StudentID) values (5, 5);
insert into CompletedCourses (CourseID, StudentID) values (6, 5);
insert into CompletedCourses (CourseID, StudentID) values (6, 7);
insert into CompletedCourses (CourseID, StudentID) values (8, 8);
insert into CompletedCourses (CourseID, StudentID) values (10, 2);
insert into CompletedCourses (CourseID, StudentID) values (10, 10);


insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (1, 1, 3.7);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (2, 2, 2.2);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (3, 3, 3.2);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (4, 4, 3.9);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (5, 5, 2.0);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (6, 6, 4.0);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (7, 7, 1.1);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (8, 8, 3.2);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (9, 9, 3.6);
insert into CompletedProgram (ProgramID, StudentID, StudentGPA) values (10, 10, 1.1);


insert into ProgramCourses (CourseID, ProgramID) values (1, 1);
insert into ProgramCourses (CourseID, ProgramID) values (2, 2);
insert into ProgramCourses (CourseID, ProgramID) values (3, 3);
insert into ProgramCourses (CourseID, ProgramID) values (4, 4);
insert into ProgramCourses (CourseID, ProgramID) values (5, 5);
insert into ProgramCourses (CourseID, ProgramID) values (6, 6);
insert into ProgramCourses (CourseID, ProgramID) values (7, 7);
insert into ProgramCourses (CourseID, ProgramID) values (8, 8);
insert into ProgramCourses (CourseID, ProgramID) values (9, 9);
insert into ProgramCourses (CourseID, ProgramID) values (10, 10);


insert into ProgramCategory (CategoryID, ProgramID) values (1, 1);
insert into ProgramCategory (CategoryID, ProgramID) values (2, 2);
insert into ProgramCategory (CategoryID, ProgramID) values (3, 3);
insert into ProgramCategory (CategoryID, ProgramID) values (4, 4);
insert into ProgramCategory (CategoryID, ProgramID) values (5, 5);
insert into ProgramCategory (CategoryID, ProgramID) values (6, 6);
insert into ProgramCategory (CategoryID, ProgramID) values (7, 7);
insert into ProgramCategory (CategoryID, ProgramID) values (8, 8);
insert into ProgramCategory (CategoryID, ProgramID) values (9, 9);
insert into ProgramCategory (CategoryID, ProgramID) values (10, 10);




