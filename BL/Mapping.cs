using AutoMapper;
using BL.DTO;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Mapping
    {
        public static void Create()
        {
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<UserDTO, User>();

            Mapper.CreateMap<Group, GroupDTO>();
            Mapper.CreateMap<GroupDTO, Group>();

            Mapper.CreateMap<UserTrial, UserTrialDTO>();
            Mapper.CreateMap<UserTrialDTO, UserTrial>();
            
            Mapper.CreateMap<TestTemplate, TestTemplateDTO>();
            Mapper.CreateMap<TestTemplateDTO, TestTemplate>();

            Mapper.CreateMap<Question, QuestionDTO>();
            Mapper.CreateMap<QuestionDTO, Question>();

            Mapper.CreateMap<QuestionCategory, QuestionCategoryDTO>();
            Mapper.CreateMap<QuestionCategoryDTO, QuestionCategory>();

            Mapper.CreateMap<Answer, AnswerDTO>();
            Mapper.CreateMap<AnswerDTO, Answer>();
        }
    }
}
