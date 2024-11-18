using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using pelis.Data;
using System.Data;


namespace pelis.Controllers
{
    public class ReporteController : Controller
    {
        private readonly pelisContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReporteController(pelisContext _context, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = _context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using var report = new LocalReport();
            var dt = new DataTable();
            report.ReportPath = $"{this._webHostEnvironment.WebRootPath}\\Reports\\rptReporte.rdlc";

            dt = GetProducts();
            report.DataSources.Add(new ReportDataSource("dsProduct", dt));
            var parameters = new[] { new ReportParameter("param1", "RDLC Sub-Report Example") };

            report.SetParameters(parameters);

            var pdf = report.Render(renderFormat);
            return File(pdf, mimetype, "report." + extension);


        }

        public DataTable GetProducts()
        {
            var productos = _dbcontext.Productos.Include(m => m.CategoriaProductos).AsNoTracking();
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Stock");
            dt.Columns.Add("Name");
            DataRow row;

            foreach (var producto in productos)
            {
                // Crear una nueva fila y asignar valores de la entidad producto
                row = dt.NewRow();
                row["Id"] = producto.Id;
                row["Nombre"] = producto.Nombre;
                row["Descripción"] = producto.Descripción;
                row["Precio"] = producto.Precio;
                row["Stock"] = producto.Stock;
                row["Name"] = producto.CategoriaProductos.Name;
                /*foreach (var NombreCategoria in _dbcontext.CategoriaProductos)
                {
                    if (NombreCategoria.Id == producto.CategoriaId)
                    {
                        row["Name"] = NombreCategoria.Name;
                    }
                }*/
               
                // Agregar la fila al DataTable
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
