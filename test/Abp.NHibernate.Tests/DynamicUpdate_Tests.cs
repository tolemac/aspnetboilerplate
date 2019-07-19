using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.NHibernate.Tests.Entities;
using Shouldly;
using Xunit;

namespace Abp.NHibernate.Tests
{
    public class DynamicUpdate_Tests : NHibernateTestBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DynamicUpdate_Tests()
        {
            _bookRepository = Resolve<IRepository<Book>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            UsingSession(session => session.Save(new Book { Id = GuidExtensions.Guid1, Name = "Hitchhikers Guide to the Galaxy" }));
        }

        [Fact]
        public void Should_Set_CreatorUserId_When_DynamicInsert_Is_Enabled()
        {
            AbpSession.UserId = GuidExtensions.Guid1;

            using (var uow = _unitOfWorkManager.Begin())
            {
                var book = _bookRepository.Get(GuidExtensions.Guid1);
                book.ShouldNotBeNull();
                book.Name = "Hitchhiker's Guide to the Galaxy";
                _bookRepository.Update(book);
                uow.Complete();
            }

            var book2 = _bookRepository.Get(GuidExtensions.Guid1);
            book2.LastModifierUserId.ShouldNotBeNull();
        }
    }
}
