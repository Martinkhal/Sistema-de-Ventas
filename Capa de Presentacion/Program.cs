﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.      
        /// </summary>
         public static int Evento;

        //Datos del Producto
         public static int IdProducto;
         public static String Descripcion;
         public static String Marca;
         public static Int32 Stock;
         public static Int32 StockMinimo;
         public static Decimal PrecioVenta;
 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmListadoProductos());      
        }
    }
}
