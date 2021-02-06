/* Group By Project */
select count(*) TotTickets, P.ProjectTitle FROM dbo.Ticket T
INNER JOIN dbo.Project P ON T.ProjectRefId=P.ProjectId
group by T.ProjectRefId, P.ProjectTitle;

/* Group By Department */
select count(*) TotTickets, D.DeptName FROM dbo.Ticket T
INNER JOIN dbo.Department D ON T.DeptRefId=D.DeptId
group by T.DeptRefId, D.DeptName;

/* Group By Employee */
select count(*) TotTickets, E.EmpName FROM dbo.Ticket T
INNER JOIN dbo.Employee E ON T.EmpRefId=E.EmpId
group by T.EmpRefId, E.EmpName;


/* Group By Status */
select count(*) TotTickets, T.Status FROM dbo.Ticket T
group by T.Status;

/* Group By Year */
select count(*) TotTickets, YEAR(T.SubmitDate) Year FROM dbo.Ticket T
group by YEAR(T.SubmitDate);


select * from dbo.Employee where DeptRefId=103;