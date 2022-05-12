using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webinar.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using webinar.Helpers;

namespace webinar.Controllers
{
    public class HomeController : Controller
    {
        

       
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin","Admin")]
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }

        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin", "Admin")]
        public System.Web.Mvc.ActionResult RejectIndex()
        {
            return View();
        }

        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin", "Admin")]
        public System.Web.Mvc.ActionResult SelectedIndex()
        {
            return View();
        }


        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin", "Admin")]
        public System.Web.Mvc.ActionResult scheduledIndex()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public System.Web.Mvc.ActionResult registrationnew()
        {
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;

            List<SelectListItem> listForSelect = new List<SelectListItem>();
            //{
            //    listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-IT", Value = "B.E/B.Tech-IT" });
            //    listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-EEE", Value = "B.E/B.Tech-EEE" });
            //    listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-ECE", Value = "B.E/B.Tech-ECE" });
            //    listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-EI", Value = "B.E/B.Tech-EI" });
            //    listForSelect.Add(new SelectListItem { Text = "B.E-CSE", Value = "B.E-CSE" });
            //    listForSelect.Add(new SelectListItem { Text = "BCA", Value = "BCA" });
            //    listForSelect.Add(new SelectListItem { Text = "MCA", Value = "MCA" });
            //    listForSelect.Add(new SelectListItem { Text = "BSC-Computer Science", Value = "BSC-Computer Science" });
            //    listForSelect.Add(new SelectListItem { Text = "MSC-Computer Science", Value = "MSC-Computer Science" });
            //    listForSelect.Add(new SelectListItem { Text = "BSC-IT", Value = "BSC-IT" });
            //    listForSelect.Add(new SelectListItem { Text = "MSC-IT", Value = "MSC-IT" });

            //}
            string sqlquery = "select DepartmentName from Departments";
            SqlConnection sqlCon = new SqlConnection(constr);
            SqlCommand cmd3 = new SqlCommand(sqlquery, sqlCon);
            sqlCon.Open();
            SqlDataReader sdr = cmd3.ExecuteReader();
            
            
            while (sdr.Read())
            {
                SelectListItem item = new SelectListItem { Text = sdr["DepartmentName"].ToString(), Value = sdr["DepartmentName"].ToString() };
                listForSelect.Add(item);
            }
            sdr.Close();
            ViewBag.customValue = listForSelect;


            List<SelectListItem> listForSelectl = new List<SelectListItem>();

            //{
            //    listForSelectl.Add(new SelectListItem { Text = "2018", Value = "2018", Selected = true });
            //    listForSelectl.Add(new SelectListItem { Text = "2019", Value = "2019", });
            //    listForSelectl.Add(new SelectListItem { Text = "2020", Value = "2020", });
            //    listForSelectl.Add(new SelectListItem { Text = "2021", Value = "2021" });
            //    listForSelectl.Add(new SelectListItem { Text = "2021", Value = "2022" });

            //}

            sqlquery = "select passedoutyear from Passedoutyear";
            cmd3 = new SqlCommand(sqlquery, sqlCon);
            sdr = cmd3.ExecuteReader();
            while (sdr.Read())
            {
                SelectListItem item = new SelectListItem { Text = sdr["passedoutyear"].ToString(), Value = sdr["passedoutyear"].ToString() };
                listForSelectl.Add(item);
            }

            ViewBag.years = listForSelectl;
            sqlCon.Close();
            return View();

        }



        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult registrationnew(Models.webinarRegistration register)
        {
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            string ip = Request.Url.ToString();
            string _path = ""; /*string urls = "http://15.206.85.18:9090/UploadedFiles/";*/
            string urls = "/UploadedFiles/";
            urls = String.Concat(ip, urls); 


            using (SqlConnection sqlCon = new SqlConnection(constr))
                {
                    List<SelectListItem> listForSelect = new List<SelectListItem>();
                //{
                //listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-IT", Value = "B.E/B.Tech-IT" });
                //listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-EEE", Value = "B.E/B.Tech-EEE" });
                //listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-ECE", Value = "B.E/B.Tech-ECE" });
                //listForSelect.Add(new SelectListItem { Text = "B.E/B.Tech-EI", Value = "B.E/B.Tech-EI" });
                //listForSelect.Add(new SelectListItem { Text = "B.E-CSE", Value = "B.E-CSE" });
                //listForSelect.Add(new SelectListItem { Text = "BCA", Value = "BCA" });
                //listForSelect.Add(new SelectListItem { Text = "MCA", Value = "MCA" });
                //listForSelect.Add(new SelectListItem { Text = "BSC-Computer Science", Value = "BSC-Computer Science" });
                //listForSelect.Add(new SelectListItem { Text = "MSC-Computer Science", Value = "MSC-Computer Science" });
                //listForSelect.Add(new SelectListItem { Text = "BSC-IT", Value = "BSC-IT" });
                //listForSelect.Add(new SelectListItem { Text = "MSC-IT", Value = "MSC-IT" });


                //}

                string sqlquery = "select DepartmentName from Departments";
                SqlCommand cmd3 = new SqlCommand(sqlquery,sqlCon);
                sqlCon.Open();
                SqlDataReader sdr = cmd3.ExecuteReader();
                while(sdr.Read())
                {
                    SelectListItem item = new SelectListItem { Text = sdr["DepartmentName"].ToString(), Value = sdr["DepartmentName"].ToString() };
                    listForSelect.Add(item);
                }

                    ViewBag.customValue = listForSelect;


                    List<SelectListItem> listForSelectl = new List<SelectListItem>();

                //{
                //    listForSelectl.Add(new SelectListItem { Text = "2018", Value = "2018", Selected = true });
                //    listForSelectl.Add(new SelectListItem { Text = "2019", Value = "2019", });
                //    listForSelectl.Add(new SelectListItem { Text = "2020", Value = "2020", });
                //    listForSelectl.Add(new SelectListItem { Text = "2021", Value = "2021" });
                //    listForSelectl.Add(new SelectListItem { Text = "2021", Value = "2022" });

                //}
                sqlquery = "select passedoutyear from Passedoutyear";
                cmd3 = new SqlCommand(sqlquery, sqlCon);    
                sdr = cmd3.ExecuteReader();
                while (sdr.Read())
                {
                    SelectListItem item = new SelectListItem { Text = sdr["passedoutyear"].ToString(), Value = sdr["passedoutyear"].ToString() };
                    listForSelectl.Add(item);
                }

                ViewBag.years = listForSelectl;
                sqlCon.Close(); 
                    sqlCon.Open();
                    try
                    {
                       
                        if (ModelState.IsValid)
                        {

                            string emailcheck = "select workemail from employee_registration where workemail=@email";
                            SqlCommand cmd = new SqlCommand(emailcheck, sqlCon);
                            cmd.Parameters.AddWithValue("@email", register.workemail);
                            string email = (string)cmd.ExecuteScalar();
                            if (email == register.workemail)
                            {
                                ModelState.AddModelError("", "User EmailID Already registered!");


                                sqlCon.Close();
                                return View();

                            }
                            else
                            {
                            if (register.files.ContentLength > 0)
                            {
                                FileUpload1 fs = new FileUpload1();
                                fs.filesize = 1000;
                                string us = fs.UploadUserFile(register.files);
                                if (us != null && us != "Your Application Successfully Registered")
                                {
                                    ViewBag.ResultErrorMessage = fs.ErrorMessage;
                                    return View();
                                }
                                if (us != null && us == "Your Application Successfully Registered")
                                {
                                    ViewBag.ResultErrorMessage = "Your Application Successfully Registered";

                                }
                                string _FileName = (Path.GetFileName(register.files.FileName)).Trim().Replace(" ", "");
                                _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                                register.files.SaveAs(_path);
                                register.url = urls + _FileName;
                            }
                            string insertquery = "INSERT INTO employee_registration (name,workemail,Mobilenumber,collegeName,department,year,location,Resume,status,CreatedTimestamp) VALUES(@name,@workemail,@Mobilenumber,@collegeName,@department,@year,@location,@Resume,@status,@CreatedTimestamp)";
                                SqlCommand Cmd3 = new SqlCommand(insertquery, sqlCon);
                                Cmd3.Parameters.AddWithValue("@name", register.name);
                                Cmd3.Parameters.AddWithValue("@workemail", register.workemail);
                                Cmd3.Parameters.AddWithValue("@Mobilenumber", register.Mobilenumber);
                                Cmd3.Parameters.AddWithValue("@collegeName", register.collegeName);
                                Cmd3.Parameters.AddWithValue("@department", register.department);
                                Cmd3.Parameters.AddWithValue("@year", register.year);
                                Cmd3.Parameters.AddWithValue("@location", register.location);
                                Cmd3.Parameters.AddWithValue("@Resume", register.url);
                                Cmd3.Parameters.AddWithValue("@status", "Inprogress");
                                Cmd3.Parameters.AddWithValue("@CreatedTimestamp", DateTime.Now);
                                Cmd3.ExecuteNonQuery();

                                emailtrigger(register.workemail, register.name);
                                ModelState.Clear();
                                webinarRegistration ObjContact = new webinarRegistration()
                                {
                                    name = string.Empty,
                                    workemail = string.Empty,
                                    Mobilenumber = string.Empty,
                                    //Address = string.Empty,
                                    collegeName = string.Empty,
                                    department = string.Empty,
                                    year = string.Empty,
                                    location = string.Empty

                                };
                            sqlCon.Close();
                            ViewBag.message = "Thank  You, Successfully Registered";
                            return View();
                            //return RedirectToAction("Index");
                        }


                        }

                    }
                    catch (Exception e)
                    {
                        string insertquery1 = "INSERT INTO catchings (catchname) VALUES(@name)";
                        SqlCommand Cmd3 = new SqlCommand(insertquery1, sqlCon);
                        Cmd3.Parameters.AddWithValue("@name", e);
                        Cmd3.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                    return View();

                }

        }


        

        public System.Web.Mvc.ActionResult emailtrigger(string email,string name )
        {
            string toEmail = email;
            string mail_subject = "RapidData Invites – Placement Drive F2F";
            string mail_body = "Hello " + name + " , Greetings from RapidData Team!<br/><br/>" +
                "Congratulations You have Sucessfully Registered for Placement Drive <h1 style ='color:blue'>“Soon You will get response from us..”</h1><br/>" +

                 "Don't miss out! <br/><br/> " +
                "Regards,<br/>" +
                "Team RapidData Technologies.<br/>";
               
               
            
            SmtpClient smtp = new SmtpClient();
           
            smtp.Host = "smtp.zoho.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("test.user@rapiddatatech.com", "Jeb@1234");
            MailMessage mailMessage = new MailMessage("test.user@rapiddatatech.com", toEmail, mail_subject, mail_body);
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            smtp.Send(mailMessage);
           

            return View();
        }


        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin", "Admin")]

        public ActionResult getregisterdetails()
        {

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {
               
                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", "Inprogress");
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name =(sdr["name"]) == DBNull.Value ? string.Empty :sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty :sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty :sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty :sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty :sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty :sdr["location"].ToString();
                    
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty :sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty :(sdr["Resume"].ToString());
                    newstud.applieddate  = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : Convert.ToDateTime(sdr["CreatedTimestamp"]);
                    newstud.rolename = Session["RoleName"].ToString();
                   
                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);




            }
          catch(Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }


         

        }


        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult Reject(int id)
        {
            string status = "Rejected";
            string query = "update employee_registration set status = @status where  employeeid=@employeeid";
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.ExecuteNonQuery();
            return Json(new { success = true, message = "Rejected Successfully" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index","Home");
        }


        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult ScheduleInterview(int id)
        {
            string status = "InterviewScheduled";
            string query = "update employee_registration set status = @status where  employeeid=@employeeid";
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.ExecuteNonQuery();
            return Json(new { success = true, message = "InterviewScheduled Successfully" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index","Home");
        }



        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult rejectitems(int[] esid)
        {
            int i;
                   
                    //int i = 0;
                    for (i = 0; i < esid.Length; i++)
                    {
                         string status = "Rejected";
                         string query = "update employee_registration set status = @status where  employeeid=@employeeid";
                         string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                         SqlConnection connection = new SqlConnection(constr);
                         SqlCommand cmd = new SqlCommand(query, connection);
                         connection.Open();
                         cmd.Parameters.AddWithValue("@status", status);
                         cmd.Parameters.AddWithValue("@employeeid", esid[i]);
                         cmd.ExecuteNonQuery();
               
            }
            return Json(new { success = true, message = "Rejected Successfully" }, JsonRequestBehavior.AllowGet);




        }


        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult InterviewReject(int id)
        {
            string status = "interview Rejected canditates";
            string query = "update employee_registration set status = @status where  employeeid=@employeeid";
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.ExecuteNonQuery();
            return Json(new { success = true, message = "Rejected Successfully" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index","Home");
        }

       
        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult Select(int id)
        {
            string status = "Selected";
            string query = "update employee_registration set status = @status where  employeeid=@employeeid";
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.ExecuteNonQuery();
            return Json(new { success = true, message = "Selected Successfully" }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult selectitems(int[] esid)
        {
            int i;

            //int i = 0;
            for (i = 0; i < esid.Length; i++)
            {
                string status = "Selected";
                string query = "update employee_registration set status = @status where  employeeid=@employeeid";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@employeeid", esid[i]);
                cmd.ExecuteNonQuery();

            }
            return Json(new { success = true, message = "Rejected Successfully" }, JsonRequestBehavior.AllowGet);




        }


        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult InterviewSelect(int id)
        {
            string status = "interview selected canditates";
            string query = "update employee_registration set status = @status where  employeeid=@employeeid";
            string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.ExecuteNonQuery();
            return Json(new { success = true, message = "Selected Successfully" }, JsonRequestBehavior.AllowGet);
        }



        

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult getSelectedregisterdetails()
        {
            string status = "Selected";

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {

                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult getintervieweddetails()
        {
            string status = "InterviewScheduled";

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {

                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }

        }






        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult getRejectedregisterdetails()
        {
            string status = "Rejected";

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {

                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult interviewSelected_canditates()
        {

            return View();
        }


        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult interview_selected_list()
        {
            string status = "interview selected canditates";

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {

                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
                    newstud.status = (sdr["status"]) == DBNull.Value ? string.Empty : (sdr["status"].ToString());

                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult interviewRejected_canditates()
        {

            return View();
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult interviewRejected_List()
        {

            string status = "interview Rejected canditates";

            List<webinarRegistration> stud = new List<webinarRegistration>();

            try
            {

                string query = "select * from employee_registration where status =@status;";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                SqlConnection connection = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@status", status);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    webinarRegistration newstud = new webinarRegistration();
                    newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
                    newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
                    newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
                    newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
                    newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
                    newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
                    newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
                    newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
                    newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
                    newstud.status = (sdr["status"]) == DBNull.Value ? string.Empty : (sdr["status"].ToString());
                    stud.Add(newstud);
                }

                return Json(new { data = stud }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(stud, JsonRequestBehavior.AllowGet);
            }
        }






        //[HttpPost]
        //public ActionResult fileexport(int[] esid)
        //{

        //    int i = 0;
        //    List<webinarRegistration> stud = new List<webinarRegistration>();
        //    for (i = 0; i < esid.Length; i++)
        //    {
        //        string query = "select * from employee_registration where  employeeid=@employeeid";
        //        string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
        //        SqlConnection connection = new SqlConnection(constr);
        //        SqlCommand cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        cmd.Parameters.AddWithValue("@employeeid", esid[i]);
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            webinarRegistration newstud = new webinarRegistration();
        //            newstud.employeeid = Convert.ToInt32(sdr["employeeid"]) == 0 ? 0 : Convert.ToInt32(sdr["employeeid"]);
        //            newstud.name = (sdr["name"]) == DBNull.Value ? string.Empty : sdr["name"].ToString();
        //            newstud.workemail = (sdr["workemail"]) == DBNull.Value ? string.Empty : sdr["workemail"].ToString();
        //            newstud.Mobilenumber = (sdr["Mobilenumber"]) == DBNull.Value ? string.Empty : sdr["Mobilenumber"].ToString();
        //            newstud.collegeName = (sdr["collegeName"]) == DBNull.Value ? string.Empty : sdr["collegeName"].ToString();
        //            newstud.department = (sdr["department"]) == DBNull.Value ? string.Empty : sdr["department"].ToString();
        //            newstud.location = (sdr["location"]) == DBNull.Value ? string.Empty : sdr["location"].ToString();
        //            newstud.year = (sdr["year"]) == DBNull.Value ? string.Empty : sdr["year"].ToString();
        //            newstud.url = (sdr["Resume"]) == DBNull.Value ? string.Empty : (sdr["Resume"].ToString());
        //            stud.Add(newstud);
        //        }
        //    }

        //    var stream = new MemoryStream();
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage(stream))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //        workSheet.Cells.LoadFromCollection(stud, true);
        //        package.Save();
        //    }
        //    stream.Position = 0;
        //    string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

        //    //return File(stream, "application/octet-stream", excelName);  
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        //    //return Json(new { data = stream, v= "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName }, JsonRequestBehavior.AllowGet);


        //    //string folder = _hostingEnvironment.WebRootPath;
        //    //string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
        //    //string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
        //    //FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    //if (file.Exists)
        //    //{
        //    //    file.Delete();
        //    //    file = new FileInfo(Path.Combine(folder, excelName));
        //    //}
        //    //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    //using (var package = new ExcelPackage(file))
        //    //{
        //    //    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //    //    workSheet.Cells.LoadFromCollection(service, true);
        //    //    package.Save();
        //    //}
        //}

    }


    }


