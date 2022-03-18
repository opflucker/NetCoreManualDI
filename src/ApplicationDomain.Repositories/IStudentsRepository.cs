﻿using NetCoreManualDI.BusinessDomain.Core;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.Repositories
{
    public interface IStudentsRepository
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student?> GetByNameAsync(StudentName name);
        void Register(Student student);
    }
}
