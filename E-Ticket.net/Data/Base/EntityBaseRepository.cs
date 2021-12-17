using E_Ticket.net.Data.DbContextConfg;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _db;

        public EntityBaseRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task AddAsync(T entity)
        {
           await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            
        }

        
        public async Task DeleteAsync(int Id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == Id);
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _db.Set<T>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] IncloudProprites)
        {
            IQueryable<T> qure = _db.Set<T>();
            qure = IncloudProprites.Aggregate(qure, (current, IncloudProprites) => current.Include(IncloudProprites));
            return await qure.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            var result = await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == Id);
            return result;
        }

        public async Task UpdateAsync(int Id, T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
