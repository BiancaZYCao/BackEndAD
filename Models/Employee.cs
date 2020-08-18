using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class Employee
    {
        [Required]
        public int Id {get; set;}
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public string email { get; set; }
        public EmpRole role { get; set; }
        public string phoneNum { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        //public List<Requisition> Requisitions { get; set; }


    }
    public enum EmpRole
    {
        HEAD, DELEGATE, REPRESENTATIVE, STAFF, MANAGER, SUPERVISOR, CLERK
    }
}
