using System;
using Abp.Domain.Entities;

namespace Abp.Zero.SampleApp.BookStore
{
    public class Book : Entity
    {
        public string Name { get; set; }
    }
}
