using IQVIAAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQVIAAssignment.Utility
{
    public class ItemEqualityComparer : IEqualityComparer<Tweet>
    {
        public bool Equals(Tweet x, Tweet y)
        {
            // Two items are equal if their keys are equal.
            return x.text == y.text;
        }

        public int GetHashCode(Tweet obj)
        {
            return obj.text.GetHashCode();
        }
    }
}