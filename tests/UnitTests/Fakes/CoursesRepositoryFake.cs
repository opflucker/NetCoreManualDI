using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreManualDI.UnitTests.Fakes
{
    internal class CoursesRepositoryFake : ICoursesRepository
    {
        private readonly Dictionary<Guid, Course> courses = new Dictionary<Guid, Course>();

        public ValueTask<Course?> GetByIdAsync(Guid id)
        {
            if (courses.TryGetValue(id, out Course? course))
                return ValueTask.FromResult<Course?>(course);
            return ValueTask.FromResult<Course?>(null);
        }

        public Task<Course?> GetByNameAsync(CourseName name)
        {
            return Task.FromResult(courses.Values.FirstOrDefault(c => c.Name == name));
        }

        public void Register(Course course)
        {
            courses.Add(course.Id, course);
        }
    }
}
