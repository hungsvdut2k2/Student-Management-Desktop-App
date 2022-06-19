using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DTO;

namespace netcuoiky.DAL
{
    public class SM_DAL : DbContext
    {

        public SM_DAL() : base("name = con")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourseClassroom>()
                .HasKey(userCourseClass => new 
                    { userCourseClass.UserId, userCourseClass.CourseClassroomId });
            modelBuilder.Entity<CourseEducationalProgram>()
                .HasKey(courseEducationalProgram => new
                    { courseEducationalProgram.CourseId, courseEducationalProgram.EducationalProgramId });
            modelBuilder.Entity<UserCourse>()
                .HasKey(UserCourse => new
                    { UserCourse.CourseId, UserCourse.UserId });
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseClassroom> CourseClassroom { get; set; }
        public DbSet<EducationalProgram> EducationalProgram { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<UserCourseClassroom> UserCourseClassroom { get; set; }
        public DbSet<CourseEducationalProgram> CourseEducationalProgram { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
    }
}
