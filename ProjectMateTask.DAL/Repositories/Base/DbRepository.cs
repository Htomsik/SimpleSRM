﻿using Microsoft.EntityFrameworkCore;
using ProjectMateTask.DAL.Context;
using ProjectMateTask.DAL.Entities.Base;

namespace ProjectMateTask.DAL.Repositories;

internal class DbRepository<T> : IRepository<T> where T : Entity, new()
{
    private readonly ProjectMateTaskDb _db;

    private readonly DbSet<T> _set;

    public DbRepository(ProjectMateTaskDb db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    public virtual IQueryable<T> Items => _set;

    public T Get(int id)
    {
        return Items.SingleOrDefault(item => item.Id == id)!;
    }

    public async Task<T> GetAsync(int id, CancellationToken cancelToken = default)
    {
        return await Items.SingleOrDefaultAsync(item => item.Id == id, cancelToken).ConfigureAwait(true);
    }


    public void Add(T item)
    {
        if(!NullChecker(item)) return;
        _db.Add(item);
        _db.SaveChanges();
    }

    public async Task AddAsync(T item, CancellationToken cancelToken = default)
    {
        if(!NullChecker(item)) return;
        _db.Add(item);
        await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }

    public void AddCollection(IEnumerable<T> items)
    {
        if (!CollectionNullChecker(items)) return;
        _db.AddRange(items);
        _db.SaveChanges();
    }

    public async Task AddCollectionAsync(IEnumerable<T> items, CancellationToken cancelToken = default)
    {
        if (!CollectionNullChecker(items)) return;
        await _db.AddRangeAsync(items);
        await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }

    public void Update(T item)
    {
        if(!NullChecker(item)) return;
        _db.Update(item);
        _db.SaveChanges();
    }
    
    public async Task UpdateAsync(T item, CancellationToken cancelToken = default)
    {
        if(!NullChecker(item)) return;
        _db.Update(item);
        await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }

    
    public void UpdateCollection(IEnumerable<T> items)
    {
        if (!CollectionNullChecker(items)) return;
        _db.UpdateRange(items);
        _db.SaveChanges();
    }

    public async Task UpdateCollectionAsync(IEnumerable<T> items, CancellationToken cancelToken = default)
    {
        if (!CollectionNullChecker(items)) return;
        _db.UpdateRange(items);
        await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }


    public void Remove(T item)
    {
        if(!NullChecker(item)) return;
        _db.Remove(item);
        _db.SaveChanges();
    }

    public async Task RemoveAsync(T item, CancellationToken cancelToken = default)
    {
        if(!NullChecker(item)) return;
        _db.Remove(item);
         await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }
    
    public void RemoveCollection(IEnumerable<T> items)
    {
        if (!CollectionNullChecker(items)) return;
        _db.RemoveRange(items);
        _db.SaveChanges();

    }

    public async Task RemoveCollectionAsync(IEnumerable<T> items, CancellationToken cancelToken = default)
    {
        if (!CollectionNullChecker(items)) return;
        _db.RemoveRange(items);
        await _db.SaveChangesAsync(cancelToken).ConfigureAwait(false);
    }

    protected bool NullChecker(T item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item) + " не должным будть пустым");
        return true;
    }

    protected bool CollectionNullChecker(IEnumerable<T> items)
    {
        if (items is null || items.All(item=>item is null)) throw new ArgumentNullException(nameof(items) + " не должным будть пустым");
        return true;
    }
}