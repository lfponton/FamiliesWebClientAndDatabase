using System.Collections.Generic;
using Models;

namespace FileData
{
    public interface IFileContext
    {
        IList<Family> Families
        {
            get;
            set;
        }
        IList<Adult> Adults
        {
            get;
            set;
        }
        public void SaveChanges(); 
    }
}