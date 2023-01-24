using Ashes.Characters;
using Ashes;
using System;
using System.Collections.Generic;

public class Knight : Character, IMovable
{
    public override List<(int x, int y)> MoveVectors
        => new List<(int x, int y)>
        {
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1)
        };
}
