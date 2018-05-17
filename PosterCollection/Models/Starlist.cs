﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosterCollection.Models
{
    public class Star
    {
        public int id;
        public string title;
        public string imagepath;
        public string comment;
        public Star(int id,string title,string imagepath,string comment)
        {
            this.id = id;
            this.title = title;
            this.imagepath = imagepath;
            this.comment = comment;
        }

    }
}
