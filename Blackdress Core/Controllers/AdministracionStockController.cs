using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
namespace Blackdress_Core.Controllers

{
    public class AdministracionStockController : Controller
    {
        public IActionResult cargaGD(string Nombreproducto, string color,
            string descripcion, string proveedores, int cantidad)

        {

            int precioUnitario = 0;

            string rut = "";
            if (proveedores == "Goth and Telas")
            {
                rut = "66521730-1";
                if (Nombreproducto == "SATIN RASO")
                {
                    precioUnitario = 1780;
                }
                else if (Nombreproducto == "CUADRILLE")
                {
                    precioUnitario = 1636;
                }
                else if (Nombreproducto == "LATEX")
                {
                    precioUnitario = 7852;
                }
                else if (Nombreproducto == "BISTRECH")
                {
                    precioUnitario = 2474;
                }
                else if (Nombreproducto == "LONA")
                {
                    precioUnitario = 3259;
                }
                else if (Nombreproducto == "LYCRA DUPONT")
                {
                    precioUnitario = 5010;
                }
                else if (Nombreproducto == "POLYESTER")
                {
                    precioUnitario = 2195;
                }
                else if (Nombreproducto == "LINO")
                {
                    precioUnitario = 4478;
                }
                else if (Nombreproducto == "FRANELA")
                {
                    precioUnitario = 2743;
                }
                else if (Nombreproducto == "TEJIDO TR")
                {
                    precioUnitario = 6477;
                }
            }
            if (proveedores == "Telas Maricio")
            {
                rut = "72004089-4";
                if (Nombreproducto == "SATIN RASO")
                {
                    precioUnitario = 1840;
                }
                else if (Nombreproducto == "CUADRILLE")
                {
                    precioUnitario = 1416;
                }
                else if (Nombreproducto == "LATEX")
                {
                    precioUnitario = 8540;
                }
                else if (Nombreproducto == "BISTRECH")
                {
                    precioUnitario = 3571;
                }
                else if (Nombreproducto == "LONA")
                {
                    precioUnitario = 2746;
                }
                else if (Nombreproducto == "LYCRA DUPONT")
                {
                    precioUnitario = 5874;
                }
                else if (Nombreproducto == "POLYESTER")
                {
                    precioUnitario = 2357;
                }
                else if (Nombreproducto == "LINO")
                {
                    precioUnitario = 4080;
                }
                else if (Nombreproducto == "FRANELA")
                {
                    precioUnitario = 2897;
                }
                else if (Nombreproducto == "TEJIDO TR")
                {
                    precioUnitario = 5896;
                }
            }
            if (proveedores == "Supertelas")
            {
                rut = "72004089-4";
                if (Nombreproducto == "SATIN RASO")
                {
                    precioUnitario = 2840;
                }
                else if (Nombreproducto == "CUADRILLE")
                {
                    precioUnitario = 3416;
                }
                else if (Nombreproducto == "LATEX")
                {
                    precioUnitario = 10540;
                }
                else if (Nombreproducto == "BISTRECH")
                {
                    precioUnitario = 5571;
                }
                else if (Nombreproducto == "LONA")
                {
                    precioUnitario = 4746;
                }
                else if (Nombreproducto == "LYCRA DUPONT")
                {
                    precioUnitario = 8874;
                }
                else if (Nombreproducto == "POLYESTER")
                {
                    precioUnitario = 4357;
                }
                else if (Nombreproducto == "LINO")
                {
                    precioUnitario = 7080;
                }
                else if (Nombreproducto == "FRANELA")
                {
                    precioUnitario = 4897;
                }
                else if (Nombreproducto == "TEJIDO TR")
                {
                    precioUnitario = 10896;
                }
            }
            double ivaBruto = precioUnitario * 0.19;
            int iva = (int)Math.Round(ivaBruto);
            int ivatotal = (iva * cantidad);
            int total = (precioUnitario * cantidad) + ivatotal;
            string gd = (DateTime.Now.ToString() + total + rut).ToString();
            ViewBag.precioUnitario = precioUnitario;
            ViewBag.iva = ivatotal;
            ViewBag.rut = rut;
            ViewBag.total = total;
            ViewBag.nombreprov = proveedores;
            ViewBag.color = color;
            ViewBag.descripcion = descripcion;
            ViewBag.nombreproducto = Nombreproducto;
            ViewBag.gd = gd;
            ViewBag.cantidad = cantidad;
            return View("Views/Home/Pedidosproveedores.cshtml");
        }
        public IActionResult GenPedido(string rutprov, string proveedores, string descripcion, int precioU, int cantidad, int iva, int totalapagar, string nombreprod, string color)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            string nrodeguia = DateTime.Now.ToString("O");
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into Compras (rut_proveedor, Nombre, Descricion, PrecioUnitario, cantidad, Iva, TotalAPagar, NroGuiaDeDespacho, Nombre_producto, Color) values (@p1, @p2, @p3,@p4,@p5, @p6, @p7, @p8, @p9, @p10)";
            sentencia.Parameters.Add(new SqlParameter("@p1", rutprov));
            sentencia.Parameters.Add(new SqlParameter("@p2", proveedores));
            sentencia.Parameters.Add(new SqlParameter("@p3", descripcion));
            sentencia.Parameters.Add(new SqlParameter("@p4", precioU));
            sentencia.Parameters.Add(new SqlParameter("@p5", cantidad));
            sentencia.Parameters.Add(new SqlParameter("@p6", iva));
            sentencia.Parameters.Add(new SqlParameter("@p7", totalapagar));
            sentencia.Parameters.Add(new SqlParameter("@p8", nrodeguia));
            sentencia.Parameters.Add(new SqlParameter("@p9", nombreprod));
            sentencia.Parameters.Add(new SqlParameter("@p10", color));
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
            }
            ViewBag.precioUnitario = precioU;
            ViewBag.iva = iva;
            ViewBag.rut = rutprov;
            ViewBag.total = totalapagar;
            ViewBag.nombreprov = proveedores;
            ViewBag.color = color;
            ViewBag.descripcion = descripcion;
            ViewBag.nombreproducto = nombreprod;
            ViewBag.gd = nrodeguia;
            ViewBag.cantidad = cantidad;
            return View("Views/Home/Guiadedespacho.cshtml");
        }

        public IActionResult registrarPedido(string nrodeguia, string rutproveedor, string nombreproveedor, string descripcion, string nombreprod,
            string color, int cantidad)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into GuiasdeDespacho (rut_proveedor, Nombre, Descripcion, cantidad, NroGuiadedespacho, Nombre_producto, Color) values (@p1, @p2, @p3,@p4,@p5, @p6, @p7)";
            sentencia.Parameters.Add(new SqlParameter("@p1", rutproveedor));
            sentencia.Parameters.Add(new SqlParameter("@p2", nombreproveedor));
            sentencia.Parameters.Add(new SqlParameter("@p3", descripcion));
            sentencia.Parameters.Add(new SqlParameter("@p4", cantidad));
            sentencia.Parameters.Add(new SqlParameter("@p5", nrodeguia));
            sentencia.Parameters.Add(new SqlParameter("@p6", nombreprod));
            sentencia.Parameters.Add(new SqlParameter("@p7", color));
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
            }
            ViewBag.nombreproducto = nombreprod;
            ViewBag.cantidad = cantidad;
            ViewBag.color = color;
            return View("Views/Home/Bodega.cshtml");
        }

        public IActionResult updateStock(string nombreprod, string color, int cantidad)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var sentencia = new SqlCommand();
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "insert into Stock (nombre_producto, color, cantidad) values (@p1,@p2,@p3)";
            sentencia.Parameters.Add(new SqlParameter("@p1", nombreprod));
            sentencia.Parameters.Add(new SqlParameter("@p2", color));
            sentencia.Parameters.Add(new SqlParameter("@p3", cantidad));
            sentencia.Connection = con;
            con.Open();
            var result = sentencia.ExecuteNonQuery();
            var mensaje = "";
            if (result > 0)
            {
                mensaje = "Registro guardado";
            }
            else
            {
                mensaje = "Algo anda mal";
            }

            return View("/Views/Home/Bodega.cshtml");
        }

        public IActionResult MostrarStock()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlackDressBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var sentencia = new SqlCommand();
            SqlDataReader dr;
            sentencia.Connection = con;
            sentencia.CommandType = System.Data.CommandType.Text;
            sentencia.CommandText = "select * from Stock";
            con.Open();
            dr = sentencia.ExecuteReader();
            var pedidos = "";
            while (dr.Read())
            {
                pedidos = pedidos + "<tr><td>" + dr["nombre_producto"] + "</td><td>" + dr["Color"] + "</td><td>" + dr["cantidad"] + "</td></tr>";
            }
            ViewBag.stock = pedidos;
            return View("/Views/Home/Bodega.cshtml");
        }
    }
}
