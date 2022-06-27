using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    { 

       private readonly ILogger<WeatherForecastController> _logger;
       public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //Employee Details CRUD Handson


        static List<EmployeeDetails> employeeList= new List<EmployeeDetails>();

        #region 24-06 hansOn
        //[HttpPost]
        //public ActionResult AddAEmployee(int EmpId,string EmployeeName,int EmployeeAge,string EmployeeAddres)
        //{
        //    employeeList.Add(new EmployeeDetails { EmpId = EmpId, EmployeeName = EmployeeName, EmployeeAge = EmployeeAge, EmployeeAddress = EmployeeAddres });
        //    int i= employeeList.Count();
        //    return Ok("New Employeed Added!!");
        //}

        //[HttpGet]
        //public ActionResult GetAllEmployee()
        //{
        //    if (employeeList.Count() == 0)
        //    {
        //        return Ok("Currently employee list is empty.");
        //    }
        //    else
        //    {
        //        return Ok(employeeList);
        //    }
            
        //}

        //[HttpPut]
        //public ActionResult UpdateAEmployeeDetails(int EmpId,string empName,int EmpAge,string Address)
        //{
        //    var editEmployee =  employeeList.Where(obj => obj.EmpId==EmpId).FirstOrDefault();
        //    if (editEmployee != null)
        //    {
        //        editEmployee.EmployeeName = empName;
        //        editEmployee.EmployeeAge = EmpAge;
        //        editEmployee.EmployeeAddress = Address;

        //        return Ok($"EmpId: {EmpId} details updated.");
        //    }
        //    else
        //    {
        //        return Ok($"EmpId: {EmpId} not found");
        //    }    

        //}

        //[HttpDelete]
        //public ActionResult RemoveEmployee(int EmpId)
        //{
        //    var editEmployee = employeeList.Where(obj => obj.EmpId == EmpId).FirstOrDefault();
        //    if (editEmployee != null)
        //    {
        //        employeeList.Remove(editEmployee);

        //        return Ok($"EmpId: {EmpId} removed from employee list.");
        //    }
        //    else
        //    {
        //        return Ok($"EmpId: {EmpId} not found");
        //    }
        //}
        #endregion


        //27-06-22 handson  crud on employeeDetails
        #region pass-parameters-using-uri
        [HttpPost]
        public ActionResult AddEmployeeFromUri([System.Web.Http.FromUri] int EmpId, [System.Web.Http.FromUri]string EmployeeName, [System.Web.Http.FromUri] int EmployeeAge,[System.Web.Http.FromUri] string EmployeeAddress)
        {
            employeeList.Add(new EmployeeDetails { EmpId = EmpId, EmployeeName = EmployeeName, EmployeeAge = EmployeeAge, EmployeeAddress = EmployeeAddress });
            var serializedOp = JsonConvert.SerializeObject(employeeList[employeeList.Count-1]);
            return Ok($"{serializedOp} added in the employeelist");
        }

        [HttpGet]
        public ActionResult GetAllEmployeeFromUri()
        {
            if (employeeList.Count() == 0)
            {
                return Ok("Currently employee list is empty.");
            }
            else
            {
                var serializedOp = JsonConvert.SerializeObject(employeeList);
                return Ok(serializedOp);
            }

        }
        [HttpGet]
        public ActionResult GetAEmployeeByIdFromUri([System.Web.Http.FromUri] int EmpId)
        {

            var employee = employeeList.Find(x => x.EmpId == EmpId);
            var serializedOp = JsonConvert.SerializeObject(employee);
            return Ok(serializedOp);

        }

        [HttpPut]
        public ActionResult UpdatedEmployeeDetails([System.Web.Http.FromUri] int EmpId, [System.Web.Http.FromUri] string UpdatedName, [System.Web.Http.FromUri] int UpdatedAge, [System.Web.Http.FromUri] string UpdatedAddress)
        {
            var emp = employeeList.Where(emp => emp.EmpId == EmpId).FirstOrDefault();
            int index = employeeList.FindIndex(emp => emp.EmpId == EmpId);
            if(emp == null)
            {
                Console.WriteLine("Employee not found");
            }
            else
            {
                emp.EmployeeName = UpdatedName;
                emp.EmployeeAge = UpdatedAge;
                emp.EmployeeAddress = UpdatedAddress;
               // employeeList[index] = emp;
            }
            var serializedOp = JsonConvert.SerializeObject(employeeList);
            return Ok($"{serializedOp} updated");

        }

        [HttpDelete]
        public ActionResult DateteAEmployee([System.Web.Http.FromUri] int EmpId)
        {
            var deleteEmployee = employeeList.Where(obj => obj.EmpId == EmpId).FirstOrDefault();
            if (deleteEmployee != null)
            {
                employeeList.Remove(deleteEmployee);

                return Ok($"EmpId: {EmpId} removed from employee list.");
            }
            else
            {
                return Ok($"EmpId: {EmpId} not found");
            }

        }
        #endregion

        #region pass-params-inside-body

        [HttpPost]
        public ActionResult AddEmployeeFromBody([System.Web.Http.FromBody] EmployeeDetails employeeDetails)
        {
            employeeList.Add(new EmployeeDetails { EmpId = employeeDetails.EmpId, EmployeeName = employeeDetails.EmployeeName, EmployeeAge = employeeDetails.EmployeeAge, EmployeeAddress = employeeDetails.EmployeeAddress });
            var serializedOp = JsonConvert.SerializeObject(employeeList[employeeList.Count - 1]);
            return Ok($"{serializedOp} added in the employeelist");
        }

        [HttpGet]
        public ActionResult GetAllEmployeeFromBody()
        {
            if (employeeList.Count() == 0)
            {
                return Ok("Currently employee list is empty.");
            }
            else
            {
                var serializedOp = JsonConvert.SerializeObject(employeeList);
                return Ok(serializedOp);
            }

        }

        [HttpGet]
        public ActionResult GetAEmployeeByIdFromBody([System.Web.Http.FromBody] int EmpId)
        {
            var employee = employeeList.Find(x => x.EmpId == EmpId);
            var serializedOp = JsonConvert.SerializeObject(employee);
            return Ok(serializedOp);

        }


        [HttpPut]
        public ActionResult UpdatedEmployeeDetailsFromBody([System.Web.Http.FromBody] EmployeeDetails employeeDetails)
        {
            var emp = employeeList.Where(emp => emp.EmpId == employeeDetails.EmpId).FirstOrDefault();
            if (emp == null)
            {
                Console.WriteLine("Employee not found");
            }
            else
            {
             
                emp.EmployeeName = employeeDetails.EmployeeName;
                emp.EmployeeAge = employeeDetails.EmployeeAge;
                emp.EmployeeAddress = employeeDetails.EmployeeAddress;
                // employeeList[index] = emp;
            }
            var serializedOp = JsonConvert.SerializeObject(employeeList);
            return Ok($"{serializedOp} updated");

        }

        [HttpDelete]
        public ActionResult DateteAEmployeeFromBody([System.Web.Http.FromBody] EmployeeDetails employeeDetails)
        {
            var deleteEmployee = employeeList.Where(obj => obj.EmpId == employeeDetails.EmpId).FirstOrDefault();
            if (deleteEmployee != null)
            {
                employeeList.Remove(deleteEmployee);

                return Ok($"EmpId: {employeeDetails.EmpId} removed from employee list.");
            }
            else
            {
                return Ok($"EmpId: {employeeDetails.EmpId} not found");
            }

        }

        [HttpPatch]
        public ActionResult UpdateASpecificProperty([System.Web.Http.FromBody] EmployeeDetails employeeDetails)
        {
            var employee = employeeList.Where(obj =>obj.EmpId == employeeDetails.EmpId).FirstOrDefault();
            if(employee != null)
            {
                employee.EmployeeName = employeeDetails.EmployeeName;
                var serializedOp = JsonConvert.SerializeObject(employee);
                return Ok($"{serializedOp} updated");
            }
            else
            {
                return Ok("Id not found");
            }
            

        }
        #endregion
    }
}