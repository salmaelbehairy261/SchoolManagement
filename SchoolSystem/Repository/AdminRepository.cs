﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Repository
{
    public class AdminRepository : IAdminRepository
    {
        SchoolDB context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminRepository(SchoolDB _context, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)//inject context from Program.cs
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }
       
    public async Task<bool> AddTeacher(TeacherViewModel teacherVM)
        {
            ApplicationUser teacher = new ApplicationUser();
            teacher.Name = teacherVM.Name;
            teacher.Email = teacherVM.Email;
            teacher.UserName = teacherVM.UserName;
            teacher.Address = teacherVM.Address;
            teacher.PhoneNumber = teacherVM.Phone;
            teacher.photoUrl = teacherVM.Photo.FileName;
            teacher.BirthDate = teacherVM.BirthDate;
            teacher.Gender = teacherVM.Gender;
            teacher.PasswordHash = teacherVM.Password;


            IdentityResult result = await userManager.CreateAsync(teacher, teacher.PasswordHash);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(teacher, "Teacher");
               // await signInManager.SignInAsync(teacher, isPersistent: false);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> AddStudent(StudentViewModel studentVM)
        {
            ApplicationUser student = new ApplicationUser()
            {
                Name = studentVM.Name,
                Email = studentVM.Email,
                UserName = studentVM.UserName,
                Address = studentVM.Address,
                PhoneNumber = studentVM.Phone,
                photoUrl = studentVM.Photo.FileName,
                BirthDate = studentVM.BirthDate,
                Gender = studentVM.Gender,
                PasswordHash=studentVM.Password,
                levelID_fk = studentVM.levelID_fk,
                classID_fk = studentVM.classID_fk
            };

            IdentityResult result = await userManager.CreateAsync(student, student.PasswordHash);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(student, "Student");
               // await signInManager.SignInAsync(student, isPersistent: false);
                return true;
            }
            else
                return false;
        }
        public bool UpdateStudent(StudentViewModel studentVM)
        {
            try
            {
                ApplicationUser student = new ApplicationUser();
                student.Name = studentVM.Name;
                student.Email = studentVM.Email;
                student.UserName = studentVM.UserName;
                student.Address = studentVM.Address;
                student.PhoneNumber = studentVM.Phone;
               // student.photoUrl = studentVM.Photo.FileName;
                student.BirthDate = studentVM.BirthDate;
                student.Gender = studentVM.Gender;
                student.PasswordHash = studentVM.Password;
                student.levelID_fk = studentVM.levelID_fk;
                student.classID_fk = studentVM.classID_fk;

                userManager.UpdateAsync(student);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<FeedbackVM> GetFeedbacks()
        {
            var list= context.Feedbacks.Select(x => new {x.Id,x.FeedbackText,x.Response,StudentName=x.ApplicationUser.Name}).ToList();
            var feedbacks=new List<FeedbackVM>();
            foreach (var item in list)
            {
                FeedbackVM obj=new FeedbackVM ();
                obj.Id = item.Id;
                obj.FeedbackText = item.FeedbackText;
                obj.Response = item.Response;
                obj.StudentName=item.StudentName;

                feedbacks.Add(obj);
            }
            return feedbacks;
        }

        public bool AddFeedback(FeedbackVM feedbackVM)
        {
            try
            {
                var feedback = context.Feedbacks.Where(x=>x.Id == feedbackVM.Id).FirstOrDefault();
                feedback.Response = feedbackVM.Response;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<StudentViewModel> GetStudentByID(StudentViewModel studentVM)
        {
            var students = await userManager.GetUsersInRoleAsync("Student");
            var student = students.Where(x => x.Id == studentVM.Id).FirstOrDefault();

            if (student != null) 
            {
                // student.photoUrl = studentVM.Photo.FileName;
                studentVM.Id = student.Id;
                studentVM.Name = student.Name;
                studentVM.Email = student.Email;
                studentVM.UserName = student.UserName;
                studentVM.Address = student.Address;
                studentVM.Phone = student.PhoneNumber;
                studentVM.BirthDate = student.BirthDate;
                studentVM.Gender = student.Gender;
                studentVM.Password = student.PasswordHash;
                studentVM.levelID_fk = student.levelID_fk;
                studentVM.classID_fk = student.classID_fk;
            }

            return studentVM;
        }
    }
}
