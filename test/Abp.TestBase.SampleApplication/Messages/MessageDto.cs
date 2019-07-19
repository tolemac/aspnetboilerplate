using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Abp.TestBase.SampleApplication.Messages
{
    [AutoMap(typeof(Message))]
    public class MessageDto : FullAuditedEntityDto
    {
        public Guid? TenantId { get; set; }

        public string Text { get; set; }
    }
}