
namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        passwordHash = c.Binary(),
                        passwordSalt = c.Binary(),
                        role = c.String(),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        nation = c.String(),
                        gender = c.Boolean(nullable: false),
                        dob = c.String(),
                        birthPlace = c.String(),
                        personalId = c.String(),
                        medicalCode = c.String(),
                        phoneNumber = c.String(),
                        classId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.userId)
                .ForeignKey("dbo.Classrooms", t => t.classId)
                .Index(t => t.classId);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        classId = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        facultyId = c.String(maxLength: 128),
                        educationalProgramId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.classId)
                .ForeignKey("dbo.EducationalPrograms", t => t.educationalProgramId)
                .ForeignKey("dbo.Faculties", t => t.facultyId)
                .Index(t => t.facultyId)
                .Index(t => t.educationalProgramId);
            
            CreateTable(
                "dbo.EducationalPrograms",
                c => new
                    {
                        EducationalProgramId = c.String(nullable: false, maxLength: 128),
                        EducationalProgramName = c.String(),
                    })
                .PrimaryKey(t => t.EducationalProgramId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        facultyId = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.facultyId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        courseId = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        teacherName = c.String(),
                    })
                .PrimaryKey(t => t.courseId);
            
            CreateTable(
                "dbo.CourseClassrooms",
                c => new
                    {
                        CourseClassId = c.String(nullable: false, maxLength: 128),
                        TeacherName = c.String(),
                        courseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseClassId)
                .ForeignKey("dbo.Courses", t => t.courseId)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.CourseEducationalPrograms",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        EducationalProgramId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.EducationalProgramId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.EducationalPrograms", t => t.EducationalProgramId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.EducationalProgramId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.String(maxLength: 128),
                        courseClassroomId = c.String(),
                        excerciseScore = c.Double(nullable: false),
                        midTermScore = c.Double(nullable: false),
                        finalTermScore = c.Double(nullable: false),
                        excerciseRate = c.Double(nullable: false),
                        midTermRate = c.Double(nullable: false),
                        finalTermRate = c.Double(nullable: false),
                        courseClassroom_CourseClassId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseClassrooms", t => t.courseClassroom_CourseClassId)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.courseClassroom_CourseClassId);
            
            CreateTable(
                "dbo.UserCourseClassrooms",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseClassroomId = c.String(nullable: false, maxLength: 128),
                        Semester = c.Int(nullable: false),
                        courseClassroom_CourseClassId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseClassroomId })
                .ForeignKey("dbo.CourseClassrooms", t => t.courseClassroom_CourseClassId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.courseClassroom_CourseClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourseClassrooms", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCourseClassrooms", "courseClassroom_CourseClassId", "dbo.CourseClassrooms");
            DropForeignKey("dbo.Scores", "userId", "dbo.Users");
            DropForeignKey("dbo.Scores", "courseClassroom_CourseClassId", "dbo.CourseClassrooms");
            DropForeignKey("dbo.CourseEducationalPrograms", "EducationalProgramId", "dbo.EducationalPrograms");
            DropForeignKey("dbo.CourseEducationalPrograms", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseClassrooms", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Accounts", "userId", "dbo.Users");
            DropForeignKey("dbo.Users", "classId", "dbo.Classrooms");
            DropForeignKey("dbo.Classrooms", "facultyId", "dbo.Faculties");
            DropForeignKey("dbo.Classrooms", "educationalProgramId", "dbo.EducationalPrograms");
            DropIndex("dbo.UserCourseClassrooms", new[] { "courseClassroom_CourseClassId" });
            DropIndex("dbo.UserCourseClassrooms", new[] { "UserId" });
            DropIndex("dbo.Scores", new[] { "courseClassroom_CourseClassId" });
            DropIndex("dbo.Scores", new[] { "userId" });
            DropIndex("dbo.CourseEducationalPrograms", new[] { "EducationalProgramId" });
            DropIndex("dbo.CourseEducationalPrograms", new[] { "CourseId" });
            DropIndex("dbo.CourseClassrooms", new[] { "courseId" });
            DropIndex("dbo.Classrooms", new[] { "educationalProgramId" });
            DropIndex("dbo.Classrooms", new[] { "facultyId" });
            DropIndex("dbo.Users", new[] { "classId" });
            DropIndex("dbo.Accounts", new[] { "userId" });
            DropTable("dbo.UserCourseClassrooms");
            DropTable("dbo.Scores");
            DropTable("dbo.CourseEducationalPrograms");
            DropTable("dbo.CourseClassrooms");
            DropTable("dbo.Courses");
            DropTable("dbo.Faculties");
            DropTable("dbo.EducationalPrograms");
            DropTable("dbo.Classrooms");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
