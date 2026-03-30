using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD02.Models
{
    public class Laptop : Equipment
    {
        public int RamGigabytes { get; set; }
        public string Processor { get; set; }
        public Laptop(string name, int ram, string processor) : base(name) { RamGigabytes = ram; Processor = processor; }
    }

    public class Projector : Equipment
    {
        public int ResolutionX { get; set; }
        public int Lumens { get; set; }
        public Projector(string name, int resX, int lumens) : base(name) { ResolutionX = resX; Lumens = lumens; }
    }

    public class Camera : Equipment
    {
        public int Megapixels { get; set; }
        public string MountType { get; set; }
        public Camera(string name, int megapixels, string mount) : base(name) { Megapixels = megapixels; MountType = mount; }
    }
}
