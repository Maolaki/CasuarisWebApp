using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Profiles
{
    public class TaskInfoToTaskInfoDTOProfile : Profile
    {
        public TaskInfoToTaskInfoDTOProfile()
        {
            CreateMap<TaskInfoDTO, BaseTaskInfo>().ReverseMap();
        }
    }
}