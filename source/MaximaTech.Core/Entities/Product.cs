using System;
using System.ComponentModel.DataAnnotations;

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
    }
}