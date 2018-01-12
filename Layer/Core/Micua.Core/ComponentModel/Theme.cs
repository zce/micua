using System;
using System.Collections.Generic;

namespace Micua.Core.ComponentModel
{
    [Serializable]
    public class Theme
    {
        /*
         Theme Name: Twenty Twelve
Theme URI: http://wordpress.org/themes/twentytwelve
Author: the WordPress team
Author URI: http://wordpress.org/
Description: The 2012 theme for WordPress is a fully responsive theme that looks great on any device. Features include a front page template with its own widgets, an optional display font, styling for post formats on both index and single views, and an optional no-sidebar page template. Make it yours with a custom menu, header image, and background.
Version: 1.3
License: GNU General Public License v2 or later
License URI: http://www.gnu.org/licenses/gpl-2.0.html
Tags: light, gray, white, one-column, two-columns, right-sidebar, fluid-layout, responsive-layout, custom-background, custom-header, custom-menu, editor-style, featured-images, flexible-header, full-width-template, microformats, post-formats, rtl-language-support, sticky-post, theme-options, translation-ready
Text Domain: twentytwelve
         */
        public string Name { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string AuthorLink { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string License { get; set; }
        public string LicenseLink { get; set; }
        public List<string> Tags { get; set; }
    }
}
