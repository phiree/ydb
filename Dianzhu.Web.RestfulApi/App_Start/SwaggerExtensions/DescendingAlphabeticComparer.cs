﻿using System;
using System.Collections.Generic;

namespace Dianzhu.Web.RestfulApi.SwaggerExtensions
{
    public class DescendingAlphabeticComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return y.CompareTo(x);
        }
    }
}
