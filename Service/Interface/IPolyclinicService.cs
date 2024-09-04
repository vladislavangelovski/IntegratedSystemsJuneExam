using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPolyclinicService
    {
        List<Polyclinic> GetAllPolyclinics();
        Polyclinic GetPolyclinic(Guid id);
        void CreatePolyclinic(Polyclinic polyclinic);
        void DeletePolyclinic(Guid id);
        void UpdatePolyclinic(Polyclinic polyclinic);
        void LowerCapacity(Polyclinic polyclinic);
    }
}
