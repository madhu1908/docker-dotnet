using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webinar.Models
{
    public class RapidDataUsers
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="This Feild is Required")]
        [Display(Name ="EmailId")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Feild is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=[A-Za-z0-9 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !\"#$%&'()*+,-.\\/:;<=>?@[\\]^_`{|}~])(?=.{8,}).*$", ErrorMessage ="Password is not Valid")]
        public string Password { get; set; }
        public DateTime CreadtedTimestamp { get; set; }
        public DateTime updatedTimestamp { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "This Feild is Required")]
        [Display(Name = "RoleName")]
        [DataType(DataType.Text)]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "This Feild is Required")]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }
        public string Sessionrolename { get; set; }
    }

    public class Roles
    {
        public int RoleId { get; set; }

        [Display(Name ="Role Name")]
        [Required(ErrorMessage ="This feild is Required")]
        public string RoleName { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime modifiedTimestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int UserId { get; set; }
        public string Sessionrolename { get; set; }
    }


    public class Departments
    {
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "This feild is Required")]
        public string DepartmentName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string UpdatedBy { get; set; }
        public string rolename { get; set; }    
    }



    public class LoginUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "This Feild is Required")]
        [Display(Name = "Employee EmailId")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

     
        [Required(ErrorMessage = "This Feild is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=[A-Za-z0-9 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !\"#$%&'()*+,-.\\/:;<=>?@[\\]^_`{|}~])(?=.{8,}).*$", ErrorMessage = "Password is not Valid")]
        public string Password { get; set; }

        public string RoleName { get; set; }

    }


    public class Passedoutyear
    {
        public int id { get; set; }
        [Display(Name = "Passed Out Year")]
        [Required(ErrorMessage = "This feild is Required")]
        public string passedoutyear { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string UpdatedBy { get; set; }
        public string RoleName { get; set; }
    }


}