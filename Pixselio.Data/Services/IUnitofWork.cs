using System;

namespace Pixselio.Data 
{
    public interface IUnitofWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        /// <summary>
        /// Değişiklikleri kaydet
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
