using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Firewall
    {
        public List<Layer> Layers { get; private set; }
        public int PacketIndex { get; private set; } = 0;
        public int CaughtCount { get; private set; } = 0;
        public int SeverityCount { get; private set; } = 0;

        public Firewall(string[] layerStrings)
        {
            Layers = new List<Layer>();
            var layers = new List<Layer>();
            foreach (var layerString in layerStrings)
            {
                layers.Add(new Layer(layerString));
            }
            for (var i = 0; i <= layers.Last().LayerId; i++)
            {
                if (layers.Exists(x => x.LayerId == i))
                {
                    Layers.Add(layers.Single(x => x.LayerId == i));
                }
                else
                {
                    Layers.Add(new Layer(i, 0));
                }
            }
        }

        public void AdvanceOnePicosecond()
        {
            var packetLayer = Layers[PacketIndex];
            if (packetLayer.SecurityScanDepth == 1 && packetLayer.Range > 0)
            {
                CaughtCount++;
                SeverityCount += packetLayer.LayerId * packetLayer.Range;
            }
            foreach (var layer in Layers)
            {
                layer.AdvanceOnePicosecond();
            }
            PacketIndex++;
        }

        public void AdvanceAllPicoseconds()
        {
            for (var i = 0; i < Layers.Count; i++)
            {
                AdvanceOnePicosecond();
            }
        }
    }
}
