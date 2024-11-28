using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CustomORMExample
{
    public class Repository<T> where T : new()
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            string query = $"SELECT * FROM {typeof(T).Name}s";

            return _context.ExecuteQuery<T>(query);
        }

        public T GetById(int id)
        {
            string query = $"SELECT * FROM {typeof(T).Name}s WHERE {typeof(T).Name}Id = @Id";
            var parameters = new List<SqlParameter> { new SqlParameter("@Id", id) };
            return _context.ExecuteQuery<T>(query).FirstOrDefault();
        }

        public void Add(T entity)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != $"{typeof(T).Name}Id");
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            string query = $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({values})";
            var parameters = properties.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(entity) ?? DBNull.Value)).ToList();
            _context.ExecuteCommand(query, parameters);
        }

        public void Update(T entity)
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != $"{typeof(T).Name}Id");
            string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            string query = $"UPDATE {typeof(T).Name}s SET {setClause} WHERE {typeof(T).Name}Id = @Id";

            var parameters = properties.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(entity) ?? DBNull.Value)).ToList();
            parameters.Add(new SqlParameter("@Id", typeof(T).GetProperty($"{typeof(T).Name}Id")?.GetValue(entity)));
            _context.ExecuteCommand(query, parameters);
        }

        public void Delete(int id)
        {
            string query = $"DELETE FROM {typeof(T).Name}s WHERE {typeof(T).Name}Id = @Id";
            var parameters = new List<SqlParameter> { new SqlParameter("@Id", id) };
            _context.ExecuteCommand(query, parameters);
        }
    }
}
