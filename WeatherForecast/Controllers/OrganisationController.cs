using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganisationController : ControllerBase
    {

        private readonly ILogger<OrganisationController> _logger;
        public OrganisationController(ILogger<OrganisationController> logger)
        {
            _logger = logger;
        }

        static List<Organisation> orgs = new List<Organisation>();

        #region pass-parameters-using-uri
        [HttpPost]
        public ActionResult AddEmployeeOrgsFromUri([System.Web.Http.FromUri] int OrganisationId, [System.Web.Http.FromUri] string OrganisationName, [System.Web.Http.FromUri] int EmployeeId)
        {
            orgs.Add(new Organisation { OrganisationId = OrganisationId, OrganisationName = OrganisationName, EmployeeId= EmployeeId });
            var serializedOp = JsonConvert.SerializeObject(orgs[orgs.Count - 1]);
            return Ok($"{serializedOp} added in the org list");
        }

        [HttpGet]
        public ActionResult GetOrgListOfAEmployeeFromUri([System.Web.Http.FromUri] int EmpId)
        {

            var empOrgList = orgs.Where(e => e.EmployeeId == EmpId).ToList();
            if (orgs.Count > 0)
            {
                var serializedOp = JsonConvert.SerializeObject(orgs);
                return Ok($"{serializedOp}");
            }
            else
            {
                return Ok($"EmpID: {EmpId} does not have any education details.");
            }


        }

        [HttpPut]
        public ActionResult UpdatedEmployeeOrgDetails([System.Web.Http.FromUri] int OrganisationId, [System.Web.Http.FromUri] string OrganisationName, [System.Web.Http.FromUri] int EmployeeId)
        {
            var emp = orgs.Where(emp => emp.OrganisationId == OrganisationId).FirstOrDefault();
            if (emp == null)
            {
                return Ok("Org id not found");
            }
            else
            {
                emp.OrganisationId = OrganisationId;
                emp.OrganisationName = OrganisationName;
                emp.EmployeeId = EmployeeId;
                var serializedOp = JsonConvert.SerializeObject(emp);
                return Ok($"{serializedOp} updated");
            }


        }

        [HttpPatch]
        public ActionResult UpdateOnlyOrgNameField([System.Web.Http.FromUri] int OrganisationId, [System.Web.Http.FromUri] string orgName)
        {
            var emp = orgs.Where(emp => emp.OrganisationId == OrganisationId).FirstOrDefault();
            if (emp == null)
            {
                return Ok("org id not found");
            }
            else
            {
                emp.OrganisationName = orgName;
                var serializedOp = JsonConvert.SerializeObject(emp);
                return Ok($"{serializedOp} updated");
            }
        }

        [HttpDelete]
        public ActionResult DateteAEmployeeOrg([System.Web.Http.FromUri] int OrganisationId)
        {
            var deleteEmployee = orgs.Where(obj => obj.OrganisationId == OrganisationId).FirstOrDefault();
            if (deleteEmployee != null)
            {
                orgs.Remove(deleteEmployee);

                return Ok($"EmpId: {OrganisationId} removed from employee edu list.");
            }
            else
            {
                return Ok($"EmpId: {OrganisationId} not found");
            }

        }
        #endregion

        #region pass-params-in-body

        [HttpPost]
        public ActionResult AddEmployeeOrgFromBody([System.Web.Http.FromBody] Organisation organisation)
        {
            orgs.Add(new Organisation { OrganisationId = organisation.OrganisationId, OrganisationName = organisation.OrganisationName,EmployeeId=organisation.EmployeeId });
            var serializedOp = JsonConvert.SerializeObject(orgs[orgs.Count - 1]);
            return Ok($"{serializedOp} added in the org list");
        }

        [HttpGet]
        public ActionResult GetOrgListOfAEmployeeFromBody([System.Web.Http.FromBody] int EmpId)
        {

            var empOrgList = orgs.Where(e => e.EmployeeId == EmpId).ToList();
            var serializedOp = JsonConvert.SerializeObject(empOrgList);
            return Ok($"{serializedOp}");

        }

        [HttpPut]
        public ActionResult UpdatedEmployeeOrgDetailsFromBody([System.Web.Http.FromBody] Organisation organisation)
        {
            var org = orgs.Where(org => org.OrganisationId == organisation.OrganisationId).FirstOrDefault();
            if (org == null)
            {
                return Ok("org id not found");
            }
            else
            {
                org.OrganisationName = organisation.OrganisationName;
                org.EmployeeId = organisation.EmployeeId;

                var serializedOp = JsonConvert.SerializeObject(org);
                return Ok($"{serializedOp} updated");
            }


        }

        [HttpPatch]
        public ActionResult UpdateOnlyOrgNamFieldFromBody([System.Web.Http.FromBody] int orgId, [System.Web.Http.FromBody] string updatedOrgName)
        {
            var org = orgs.Where(org => org.OrganisationId == orgId).FirstOrDefault();
            if (org == null)
            {
                return Ok("org id not found");
            }
            else
            {
                org.OrganisationName = updatedOrgName;
                var serializedOp = JsonConvert.SerializeObject(org);
                return Ok($"{serializedOp} updated");
            }
        }

        [HttpDelete]
        public ActionResult DateteAEmployeeOrgFromBody([System.Web.Http.FromBody] int OrgId)
        {
            var deleteOrg = orgs.Where(obj => obj.OrganisationId == OrgId).FirstOrDefault();
            if (deleteOrg != null)
            {
                orgs.Remove(deleteOrg);

                return Ok($"EmpId: {OrgId} removed from employee edu list.");
            }
            else
            {
                return Ok($"EmpId: {OrgId} not found");
            }

        }
        #endregion

    }
}
