using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;

namespace Core.DataAccess
{
                         // veri tipini sınırlandırdık
                         // generic constraint
                         //class : referans tip
                         //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
                         //new() : new'lenebilir olmalı
                         //sistemimiz artık vt ile çalışabilen bir repository oldu
  public  interface IEntityRepository<T> where T:class, IEntity,new()
    {               
                    //filtrelemeyi sağladı  -- filtre vermeyebilirsin
        List<T> GetAll(Expression<Func<T,bool>> filter=null);//ürünleri listeledim 
        T Get(Expression<Func<T, bool>> filter );//refactoring
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

       

    }
}
