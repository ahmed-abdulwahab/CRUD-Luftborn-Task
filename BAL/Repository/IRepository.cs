using BAL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        IQueryable<AppUser> GetAllAsQueryable();
        Task<TEntity> GetByIdAsync(int id);
        Task<MemberDto?> GetMemberAsync(string username);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);
    }
}
