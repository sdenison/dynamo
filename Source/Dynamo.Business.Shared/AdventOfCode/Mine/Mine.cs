using Dynamo.Business.Shared.AdventOfCode.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Mine
{
    public class Mine
    {
        public Point[,] Points { get; }
        public List<Track> Tracks { get; }

        public Mine(string[] mineLayout)
        {
            var xLength = mineLayout[0].Length;
            var yLength = mineLayout.Length;
            Points = new Point[xLength, yLength];
            for (var y = 0; y < yLength; y++)
                for (var x = 0; x < xLength; x++)
                    Points[x, y] = new Point(x, y, mineLayout[y][x]);

            Tracks = new List<Track>();
            for (var y = 0; y < yLength; y++)
                for (var x = 0; x < xLength; x++)
                {
                    //Find all the starting points. They'll be top left forward slashes.
                    if (Points[x, y].PointChar == '/')
                    {
                        var alreadyExists = false;
                        foreach (var track in Tracks)
                        {
                            foreach (var section in track.Sections)
                            {
                                if (section.Point == Points[x, y])
                                    alreadyExists = true;
                            }
                        }
                        if (!alreadyExists)
                            Tracks.Add(new Track(Points[x, y], Points));
                    }
                }

            for (var i = 0; i < Tracks.Count; i++)
            {
                for (var j = i + 1; j < Tracks.Count; j++)
                {
                    foreach (var sectionA in Tracks[i].Sections.Where(x => x.Type == TrackSectionType.Intersection))
                    {
                        foreach (var sectionB in Tracks[j].Sections.Where(x => x.Type == TrackSectionType.Intersection))
                        {
                            if (sectionA.Point == sectionB.Point)
                            {
                                sectionA.IntersectionTrack = Tracks[j];
                                sectionB.IntersectionTrack = Tracks[i];
                                Tracks[i].Intersections.Add(sectionA.Point, Tracks[j]);
                                Tracks[j].Intersections.Add(sectionA.Point, Tracks[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}
