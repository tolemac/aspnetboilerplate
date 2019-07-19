using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Abp.TestBase.SampleApplication.People.Dto
{
    [AutoMapTo(typeof(Person))]
    public class CreatePersonInput
    {        
        public Guid ContactListId { get; set; }

        [Required]
        [StringLength(Person.MaxNameLength)]
        public string Name { get; set; }
    }
}