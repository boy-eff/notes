namespace Notes.Application.Common.Interfaces;

public interface IRepositorySpecification<T>
{
    IQueryable<T> BuildFilters(IQueryable<T> entities);
}