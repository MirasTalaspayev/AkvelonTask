using AkvelonTask.Data;
using AkvelonTask.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestAkevelonTask
{
    public class Tests
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
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            
        }
    }
}