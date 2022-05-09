using System;
using FreshMvvm;

namespace JuniperAssignment.Models
{
    public class FreshViewModelMapper : IFreshPageModelMapper
    {
        public string GetPageTypeName(Type pageModelType)
        {
            //freshmvvm defaults to using "..PageModel" and "..Page" rather than "..ViewModel" and "..View"
            return pageModelType?.AssemblyQualifiedName?.Replace("ViewModel", "View");
        }
    }
}