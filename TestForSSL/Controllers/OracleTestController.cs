using System;
using System.Configuration;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace TestForSSL.Controllers
{
	public class OracleTestController : Controller
	{
		public ActionResult TestConnection()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
			string connectionStringSSL = ConfigurationManager.ConnectionStrings["OracleDbContextSSL"].ConnectionString;
			string result = "";

			try
			{
				using (OracleConnection connection = new OracleConnection(connectionString))
				{
					connection.Open();
					result += "Successfully connected to Oracle Database!<br/>";

					using (OracleCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT SYSDATE FROM DUAL";
						object dateResult = command.ExecuteScalar();
						result += $"Current date from Oracle: {dateResult}";
						result += "<br/>";
					}
				}

				using (OracleConnection connection = new OracleConnection(connectionStringSSL))
				{
					connection.Open();
					result += "Successfully connected to Oracle Database with SSL!<br/>";

					using (OracleCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT SYSDATE FROM DUAL";
						object dateResult = command.ExecuteScalar();
						result += $"Current date from Oracle: {dateResult}";
					}
				}
			}
			catch (Exception ex)
			{
				result += $"Error: {ex.Message}";
			}

			ViewBag.Result = result;
			return View();
		}
	}
}