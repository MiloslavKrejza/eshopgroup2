using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Alza.Module.Catalog.Dal.Entities
{
    public class Media : Entity
    {
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public DateTime CreatedDate { get; set; } = new DateTime(1990,1,1);

        public int ViewCount { get; set; } = 0;

        //VAZBY

        public int MediaTypeId { get; set; }
        public MediaType MediaType { get; set; } = new MediaType();





		/// <summary>
		/// Return video thumbnail url on youtube or empty string
		/// 
		/// Small custom thumbnail [320x180]
		/// http://img.youtube.com/vi/VIDEOID/mqdefault.jpg
		/// 
		/// Medium custom thumbnail at 4:3 ratio [480x360]
		/// http://img.youtube.com/vi/VIDEOID/0.jpg
		/// 
		/// Large custom thumbnail [1280x720 or 1920x1080] 
		/// http://img.youtube.com/vi/VIDEOID/maxresdefault.jpg
		/// 
		/// </summary>
		/// <param name="videoUrl"></param>
		/// <returns></returns>
		public string getVideoThumbnail()
		{
			var youtubeMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(Url);
			var id = youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty;
			var result = "http://img.youtube.com/vi/" + id + "/mqdefault.jpg";
			return result;
		}

	}
}
