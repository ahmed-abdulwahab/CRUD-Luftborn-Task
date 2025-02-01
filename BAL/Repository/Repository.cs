using AutoMapper;
using AutoMapper.QueryableExtensions;
using BAL.DTOs;
using BAL.UnitOfWork;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Repository(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbSet = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public IQueryable<AppUser> GetAllAsQueryable()
        {
            return _context.Users.AsQueryable(); 
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<MemberDto?> GetMemberAsync(string username)
        {
            return await _context.Users.
                Where(x => x.UserName == username).
                ProjectTo<MemberDto>(_mapper.ConfigurationProvider).
                SingleOrDefaultAsync();
        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _unitOfWork.CommitAsync();
        }
    }
}
