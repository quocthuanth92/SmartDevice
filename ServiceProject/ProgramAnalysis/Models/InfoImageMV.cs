using ExifMetadata.Exif;
using ProgramAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramAnalysis.Models
{
    public class InfoImageMV
    {
        public string ImageName { get; set; }
        [Required]
        [Display(Name = "Image path:")]
        public string ImagePath { get; set; }
        public List<ExifTag> ResultImage { get; set; }
        public List<ImageInfoMark> ListItem { get; set; }
        public InfoImageMV()
        {
            this.ResultImage = new List<ExifTag>();
            this.ListItem = new List<ImageInfoMark>();
        }
    }
}