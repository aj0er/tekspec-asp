﻿using ExampleForum.Models.Response;

namespace ExampleForum.Models
{
    public class ThreadContext
    {

        public ThreadResponse Thread { get;set; }
        public IEnumerable<PostResponse> Posts { get; set; }

    }
}
