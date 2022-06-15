using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
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
            {                return View("/Views/Shared/Error.cshtml");
            }
        }


        public ActionResult login(string rut, string pass)
        {
            MySqlConnection con = new MySqlConnection("Server = 127.0.0.1; Database = blackdress; Uid = root; Pwd =;");
            con.Open();
            var sentencia = new MySqlCommand();

            sentencia.Connection = con;
            sentencia.CommandText = "select * from Usuarios";
            sentencia.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dr = sentencia.ExecuteReader();
           

            var mensaje = "";
            while (dr.Read())
            {
                if (dr["rut"].ToString() == rut)
                {
                    if (dr["clave"].ToString() == pass)
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

        public ActionResult productos()
        {

             MySqlConnection conexion;
            conexion = new MySqlConnection("Server = 127.0.0.1; Database = blackdress; Uid = root; Pwd =;");
            conexion.Open();
            string query = "SELECT * FROM productos";
            MySqlCommand sql = new MySqlCommand(query, conexion);
            MySqlDataReader reader = sql.ExecuteReader();
            var producto = "";
          
                while (reader.Read())
                {
                  
                    string imagen= reader.GetString("imagen");
                string precio = reader.GetString("precio");
                string id = reader.GetString("id_producto");
                string nombre = reader.GetString("nombre_producto");

                producto += " <div class='product text-center col-3 '> ";
                producto += " <form method='GET' action='sproductoU.php'>";
                producto += "<img class= img-fluid mb-3  style=' width: 350px; height: 250px;' src= " + imagen + " alt=>";
                producto += " <div class='star'>";
                producto += "   <i class='fas fa-star'></i>";
                producto += "    <i class='fas fa-star'></i>";
                producto += "    <i class='fas fa-star'></i>";
                producto += "    <i class='fas fa-star'></i>";
                producto += "    <i class='fas fa-star'></i>";
                producto += " </div>";
                producto += " <input type='hidden' name='ID' value= " + id + ">";
                producto += " <h5 class='p.name'> " + nombre + "</h5>";
                producto += " <h4 class='p-price'> " + precio + "</h4>";
                producto += "   <input type='submit' name='producto' value='comprar' class='buy-btn'>";
                producto += "</div>";
                producto += "</form>";

            }
        
            ViewBag.producto = producto;
          
                return View("Views/Home/Productos.cshtml");
            
          
        }


        public ActionResult register(string rut, string user, string pass, string mail, string apellido)
        {

            string tipoU = "Cliente";
            MySqlConnection con = new MySqlConnection("server=127.0.0.1; user=root; database=blackdress; password='';");
            con.Open();
            var sentencia = new SqlCommand();
            MySqlDataReader dr;
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
            else if (rut[9].ToString() == "K" && digito == 10)
            {
                digito = char.Parse("K");
                sw = "OK";
            }
            else if (rut[9].ToString() == "0" && digito == 11)
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
            if (sw == "OK")
            {
                sentencia.Parameters.Add(new MySqlParameter("@prut", rut));
                sentencia.Parameters.Add(new MySqlParameter("@puser", user));
                sentencia.Parameters.Add(new MySqlParameter("@pnameape", apellido));
                sentencia.Parameters.Add(new MySqlParameter("@ppass", pass));
                sentencia.Parameters.Add(new MySqlParameter("@tipou", tipoU));
                sentencia.Parameters.Add(new MySqlParameter("@pmail", mail));
      
                var result = sentencia.ExecuteNonQuery();


                if (result > 0)
                {
                    mensaje = "Registro hecho" + state;

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
