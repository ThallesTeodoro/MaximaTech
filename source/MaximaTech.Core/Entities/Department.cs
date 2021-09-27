using System;
using Bogus;

namespace MaximaTech.Core.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Faker
        /// </summary>
        public static Faker<Department> Faker()
        {
            return new Faker<Department>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName());
        }
    }
}