//
// Face.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Fluint.Engine.GL46.Graphics
{
    [Serializable]
    public class Face
    { 
        public Face()
        {
            Indecies = new List<int>();
        }

        /// <summary>
        /// To Initialize a triangle face.
        /// </summary>
        public Face(int A, int B, int C)
        {
            Indecies = new List<int>
            {
                A,
                B,
                C
            };
        }

        /// <summary>
        /// To Initialize a Quad face.
        /// </summary>
        public Face(int A, int B, int C, int D)
        {
            Indecies = new List<int>
            {
                A,
                B,
                C,
                D
            };
        }

        /// <summary>
        /// To Initialize a Line face.
        /// </summary>
        public Face(int A, int B)
        {
            Indecies = new List<int>
            {
                A,
                B,
            };
        }

        /// <summary>
        /// Initialize a face with Indices.
        /// </summary>
        public Face(IEnumerable<int> indecies)
        {
            Indecies = (List<int>)indecies;
        }

        /// <summary>
        /// Transforms The Faces relative to the parent mesh.
        /// </summary>
        /// <param name="Parent">Parent Mesh (The one that contains this face)</param>
        /// <param name="Transformation">The transformation to apply</param>
        public void Transform(ref Mesh Parent, Matrix4 Transformation)
        {
            foreach (int indecie in Indecies)
            {
                Vector4 temp = new Vector4(Parent.Vertices[indecie].X, Parent.Vertices[indecie].Y, Parent.Vertices[indecie].Z, 1.0f);
                temp *= Transformation;
                Parent.Vertices[indecie] = temp.Xyz;
            }
        }

        /// <summary>
        /// Returns if face is a Quad.
        /// </summary>
        /// <returns>True/False</returns>
        public bool IsQuad()
        {
            return Indecies.Count == 4;
        }

        /// <summary>
        /// Returns if face is a Triangle.
        /// </summary>
        /// <returns>True/False</returns>
        public bool IsTriangle()
        {
            return Indecies.Count == 3;
        }

        /// <summary>
        /// Merges Faces. 
        /// Example 2 Triangles become a quad.
        /// ------
        /// |\   |
        /// | \  |
        /// |  \ |
        /// ------
        /// to 
        /// ------
        /// |    |
        /// |    |
        /// |    |
        /// ------
        /// </summary>
        /// <param name="faces">input faces</param>
        /// <returns></returns>
        public static Face MergeFaces(IEnumerable<Face> faces)
        {
            Face temp = new Face();

            foreach (Face face in faces)
            {
                foreach (int index in face.Indecies)
                {
                    temp.Indecies.Add(index);
                }
            }
            temp.Indecies = (List<int>)Mesh.FilterIndexes(temp.Indecies);
            return temp;
        }

        /// <summary>
        /// List of Indexes for all the vectors of this face.
        /// </summary>
        public List<int> Indecies { get; set; }
    }
}
