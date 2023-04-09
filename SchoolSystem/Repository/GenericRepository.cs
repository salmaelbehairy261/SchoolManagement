﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;
using System.Linq.Expressions;

namespace App.Repos
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly SchoolDB _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolDB context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
          
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            
        }

        public void Delete(int id)
        {
            T entityToDelete =GetById(id);
            if (entityToDelete != null)
            {
                _context.Remove(entityToDelete);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }

}
