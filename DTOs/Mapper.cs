using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContosoUniversity.DTOs;
using ContosoUniversity.Models;

namespace ContosoUniversity.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<StudentDTO,Student>();
            CreateMap<Student, StudentDTO>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
        }
    }
}
