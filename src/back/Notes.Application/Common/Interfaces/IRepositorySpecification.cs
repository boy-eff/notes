using System.Linq.Expressions;
using Notes.Application.Common.CQRS;

namespace Notes.Application.Common.Interfaces;

public interface IRepositorySpecification<T> where T : class
{
    IQueryable<T> BuildFilters(IQueryable<T> entities);
}