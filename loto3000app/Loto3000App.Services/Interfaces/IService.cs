using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface IService<T>
    {
        void AddEntity(T entity);
        void DeleteEntity(int id);

        List<T> GetAllEntities();

        T GetEntityById(int id);

        void UpdateEntity(T entity);
    }
}
