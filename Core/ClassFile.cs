//This struct was offered by "Cippman".
using System.Collections.Generic;

namespace CippSharp.ClassTemplates
{
    public struct ClassFile
    {
        public static ClassFile Empty = new ClassFile(null);
        
        public readonly List<string> lines;

        public ClassFile(List<string> lines)
        {
            this.lines = new List<string>();
            this.lines.AddRange(lines);
        }

        public bool Equals(ClassFile other)
        {
            return Equals(lines, other.lines);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is ClassFile && Equals((ClassFile) obj);
        }

        public override int GetHashCode()
        {
            return (lines != null ? lines.GetHashCode() : 0);
        }

        public static bool operator ==(ClassFile left, ClassFile right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ClassFile left, ClassFile right)
        {
            return !left.Equals(right);
        }

        public static implicit operator List<string>(ClassFile classFile)
        {
            List<string> lines = new List<string>();
            lines.AddRange(classFile.lines);
            return lines;
        }

        public static implicit operator ClassFile(List<string> lines)
        {
            return new ClassFile(lines);
        }
    }
}