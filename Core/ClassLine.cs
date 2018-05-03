using System;
using UnityEngine;
using System.Text.RegularExpressions;

namespace CippSharp.ClassTemplates
{
    public enum LineType
    {
        unknown = -1,
        _return = 0,
        _begin = 1,
        _end = 2,
        _complete = 3
    }
    
    public struct ClassLine
    {
        public static ClassLine Empty = new ClassLine("");
        
        private LineType lineType;
        private string m_tabs;

        public string tabs
        {
            get { return m_tabs; }
        }

        private string m_beginLineOf;

        public string beginLineOf
        {
            get { return m_beginLineOf; }
        }

        public string enclosedBegin
        {
            get { return string.Format("<{0}>", m_beginLineOf); }
        }
        
        private string m_value;

        public string value
        {
            get { return m_value; }
        }

        private string m_endLineOf;

        public string endLineOf
        {
            get { return m_endLineOf; }
        }

        public string enclosedEnd
        {
            get { return string.Format("</{0}>", m_endLineOf); }
        }


        public ClassLine(string line, string beginLineOf = "", string endLineOf = "")
        {
            this.m_tabs = "";
            this.m_value = line;
            this.m_beginLineOf = beginLineOf;
            this.m_endLineOf = endLineOf;
            this.lineType = LineType.unknown;
            
            if (line == Templates.carriageReturnAndLineFeed)
            {
                this.lineType = LineType._return;
            }
            else if (!string.IsNullOrEmpty(beginLineOf) && !string.IsNullOrEmpty(endLineOf))
            {
                this.lineType = LineType._complete;
            }
            else if (string.IsNullOrEmpty(beginLineOf) && !string.IsNullOrEmpty(endLineOf))
            {
                this.lineType = LineType._begin;
            }
            else if (!string.IsNullOrEmpty(beginLineOf) && string.IsNullOrEmpty(endLineOf))
            {
                this.lineType = LineType._end;
            }
        }

        public bool Equals(ClassLine other)
        {
            return lineType == other.lineType && 
                   string.Equals(m_tabs, other.m_tabs) && 
                   string.Equals(m_beginLineOf, other.m_beginLineOf) && 
                   string.Equals(m_value, other.m_value) && 
                   string.Equals(m_endLineOf, other.m_endLineOf);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is ClassLine && Equals((ClassLine) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) lineType;
                hashCode = (hashCode * 397) ^ (m_tabs != null ? m_tabs.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (m_beginLineOf != null ? m_beginLineOf.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (m_value != null ? m_value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (m_endLineOf != null ? m_endLineOf.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ClassLine left, ClassLine right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ClassLine left, ClassLine right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Add a tab.
        /// </summary>
        public void AddTab()
        {
            m_tabs += Templates.tab;
        }

        /// <summary>
        /// Remove a tab.
        /// </summary>
        public void RemoveTabs()
        {
            m_tabs = string.Empty;
        }


        public bool ValueContains(string target)
        {
            return m_value.Contains(target);
        }

        public bool ValueEndsWith(string target)
        {
            return m_value.EndsWith(target);
        }

        /// <summary>
        /// Returns the complete line;
        /// </summary>
        /// <returns></returns>
        public string ToLine()
        {
            switch (lineType)
            {
                case LineType._return:
                    return string.Concat(m_value, m_tabs);
                default:
                    return string.Concat(m_tabs, enclosedBegin, m_value, enclosedEnd);
            }
           
        }

        /// <summary>
        /// Return a valid writable line.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override string ToString()
        {
            switch (lineType)
            {
                case LineType._return:
                    return string.Concat(m_value, m_tabs);
                default:
                    return string.Concat(m_tabs, m_value);
            }
        }
    }
}
