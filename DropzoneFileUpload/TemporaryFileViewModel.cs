using System;

namespace DropzoneFileUpload
{
    public class TemporaryFileViewModel
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
    }
}
