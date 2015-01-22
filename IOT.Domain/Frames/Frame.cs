using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Domain.Frames
{
    public class Frame
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<FrameElement> Elements { get; protected set; }

        public Frame()
        {
            Elements = new List<FrameElement>();
        }

        public void AddElement(FrameElement f)
        {
            Elements.Add(f);
        }

        public void RemoveElement(FrameElement f)
        {
            Elements.Remove(f);
        }


    }
}
