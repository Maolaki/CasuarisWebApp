using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Profiles
{
    public class TaskInfoToTaskDataDTOProfile : Profile
    {
        public TaskInfoToTaskDataDTOProfile()
        {
            CreateMap<TaskDataDTO, BaseTaskInfo>().ReverseMap();
        }
    }
}