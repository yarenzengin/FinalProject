using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Core.DataAccess.EntityFramework
{

    //evrensel kod
    //bir kere yazıp her yerde kullanabiliriz
    //Alt yapımız bütün vt tabloları için hazır
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
      where TEntity : class, IEntity, new()
      where TContext : DbContext, new()
        //dbcontext inheritance eden sınıflarda kullanılsın
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using içine yazdığımız nesneler kullanıldıktan sonra garbage a gider ve bellekten atılır 
            using (TContext context = new TContext())
            {
                //var ile değişkeni tanımlamaya gerek yok
                //referansı yakalama
                var addedEntity = context.Entry(entity);//git veri kaynağından benim gönderdiğim producta bir nesneyi eşleştir
                addedEntity.State = EntityState.Added;
                context.SaveChanges();//refi yakaladı ,ekledi 
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //var ile değişkeni tanımlamaya gerek yok
                //referansı yakalama
                var deletedEntity = context.Entry(entity);//git veri kaynağından benim gönderdiğim producta bir nesneyi eşleştir
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();//refi yakaladı ,ekledi 
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)//TEK DATA GETİRECEK
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);//tabloyu bizim yerimize liste gibi ele alıyor
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())

            {
                //TERNARY
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
                //select* products

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //var ile değişkeni tanımlamaya gerek yok
                //referansı yakalama
                var updatedEntity = context.Entry(entity);//git veri kaynağından benim gönderdiğim producta bir nesneyi eşleştir
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();//refi yakaladı ,ekledi 
            }
        }
    }
}
