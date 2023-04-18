namespace CollectorGeneric.Data.Entities
{
    public class Banknotes : Numismatics
    {
        public float? Length { get; set; }
        public float? Width { get; set; }
        public string? Watermark { get; set; }

        public override string ToString() => base.ToString() + $"Length: {Length} mm, Width: {Width} mm, Watermark: {Watermark}";
    }
}
