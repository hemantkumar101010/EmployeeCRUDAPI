using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("{EmpId},{EmployeeName},{EmployeeAge},{EmployeeAddres}")]
        public ActionResult AddAEmployee(int EmpId,string EmployeeName,int EmployeeAge,string EmployeeAddres)
        {
            employeeList.Add(new EmployeeDetails { EmpId = EmpId, EmployeeName = EmployeeName, EmployeeAge = EmployeeAge, EmployeeAddress = EmployeeAddres });
            int i= employeeList.Count();
            return Ok("New Employeed Added!!");
        }


        [HttpGet]
        public ActionResult GetAllEmployee()
        {
            if (employeeList.Count() == 0)
            {
                return Ok("Currently employee list is empty.");
            }
            else
            {
                return Ok(employeeList);
            }
            
        }

        [HttpPut("EmpId")]
        public ActionResult UpdateAEmployeeDetails(int EmpId,string empName,int EmpAge,string Address)
        {
            var editEmployee =  employeeList.Where(obj => obj.EmpId==EmpId).FirstOrDefault();
            if (editEmployee != null)
            {
                editEmployee.EmployeeName = empName;
                editEmployee.EmployeeAge = EmpAge;
                editEmployee.EmployeeAddress = Address;

                return Ok($"EmpId: {EmpId} details updated.");
            }
            else
            {
                return Ok($"EmpId: {EmpId} not found");
            }    

        }


        [HttpDelete("{EmpId}")]
        public ActionResult RemoveEmployee(int EmpId)
        {
            var editEmployee = employeeList.Where(obj => obj.EmpId == EmpId).FirstOrDefault();
            if (editEmployee != null)
            {
                employeeList.Remove(editEmployee);

                return Ok($"EmpId: {EmpId} removed from employee list.");
            }
            else
            {
                return Ok($"EmpId: {EmpId} not found");
            }
        }
    }
}