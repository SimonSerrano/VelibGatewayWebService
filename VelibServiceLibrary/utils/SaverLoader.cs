using System.IO;

namespace VelibServiceLibrary
{
    class SaverLoader
    {

        /// <summary>
        /// Creates a binary file for a serializable object
        /// </summary>
        /// <typeparam name="T">Object type to store</typeparam>
        /// <param name="filePath">Path to file</param>
        /// <param name="objectToWrite">Object to write</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Create a serializable object from a binary file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">Path to file</param>
        /// <returns>the object or null if file not found</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default(T);

            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
