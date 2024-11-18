namespace pelis.Models
{
    public class MediosPago
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descrip { get; set; }
        public bool Estado { get; set; }
        public int Comision { get; set; }
    }
}
