﻿namespace StatisticsService.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, Stream attachmentStream, string attachmentFileName);
    }
}