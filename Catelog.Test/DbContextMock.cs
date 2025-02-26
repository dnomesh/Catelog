using Catelog.Domain.Models;
using Catelog.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Catelog.Test
{
    public class DbContextMock
    {
        public static Mock<TContext> GetMock<TData, TContext>(List<TData> lstData, Expression<Func<TContext, DbSet<TData>>> dbSetSelectionExpression) where TData : class where TContext : DbContext
        {
            IQueryable<TData> lstDataQueryable = lstData.AsQueryable();
            Mock<DbSet<TData>> dbSetMock = new Mock<DbSet<TData>>();
            Mock<TContext> dbContext = new Mock<TContext>();

            dbSetMock.As<IQueryable<TData>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
            dbSetMock.Setup(x => x.Add(It.IsAny<TData>())).Callback<TData>(lstData.Add);
            dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(lstData.AddRange);
            dbSetMock.Setup(x => x.Remove(It.IsAny<TData>())).Callback<TData>(t => lstData.Remove(t));
            dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(ts =>
            {
                foreach (var t in ts) { lstData.Remove(t); }
            });


            dbContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);

            return dbContext;
        }
    }

    public class IEmployeeRepositoryMock
    {
        public static Mock<CatelogDbContext> GetCategoriesMock()
        {
            Fixture fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            List<Category> lstUser = new List<Category>();
            var category = fixture.Create<Category>();
            category.Code = "ELEC";
            lstUser.Add(category);

            category = fixture.Create<Category>();
            category.Code = "CLOTH";
            lstUser.Add(category);

            var dbContextMock = DbContextMock.GetMock<Category, CatelogDbContext>(lstUser, x => x.Categories);
            return dbContextMock;
        }

    }
}
