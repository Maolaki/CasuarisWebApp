var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AuthService_API>("authservice-api");

builder.AddProject<Projects.StatisticsService_API>("statisticsservice-api");

builder.AddProject<Projects.TaskService_API>("taskservice-api");

builder.AddProject<Projects.UnionService_API>("unionservice-api");

builder.Build().Run();
