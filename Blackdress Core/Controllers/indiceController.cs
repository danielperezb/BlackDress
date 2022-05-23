using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Blackdress_Core.Controllers
{
	public class indiceController : Controller
	{


		public IActionResult loggeo(string rut, string pass)
		{
			try
			{
				var mensaje = "";
				SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
				con.Open();
				System.Text.StringBuilder sel = new System.Text.StringBuilder();
				sel.Append("SELECT COUNT (*) FROM Usuarios");
				sel.Append("where Rut = @Rut and Clave = @Clave");
				var sentencia = new SqlCommand(sel.ToString(), con);
				sentencia.Parameters.Add("@Rut", System.Data.SqlDbType.VarChar, 10);
				sentencia.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar, 8);
				sentencia.Parameters["@Rut"].Value = rut;
				sentencia.Parameters["@clave"].Value = pass;
				var result = sentencia.ExecuteNonQuery();
				if (result > 0)
				{
					mensaje = "Login exitoso";
				}
				else
				{
					mensaje = "Algo anda mal";
				}
				ViewBag.mensaje = mensaje;
				return View("Views/Home/Index.cshtml");
			}
			catch (Exception e)
			{
				return View("/Views/Shared/Error.cshtml");
			}
		}


		public ActionResult login(string rut, string pass)
		{
			SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			con.Open();
			var sentencia = new SqlCommand();
			SqlDataReader dr = sentencia.ExecuteReader();
			sentencia.Connection = con;
			sentencia.CommandText = "select * from Usuarios";
			sentencia.CommandType = System.Data.CommandType.Text;

			var mensaje = "";
			while (dr.Read())
			{
				if (dr["rut"].ToString() == rut)
				{
					if (dr["pass"].ToString() == pass)
					{
						mensaje = "ACCESO_OK";
					}
				}
			}
			if (mensaje == "ACCESO_OK")
			{
				return View("Views/Home/Index.cshtml");
			}
			else
			{
				ViewBag.mensaje = mensaje;
				return View();
			}
		}



		public ActionResult register(string rut, string user, string pass, string mail, string apellido)
		{
			string tipoU = "Cliente";
			SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
				con.Open();
				var sentencia = new SqlCommand();
				SqlDataReader dr;
				sentencia.CommandType = System.Data.CommandType.Text;
				sentencia.CommandText = "insert into Usuarios (Rut,NombreUsuario,NombreYapellido,contraseña,TipoUsuario,email) values (@prut,@puser,@pnameape,@ppass,@tipou,@pmail)";
				if (rut.Length == 9)
				{
					rut = "0" + rut;
				}

				string[] arrestring = new string[10];
				for (int i = 0; i < 10; i++)
				{
					arrestring[i] = rut[i].ToString();
				}
				int[] arreint = new int[8];
				for (int i = 0; i < 8; i++)
				{
					arreint[i] = int.Parse(arrestring[i]);
				}
				double suma = 0, divisiondecimal = 0;

				suma = (3 * arreint[0] + 2 * arreint[1] + 7 * arreint[2] +
				6 * arreint[3] + 5 * arreint[4] + 4 * arreint[5] + 3 *
				arreint[6] + 2 * arreint[7]);

				int divisionentero = 0;
				divisiondecimal = suma / 11;
				divisionentero = (int)divisiondecimal;

				double valordecimal = 0;
				valordecimal = divisiondecimal - divisionentero;

				double resta11 = 0;
				resta11 = (11 - (11 * (valordecimal)));



				int digito = 0;
				digito = (int)Math.Round(resta11);


				string sw = "";
			if (arrestring[9].ToString() == digito.ToString())
			{
				sw = "OK";
			}
			else if (rut[9].ToString() == "K" && digito==10)
			{
				digito = char.Parse("K");
				sw = "OK";
			}
			else if (rut[9].ToString() == "0" && digito==11)
			{ 
						digito = char.Parse("0");
						sw = "OK";
			}
			else
			{
				sw = "ERROR";

			}
			var state = "";
			var mensaje = "";
			if (sw=="OK")
			{ 
					sentencia.Parameters.Add(new SqlParameter("@prut", rut));
					sentencia.Parameters.Add(new SqlParameter("@puser", user));
					sentencia.Parameters.Add(new SqlParameter("@pnameape", apellido));
					sentencia.Parameters.Add(new SqlParameter("@ppass", pass));
					sentencia.Parameters.Add(new SqlParameter("@tipou", tipoU));
					sentencia.Parameters.Add(new SqlParameter("@pmail", mail));
					sentencia.Connection = con;
					var result = sentencia.ExecuteNonQuery();
					
					
					if (result > 0)
					{
						mensaje = "Registro hecho"+state;

					}
					else
					{
						mensaje = "Error";
						state = "Rut incorrecto";
					}
					con.Close();
				
			}
			ViewBag.state = state;
			ViewBag.mensaje = mensaje;
			ViewBag.sw = sw;
			return View("/Views/Home/registrarse.cshtml");
		}
	}
}
