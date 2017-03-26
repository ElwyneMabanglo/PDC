using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDC
{
    // Create obhttps://msdn.microsoft.com/en-gb/library/ms173110.aspx
    class room
    {

        public int IdHouse { get; set; }
        public string RoomType { get; set; }
        public int North { get; set; }
        public int East { get; set; }
        public int South { get; set; }
        public int West { get; set; }
        public int X { get; set; }

        public int Y { get; set; }
        public room(int idHouse, string roomType, int north, int east, int sout, int west, int x, int y)
        {
            IdHouse = idHouse;
            RoomType = roomType;
            North = north;
            East = east;
            South = sout;
            West = west;
            X = x;
            Y = y;
        }
    }
}
