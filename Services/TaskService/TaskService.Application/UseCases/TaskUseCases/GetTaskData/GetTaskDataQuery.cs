﻿using MediatR;
using TaskService.Application.DTOs;

namespace TaskService.Application.UseCases
{
    public record GetTaskDataQuery(
        string? username,
        int? companyId,
        int? taskId
    ) : IRequest<TaskDataDTO>;
}