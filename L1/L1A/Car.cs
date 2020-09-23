using System.Runtime.Serialization;

namespace L1A
{
    [DataContract]
    class Car
    {
        [DataMember(Name = "mileage")]
        public double Mileage { get; set; }
        [DataMember(Name = "year")]
        public int Year { get; set; }
        [DataMember(Name = "brand")]
        public string Brand { get; set; }
    }
}
