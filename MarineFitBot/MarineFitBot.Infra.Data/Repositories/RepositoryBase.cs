using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Infra.Data.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        private readonly IDbContextFactory<MarineFitBotContext> _dbContextFactory;

        public RepositoryBase(IDbContextFactory<MarineFitBotContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Возвращает все элементы типа T БЕЗ ОТСЛЕЖИВАНИЯ ИЗМЕНЕНИЙ.
        /// </summary>
        protected async Task<List<T>> FindAllAsync(CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Находит элемент типа T по заданному идентификатору
        /// </summary>
        protected async Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Set<T>().FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Находит элементы T по заданнаму условию
        /// </summary>
        protected async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Set<T>().Where(expression)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Находит элемент T по заданнаму условию
        /// </summary>
        protected async Task<T?> FindSingleByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression, cancellationToken);
               
        }

        /// <summary>
        /// Добавляет новый элемент типа T в базу данных
        /// </summary>
        protected async Task InsertEntityAsync(T entity, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            await context.Set<T>().AddAsync(entity, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Обновляет существующий элемент типа T в базе данных
        /// </summary>
        protected async Task UpdateEntityAsync(T entity, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Удаляет элемент типа T из базы данных
        /// </summary>
        protected async Task DeleteEntityAsync(T entity, CancellationToken cancellationToken)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Set<T>().Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
