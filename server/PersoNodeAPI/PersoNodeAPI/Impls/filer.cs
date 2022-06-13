using System.Xml.Serialization;

namespace PersoNodeApi.Impls
{
    using Models;

    /// <summary>
    /// Functions for performing common XML Serialization operations.
    /// <para>Only public properties and variables will be serialized.</para>
    /// <para>Use the [XmlIgnore] attribute to prevent a property/variable from being serialized.</para>
    /// <para>Object to be serialized must have a parameterless constructor.</para>
    /// </summary>
    internal static class XmlFilerDeluxe
    {
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        private static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter? writer = null;
            try
            {
                XmlSerializer serializer = new(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                writer?.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        private static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader? reader = null;
            try
            {
                XmlSerializer serializer = new(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader)!;
            }
            finally
            {
                reader?.Close();
            }
        }

        public static void SavePerson(Person person)
        {
            XmlFilerDeluxe.WriteToXmlFile(person.Name + ".xml", person);
        }

        public static Person LoadPerson(string personName)
        {
            Person person = XmlFilerDeluxe.ReadFromXmlFile<Person>(personName + ".xml");

            return person;
        }
    }
}