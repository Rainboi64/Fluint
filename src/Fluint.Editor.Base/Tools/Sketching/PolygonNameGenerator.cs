// PolygonNameGenerator.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

namespace Fluint.Editor.Base.Tools.Sketching;

using System;
using System.Collections.Generic;

// https://en.wikipedia.org/wiki/List_of_polygons
public class PolygonNameGenerator
{
    private static readonly Dictionary<int, string> GreekPrefixes = new()
    {
        { 1, "Mono" },
        { 2, "Di" },
        { 3, "Tri" },
        { 4, "Tetra" },
        { 5, "Penta" },
        { 6, "Hexa" },
        { 7, "Hepta" },
        { 8, "Octa" },
        { 9, "Ennea" },
        { 10, "Deca" },
        { 20, "Icosi" },
        { 30, "Triaconta" },
        { 40, "Tetraconta" },
        { 50, "Pentaconta" },
        { 60, "Hexaconta" },
        { 70, "Heptaconta" },
        { 80, "Octaconta" },
        { 90, "Enneaconta" },
        { 100, "Hecto" },
        { 200, "Dihecto" },
        { 300, "Trihecto" },
        { 400, "Tetrahecto" },
        { 500, "Pentahecto" },
        { 600, "Hexahecto" },
        { 700, "Heptahecto" },
        { 800, "Octahecto" },
        { 900, "Enneahecto" },
        { 1000, "Kilo" },
        { 2000, "Diakilo" },
        { 3000, "Triakilo" },
        { 4000, "Tetrakilo" },
        { 5000, "Pentakilo" },
        { 6000, "Hexakilo" },
        { 7000, "Heptakilo" },
        { 8000, "Octakilo" },
        { 9000, "Enneakilo" }
    };
    
    public static string GenerateNodeName(int segments)
    {
        if (segments is 1 or 2)
        {
            return "Line";
        }

        if (GreekPrefixes.TryGetValue(segments, out var prefix))
        {
            return $"{prefix}gon";
        }

        return GenerateLargePolygonName(segments);
    }

    private static string GenerateLargePolygonName(int sides)
    {
        var name = string.Empty;

        if (sides >= 1000)
        {
            var thousands = (sides / 1000) * 1000;
            sides %= 1000;
            name += GreekPrefixes[thousands];
        }

        if (sides >= 100)
        {
            var hundreds = (sides / 100) * 100;
            sides %= 100;
            name += GreekPrefixes[hundreds];
        }

        if (sides >= 10)
        {
            var tens = (sides / 10) * 10;
            sides %= 10;
            name += GreekPrefixes[tens];
        }

        if (sides > 0)
        {
            name += GreekPrefixes[sides].ToLower();
        }

        return $"{name}gon";
    }
}