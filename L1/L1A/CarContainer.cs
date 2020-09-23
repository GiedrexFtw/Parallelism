using System.Runtime.Serialization;

namespace L1A
{
    [DataContract]
    class CarContainer
    {
        [DataMember(Name = "cars")]
        public Car[] Cars { get; set; }

    }
}
