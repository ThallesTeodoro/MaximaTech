using System;
using System.ComponentModel.DataAnnotations;
using Bogus;

namespace MaximaTech.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation properties
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Faker
        /// </summary>
        public static Faker<Product> Faker()
        {
            return new Faker<Product>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Code, f => f.Lorem.Letter(10))
                .RuleFor(u => u.Description, f => f.Lorem.Paragraph())
                .RuleFor(u => u.Price, f => Math.Round(f.Random.Double(1, 100), 2))
                .RuleFor(u => u.Status, f => true)
                .RuleFor(u => u.CreatedAt, f => DateTime.UtcNow)
                .RuleFor(u => u.UpdatedAt, f => DateTime.UtcNow);
        }
    }
}