using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OrderService.Queries
{
	public class InMemoryRepository<T> where T : IEntity
	{
		private readonly List<T> _database;

		public InMemoryRepository()
		{
			_database = new List<T>();
		}

		public T FindOne(int id)
		{
			return _database.Where(item => item.Id == id).FirstOrDefault();
		}

		public IEnumerable<T> FindAll()
		{
			return _database;
		}

		public void Delete(T target)
		{
			_database.Remove(target);
		}

		public void Save(T target)
		{
			_database.Add(target);
		}

		public void SaveOrUpdate(T target)
		{
			if (_database.Contains(target))
				_database.Remove(target);
			_database.Add(target);
		}

		public IEnumerable<T> FindAllBy(Expression<Func<T, bool>> where)
		{
			return _database.Where(where.Compile());
		}
	}

	public interface IEntity
	{
		int Id { get; set; }
	}
}