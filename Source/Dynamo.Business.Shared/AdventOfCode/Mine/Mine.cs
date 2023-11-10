using Dynamo.Business.Shared.AdventOfCode.Navigation;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Sockets;
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
                    Points[x, y] = new Point(x, y, mineLayout[y].PadLeft(xLength)[x]);

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
                                sectionA.IntersectingTrackSection = Tracks[j].Sections.Single(x => x.Point == sectionA.Point);
                                sectionB.IntersectingTrackSection = Tracks[i].Sections.Single(x => x.Point == sectionB.Point);
                                Tracks[i].Intersections.Add(sectionA.Point, Tracks[j]);
                                Tracks[j].Intersections.Add(sectionA.Point, Tracks[i]);
                            }
                        }
                    }
                }
            }
        }

        public List<Cart> GetCarts()
        {
            var carts = new List<Cart>();
            foreach (var track in Tracks)
            {
                carts.AddRange(track.Carts);
            }
            return carts.OrderBy(c => c.Point.Y).ThenBy(c => c.Point.X).ToList();
        }

        public Point GetFirstCollision()
        {
            Point returnValue = null;
            while (true)
            {
                returnValue = Step();
                if (returnValue != null)
                    return returnValue;
            }
        }

        public Point Step()
        {
            var carts = GetCarts();
            foreach (var cart in carts)
            {
                cart.Step();
                var collision = GetCollision();
                if (collision != null)
                    return collision;
            }

            return null;
        }

        public Point GetCollision()
        {
            foreach (var cartA in GetCarts())
            {
                foreach (var cartB in GetCarts())
                {
                    if (cartA.Point == cartB.Point && cartA != cartB)
                        return cartA.Point;
                }
            }
            return null;
        }

        public List<Cart> GetActiveCarts()
        {
            return GetCarts().Where(x => !x.IsDeleted).ToList();
        }

        public void DeleteCollisions()
        {
            foreach (var cartA in GetActiveCarts())
            {
                foreach (var cartB in GetActiveCarts())
                {
                    if (cartA.Point == cartB.Point && cartA != cartB) // && !cartA.IsDeleted && !cartB.IsDeleted)
                    //if (cartA.Point.X == cartB.Point.X && cartA.Point.Y == cartB.Point.Y && cartA != cartB) // && !cartA.IsDeleted && !cartB.IsDeleted)
                    {
                        cartA.IsDeleted = true;
                        cartB.IsDeleted = true;
                        return;
                    }

                    if (GetActiveCarts().Count() == 1)
                    {
                        return;
                    }

                }
            }
        }

        public void StepDeletingCollisions()
        {
            foreach (var cart in GetActiveCarts())
            {
                cart.Step();
                DeleteCollisions();
                if (GetActiveCarts().Count() == 1)
                {
                    var x = "got here";
                }
            }
        }

        //Gets the final cart after all others have collided
        public Cart GetLastCart()
        {
            var step = 0;
            while (GetActiveCarts().Count > 1)
            {
                StepDeletingCollisions();
                step++;
            }

            var lastCart = GetActiveCarts()[0];
            //lastCart.Step();
            return lastCart;
        }
    }
}
