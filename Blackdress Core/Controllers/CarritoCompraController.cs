using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Blackdress_Core.Controllers
{
    public class CarritoCompraController : Controller
    {
        public IActionResult agregaralcarrito(string nombre_producto, string precio,
            string talla, string color, string medidasCorporales)
        {
            string nroOrden = DateTime.UtcNow.ToLongDateString();
            string estadoconfeccion = "Alistado en Recepcion";
            DateTime fechacotizacion = DateTime.Today;
            DateTime fechaestimada = fechacotizacion.AddDays(15);
            int precioEntero = int.Parse(precio);
            int subtotal = 0;
            string tabla = "";
            string compra = nombre_producto + "," + talla + "," + color + "," + precio + ".";
            string[] arreglo = compra.Split('.');

            for (int i = 0; i < arreglo.Length; i++)
            {
                tabla = tabla + "<tr>";
                string[] arreglo2 = arreglo[i].Split(',');
                for (int j = 0; j < arreglo2.Length; j++)
                {
                    tabla = tabla + "<td>" + arreglo2[j] + "</td>";
                    ViewBag.tabla = tabla;
                }
                ViewBag.tabla = tabla;

            }

            tabla = tabla + "</tr>";
            ViewBag.subtotal = subtotal;
            ViewBag.nroOrden = nroOrden;
            ViewBag.estadoconfeccion = estadoconfeccion;
            ViewBag.fechacotizacion = fechacotizacion;
            ViewBag.fechaestimada = fechaestimada;
            ViewBag.precio = precio;
            ViewBag.nombreproducto = nombre_producto;
            ViewBag.talla = talla;
            ViewBag.color = color;
            ViewBag.medidascorp = medidasCorporales;

            return View("Views/Home/carritodecompra.cshtml");
        }

        public IActionResult CrearBoleta(string ordendecompra, string estadoconfeccion, DateTime fechasolicitud, DateTime fechaestimada, int valorcotizacion, string nombre_producto,
            string tallas, string color, string medidascorporales, string rut, string email)
        {
            estadoconfeccion = "pendiente de envio a taller";
            ordendecompra = DateTime.Now.ToString() + rut + valorcotizacion.ToString();

            MySqlConnection conexion = new MySqlConnection("server=127.0.0.1; user=root; database=blackdress; password='';");
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion;
            conexion.Open();
            comando.CommandText = "insert into PedidosDeVestuario (NroOrdenDeCompra, medidasCorporales, Rut, EstadoDeConfeccion, FechaEstimada, ValorCotizacion, Email, FechaSolicitud," +
            " tallas, color, nombreproducto) values (@NroOrdenDeCompra, @medidasCorporales, @Rut, @EstadoDeConfeccion, @FechaEstimada, @ValorCotizacion, @Email, @FechaSolicitud," +
            " @tallas, @color, @nombreproducto)";

            string mensaje = "";

            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("NroOrdenDeCompra", ordendecompra);
            comando.Parameters.AddWithValue("medidasCorporales", medidascorporales);
            comando.Parameters.AddWithValue("Rut", rut);
            comando.Parameters.AddWithValue("EstadoDeConfeccion", estadoconfeccion);
            comando.Parameters.AddWithValue("FechaEstimada", fechaestimada);
            comando.Parameters.AddWithValue("ValorCotizacion", valorcotizacion);
            comando.Parameters.AddWithValue("Email", email);
            comando.Parameters.AddWithValue("FechaSolicitud", fechasolicitud);
            comando.Parameters.AddWithValue("tallas", tallas);
            comando.Parameters.AddWithValue("Color", color);
            comando.Parameters.AddWithValue("nombreproducto", nombre_producto);

            int nfilas = comando.ExecuteNonQuery();
            if (nfilas > 0)
            {
                mensaje = ("Datos guardados correctamente , que jebus te bendiga :)");
            }
            else
            {
                mensaje = "Ha ocurrido un error, favor de verificar datos ingresados";
            }

            comando.Dispose();

            /*SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into PedidosDeVestuario (NroOrdenDeCompra, medidasCorporales, Rut, EstadoDeConfeccion, FechaEstimada, ValorCotizacion, Email, FechaSolicitud, tallas, color, nombreproducto) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)";
            sentencia.Parameters.Add(new SqlParameter("@p1", ordendecompra));
            sentencia.Parameters.Add(new SqlParameter("@p2", medidascorporales));
            sentencia.Parameters.Add(new SqlParameter("@p3", rut));
            sentencia.Parameters.Add(new SqlParameter("@p4", estadoconfeccion));
            sentencia.Parameters.Add(new SqlParameter("@p5", fechaestimada));
            sentencia.Parameters.Add(new SqlParameter("@p6", valorcotizacion));
            sentencia.Parameters.Add(new SqlParameter("@p7", email));
            sentencia.Parameters.Add(new SqlParameter("@p8", fechasolicitud));
            sentencia.Parameters.Add(new SqlParameter("@p9", tallas));
            sentencia.Parameters.Add(new SqlParameter("@p10", color));
            sentencia.Parameters.Add(new SqlParameter("@p11", nombre_producto));
            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result > 0)
            {
                mensaje = "Registro guardado";
            }
            else
            {
                mensaje = "Algo anda mal";
            }*/
            ViewBag.mensaje = mensaje;
            ViewBag.nroOrden = ordendecompra;
            ViewBag.estadoconfeccion = estadoconfeccion;
            ViewBag.fechacotizacion = fechasolicitud;
            ViewBag.fechaestimada = fechaestimada;
            ViewBag.precio = valorcotizacion;
            ViewBag.nombreproducto = nombre_producto;
            ViewBag.talla = tallas;
            ViewBag.color = color;
            ViewBag.medidascorp = medidascorporales;
            ViewBag.rut = rut;
            ViewBag.email = email;
            return View("Views/Home/BoletaCliente.cshtml");
        }

        public IActionResult MostrarConfecciones()
        {
            /*SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var sentencia = new SqlCommand();
            SqlDataReader dr;
            sentencia.Connection = con;
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "select * from PedidosDeVestuario";
            con.Open();
            dr = sentencia.ExecuteReader();
            var pedidos = "";
            while (dr.Read())
            {
                pedidos = pedidos + "<tr><td>" + dr["NroOrdenDeCompra"].ToString() + "</td><td>" + dr["medidasCorporales"].ToString() + "</td><td>" + dr["Rut"].ToString() + "</td><td>" +
                dr["EstadoDeConfeccion"].ToString() + "</td><td>" + dr["FechaEstimada"].ToString() + "</td><td>" + dr["ValorCotizacion"].ToString() + "</td><td>" + dr["Email"].ToString() +
                "</td><td>" + dr["FechaSolicitud"].ToString() + "</td><td>" + dr["tallas"].ToString() + "</td><td>" + dr["color"].ToString() + "</td><td>" + dr["nombreproducto"].ToString() + "</td></tr>";
            }*/

            MySqlConnection conexion;
            conexion = new MySqlConnection("Server = 127.0.0.1; Database = blackdress; Uid = root; Pwd =;");
            conexion.Open();
            string query = "SELECT * FROM PedidosDeVestuario";
            MySqlCommand sql = new MySqlCommand(query, conexion);
            MySqlDataReader reader = sql.ExecuteReader();
            var pedidos = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    pedidos = "";
                    string NroOrdenDeCompra = reader.GetString("NroOrdenDeCompra");
                    string medidasCorporales = reader.GetString("medidasCorporales");
                    string Rut = reader.GetString("Rut");
                    string EstadoDeConfeccion = reader.GetString("EstadoDeConfeccion");
                    string FechaEstimada = reader.GetString("FechaEstimada");
                    string ValorCotizacion = reader.GetString("ValorCotizacion");
                    string Email = reader.GetString("Email");
                    string FechaSolicitud = reader.GetString("FechaSolicitud");
                    string tallas = reader.GetString("tallas");
                    string color = reader.GetString("color");
                    string nombreproducto = reader.GetString("nombreproducto");
                    pedidos += "<tr><td>" + NroOrdenDeCompra + "</td><td>" + medidasCorporales + "</td><td>" + Rut + "</td><td>"+ EstadoDeConfeccion + "</td><td>" + FechaEstimada + "</td><td>" +
                    ValorCotizacion+ "</td><td>" + Email + "</td><td>" + FechaSolicitud + "</td><td>" + tallas + "</td><td>" + color + "</td><td>" + nombreproducto + "</td></tr>";

                }
            }

            
            ViewBag.pedidos = pedidos;
            return View("/Views/Home/EstadoConfecciones.cshtml");
        }
        public IActionResult modificarPedido(string numerodeorden, string estadodeconfeccion)
        {
            /*SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "update PedidosDeVestuario set EstadoDeConfeccion = @estadoconf where NroOrdenDeCompra= @nroroden";
            con.Open();
            sentencia.Parameters.Add(new SqlParameter("@estadoconf", estadodeconfeccion));
            sentencia.Parameters.Add(new SqlParameter("@nroroden", numerodeorden));
            sentencia.Connection = con;
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result > 0)
            {
                mensaje = "Pedido Modificado";
            }
            else { mensaje = "No se ha podido modificar el pedido"; }*/

            MySqlConnection conexion = new MySqlConnection("server=127.0.0.1; user=root; database=blackdress; password='';");
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "update PedidosDeVestuario set EstadoDeConfeccion = @estadoconf where NroOrdenDeCompra= @nroroden";
            conexion.Open();
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("EstadoDeConfeccion", estadodeconfeccion);
            comando.Parameters.AddWithValue("NroOrdenDeCompra", numerodeorden);
            int nfilas = comando.ExecuteNonQuery();
            var mensaje = "";
            if (nfilas > 0)
            {
                mensaje = "Datos guardados correctamente , que jebus te bendiga ;)";
            }
            comando.Dispose();
            
            ViewBag.mensaje = mensaje;
            return View("Views/Home/EstadoConfecciones.cshtml");
        }

        public IActionResult consultaModificacion(string numerodeorden)
        {
            /*SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var sentencia = new SqlCommand();
            SqlDataReader dr;
            sentencia.Connection = con;
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "select * from PedidosDeVestuario where " +
                "NroOrdenDeCompra = '" + numerodeorden + "'";
            con.Open();
            dr = sentencia.ExecuteReader();
            var pedidos = "";
            while (dr.Read())
            {
                pedidos = pedidos + "<tr><td>" + dr["NroOrdenDeCompra"].ToString() + "</td><td>" + dr["medidasCorporales"].ToString() + "</td><td>" + dr["Rut"].ToString() + "</td><td>" +
                dr["EstadoDeConfeccion"].ToString() + "</td><td>" + dr["FechaEstimada"].ToString() + "</td><td>" + dr["ValorCotizacion"].ToString() + "</td><td>" + dr["Email"].ToString() +
                "</td><td>" + dr["FechaSolicitud"].ToString() + "</td><td>" + dr["tallas"].ToString() + "</td><td>" + dr["color"].ToString() + "</td><td>" + dr["nombreproducto"].ToString() + "</td></tr>";
            }*/

            MySqlConnection conexion;
            conexion = new MySqlConnection("Server = 127.0.0.1; Database = blackdress; Uid = root; Pwd =;");
            conexion.Open();
            string query = "SELECT * FROM PedidosDeVestuario where NroOrdenDeCompra = '" + numerodeorden + "'";
            MySqlCommand sql = new MySqlCommand(query, conexion);
            MySqlDataReader reader = sql.ExecuteReader();
            var pedidoBuscado = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string NroOrdenDeCompra = reader.GetString("NroOrdenDeCompra");
                    string medidasCorporales = reader.GetString("medidasCorporales");
                    string Rut = reader.GetString("Rut");
                    string EstadoDeConfeccion = reader.GetString("EstadoDeConfeccion");
                    string FechaEstimada = reader.GetString("FechaEstimada");
                    string ValorCotizacion = reader.GetString("ValorCotizacion");
                    string Email = reader.GetString("Email");
                    string FechaSolicitud = reader.GetString("FechaSolicitud");
                    string tallas = reader.GetString("tallas");
                    string color = reader.GetString("color");
                    string nombreproducto = reader.GetString("nombreproducto");
                    pedidoBuscado += "<tr><td>" + NroOrdenDeCompra + "</td><td>" + medidasCorporales + "</td><td>" + Rut + "</td><td>" + EstadoDeConfeccion + "</td><td>" + FechaEstimada + "</td><td>" +
                    ValorCotizacion + "</td><td>" + Email + "</td><td>" + FechaSolicitud + "</td><td>" + tallas + "</td><td>" + color + "</td><td>" + nombreproducto + "</td></tr>";

                }
            }
            
            ViewBag.pedidoBuscado = pedidoBuscado;

            return View("Views/Home/ComprobaciondeModificacion.cshtml");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
