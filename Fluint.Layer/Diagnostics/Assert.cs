using System.Diagnostics;

namespace Fluint.Layer.Diagnostics
{
    public static class Assert
    {
        public static void IsFalse(bool data, string message = "No additional information given.")
        {    
            Debug.Assert(!data, message);
        }

        public static void IsTrue(bool data, string message = "No additional information given.")
        {    
            Debug.Assert(data, message);
        }

        public static void AreEqual(object object1, object object2, string message = "No additional information given.")
        {
            Debug.Assert(object1 == object2, message);
        }

        public static void AreNotEqual(object object1, object object2, string message = "No additional information given.")
        {
            Debug.Assert(object1 != object2, message);
        }

    }
}
