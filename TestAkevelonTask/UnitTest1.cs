using AkvelonTask.Data;
using AkvelonTask.Models;
using AkvelonTask.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAkevelonTask
{
    public class ExceptionTests
    {
        private static DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        AppDbContext context;
        ProjectService projectService;
        TaskInfoService taskService;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(options);
            context.Database.EnsureCreated();
            projectService = new ProjectService(context);
            taskService = new TaskInfoService(context);
        }
        [OneTimeTearDown]
        public void Clean()
        {
            context.Database.EnsureDeleted();
        }
        [Test]
        public void InvalidProjectDates()
        {
            Assert.Throws<ArgumentException>(
                () =>
                new Project()
                {
                    Id = 1,
                    IsDeleted = false,
                    Name = "Twitter",
                    StartDate = DateTime.Parse("2022-01-01"),
                    EndDate = DateTime.Parse("2021-12-31"),
                    Priority = 1,
                    Status = 0,
                    Tasks = new List<TaskInfo>()
                }
            );

        }
        [Test]
        public void GetNotExistingProject()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var p = await projectService.Get(1);
            });
        }
        [Test]
        public void GetNotExistingTask()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var p = await taskService.Get(1);
            });
        }
    }
}