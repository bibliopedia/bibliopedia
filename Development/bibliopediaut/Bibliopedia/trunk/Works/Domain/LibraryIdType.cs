namespace PublishedWorks.Domain
{
    public enum LibraryIdType
    {
        Other,
        /// <summary>
        /// Dewey Decimal System
        /// </summary>
        Dewey, 
        /// <summary>
        /// Digital Object Identifier
        /// </summary>
        Doi, 
        /// <summary>
        /// International Standard Book Number
        /// </summary>
        Isbn, 
        /// <summary>
        /// International Standard Serial Number
        /// </summary>
        Issn,
        /// <summary>
        /// Library of Congress Classification
        /// </summary>
        Lcc,
        /// <summary>
        /// Library of Congress Control Number (also known as "Library of Congress Card Number")
        /// </summary>
        Lccn,
        /// <summary>
        /// Open Scriptural Information Standard
        /// </summary>
        Osis,
        /// <summary>
        /// Serial Item and Contribution Identifier
        /// </summary>
        Sici,
        /// <summary>
        /// Uniform Resource Identifier
        /// </summary>
        Uri,
        /// <summary>
        /// Uniform Resource Locator
        /// </summary>
        Url, 
        /// <summary>
        /// Uniform Resource Name         
        /// </summary>
        Urn
    }
}