using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CWRetail.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        
        public double Price { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }

        public class Validator : AbstractValidator<ProductDto>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();
                
                RuleFor(x => x.Price)
                    .NotEmpty();
            }
        }
    }
}
