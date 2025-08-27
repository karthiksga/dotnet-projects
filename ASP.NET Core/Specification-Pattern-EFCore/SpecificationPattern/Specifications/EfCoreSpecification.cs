﻿using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Specifications.Base;

namespace SpecificationPattern.Specifications;

/// <summary>
/// The Ef Core specification implementation
/// </summary>
/// <typeparam name="TEntity">Type of the entity</typeparam>
public class EfCoreSpecification<TEntity> : Specification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the class
    /// </summary>
    public EfCoreSpecification(ISpecification<TEntity> specification) : base(specification)
    {
    }

    /// <summary>
    /// Returns a specification expression.<br/>
    /// IMPORTANT: EfCore Specification supports IncludeQueries
    /// </summary>
    /// <returns>Expression</returns>
    public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
    {
        if (FilterQuery is not null)
        {
            queryable = queryable.Where(FilterQuery);
        }

        if (IncludeQueries?.Count > 0)
        {
            queryable = IncludeQueries.Aggregate(queryable, (current, includeQuery) => current.Include(includeQuery));
        }

        if (OrderByQueries?.Count > 0)
        {
            var orderedQueryable = queryable.OrderBy(OrderByQueries.First());
            orderedQueryable = OrderByQueries.Skip(1).Aggregate(orderedQueryable, (current, orderQuery) => current.ThenBy(orderQuery));
            queryable = orderedQueryable;
        }
        
        if (OrderByDescendingQueries?.Count > 0)
        {
            var orderedQueryable = queryable.OrderByDescending(OrderByDescendingQueries.First());
            orderedQueryable = OrderByDescendingQueries.Skip(1).Aggregate(orderedQueryable, (current, orderQuery) => current.ThenByDescending(orderQuery));
            queryable = orderedQueryable;
        }

        return queryable;
    }
}