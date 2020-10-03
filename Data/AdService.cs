using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class AdService
    {
        public List<AdData> GetAds()
        {
            var result = new List<AdData>();
            result.Add(new AdData()
            {
                Url = "https://www.youtube.com/user/13DOMEK",
                Description = "Subscibe to my channel on Youtube",
                ImageSource = "https://www.vid-marketing.com/wp-content/uploads/2017/01/YouTube-icon-full_color-1024x721.png"
            });
            result.Add(new AdData()
            {
                Url = "https://github.com/DominikNITA",
                Description = "Check my other projects on github",
                ImageSource = "https://res.cloudinary.com/practicaldev/image/fetch/s--i_sb3chq--/c_imagga_scale,f_auto,fl_progressive,h_900,q_auto,w_1600/https://thepracticaldev.s3.amazonaws.com/i/fk0849hvg2rt13bpqhjy.jpg"
            });
            return result;
        }
    }
}
