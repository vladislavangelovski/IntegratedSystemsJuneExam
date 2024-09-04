using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class PolyclinicServiceImpl : IPolyclinicService
    {
        private readonly IRepository<Polyclinic> _polyclinicRepository;

        public PolyclinicServiceImpl(IRepository<Polyclinic> polyclinicRepository)
        {
            _polyclinicRepository = polyclinicRepository;
        }

        public void CreatePolyclinic(Polyclinic polyclinic)
        {
            _polyclinicRepository.Insert(polyclinic);
        }

        public void DeletePolyclinic(Guid id)
        {
            var polyclinic = _polyclinicRepository.Get(id);
            _polyclinicRepository.Delete(polyclinic);
        }

        public List<Polyclinic> GetAllPolyclinics()
        {
            return _polyclinicRepository.GetAll().ToList();
        }

        public Polyclinic GetPolyclinic(Guid id)
        {
            return _polyclinicRepository.Get(id);
        }

        public void LowerCapacity(Polyclinic polyclinic)
        {
            --polyclinic.AvailableSlots;
        }

        public void UpdatePolyclinic(Polyclinic polyclinic)
        {
            _polyclinicRepository.Update(polyclinic);
        }
    }
}
