using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineTicketingSystem.Data;
using OnlineTicketingSystem.Models;
using System.Data.Entity.Core.Objects;
using System.Threading;
using System.Globalization;

namespace OnlineTicketingSystem.Controllers
{
    public class TicketsController : Controller
    {
        private OnlineTicketingSystemContext db = new OnlineTicketingSystemContext();

        // GET: Tickets
        //public async Task<ActionResult> Index(string sortOrder, string language)
        public ActionResult Index(string sortOrder, string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            else
            {
                HttpCookie cookie1 = Request.Cookies["Languages"];

                if (cookie1 != null && cookie1.Value != null)
                {
                    language = cookie1.Value;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                }
            }
                
            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            var tickets = db.Tickets.Include(t => t.Department).Include(t => t.Employee).Include(t => t.Project);

            ViewBag.DeptSortParm = String.IsNullOrEmpty(sortOrder) ? "dept_desc" : "";
            ViewBag.EmpSortParm = sortOrder == "Emp" ? "emp_desc" : "Emp";
            ViewBag.ProjSortParm = sortOrder == "Proj" ? "proj_desc" : "Proj";
            ViewBag.DesSortParm = sortOrder == "Des" ? "dec_desc" : "Des";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

            //var students = from s in db.Students select s;

            switch (sortOrder)
            {
                case "dept_desc":
                    tickets = tickets.OrderByDescending(t => t.Department.DeptName);
                    break;
                case "Emp":
                    tickets = tickets.OrderBy(t => t.Employee.EmpName);
                    break;
                case "emp_desc":
                    tickets = tickets.OrderByDescending(t => t.Employee.EmpName);
                    break;
                case "Proj":
                    tickets = tickets.OrderBy(t => t.Project.ProjectTitle);
                    break;
                case "proj_desc":
                    tickets = tickets.OrderByDescending(t => t.Project.ProjectTitle);
                    break;
                case "Des":
                    tickets = tickets.OrderBy(t => t.Description);
                    break;
                case "des_desc":
                    tickets = tickets.OrderByDescending(t => t.Description);
                    break;
                case "Date":
                    tickets = tickets.OrderBy(t => t.SubmitDate);
                    break;
                case "date_desc":
                    tickets = tickets.OrderByDescending(t => t.SubmitDate);
                    break;
                case "Status":
                    tickets = tickets.OrderBy(t => t.Status);
                    break;
                case "status_desc":
                    tickets = tickets.OrderByDescending(t => t.Status);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.Department.DeptName);
                    break;
            }

