using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webinar.Models
{
    public class webinarRegistration
    {
        public int employeeid { get; set; }


        [Required(ErrorMessage = "This field  is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = " This field  is Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]

        public string workemail { get; set; }


        [Required(ErrorMessage = "This field  is Required")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string Mobilenumber { get; set; }

        [Required(ErrorMessage = "This field  is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "College Name")]

        public string collegeName { get; set; }

       

        [Required(ErrorMessage = "This field  is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Department Name")]
        public string department { get; set; }

        [Required(ErrorMessage = "This field  is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Current Location")]
        public string location { get; set; }

        [Required(ErrorMessage = "This field  is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Year of passedout")]
        public string year { get; set; }


        //[Required(ErrorMessage = "Address is Required")]
        //[DataType(DataType.Text)]
        //[Display(Name = "Address")]
        //public string Address { get; set; }

       
        [Display(Name = "Resume")]
        [Required(ErrorMessage = "This field  is Required")]
        public HttpPostedFileBase files { get; set; }

      
        [Display(Name = "Resume")]
        public string url { get; set; }

        public string status { get; set; }

        public string rolename { get; set; }

        public DateTime applieddate { get; set; }

    }
}