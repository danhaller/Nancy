﻿namespace Nancy.ViewEngines.Razor.VisualBasic
{
    using System;
    using System.CodeDom;
    using System.Web.Razor;
    using System.Web.Razor.Generator;
    using System.Web.Razor.Parser.SyntaxTree;

    /// <summary>
    /// A nancy version of the visual basic code generator for razor.
    /// </summary>
    public class NancyVisualBasicRazorCodeGenerator : VBRazorCodeGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NancyVisualBasicRazorCodeGenerator"/> class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="rootNamespaceName">Name of the root namespace.</param>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="host">The host.</param>
        public NancyVisualBasicRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
            : base(className, rootNamespaceName, sourceFileName, host)
		{
            this.SetBaseType(typeof(object).FullName);
        }

		protected override bool TryVisitSpecialSpan(Span span)
		{
			return TryVisit(span, new Action<ModelSpan>(this.VisitModelSpan));
		}

		private void VisitModelSpan(ModelSpan span)
		{
			this.SetBaseType(span.ModelTypeName);
		}

        private void SetBaseType(string modelTypeName)
        {
            this.GeneratedClass.BaseTypes.Clear();
            this.GeneratedClass.BaseTypes.Add(new CodeTypeReference(this.Host.DefaultBaseClass + "(Of " + modelTypeName + ")"));
        }
    }
}
