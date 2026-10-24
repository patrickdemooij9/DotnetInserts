namespace DotNetInserts.Api.Models
{
    public class InsertRenderResult
    {
        public required string InsertName { get; set; }
        public required string StlFilePath { get; set; }
        public bool IsExtra { get; set; }

        /// <summary>
        /// Index of the (visible) part this STL is, or - for an extra - the part whose
        /// lid/inlay/etc. it was generated from.
        /// </summary>
        public int PartIndex { get; set; }
    }
}