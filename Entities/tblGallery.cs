using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblGallery
    {
        [Key]
        public int GalleryID { get; set; }
        public string ImageName { get; set; }
    }
}
