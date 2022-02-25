using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
using Loto3000App.Models.Prize;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class PrizeService:IService<PrizeModel>
    {
        private IPrizeRepository _prizeRepository;
        public PrizeService(IPrizeRepository prizeRepository)
        {
            _prizeRepository = prizeRepository;
        }

        public void AddEntity(PrizeModel entity)
        {
            
            if (string.IsNullOrEmpty(entity.PrizeType))
            {
                throw new PrizeException("The property PrizeType is required");
            }
            if (entity.PrizeType.Length > 30)
            {
                throw new PrizeException("The property PrizeType can't contain more than 30 characters");
            }
            if (entity.Id != 0)
            {
                throw new PrizeException("Id must not be set!");
            }
            Prize prizeForDb = entity.ToPrize();
            _prizeRepository.Add(prizeForDb);
        }

        public void DeleteEntity(int id)
        {
            Prize prize = _prizeRepository.GetById(id);
            if(prize == null)
            {
                throw new NotFoundException($"Prize with id {id} was not found");
            }
            _prizeRepository.Delete(prize);
        }

        public List<PrizeModel> GetAllEntities()
        {
            List<Prize> prizeDb = _prizeRepository.GetAll().ToList();
            List<PrizeModel> prizeModels = new List<PrizeModel>();
            foreach(Prize prize in prizeDb)
            {
                prizeModels.Add(prize.ToPrizeModel());
            }
            return prizeModels;
        }

        public PrizeModel GetEntityById(int id)
        {
            Prize prize = _prizeRepository.GetById(id);
            if (prize == null)
            {
                throw new NotFoundException($"Prize with id {id} was not found!");
            }
            return prize.ToPrizeModel();
        }

        public void UpdateEntity(PrizeModel entity)
        {
            Prize prizeDb = _prizeRepository.GetById(entity.Id);
            if (prizeDb == null)
            {
                throw new NotFoundException($"Prize with id {entity.Id} was not found!");
            }

            if (string.IsNullOrEmpty(entity.PrizeType))
            {
                throw new PrizeException("The property PrizeType is required");
            }
            if (entity.PrizeType.Length > 30)
            {
                throw new PrizeException("The property PrizeType can't contain more than 30 characters");
            }


            prizeDb.PrizeType = entity.PrizeType;
            _prizeRepository.Update(prizeDb);
        }
    }
}
