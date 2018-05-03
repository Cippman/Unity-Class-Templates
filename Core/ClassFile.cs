//This struct was offered by "Cippman".

using System;
using System.Collections.Generic;
using CippSharp.ClassTemplates.Extensions;

namespace CippSharp.ClassTemplates
{
    public class ClassFile
    {
        public const string _using = "using";
        public const string _declaration = "declaration";
        //Usings
        
        private List<ClassLine> usingsLines = new List<ClassLine>();
        
        //Space
        
        //Namespace declaration
        private ClassLine namespaceLine = ClassLine.Empty;
        
        //Class declaration
        private List<ClassLine> classAttributes = new List<ClassLine>(); 
        private ClassLine classDeclaration = new ClassLine();
        
        //Class body
        //minimum indent level 1
        private readonly List<ClassLine> classBody = new List<ClassLine>();
        
        //Close class
        
        //Close namespace

        public ClassFile(ClassLine classDeclaration)
        {
            this.classDeclaration = classDeclaration;
            classBody.Clear();
        }

        public void AddUsing(ClassLine newUsing)
        {
            usingsLines.Add(newUsing);
        }

        public void ClearUsings()
        {
            usingsLines.Clear();
        }

        public void SetNamespace(ClassLine _namespace)
        {
            namespaceLine = _namespace;
        }

        public void RemoveNamespace()
        {
            namespaceLine = ClassLine.Empty;
        }

        public void AddClassAttributes(ClassLine newClassAttribute)
        {
            classAttributes.Add(newClassAttribute);
        }

        public void ClearClassAttributes()
        {
            classAttributes.Clear();
        }

        public void SetClassDeclaration(ClassLine newClassDeclaration)
        {
            this.classDeclaration = newClassDeclaration;
        }

        public void AddLineToClassBody(ClassLine newClassBodyLine)
        {
            classBody.Add(newClassBodyLine);
        }

        public void ClearClassBody()
        {
            classBody.Clear();
        }

        public List<string> Compile()
        {
            List<string> tmpLines = new List<string>();
            foreach (ClassLine usingsLine in usingsLines)
            {
                tmpLines.Add(Templates.carriageReturnAndLineFeed);
                tmpLines.Add(usingsLine.ToString());
            }
            
            tmpLines.Add(Templates.carriageReturnAndLineFeed);
            
            bool hasNamespace = namespaceLine != ClassLine.Empty;
            if (hasNamespace)
            {
               tmpLines.Add(namespaceLine.ToString());
               tmpLines.Add(Templates.carriageOpenBrace);
            }
            
            tmpLines.Add(Templates.carriageReturnAndLineFeed);
            
            foreach (ClassLine attributeLine in classAttributes)
            {
                if (hasNamespace)
                {
                    attributeLine.AddTab();
                }
                
                tmpLines.Add(Templates.carriageReturnAndLineFeed);
                tmpLines.Add(attributeLine.ToString());
                
                if (hasNamespace)
                {
                    attributeLine.RemoveTabs();
                }
            }
            
            if (hasNamespace)
            {
                classDeclaration.AddTab();
            }
            
            tmpLines.Add(classDeclaration.ToString());
            
            if (hasNamespace)
            {
                classDeclaration.RemoveTabs();
            }

            string tabbedClassDeclarationOpenBrace = (hasNamespace) ? Templates.carriageReturnAndLineFeed + Templates.tab + Templates.tab + Templates.openBrace : Templates.carriageOpenBrace;
            tmpLines.Add(tabbedClassDeclarationOpenBrace);

            tmpLines.Add(Templates.carriageReturnAndLineFeed);
            
            foreach (ClassLine classBodyLine in classBody)
            {
                if (hasNamespace)
                {
                    classBodyLine.AddTab();
                }
                classBodyLine.AddTab();
                
                tmpLines.Add(classBodyLine.ToString());
                
                classBodyLine.RemoveTabs();
            }
            
            tmpLines.Add(Templates.carriageReturnAndLineFeed);
            
            string tabbedClassDeclarationCloseBrace = (hasNamespace) ? Templates.carriageReturnAndLineFeed + Templates.tab + Templates.tab + Templates.closeBrace : Templates.carriageCloseBrace;
            tmpLines.Add(tabbedClassDeclarationCloseBrace);

            if (hasNamespace)
            {
                tmpLines.Add(Templates.closeBrace);
            }

            return tmpLines;
        }
    }
}