namespace CollectorGeneric.Data.Entities
{
    public class Numismatics : EntityBase
    {
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public float Denomination { get; set; }
        public string? Currency { get; set; }
        public float YearOfRelease { get; set; }

        public override string ToString() => $"Id: {Id}, Symbol: {Symbol}, Name: {Name}, Denominaton: {Denomination}, Currency: {Currency}, Year of release: {YearOfRelease} ";
    }
}
