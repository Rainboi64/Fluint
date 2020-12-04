// 
// VertexArrayObject.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Graphics;

namespace Fluint.Engine.GL46.Graphics
{
    public class VertexArrayObject<VertexType> : IVertexLayout<VertexType> where VertexType : struct
    {
        private readonly List<VertexLayoutAttribute> _attributes;
        public int VertexSize { get; private set; }
        public int Handle { get; private set; }

        public VertexArrayObject()
        {
            //Handle = GL.GenVertexArray();
            _attributes = new List<VertexLayoutAttribute>();
            Handle = GL.GenVertexArray();

            var type = typeof(VertexType);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {

                //TODO: Finish these..
                var @switch = new Dictionary<Type, Action>
                {
                    { typeof(int),     () => Add(VertexLayoutAttributeType.Int, 1)                  },
                    { typeof(uint),    () => Add(VertexLayoutAttributeType.UnsignedInt, 1)          },
                    { typeof(short),   () => Add(VertexLayoutAttributeType.Short, 1)                },
                    { typeof(ushort),  () => Add(VertexLayoutAttributeType.UnsignedShort, 1)        },
                    { typeof(float),   () => Add(VertexLayoutAttributeType.Float, 1)                },
                    { typeof(double),  () => Add(VertexLayoutAttributeType.Double, 1)               },
                    { typeof(Vector2), () => Add(VertexLayoutAttributeType.Float, 2)                },
                    { typeof(Vector3), () => Add(VertexLayoutAttributeType.Float, 3)                },
                    { typeof(Vector4), () => Add(VertexLayoutAttributeType.Float, 4)                },
                };

                @switch[property.PropertyType]();
            }

            foreach (var attribute in _attributes)
            {
                VertexSize += GetAttributeTypeSize(attribute.AttributeType) * attribute.ComponentsCount;
            }

        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Load()
        {
            Bind();
            GL.EnableVertexAttribArray(0);
        }

        private void Add(VertexLayoutAttributeType type, int components)
        {
            _attributes.Add(new VertexLayoutAttribute(type, components));
        }

        public void Enable()
        {
            Bind();
            var offset = 0;
            for (int i = 0; i < _attributes.Count; i++)
            {
                var attribute = _attributes[i];
                GL.VertexAttribPointer(i, attribute.ComponentsCount, GetVertexPointerType(attribute.AttributeType),
                    false, VertexSize, offset);

                offset += GetAttributeTypeSize(attribute.AttributeType) * attribute.ComponentsCount;
            }
            GL.EnableVertexAttribArray(0);

        }

        public void Disable()
        {
            Bind();
            for (int i = 0; i < _attributes.Count; i++)
            {
                GL.DisableVertexAttribArray(i);
            }
            Unbind();
        }

        private static VertexAttribPointerType GetVertexPointerType(VertexLayoutAttributeType type)
        {
            return type switch
            {
                VertexLayoutAttributeType.Byte => VertexAttribPointerType.Byte,
                VertexLayoutAttributeType.Int => VertexAttribPointerType.Int,
                VertexLayoutAttributeType.Short => VertexAttribPointerType.Short,
                VertexLayoutAttributeType.Float => VertexAttribPointerType.Float,
                VertexLayoutAttributeType.Double => VertexAttribPointerType.Double,

                VertexLayoutAttributeType.UnsignedByte => VertexAttribPointerType.UnsignedByte,
                VertexLayoutAttributeType.UnsignedInt => VertexAttribPointerType.UnsignedInt,
                VertexLayoutAttributeType.UnsignedShort => VertexAttribPointerType.UnsignedShort,

                _ => throw new NotImplementedException($"Unsupported AttributeType: {type}."),
            };
        }

        private static int GetAttributeTypeSize(VertexLayoutAttributeType type)
        {
            return type switch
            {
                VertexLayoutAttributeType.Byte => sizeof(byte),
                VertexLayoutAttributeType.Double => sizeof(double),
                VertexLayoutAttributeType.Float => sizeof(float),
                VertexLayoutAttributeType.Int => sizeof(int),
                VertexLayoutAttributeType.Short => sizeof(short),
                VertexLayoutAttributeType.UnsignedByte => sizeof(byte),
                VertexLayoutAttributeType.UnsignedInt => sizeof(uint),
                VertexLayoutAttributeType.UnsignedShort => sizeof(ushort),
                _ => throw new NotImplementedException($"Unsupported AttributeType: {type}."),
            };
        }
    }
}
