﻿using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly MyApplicationDbContext _db;
		internal DbSet<T> dbSet;

		public Repository(MyApplicationDbContext db)
		{
			_db = db;
			//Foodtype, Category
			///_db.MenuItem.Include(u => u.FoodType).Include(u => u.Category);
			this.dbSet = db.Set<T>();
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public IEnumerable<T> GetAll(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			string? includeProperties = null
			)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split(
					new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}
			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}

			return query.ToList();
		}

		public T GetFirstOrDefault(
			Expression<Func<T, bool>>? filter = null, 
			string? includeProperties = null
			)
		{
			IQueryable<T> query = dbSet;

			if(filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split(
					new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			return query.FirstOrDefault();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
