using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendPlayground.ViewModels
{
    public class FileUp
    {
        [IsImage(ErrorMessage ="Das is nicht ein Bild!!!")]
        public IFormFile File { get; set; }
    }
}
