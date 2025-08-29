using System.ComponentModel.DataAnnotations;

namespace ABCretailpoe1.Models
{
    public class UploadModel
    {
        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? FileUrl { get; set; }
    }
}
