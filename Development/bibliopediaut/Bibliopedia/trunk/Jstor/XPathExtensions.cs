﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.XPath;
using System.Xml.Linq;

namespace Jstor
{
    public static class XPathExtensions
    {
        public static IEnumerable<object> SelectMultipleResults(this XPathDocument doc, string expression)
        {
            var nav = doc.CreateNavigator();

            var results = nav.Select(expression);
            while (results.MoveNext())
            {
                yield return results.Current;
            }
        }

        public static object Evaluate(this XPathDocument doc, string expression)
        {
            var nav = doc.CreateNavigator();
            return nav.Evaluate(expression);
        }


        /// <summary>
        /// Get the absolute XPath to a given XElement, including the namespace.
        /// (e.g. "/a:people/b:person[6]/c:name[1]/d:last[1]").
        /// </summary>
        public static string GetAbsoluteXPath(this XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            Func<XElement, string> relativeXPath = e =>
            {
                int index = e.IndexPosition();

                var currentNamespace = e.Name.Namespace;

                string name;
                if (currentNamespace == null)
                {
                    name = e.Name.LocalName;
                }
                else
                {
                    string namespacePrefix = e.GetPrefixOfNamespace(currentNamespace);
                    name = namespacePrefix + ":" + e.Name.LocalName;
                }

                // If the element is the root, no index is required
                return (index == -1) ? "/" + name : string.Format
                (
                    "/{0}[{1}]",
                    name,
                    index.ToString()
                );
            };

            var ancestors = from e in element.Ancestors()
                            select relativeXPath(e);

            return string.Concat(ancestors.Reverse().ToArray()) +
                   relativeXPath(element);
        }

        /// <summary>
        /// Get the index of the given XElement relative to its
        /// siblings with identical names. If the given element is
        /// the root, -1 is returned.
        /// </summary>
        /// <param name="element">
        /// The element to get the index of.
        /// </param>
        public static int IndexPosition(this XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (element.Parent == null)
            {
                return -1;
            }

            int i = 1; // Indexes for nodes start at 1, not 0

            foreach (var sibling in element.Parent.Elements(element.Name))
            {
                if (sibling == element)
                {
                    return i;
                }

                i++;
            }

            throw new InvalidOperationException("element has been removed from its parent.");
        }
    }
}