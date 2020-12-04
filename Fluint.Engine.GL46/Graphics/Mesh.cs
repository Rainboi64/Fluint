//
// MeshVertex.cs
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
    public class Mesh
    {
        public Mesh()
        {
            Vertices = new List<Vector3>();
            Normals = new List<Vector3>();
            Faces = new List<Face>();
        }

        /// <summary>
        /// List of vector for the mesh
        /// </summary>
        public List<Vector3> Vertices { get; set; }

        /// <summary>
        /// List of normals for the mesh.
        /// </summary>
        public List<Vector3> Normals { get; set; }


        /// <summary>
        /// List of faces for the mesh.
        /// </summary>
        public List<Face> Faces { get; set; }


        /// <summary>
        /// Returns if mesh is made of Quads.
        /// </summary>
        /// <returns>True/False</returns>
        public bool IsQuadBased()
        {
            foreach (Face temp in Faces)
            {
                if (temp.Indecies.Count != 4)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Triangulates Quads in Meshes.
        /// not sure if working properly... ;D
        /// </summary>
        public static Mesh Triangulate(Mesh x)
        {
            int facecount = x.Faces.Count;
            for (int i = 0; i < facecount; i++)
            {
                var face = x.Faces[i];
                if (face.IsQuad())
                {
                    double dist1 = Vector3.Distance(x.Vertices[face.Indecies[0]], x.Vertices[face.Indecies[2]]);
                    double dist2 = Vector3.Distance(x.Vertices[face.Indecies[1]], x.Vertices[face.Indecies[3]]);
                    if (dist1 > dist2)
                    {
                        x.Faces.Add(new Face(face.Indecies[0], face.Indecies[1], face.Indecies[3]));
                        //x.Faces.Add(face.A, face.B, face.D);
                        x.Faces.Add(new Face(face.Indecies[1], face.Indecies[2], face.Indecies[3]));
                        //x.Faces.Add(face.B, face.C, face.D);
                    }
                    else
                    {
                        x.Faces.Add(new Face(face.Indecies[0], face.Indecies[1], face.Indecies[2]));
                        //x.Faces.Add(face.A, face.B, face.C);
                        x.Faces.Add(new Face(face.Indecies[0], face.Indecies[2], face.Indecies[3]));
                        //x.Faces.Add(face.A, face.C, face.D);
                    }
                }
            }

            var newfaces = new List<Face>();
            foreach (var face in x.Faces)
            {
                if (face.IsTriangle()) newfaces.Add(face);
            }
            x.Faces.Clear();

            x.Faces = newfaces;
            return x;
        }

        /// <summary>
        /// Returns if mesh is made of Triangles.
        /// </summary>
        /// <returns>True/False</returns>
        public bool IsTriangleBased()
        {
            foreach (Face temp in Faces)
            {
                if (temp.Indecies.Count != 3)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsLine()
        {
            foreach (Face temp in Faces)
            {
                if (temp.Indecies.Count != 2)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a transformed instance of the mesh.
        /// </summary>
        /// <param name="matrix">Transform Matrix</param>
        /// <param name="mesh">MeshVertex to apply transform.</param>
        /// <returns>transformed instance of the mesh.</returns>
        public static Mesh Transform(Matrix4 matrix, Mesh mesh)
        {
            var tempmesh = new Mesh();
            foreach (var vector in mesh.Vertices)
            {
                var tempvector = new Vector4(vector.X, vector.Z, vector.Y, 1);
                tempvector *= matrix;
                tempmesh.Vertices.Add(tempvector.Xyz);
            }
            foreach (var vector in mesh.Normals)
            {
                var tempnormal = new Vector4(vector.X, vector.Z, vector.Y, 1);
                tempnormal *= matrix;
                tempmesh.Normals.Add(tempnormal.Xyz);
            }
            tempmesh.Faces = mesh.Faces;
            return tempmesh;
        }


        /// <summary>
        /// Only return single instance of each index (Filter Duplicates)
        /// </summary>
        /// <param name="indexes">input indexes</param>
        /// <returns>the input without duplicates</returns>
        public static IEnumerable<int> FilterIndexes(IEnumerable<int> indexes)
        {
            List<int> temp = new List<int>();
            foreach (int index in indexes)
            {
                if (!temp.Contains(index))
                    temp.Add(index);
            }
            return temp;
        }

        /// <summary>
        /// Applies transform to vectors. 
        /// </summary>
        /// <param name="index">the index of the vector</param>
        /// <param name="Transform">the transform to apply</param>
        public void TransformVector(int index, Matrix4 Transform)
        {
            Vector4 temp = new Vector4(Vertices[index], 1.0f);
            temp *= Transform;
            Vertices[index] = temp.Xyz;
        }

        /// <summary>
        /// Applies A transform on a list of vectors.
        /// </summary>
        /// <param name="indexes">list of index vectors</param>
        /// <param name="Transform">the transform to apply</param>
        public void TransformVectors(IEnumerable<int> indexes, Matrix4 Transform)
        {
            foreach (int index in indexes)
            {
                TransformVector(index, Transform);
            }
        }


        /// <summary>
        /// Get instances of the same vector, useful if you wanna apply transforms on vectors.
        /// to prevent getting open meshes
        /// </summary>
        /// <param name="vector">input vector</param>
        /// <returns>all the instances(indexes) of the same vector</returns>
        public IEnumerable<int> GetNeighboringVertices(Vector3 vector)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].X == vector.X && Vertices[i].Y == vector.Y && Vertices[i].Z == vector.Z)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Get instances of the same vector, useful if you wanna apply transforms on vectors.
        /// to prevent getting open meshes
        /// </summary>
        /// <param name="index">input vector as an index</param>
        /// <returns>all the instances(indexes) of the same vector</returns>
        public IEnumerable<int> GetNeighboringVertices(int index)
        {
            return GetNeighboringVertices(Vertices[index]);
        }


    }
}
