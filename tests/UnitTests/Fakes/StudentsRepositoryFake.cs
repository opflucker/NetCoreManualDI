using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreManualDI.UnitTests.Fakes
{
    internal class StudentsRepositoryFake : IStudentsRepository
    {
        private readonly Dictionary<Guid, Student> students = new Dictionary<Guid, Student>();

        public Task<Student?> GetByIdAsync(Guid id)
        {
            if (students.TryGetValue(id, out Student? student))
                return Task.FromResult<Student?>(student);
            return Task.FromResult<Student?>(null);
        }

        public Task<Student?> GetByNameAsync(StudentName name)
        {
            return Task.FromResult(students.Values.FirstOrDefault(c => c.Name == name));
        }

        public void Register(Student student)
        {
            students.Add(student.Id, student);
        }
    }
}
