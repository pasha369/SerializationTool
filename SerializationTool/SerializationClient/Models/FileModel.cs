using System;

namespace SerializationClient.Models
{
    /// <summary>
    /// Represents file model.
    /// </summary>
    [Serializable]
    public class FileModel
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets file bytes.
        /// </summary>
        public byte[] DataBytes { get; set; }
    }
}
