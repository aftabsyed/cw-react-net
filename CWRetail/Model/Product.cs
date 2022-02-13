using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace CWRetail.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(5, 2)")]
        public double Price { get; set; }

        public string Type { get; set; }
        public bool Active { get; set; }

        //public class Validator : AbstractValidator<Product>
        //{
        //    public Validator()
        //    {
        //        RuleFor(x => x.Name)
        //            .NotEmpty();

        //        RuleFor(x => x.Description)
        //            .NotEmpty();

        //        RuleFor(x => x.Price)
        //            .NotEmpty();
        //    }
        //}
    }
}