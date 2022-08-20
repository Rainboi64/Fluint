//
// Binding.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Input
{
    public class Binding
    {
        public string Tag { get; set; }
        public Key[] MainCombination { get; set; }
        public Key[] SecondaryCombination { get; set; }

        public override string ToString()
        {
            return Tag;
        }
    }
}
