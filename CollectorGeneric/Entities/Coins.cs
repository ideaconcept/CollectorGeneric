namespace CollectorGeneric.Entities
{
    public class Coins : Numismatics
    {
        public string? Material { get; set; }
        public float Diameter { get; set; }
        public float Weight { get; set; }

        public override string ToString() => base.ToString() + $"Material: {Material}, Diameter: {Diameter}, Weight: {Weight} g";
    }
}
