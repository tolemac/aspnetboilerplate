﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.TestBase.SampleApplication.GuidEntities
{
    public class TestEntityWithGuidPkAndDbGeneratedValue : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }
    }
}