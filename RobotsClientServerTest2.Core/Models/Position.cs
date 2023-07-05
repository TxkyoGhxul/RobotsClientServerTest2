using System.Diagnostics.CodeAnalysis;

namespace RobotsClientServerTest2.Core.Models;

/// <summary>
/// Represents robot's coordinates
/// </summary>
public struct Position
{
    /// <summary>
    /// Position by X
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// Position by Y
    /// </summary>
    public int Y { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="x">Position by X</param>
    /// <param name="y">Position by Y</param>
    public Position(int x, int y) => (X, Y) = (x, y);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is Position pos && X == pos.X && Y == pos.Y;

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// Compare operation
    /// </summary>
    /// <param name="left">Left position</param>
    /// <param name="right">Right position</param>
    /// <returns>True if coordinates are the same; otherwise, false</returns>
    public static bool operator ==(Position left, Position right) => left.Equals(right);

    /// <summary>
    /// Not compare operation
    /// </summary>
    /// <param name="left">Left position</param>
    /// <param name="right">Right position</param>
    /// <returns>False if coordinates are the same; otherwise, true</returns>
    public static bool operator !=(Position left, Position right) => !(left == right);
}
