using System;
using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Compute.Security
{
    public class Firewall
    {
        public List<Layer> Layers { get; private set; }
        public Dictionary<int, Layer> LayerDict { get; private set; }
        public int PacketIndex { get; private set; } = 0;
        public int LayerIndex { get; private set; } = 0;
        public int ElapsedPicoseconds { get; private set; } = 0;
        public int CaughtCount { get; private set; } = 0;
        public int SeverityCount { get; private set; } = 0;

        public Firewall(string[] layerStrings)
        {
            Layers = new List<Layer>();
            LayerDict = new Dictionary<int, Layer>();
            var indexLayerDict = new Dictionary<int, Layer>();
            var layers = new List<Layer>();
            var maxLayerId = 0;
            var zeroRangeLayer = new Layer(0);

            foreach (var layerString in layerStrings)
            {
                var parts = layerString.Split(' ');
                int layerId = int.Parse(parts[0].Replace(":", ""));
                if (maxLayerId < layerId)
                    maxLayerId = layerId;
                int range = int.Parse(parts[1]);
                layers.Add(new Layer(layerString));
                var newLayer = new Layer(range);
                if (!LayerDict.Keys.Contains(range))
                {
                    LayerDict.Add(range, newLayer);
                }
                if (!indexLayerDict.Keys.Contains(layerId))
                {
                    indexLayerDict.Add(layerId, LayerDict[range]);
                }
            }


            for (var i = 0; i <= maxLayerId; i++)
            {
                if (indexLayerDict.Keys.Contains(i))
                {
                    Layers.Add(indexLayerDict[i]);
                }
                else
                {
                    Layers.Add(zeroRangeLayer);
                }
            }
        }

        public void AdvanceOnePicosecond()
        {
            var packetLayer = Layers[PacketIndex];
            if (packetLayer.SecurityScanDepth == 1 && packetLayer.Range > 0)
            {
                CaughtCount++;
                //SeverityCount += packetLayer.LayerId * packetLayer.Range;
                SeverityCount += PacketIndex * packetLayer.Range;
            }

            foreach (var layer in LayerDict.Values)
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



        public bool AdvanceAllPicosecondsWithDelay(int delay)
        {
            ElapsedPicoseconds = 0;
            PacketIndex = 0;
            LayerIndex = 0;

            while (delay > ElapsedPicoseconds)
            {
                foreach (var layer in LayerDict.Values)
                {
                    layer.AdvanceOnePicosecond();
                }
                ElapsedPicoseconds++;
            }
            while (PacketIndex < Layers.Count)
            {
                var packetLayer = Layers[PacketIndex];
                if (packetLayer.SecurityScanDepth == 1 && packetLayer.Range > 0)
                {
                    return false;
                }

                foreach (var layer in LayerDict.Values)
                {
                    layer.AdvanceOnePicosecond();
                }

                PacketIndex++;
                ElapsedPicoseconds++;
            }
            return true;
        }

        public int[,] CachedValues { get; set; }
        public int CacheSize { get; set; }

        public bool ScannerCatchesPacket(int delay)
        {
            var packetIndex = 0;
            for (var i = delay; i < CacheSize; i++)
            {
                if (packetIndex == Layers.Count)
                {
                    return false;
                }
                if (CachedValues[i, packetIndex] == 1)
                    return true;
                else
                {
                    packetIndex++;
                }
            }
            return false;
        }

        public void CacheValues(int size)
        {
            CacheSize = size;
            int[,] cachedValues = new int[size, Layers.Count()];
            for (var i = 0; i < size; i++)
            {
                for (int j = 0; j < Layers.Count; j++)// layer in LayerDict.Values)
                {
                    cachedValues[i, j] = Layers[j].SecurityScanDepth;
                }
                foreach (var layer in LayerDict.Values)
                {
                    layer.AdvanceOnePicosecond();
                }
            }
            CachedValues = cachedValues;
        }
    }
}