            //return View(await tickets.ToListAsync());
            return View(tickets.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(string Search, string SearchString)
        {
            var tickets = db.Tickets.Include(t => t.Department).Include(t => t.Employee).Include(t => t.Project);

            if (!string.IsNullOrEmpty(SearchString))
            {
                switch (Search)
                {
                    case "DeptName":
                        tickets = tickets.Where(s => s.Department.DeptName.Contains(SearchString));
                        break;
                    case "EmpName":
                        tickets = tickets.Where(s => s.Employee.EmpName.Contains(SearchString));
                        break;
                    case "Date":
                        var SearchText = Convert.ToDateTime(SearchString);
                        tickets = tickets.Where(s => s.SubmitDate.Year == SearchText.Year && 
                                                    s.SubmitDate.Month == SearchText.Month && 
                                                    s.SubmitDate.Day == SearchText.Day);
                        break;
                    case "ProjectTitle":
                        tickets = tickets.Where(s => s.Project.ProjectTitle.Contains(SearchString));
                        break;
                    case "Desc":
                        tickets = tickets.Where(s => s.Description.Contains(SearchString));
                        break;
                }
            }

            return View(await tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Ticket ticket = await db.Tickets.FindAsync(id);

            var ticket = db.Tickets.Where(t => t.TicketId == id).Include(t => t.Department).Include(t => t.Employee).Include(t => t.Project);

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(await ticket.FirstOrDefaultAsync());
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            /*ViewBag.DeptRefId = new SelectList(db.Departments, "DeptId", "DeptName");
            ViewBag.EmpRefId = new SelectList(db.Employees, "EmpId", "EmpName");
            ViewBag.ProjectRefId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();*/

            ViewBag.DeptRefId = new SelectList(db.Departments, "DeptId", "DeptName");
            ViewBag.EmpRefId = new List<SelectListItem> { }; //new SelectList(db.Employees, "EmpId", "EmpName");
            ViewBag.ProjectRefId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();

        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TicketId,ProjectRefId,DeptRefId,EmpRefId,Description,SubmitDate,Status")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //ticket.Status = ticket.Status.DefaultIfEmpty<Ticket>;
                db.Tickets.Add(ticket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeptRefId = new SelectList(db.Departments, "DeptId", "DeptName", ticket.DeptRefId);

            int _deptId = ticket.DeptRefId; 
            var empList = db.Employees.Include(t => t.Department).Where(t => t.DeptRefId == _deptId);

            ViewBag.EmpRefId = new SelectList(empList, "EmpId", "EmpName", ticket.EmpRefId); // new SelectList(db.Employees, "EmpId", "EmpName", ticket.EmpRefId);
            ViewBag.ProjectRefId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", ticket.ProjectRefId);
            return View(ticket);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getEmployee(string DeptRefId)
        {
            //int val = Ticket.
            //IEnumerable<SelectListItem> empList = new List<SelectListItem>();
            //List<SelectListItem> empList = new List<SelectListItem>();
            

            //var empList = db.Employees.Include(t => t.Department);
            if (!string.IsNullOrEmpty(DeptRefId))
            {
                int _deptId = Convert.ToInt32(DeptRefId); //.ToInt32(DeptRefId);
                var empList = db.Employees.Include(t => t.Department).Where(t => t.DeptRefId == _deptId);

                var classesData = empList.Select(m => new SelectListItem()
                {
                    Text = m.EmpName,
                    Value = m.EmpId.ToString(),
                });
                return Json(classesData, JsonRequestBehavior.AllowGet);

                /*empList = (from e in db.Employees where e.DeptRefId == _deptId select e)
                           .AsEnumerable()
                           .Select(m => new SelectListItem() { Text = m.EmpName, Value = m.EmpId.ToString() });*/
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            //return new SelectList(empList, "Value", "Text", "EmpRefId");
            //return View((new SelectList(empList, "Value", "Text", "EmpRefId")).ToList());
            //ViewBag.EmpRefId = new SelectList(empList, "EmpId", "EmpName");
            //return View(new SelectList(empList, "EmpId", "EmpName", "EmpRefId"));
             
            //return View(new SelectList(empList, "EmpId", "EmpName"));
        }

        // GET: Tickets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            int _deptId = ticket.DeptRefId;
            var empList = db.Employees.Include(t => t.Department).Where(t => t.DeptRefId == _deptId);

            List<SelectListItem> status = new List<SelectListItem>() {
                                                new SelectListItem {    Text = "Open", Value = "O"  },
                                                new SelectListItem {    Text = "Close", Value = "C" } 
                                            };

            ViewBag.DeptRefId = new SelectList(db.Departments, "DeptId", "DeptName", ticket.DeptRefId);
            ViewBag.EmpRefId = new SelectList(empList, "EmpId", "EmpName", ticket.EmpRefId); //new SelectList(db.Employees, "EmpId", "EmpName", ticket.EmpRefId);
            ViewBag.ProjectRefId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", ticket.ProjectRefId);
            ViewBag.Status = new SelectList(status, "Value", "Text", ticket.Status);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TicketId,ProjectRefId,DeptRefId,EmpRefId,Description,SubmitDate,Status")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeptRefId = new SelectList(db.Departments, "DeptId", "DeptName", ticket.DeptRefId);

            int _deptId = ticket.DeptRefId;
            var empList = db.Employees.Include(t => t.Department).Where(t => t.DeptRefId == _deptId);

            ViewBag.EmpRefId = new SelectList(empList, "EmpId", "EmpName", ticket.EmpRefId); //new SelectList(db.Employees, "EmpId", "EmpName", ticket.EmpRefId);
            ViewBag.ProjectRefId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", ticket.ProjectRefId);

            List<SelectListItem> status = new List<SelectListItem>() {
                                                new SelectListItem {    Text = "Open", Value = "O"  },
                                                new SelectListItem {    Text = "Close", Value = "C" }
                                            };
            ViewBag.Status = new SelectList(status, "Value", "Text", ticket.Status);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Ticket ticket = await db.Tickets.FindAsync(id);
            var ticket = db.Tickets.Where(t => t.TicketId == id).Include(t => t.Department).Include(t => t.Employee).Include(t => t.Project);

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(await ticket.FirstOrDefaultAsync());
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ticket ticket = await db.Tickets.FindAsync(id);
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Chart()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Chart(string ShowType)
        {
            var tickets= db.Tickets.Include(t => t.Project)
                              .GroupBy(t => t.Project.ProjectTitle)
                              .Select(g => new Chart { ProjectTitle = g.Key, TotTickets = g.Count() });
            
            switch (ShowType)
            {
                case "DeptName":
                    tickets = db.Tickets.Include(t => t.Department)
                              .GroupBy(t => t.Department.DeptName)
                              .Select(g => new Chart { DeptName = g.Key, TotTickets = g.Count() });
                    break;
                case "EmpName":
                    tickets = db.Tickets.Include(t => t.Employee)
                              .GroupBy(t => t.Employee.EmpName)
                              .Select(g => new Chart { EmpName = g.Key, TotTickets = g.Count() });
                    break;
                case "Year":
                    tickets = db.Tickets
                              //.GroupBy(t=>t.SubmitDate.Year)
                              .GroupBy(t => new
                              {                                  
                                  Year = t.SubmitDate.Year
                              })
                              .Select(g => new Chart { Year = g.Key.Year, TotTickets = g.Count() });
                    break;
                case "Status":
                    tickets = db.Tickets
                              .GroupBy(t => t.Status)
                              .Select(g => new Chart { Status = (g.Key=="O"? "Open":"Close"), TotTickets = g.Count() });
                    break;
            }

            return View(await tickets.ToListAsync());
        }
    }
}
