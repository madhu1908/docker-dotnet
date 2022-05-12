using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using webinar.Helpers;
using webinar.Models;

namespace webinar.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly string constrs = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;


        // GET: Admin
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("Normal", "SuperAdmin", "Admin")]
        public ActionResult Index()
        {

            return View();
        }

        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("Normal", "SuperAdmin", "Admin")]
        public ActionResult GetData()
        {
            List<RapidDataUsers> employee = new List<RapidDataUsers>();

            try
            {

                string query = "select * from  RapidDataUsers;";
                SqlConnection connection = new SqlConnection(constrs);
                SqlCommand cmd = new SqlCommand(query, connection);
                 connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    RapidDataUsers newstud = new RapidDataUsers();
                    newstud.UserId = Convert.ToInt32(sdr["UserId"]) == 0 ? 0 : Convert.ToInt32(sdr["UserId"]);
                    newstud.UserName = (sdr["UserName"]) == DBNull.Value ? string.Empty : sdr["UserName"].ToString();
                    newstud.phoneNumber = (sdr["phoneNumber"]) == DBNull.Value ? string.Empty : sdr["phoneNumber"].ToString();
                    newstud.RoleName = (sdr["RoleName"]) == DBNull.Value ? string.Empty : sdr["RoleName"].ToString();
                    newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                    newstud.CreadtedTimestamp = (sdr["CreadtedTimestamp"]) == DBNull.Value ? DateTime.Now : Convert.ToDateTime(sdr["CreadtedTimestamp"]);
                    newstud.Sessionrolename = Session["RoleName"].ToString();
                    employee.Add(newstud);
                }
                sdr.Close();
                connection.Close();
                return Json(new { data = employee }, JsonRequestBehavior.AllowGet);




            }
            catch (Exception e)
            {
                return Json(employee, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult createEmployeorEdit(int id = 0)
        {
            
          

            if (id == 0) 
            {
                using (SqlConnection sqlCon = new SqlConnection(constrs))
                {
                    string query1 = "select  RoleId, RoleName from Role";
                    List<SelectListItem> Rolelist = new List<SelectListItem>();
                    SqlCommand cmd = new SqlCommand(query1, sqlCon);
                    sqlCon.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        Rolelist.Add(new SelectListItem { Text = sdr["RoleName"].ToString(), Value = sdr["RoleName"].ToString() });

                    }

                    ViewBag.employerole = Rolelist;
                    sdr.Close();


                }
                return View(new RapidDataUsers());
            }
               
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(constrs))
                {
                    string query1 = "select  RoleId, RoleName from Role";
                    List<SelectListItem> Rolelist = new List<SelectListItem>();
                    SqlCommand cmd = new SqlCommand(query1, sqlCon);
                    cmd.Parameters.AddWithValue("@UserId", id);
                    sqlCon.Open(); 
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        Rolelist.Add(new SelectListItem { Text = sdr["RoleName"].ToString(), Value = sdr["RoleName"].ToString() });

                    }
                    sdr.Close();
                    ViewBag.employerole = Rolelist;
                    List<RapidDataUsers> employee = new List<RapidDataUsers>();
                    query1 = "select * from  RapidDataUsers where UserId=@UserId;";
                    cmd = new SqlCommand(query1, sqlCon);
                    cmd.Parameters.AddWithValue("@UserId", id);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        RapidDataUsers newstud = new RapidDataUsers();
                        newstud.UserId = Convert.ToInt32(sdr["UserId"]) == 0 ? 0 : Convert.ToInt32(sdr["UserId"]);
                        newstud.UserName = (sdr["UserName"]) == DBNull.Value ? string.Empty : sdr["UserName"].ToString();
                        newstud.phoneNumber = (sdr["phoneNumber"]) == DBNull.Value ? string.Empty : sdr["phoneNumber"].ToString();
                        newstud.RoleName = (sdr["RoleName"]) == DBNull.Value ? string.Empty : sdr["RoleName"].ToString();
                        newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                        newstud.CreadtedTimestamp = (DateTime)sdr["CreadtedTimestamp"];
                        newstud.Password = (sdr["Password"]) == DBNull.Value ? string.Empty : sdr["Password"].ToString();
                        newstud.Password = DecryptString(newstud.Password);

                        employee.Add(newstud);
                        return View(newstud);
                    }
                    sdr.Close();
                    return View();
                }
            }

        }



        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult createEmployeorEdit(Models.RapidDataUsers rapidData)
        {
            string query = "insert into RapidDataUsers (UserName,Password,CreadtedTimestamp,CreatedBy,RoleName,phoneNumber) values (@UserName,@Password,@CreadtedTimestamp,@CreatedBy,@RoleName,@phoneNumber)";

          
               using (SqlConnection sqlCon = new SqlConnection(constrs))
                {
                    string query1 = "select  RoleId, RoleName from Role";
                    List<SelectListItem> Rolelist = new List<SelectListItem>();
                    SqlCommand cmd = new SqlCommand(query1, sqlCon);
                    sqlCon.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        Rolelist.Add(new SelectListItem { Text = sdr["RoleName"].ToString(), Value = sdr["RoleName"].ToString() });

                    }
                    sdr.Close();
                    ViewBag.employerole = Rolelist;
                     if(ModelState.IsValid)
                {
                    if (rapidData.UserId == 0)
                    {


                        cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@UserName", rapidData.UserName);
                        cmd.Parameters.AddWithValue("@Password", EncryptString(rapidData.Password));
                        cmd.Parameters.AddWithValue("@CreadtedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]);
                        cmd.Parameters.AddWithValue("@RoleName", rapidData.RoleName);
                        cmd.Parameters.AddWithValue("@phoneNumber", rapidData.phoneNumber);
                        cmd.ExecuteNonQuery();
                       
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        query = "update RapidDataUsers set UserName=UserName,Password=@Password,CreatedBy=@CreatedBy,RoleName=@RoleName,updatedTimestamp=@updatedTimestamp,phoneNumber=@phoneNumber where UserId=@UserId;";
                        cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@UserName", rapidData.UserName);
                        cmd.Parameters.AddWithValue("@Password", EncryptString(rapidData.Password));
                        cmd.Parameters.AddWithValue("@updatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]);
                        cmd.Parameters.AddWithValue("@RoleName", rapidData.RoleName);
                        cmd.Parameters.AddWithValue("@phoneNumber", rapidData.phoneNumber);
                        cmd.Parameters.AddWithValue("@UserId", rapidData.UserId);
                        cmd.ExecuteNonQuery();
                      
                        return RedirectToAction("Index");

                    }
                }
                   
                return View();
            }


            
        }

        [CustomAuthenticationFilter]
        public ActionResult roleIndexpage()
        {
            return View();
        }

        [CustomAuthenticationFilter]
        public ActionResult GetRole()
        {

            List<Roles> role = new List<Roles>();

            try
            {

                string query = "select * from  role;";
                SqlConnection connection = new SqlConnection(constrs);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Roles newstud = new Roles();
                    newstud.RoleId = Convert.ToInt32(sdr["RoleId"]) == 0 ? 0 : Convert.ToInt32(sdr["RoleId"]);
                    newstud.RoleName = (sdr["RoleName"]) == DBNull.Value ? string.Empty : sdr["RoleName"].ToString();
                    newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                    newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ?DateTime.Now : Convert.ToDateTime(sdr["CreatedTimestamp"]);
                    newstud.Sessionrolename = Session["RoleName"].ToString();
                    role.Add(newstud);
                }
                sdr.Close();
                connection.Close();
                return Json(new { data = role }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(role, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult CreateorEditRole(int id=0)
        {
            if (id == 0)
            {
                
                return View(new Roles());
            }

            else
            {
                
                using (SqlConnection sqlCon = new SqlConnection(constrs))
                {
                    
                    List<Roles> role = new List<Roles>();
                    string query1 = "select * from  role where RoleId=@RoleId;";
                    SqlCommand  cmd = new SqlCommand(query1, sqlCon);
                    sqlCon.Open();
                    cmd.Parameters.AddWithValue("@RoleId", id);
                    SqlDataReader  sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Roles newstud = new Roles();
                        newstud.RoleId = Convert.ToInt32(sdr["RoleId"]) == 0 ? 0 : Convert.ToInt32(sdr["RoleId"]);
                        newstud.RoleName = (sdr["RoleName"]) == DBNull.Value ? string.Empty : sdr["RoleName"].ToString();
                        newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                        newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : (DateTime)sdr["CreatedTimestamp"];

                        role.Add(newstud);
                        return View(newstud);
                    }
                    sdr.Close();
                    return View();
                }
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute( "SuperAdmin")]
        public ActionResult CreateorEditRole(Models.Roles role)
        {
            string query = "insert into role (RoleName,CreatedTimestamp,CreatedBy) values (@RoleName,@CreatedTimestamp,@CreatedBy)";

            using (SqlConnection sqlCon = new SqlConnection(constrs))
            {
               
                if (ModelState.IsValid)
                {
                    if (role.RoleId == 0)
                    {


                       SqlCommand cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
                        cmd.Parameters.AddWithValue("@CreatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("roleIndexpage");
                    }
                    else
                    {
                        query = "update role set RoleName=@RoleName,UpdatedBy=@UpdatedBy,modifiedTimestamp=@modifiedTimestamp where RoleId=@RoleId;";
                         SqlCommand  cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
                        cmd.Parameters.AddWithValue("@modifiedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"]);  
                        cmd.Parameters.AddWithValue("@RoleId", role.RoleId);
                      
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("roleIndexpage");

                    }
                }

                return View();
            }         
        }

        [CustomAuthenticationFilter]
        public ActionResult departIndexpage()
        {
            return View();
        }

        [CustomAuthenticationFilter]
        public ActionResult GetallDepartments()
        {
            List<Departments> depart = new List<Departments>();

            try
            {

                string query = "select * from  Departments;";
                SqlConnection connection = new SqlConnection(constrs);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Departments newstud = new Departments();
                    newstud.DepartmentId = Convert.ToInt32(sdr["DepartmentId"]) == 0 ? 0 : Convert.ToInt32(sdr["DepartmentId"]);
                    newstud.DepartmentName = (sdr["DepartmentName"]) == DBNull.Value ? string.Empty : sdr["DepartmentName"].ToString();
                    newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                    newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : Convert.ToDateTime(sdr["CreatedTimestamp"]);
                    newstud.rolename = Session["RoleName"].ToString();
                    depart.Add(newstud);
                }

                sdr.Close();
               string role = (string)Session["RoleName"];
                connection.Close();
                return Json(new { data = depart,role = role }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(depart, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute( "SuperAdmin")]
        public ActionResult createOrEditDepartment(int id= 0)
        {
            if (id == 0)
            {

                return View(new Departments());
            }

            else
            {

                using (SqlConnection sqlCon = new SqlConnection(constrs))
                {

                    List<Departments> depart = new List<Departments>();
                    string query1 = "select * from  Departments where DepartmentId=@DepartmentId;";
                    SqlCommand cmd = new SqlCommand(query1, sqlCon);
                    sqlCon.Open();
                    cmd.Parameters.AddWithValue("@DepartmentId", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Departments newstud = new Departments();
                        newstud.DepartmentId = Convert.ToInt32(sdr["DepartmentId"]) == 0 ? 0 : Convert.ToInt32(sdr["DepartmentId"]);
                        newstud.DepartmentName = (sdr["DepartmentName"]) == DBNull.Value ? string.Empty : sdr["DepartmentName"].ToString();
                        newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                        newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : (DateTime)sdr["CreatedTimestamp"];

                        depart.Add(newstud);
                        return View(newstud);
                    }
                    sdr.Close();
                    return View();
                }
            }
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult createOrEditDepartment(Models.Departments depart)
        {
            string query = "insert into Departments (DepartmentName,CreatedTimestamp,CreatedBy) values (@DepartmentName,@CreatedTimestamp,@CreatedBy)";

            using (SqlConnection sqlCon = new SqlConnection(constrs))
            {

                if (ModelState.IsValid)
                {
                    if (depart.DepartmentId == 0)
                    {


                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@DepartmentName", depart.DepartmentName);
                        cmd.Parameters.AddWithValue("@CreatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("departIndexpage");
                    }
                    else
                    {
                        query = "update Departments set DepartmentName=@DepartmentName,UpdatedBy=@UpdatedBy,UpdatedTimestamp=@UpdatedTimestamp where DepartmentId=@DepartmentId;";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@DepartmentName", depart.DepartmentName);
                        cmd.Parameters.AddWithValue("@UpdatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"]);
                        cmd.Parameters.AddWithValue("@DepartmentId", depart.DepartmentId);

                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("departIndexpage");

                    }
                }

                return View();
            }
        }



        [CustomAuthenticationFilter]
        public ActionResult PassedOutIndexpage()
        {
            return View();
        }

        [CustomAuthenticationFilter]
        public ActionResult GetallPassedout()
        {
            List<Passedoutyear> depart = new List<Passedoutyear>();

            try
            {

                string query = "select * from  Passedoutyear;";
                SqlConnection connection = new SqlConnection(constrs);
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Passedoutyear newstud = new Passedoutyear();
                    newstud.id = Convert.ToInt32(sdr["id"]) == 0 ? 0 : Convert.ToInt32(sdr["id"]);
                    newstud.passedoutyear = (sdr["passedoutyear"]) == DBNull.Value ? string.Empty : sdr["passedoutyear"].ToString();
                    newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                    newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : Convert.ToDateTime(sdr["CreatedTimestamp"]);
                    newstud.RoleName = Session["RoleName"].ToString();
                    depart.Add(newstud);
                }
                sdr.Close();
                connection.Close();
                return Json(new { data = depart }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(depart, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult createOrEditpassedoutyear(int id = 0)
        {
            if(id == 0)
            {

                return View(new Passedoutyear());
            }

            else
            {

                using (SqlConnection sqlCon = new SqlConnection(constrs))
                {

                    List<Passedoutyear> depart = new List<Passedoutyear>();
                    string query1 = "select * from  Passedoutyear where id=@id;";
                    SqlCommand cmd = new SqlCommand(query1, sqlCon);
                    sqlCon.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Passedoutyear newstud = new Passedoutyear();
                        newstud.id = Convert.ToInt32(sdr["id"]) == 0 ? 0 : Convert.ToInt32(sdr["id"]);
                        newstud.passedoutyear = (sdr["passedoutyear"]) == DBNull.Value ? string.Empty : sdr["passedoutyear"].ToString();
                        newstud.CreatedBy = (sdr["CreatedBy"]) == DBNull.Value ? string.Empty : sdr["CreatedBy"].ToString();
                        newstud.CreatedTimestamp = (sdr["CreatedTimestamp"]) == DBNull.Value ? DateTime.Now : (DateTime)sdr["CreatedTimestamp"];

                        depart.Add(newstud);
                        return View(newstud);
                    }
                    sdr.Close();
                    return View();
                }
            }
        }


            [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizationAttribute("SuperAdmin")]
        public ActionResult createOrEditpassedoutyear(Models.Passedoutyear year)
        {
            string query = "insert into Passedoutyear (passedoutyear,CreatedTimestamp,CreatedBy) values (@passedoutyear,@CreatedTimestamp,@CreatedBy)";

            using (SqlConnection sqlCon = new SqlConnection(constrs))
            {

                if (ModelState.IsValid)
                {
                    if (year.id == 0)
                    {


                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@passedoutyear", year.passedoutyear);
                        cmd.Parameters.AddWithValue("@CreatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"]);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("PassedOutIndexpage");
                    }
                    else
                    {
                        query = "update Passedoutyear set passedoutyear=@passedoutyear,UpdatedBy=@UpdatedBy,UpdatedTimestamp=@UpdatedTimestamp where id=@id;";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@passedoutyear", year.passedoutyear);
                        cmd.Parameters.AddWithValue("@UpdatedTimestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserName"]);
                        cmd.Parameters.AddWithValue("@id", year.id);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        return RedirectToAction("PassedOutIndexpage");

                    }
                }

                return View();
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser users)
        {
            string query = "select * from RapidDataUsers where UserName=@UserName";

            if(ModelState.IsValid)
            {
                using(SqlConnection sqlcon = new SqlConnection(constrs))
                {
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    cmd.Parameters.AddWithValue("@UserName", users.UserName);
                    sqlcon.Open();
                    SqlDataReader sdr =cmd.ExecuteReader();
                    if(sdr.HasRows)
                    {
                        while(sdr.Read())
                        {
                            LoginUser databaseuser = new LoginUser();

                            databaseuser.UserName = sdr["UserName"].ToString();
                            databaseuser.Password = sdr["Password"].ToString();
                            databaseuser.UserId = (int)sdr["UserId"];
                            databaseuser.Password = DecryptString(databaseuser.Password);
                            databaseuser.RoleName = sdr["RoleName"].ToString();
                            Session["UserName"] = databaseuser.UserName;
                            Session["UserId"] = databaseuser.UserId;
                            Session["RoleName"] = databaseuser.RoleName;
                           

                            if (databaseuser.Password ==users.Password)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Password Does Not Match Please Check!!");
                                return View();
                            }
                        }
                        sdr.Close();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Not Exist. Please check Your Email Address!!");
                        return View();
                    }
                }
            }
            
            return View();
        }


        [CustomAuthenticationFilter]
        public ActionResult Logoff()
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            Session["RoleName"] = string.Empty;
            return RedirectToAction("Login", "Admin");
        }

        public static string EncryptString(string plainText)
        {
            
            string key = "RapidDataTechnol";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }



        public static string DecryptString(string cipherText)
        {
           string key = "RapidDataTechnol";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }


        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Un Authorized Page!";

            return View();
        }

        [HttpPost]
        public ActionResult delete(string table,string idname, int id)
        {

            string query = "delete  from "+ table +" where "+ idname + "=@id";
            using(SqlConnection sqlcon = new SqlConnection(constrs))
            {
             sqlcon.Open();
             SqlCommand cmd = new SqlCommand(query, sqlcon);
             cmd.Parameters.AddWithValue("@id", id);
             cmd.ExecuteNonQuery();
               
            }

            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult orgtree()
        //{

        //    string query = "select * from orgTree ";
        //    List<orgtTress> list = new List<orgtTress>();
           
        //    using (SqlConnection sqlcon = new SqlConnection(constrs))
        //    {
        //        sqlcon.Open();
        //        SqlCommand cmd = new SqlCommand(query, sqlcon);
        //        SqlDataReader sdr =  cmd.ExecuteReader();
        //        if (sdr.HasRows)
        //        {
        //            while (sdr.Read())
        //            {
        //                child org = new child();
        //                org.id = sdr["id"]!=DBNull.Value ?0: Convert.ToInt32(sdr["id"]);
        //                org.name = sdr["id"] != null ? "" : sdr["id"].ToString();
        //                org.reportingto = sdr["id"] != DBNull.Value ? 0 : Convert.ToInt32(sdr["id"]);
        //                list.Add(org);  
        //            }
        //        }

        //        var root = list.FindAll(ele =>
        //         {
        //             return ele.reportingto == "";
                     
        //         });
        //        List<parent> orgtree = new List<parent>();
        //      for (int i = 0; i < root.Count; i++)
        //        {
                   
        //                var newchild = list.FindAll(child =>
        //                {
        //                    return  child.reportingto == (root[i].id).ToString();
                           
        //                });
                 
        //            if (newchild.Count>0)
        //                {
        //                    for (int j = 0; j < newchild.Count; j++)
        //                    {
        //                        var parentchild = list.FindAll(eles =>
        //                        {
        //                            return root[i].reportingto == newchild[j].reportingto;  
        //                        });
        //                    }
        //                }

                  
        //        }

               

        //    }

        //    return Json(new { success = true, });
        //}


        public ActionResult orgtree()
        {
            List<orgtree> categories = new List<orgtree>()
     {
         new orgtree () { Id = 1, Name = "aysha", reportingto = 0},
         new orgtree() { Id = 2, Name = "maree", reportingto = 1 },
         new orgtree() { Id = 3, Name = "vijay", reportingto = 2 },
         new orgtree() { Id = 4, Name = "nisha", reportingto = 1 },
         new orgtree() { Id = 5, Name = "ajay", reportingto = 3 },
         new orgtree() { Id = 6, Name = "sriram", reportingto = 4 },
         new orgtree() { Id = 7, Name = "vinoth", reportingto = 2 }
     };

            List<orgtree> hierarchy = new List<orgtree>();
            hierarchy = categories
                            .Where(c => c.reportingto == 0)
                            .Select(c => new orgtree()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                reportingto = c.reportingto,
                               
                                Children = GetChildren(categories, c.Id)
                            })
                            .ToList();

            return Json(new { success = true, message = " Successfully", hierarchy }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public List<orgtree> GetChildren(List<orgtree> comments, int parentId)
        {
            return comments
                    .Where(c => c.reportingto == parentId)
                    .Select(c => new orgtree
                    {
                        Id = c.Id,
                        Name = c.Name,
                        reportingto = c.reportingto,
                        Children = GetChildren(comments, c.Id)
                    })
                    .ToList();
        }


    }

    
}